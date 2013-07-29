using System;
using System.Runtime.CompilerServices;

namespace QUnit {
	[Imported, Serializable]
	public class UrlConfig {
		/// <summary>
		/// Will be used as the config and query-string key.
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Will be used as the display propery (text in the UI).
		/// </summary>
		public string Label { get; set; }

		/// <summary>
		/// Will be used as the title attribute, and should explain what the checkbox does.
		/// </summary>
		public string Tooltip { get; set; }
	}
}