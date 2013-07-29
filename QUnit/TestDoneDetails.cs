using System;
using System.Runtime.CompilerServices;

namespace QUnit {
	[Serializable, Imported]
	public class TestDoneDetails {
		/// <summary>
		/// Name of the test.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Name of the current module.
		/// </summary>
		public string Module { get; set; }

		/// <summary>
		/// The number of failed assertions.
		/// </summary>
		public int Failed { get; set; }

		/// <summary>
		/// The number of passed assertions.
		/// </summary>
		public int Passed { get; set; }

		/// <summary>
		/// The total number of assertions.
		/// </summary>
		public int Total { get; set; }

		/// <summary>
		/// The total runtime, including setup and teardown.
		/// </summary>
		public double Duration { get; set; }
	}
}