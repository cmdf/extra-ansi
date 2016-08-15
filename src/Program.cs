using System;
using orez.oansi.esc;

namespace orez.oansi {
	class Program {
		static void Main(string[] args) {
			int a = 0;
			while(a >= 0)
				a = oC0.Write(null);
			Console.ResetColor();
		}
	}
}
