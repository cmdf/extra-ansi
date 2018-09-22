using System;
using System.Collections.Generic;
using App.io;

namespace App.esc {
	/// <summary>
	/// C1 control character set.
	/// </summary>
	class C1 {

		// constant data
		/// <summary>
		/// ESC @: padding character.
		/// </summary>
		private const int PAD = '@';
		/// <summary>
		/// ESC A: high octet preset.
		/// </summary>
		private const int HOP = 'A';
		/// <summary>
		/// ESC B: break permitted here.
		/// </summary>
		private const int BPH = 'B';
		/// <summary>
		/// ESC C: no break here.
		/// </summary>
		private const int NBH = 'C';
		/// <summary>
		/// ESC D: index.
		/// </summary>
		private const int IND = 'D';
		/// <summary>
		/// ESC E: next line.
		/// </summary>
		private const int NEL = 'E';
		/// <summary>
		/// ESC F: start of selected area.
		/// </summary>
		private const int SSA = 'F';
		/// <summary>
		/// ESC G: end of selected area.
		/// </summary>
		private const int ESA = 'G';
		/// <summary>
		/// ESC H: character tabulation set (horizontal tabulation set).
		/// </summary>
		private const int HTS = 'H';
		/// <summary>
		/// ESC I: character tabulation with justification (horizontal tabulation with justification).
		/// </summary>
		private const int HTJ = 'I';
		/// <summary>
		/// ESC J: line tabulation set (vertical tabulation set).
		/// </summary>
		private const int VTS = 'J';
		/// <summary>
		/// ESC K: partial line forward (partial line down).
		/// </summary>
		private const int PLD = 'K';
		/// <summary>
		/// ESC L: partial line backward (partial line up).
		/// </summary>
		private const int PLU = 'L';
		/// <summary>
		/// ESC M: reverse line feed (reverse index).
		/// </summary>
		private const int RI = 'M';
		/// <summary>
		/// ESC N: single-shift 2.
		/// </summary>
		private const int SS2 = 'N';
		/// <summary>
		/// ESC O: single-shift 3.
		/// </summary>
		private const int SS3 = 'O';
		/// <summary>
		/// ESC P: device control string.
		/// </summary>
		private const int DCS = 'P';
		/// <summary>
		/// ESC Q: private use 1.
		/// </summary>
		private const int PU1 = 'Q';
		/// <summary>
		/// ESC R: private use 2.
		/// </summary>
		private const int PU2 = 'R';
		/// <summary>
		/// ESC S: set transmit state.
		/// </summary>
		private const int STS = 'S';
		/// <summary>
		/// ESC T: cancel character.
		/// </summary>
		private const int CCH = 'T';
		/// <summary>
		/// ESC U: MW: message waiting.
		/// </summary>
		private const int MW = 'U';
		/// <summary>
		/// ESC V: start of protected area.
		/// </summary>
		private const int SPA = 'V';
		/// <summary>
		/// ESC W: end of protected area.
		/// </summary>
		private const int EPA = 'W';
		/// <summary>
		/// ESC X: start of string.
		/// </summary>
		private const int SOS = 'X';
		/// <summary>
		/// ESC Y: single graphic character introducer.
		/// </summary>
		private const int SGCI = 'Y';
		/// <summary>
		/// ESC Z: single character introducer.
		/// </summary>
		private const int SCI = 'Z';
		/// <summary>
		/// ESC [: control sequence introducer.
		/// </summary>
		private const int CSI = '[';
		/// <summary>
		/// ESC \: string terminator.
		/// </summary>
		private const int ST = '\\';
		/// <summary>
		/// ESC ]: operating system command.
		/// </summary>
		private const int OSC = ']';
		/// <summary>
		/// ESC ^: privacy message.
		/// </summary>
		private const int PM = '^';
		/// <summary>
		/// ESC _: application program command.
		/// </summary>
		private const int APC = '_';
		/// <summary>
		/// ESC c: reset to intitial state.
		/// </summary>
		private const int RIS = 'c';


		// static method
		/// <summary>
		/// Process C1 control set character.
		/// </summary>
		/// <param name="a">NA.</param>
		/// <returns>-ve if end of stream, else +ve.</returns>
		public static int Write(IList<string> a) {
			int c = Console.Read();
			if(c == NEL) return Cmd.Write('\n');
			else if(c == SCI) return Cmd.Write(Console.Read());
			else if(c == CSI) return Csi.Write(a);
			else if(c == RIS) Console.ResetColor();
			return c;
		}
	}
}
