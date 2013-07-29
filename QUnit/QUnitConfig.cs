using System;
using System.Runtime.CompilerServices;

namespace QUnit {
	[Serializable, Imported]
	public class QUnitConfig {
		/// <summary>
		/// By default, QUnit updates document.title to add a checkmark or x-mark to indicate if a testsuite passed or failed. This makes it easy to see a suites result even without looking at a tab's content. If you're dealing with code that tests document.title changes or have some other problem with this feature, set this option to false to disable it.
		/// </summary>
		[ScriptName("altertitle")] public bool AlterTitle { get; set; }

		/// <summary>
		/// By default, QUnit runs tests when load event is triggered on the window. If you're loading tests asynchronsly, you can set this property to false, then call QUnit.start() once everything is loaded.
		/// </summary>
		public bool Autostart { get; set; }

		/// <summary>
		/// This object isn't actually a configuration property, but is listed here anyway, as its exported through QUnit.config. This gives you access to some QUnit internals at runtime.
		/// </summary>
		public object Current { get; set; }

		/// <summary>
		/// By default, QUnit will run tests first that failed on a previous run. In a large testsuite, this can speed up testing a lot. It can also lead to random errors, in case your testsuite has non-atomic tests, where the order is important. You should fix those issues, instead of disabling reordering!
		/// </summary>
		public bool Reorder { get; set; }

		/// <summary>
		/// The expect() method is optional by default, though it can be useful to require each test to specify the number of expected assertions. Enabling this option will cause tests to fail, if they don't call expect() at all.
		/// </summary>
		public bool RequireExpects { get; set; }

		/// <summary>
		/// Specify a global timeout in milliseconds after which all tests will fail with an appropiate message. Useful when async tests aren't finishing, to prevent the testrunner getting stuck. Set to something high, e.g. 30000 (30 seconds) to avoid slow tests to time out by accident.
		/// </summary>
		public double TestTimeout { get; set; }

		/// <summary>
		/// This property controls which checkboxes to put into the QUnit toolbar element (below the header). By default, the "noglobals" and "notrycatch" checkboxes are there. By extending this array, you can add your own checkboxes.
		/// </summary>
		public UrlConfig[] UrlConfig { get; set; }
	}
}