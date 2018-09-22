using System;

namespace App.data {
	class MathExt {

		// static method
		/// <summary>
		/// Keep an integer within specified limit.
		/// </summary>
		/// <param name="v">Value.</param>
		/// <param name="b">Limit begin (inc).</param>
		/// <param name="e">Limit end (exc).</param>
		/// <returns>Value within limit.</returns>
		public static int Limit(int v, int b, int e) {
			return v = v < b ? b : ((v >= e) ? e - 1 : v);
		}


		/// <summary>
		/// Get positive modulus a number.
		/// </summary>
		/// <param name="v">Dividend.</param>
		/// <param name="d">Divisor.</param>
		/// <returns>Modulus value.</returns>
		public static int Mod(int v, int d) {
			return ((v % d) + d) % d;
		}
	}
}
