using System;
using System.Runtime.CompilerServices;

namespace QUnit {
	[Serializable, Imported]
	public class LogDetails {
		/// <summary>
		/// The boolean result of an assertion, true means passed, false means failed.
		/// </summary>
		public bool Result { get; set; }

		/// <summary>
		/// One side of a comparision assertion. Can be undefined when ok() is used.
		/// </summary>
		public object Actual { get; set; }

		/// <summary>
		/// One side of a comparision assertion. Can be undefined when ok() is used.
		/// </summary>
		public object Expected { get; set; }

		/// <summary>
		/// A string description provided by the assertion.
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// The associated stacktrace, either from an exception or pointing to the source of the assertion. Depends on browser support for providing stacktraces, so can be undefined.
		/// </summary>
		public string Source { get; set; }
	}
}