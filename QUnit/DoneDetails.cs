using System;
using System.Runtime.CompilerServices;

namespace QUnit {
	[Serializable, Imported]
	public class DoneDetails {
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
		/// The time in milliseconds it took tests to run from start to finish.
		/// </summary>
		public double Runtime { get; set; }
	}
}