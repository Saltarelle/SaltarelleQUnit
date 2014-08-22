using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saltarelle.Compiler;
using Saltarelle.Compiler.JSModel.Expressions;
using Saltarelle.Compiler.JSModel.Statements;
using Saltarelle.Compiler.JSModel.TypeSystem;
using ICSharpCode.NRefactory.TypeSystem;

namespace QUnit.Plugin {
	public class TestRewriter : IJSTypeSystemRewriter {
		private class DummyRuntimeContext : IRuntimeContext {
			public JsExpression ResolveTypeParameter(ITypeParameter tp) {
				throw new NotSupportedException();
			}

			public JsExpression EnsureCanBeEvaluatedMultipleTimes(JsExpression expression, IList<JsExpression> expressionsThatMustBeEvaluatedBefore) {
				throw new NotSupportedException();
			}

			private DummyRuntimeContext() {}

			public static readonly DummyRuntimeContext Instance = new DummyRuntimeContext();
		}

		private class TestData
		{
			public string Description { get; set; }
			public string Category { get; set; }
			public bool IsAsync { get; set; }
			public bool TaskMethod { get; set; }
			public int? ExpectedAssertionCount { get; set; }
			public JsFunctionDefinitionExpression Function { get; set; }
			
		}

		private readonly IErrorReporter _errorReporter;
		private readonly IRuntimeLibrary _runtimeLibrary;
		private readonly IAttributeStore _attributeStore;

		public TestRewriter(IErrorReporter errorReporter, IRuntimeLibrary runtimeLibrary, IAttributeStore attributeStore) {
			_errorReporter  = errorReporter;
			_runtimeLibrary = runtimeLibrary;
			_attributeStore = attributeStore;
		}

		private JsType ConvertType(JsClass type) {
			if (type.InstanceMethods.Any(m => m.Name == "runTests")) {
				_errorReporter.Region = type.CSharpTypeDefinition.Region;
				_errorReporter.Message(MessageSeverity.Error, 7019, string.Format("The type {0} cannot define a method named 'runTests' because it has a [TestFixtureAttribute].", type.CSharpTypeDefinition.FullName));
				return type;
			}

			var instanceMethods = new List<JsMethod>();
			var tests = new List<TestData>();

			foreach (var method in type.InstanceMethods) {
				var testAttr = _attributeStore.AttributesFor(method.CSharpMember).GetAttribute<TestAttribute>();
				if (testAttr != null) {

					var returnType = method.CSharpMember.ReturnType;
					if (!method.CSharpMember.IsPublic || (!returnType.IsKnownType(KnownTypeCode.Void) && !returnType.IsKnownType(KnownTypeCode.Task))
						|| ((IMethod)method.CSharpMember).Parameters.Count > 0 || ((IMethod)method.CSharpMember).TypeParameters.Count > 0) {
						_errorReporter.Region = method.CSharpMember.Region;
						_errorReporter.Message(MessageSeverity.Error, 7020, string.Format("Method {0}: Methods decorated with a [TestAttribute] must be public, non-generic, parameterless instance methods that return void.", method.CSharpMember.FullName));
					}

					tests.Add(new TestData()
						{
							Description = testAttr.Description ?? method.CSharpMember.Name, 
							Category = testAttr.Category, 
							IsAsync = testAttr.IsAsync, 
							TaskMethod = returnType.IsKnownType(KnownTypeCode.Task),
							ExpectedAssertionCount = testAttr.ExpectedAssertionCount >= 0 ? (int?)testAttr.ExpectedAssertionCount : null, 
							Function = method.Definition
						});
				}
				else
					instanceMethods.Add(method);
			}

			var testInvocations = new List<JsExpression>();

			foreach (var category in tests.GroupBy(t => t.Category).Select(g => new { Category = g.Key, Tests = g}).OrderBy(x => x.Category)) {
				if (category.Category != null)
					testInvocations.Add(JsExpression.Invocation(JsExpression.Member(JsExpression.Identifier("QUnit"), "module"), JsExpression.String(category.Category)));
				testInvocations.AddRange(category.Tests.Select(t => 
					t.TaskMethod ? ProduceAsyncTaskTestInvocation(t) :
					JsExpression.Invocation(JsExpression.Identifier(t.IsAsync ? "asyncTest" : "test"), 
						t.ExpectedAssertionCount != null ? new JsExpression[] { 
							JsExpression.String(t.Description), 
							JsExpression.Number(t.ExpectedAssertionCount.Value), 
							_runtimeLibrary.Bind(t.Function, JsExpression.This, DummyRuntimeContext.Instance) 
						} : new JsExpression[] { 
							JsExpression.String(t.Description), 
							_runtimeLibrary.Bind(t.Function, JsExpression.This, DummyRuntimeContext.Instance) 
						})));
			}

			instanceMethods.Add(new JsMethod(null, "runTests", null, JsExpression.FunctionDefinition(new string[0], JsStatement.Block(testInvocations.Select(t => (JsStatement)t)))));

			var result = type.Clone();
			result.InstanceMethods.Clear();
			foreach (var m in instanceMethods)
				result.InstanceMethods.Add(m);
			return result;
		}

		private JsExpression ProduceAsyncTaskTestInvocation(TestData t)
		{
			var continueWithClause = JsExpression.Invocation(
								JsExpression.Member(JsExpression.Invocation(JsExpression.Identifier("m")), "continueWith"), 
								JsExpression.FunctionDefinition(new string [] {"t" }, JsExpressionStatement.Block(
									JsExpressionStatement.If(JsExpression.Invocation(JsExpression.Member(JsExpression.Identifier("t"), "isFaulted")), 
										JsExpression.Invocation(JsExpression.Identifier("ok"), 
											JsExpression.Boolean(false),
											JsExpression.Add(
												JsExpression.String("Exception thrown in test: "), 
												JsExpression.Invocation(JsExpression.Member(JsExpression.Invocation(JsExpression.Member(JsExpression.Member(JsExpression.Identifier("t"), "exception"), "get_innerException")), "get_message")))),
												null),
									JsExpression.Invocation(JsExpression.Member(JsExpression.Identifier("QUnit"), "start")))));

			return JsExpression.Invocation(
				JsExpression.Identifier("asyncTest"),
				new JsExpression[] {
					JsExpression.String(t.Description),
					JsExpression.Invocation(
						JsExpression.FunctionDefinition(
							new string[] { "m" },
							JsExpressionStatement.Return(
								JsExpression.FunctionDefinition(
								new string[0], 
								t.ExpectedAssertionCount.HasValue ? 
									(JsStatement)JsExpressionStatement.Block(
										JsExpression.Invocation(JsExpression.Identifier("expect"), JsExpression.Number(t.ExpectedAssertionCount.Value)),
										continueWithClause) :
									(JsStatement)continueWithClause))),
						_runtimeLibrary.Bind(t.Function, JsExpression.This, DummyRuntimeContext.Instance))
				});
		}

		public IEnumerable<JsType> Rewrite(IEnumerable<JsType> types) {
			foreach (var type in types) {
				var cls = type as JsClass;
				if (cls != null) {
					var attr = _attributeStore.AttributesFor(type.CSharpTypeDefinition).GetAttribute<TestFixtureAttribute>();
					yield return attr != null ? ConvertType(cls) : type;
				}
				else {
					yield return type;
				}
			}
		}
	}
}
