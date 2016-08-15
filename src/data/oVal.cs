using System.Collections.Generic;

namespace orez.oansi.data {
	class oVal {

		// static method
		/// <summary>
		/// Get integer value from list.
		/// </summary>
		/// <param name="a">Input arguments (list).</param>
		/// <param name="i">Index.</param>
		/// <param name="vd">Default value.</param>
		/// <returns>Integer from list.</returns>
		public static int Int(IList<string> a, int i, int vd = 0) {
			if(a.Count > i) int.TryParse(a[i], out vd);
			return vd;
		}
	}
}
