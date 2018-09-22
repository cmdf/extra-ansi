using System;
using System.Collections.Generic;

namespace App.data {
	/// <summary>
	/// List operations.
	/// </summary>
	class ListExt {

		// static method
		/// <summary>
		/// Remove n items from begin of list.
		/// </summary>
		/// <typeparam name="T">List type.</typeparam>
		/// <param name="l">List.</param>
		/// <param name="n">Number of items to remove.</param>
		public static void ShiftN<T>(IList<T> l, int n) {
			for(int i = 0; i < n; i++)
				l.RemoveAt(0);
		}
	}
}
