using System;
using System.Runtime.CompilerServices;

namespace QUnit {
	[Imported, Serializable]
	public class ModuleDoneDetails {
		/// <summary>
		/// Name of this module.
		/// </summary>
		public string Name { get; set; }

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
	}
}