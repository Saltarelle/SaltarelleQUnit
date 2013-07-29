using System;
using System.Runtime.CompilerServices;

namespace QUnit {
	[Serializable, Imported]
	public class ModuleStartDetails {
		/// <summary>
		/// Name of the next module to run.
		/// </summary>
		public string Name { get; set; }
	}
}