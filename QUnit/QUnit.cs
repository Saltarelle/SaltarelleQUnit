using System;
using System.Runtime.CompilerServices;

namespace QUnit {
	[IgnoreNamespace]
	[Imported]
	[ScriptName("QUnit")]
	public static class Engine {
		/// <summary>
		/// Start running tests again after the testrunner was stopped. When your async test has multiple exit points, call start() for the corresponding number of stop() increments.
		/// </summary>
		public static void Start() {
		}

		/// <summary>
		/// Start running tests again after the testrunner was stopped. When your async test has multiple exit points, call start() for the corresponding number of stop() increments.
		/// </summary>
		/// <param name="decrement">Optional argument to merge multiple start() calls into one. Use with multiple corrsponding stop() calls.</param>
		public static void Start(int decrement) {
		}

		/// <summary>
		/// Stop the testrunner to wait for async tests to run. Call start() to continue. When your async test has multiple exit points, call stop() with the increment argument, corresponding to the number of start() calls you need.
		/// </summary>
		public static void Stop() {
		}

		/// <summary>
		/// Stop the testrunner to wait for async tests to run. Call start() to continue. When your async test has multiple exit points, call stop() with the increment argument, corresponding to the number of start() calls you need.
		/// </summary>
		/// <param name="increment">Optional argument to merge multiple stop() calls into one. Use with multiple corrsponding start() calls.</param>
		public static void Stop(int increment) {
		}

		/// <summary>
		/// Register a callback to fire whenever the test suite begins. QUnit.begin() is called once before running any tests. (a better would've been QUnit.start, but thats already in use elsewhere and can't be changed.)
		/// </summary>
		public static void Begin(Action callback) {
		}

		/// <summary>
		/// Configuration for QUnit. QUnit has a bunch of internal configuration defaults, some of which are useful to override. Check the description for each option for details.
		/// </summary>
		[IntrinsicProperty]
		public static QUnitConfig Config { get; set; }

		/// <summary>
		/// Register a callback to fire whenever the test suite ends.
		/// </summary>
		public static void Done(Action<DoneDetails> callback) {
		}

		/// <summary>
		/// Initialize the test runner. If the runner has already run, calling QUnit.init() will re-initialize the runner, effectively resetting it. This method does not need to be called in the normal use of QUnit. This is useful for being able to use the test runner for multiple batches of dynamically-loaded tests (load a batch of tests, get the results, re-init, load a new batch, etc.).
		/// </summary>
		public static void Init() {
		}

		/// <summary>
		/// Register a callback to fire whenever an assertion completes. This is one of several callbacks QUnit provides. Its intended for integration scenarios like PhantomJS or Jenkins.
		/// </summary>
		public static void Log(Action<LogDetails> callback) {
		}

		/// <summary>
		/// Register a callback to fire whenever a module ends.
		/// </summary>
		public static void ModuleDone(Action<ModuleDoneDetails> callback) {
		}

		/// <summary>
		/// Register a callback to fire whenever a module begins.
		/// </summary>
		public static void ModuleStart(Action<ModuleStartDetails> callback) {
		}

		/// <summary>
		/// DEPRECATED: Reset the test fixture in the DOM. This methods is DEPRECATED. Don't use it!
		///  * Use multiple tests instead of resetting inside a test.
		///  * Use testStart or testDone for custom cleanup.
		///  * This method will throw an error in 2.0, and will be removed in 2.1
		/// Automatically called by QUnit after each test. Can be called by test code, though usually its better to seperate test code with multiple calls to test().
		/// When QUnit is run in a browser, it looks for #qunit-fixture element. If found, QUnit will store the contained markup before running any test, then restoring that markup after each test. If tests manipulate elements only within one of those elements, they won't affect each other.
		/// </summary>
		[Obsolete("This methods is DEPRECATED. Don't use it! It will throw an error in 2.0, and will be removed in 2.1")]
		public static void Reset() {
		}

		/// <summary>
		/// Register a callback to fire whenever a test ends.
		/// </summary>
		public static void TestDone(Action<TestDoneDetails> callback) {
		}

		/// <summary>
		/// Register a callback to fire whenever a test begins.
		/// </summary>
		public static void TestStart(Action<TestStartDetails> callback) {
		}
	}
}
