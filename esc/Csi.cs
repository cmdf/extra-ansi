using System;
using System.Text;
using System.Collections.Generic;
using App.data;
using App.io;

namespace App.esc {
	/// <summary>
	/// Control Sequence Introducer control character set.
	/// </summary>
	class Csi {

		// constant data
		/// <summary>
		/// CSI A: cursor up.
		/// </summary>
		private const int CUU = 'A';
		/// <summary>
		/// CSI B: cursor down.
		/// </summary>
		private const int CUD = 'B';
		/// <summary>
		/// CSI C: cursor forward.
		/// </summary>
		private const int CUF = 'C';
		/// <summary>
		/// CSI D: cursor back.
		/// </summary>
		private const int CUB = 'D';
		/// <summary>
		/// CSI E: cursor next line.
		/// </summary>
		private const int CNL = 'E';
		/// <summary>
		/// CSI F: cursor previous line.
		/// </summary>
		private const int CPL = 'F';
		/// <summary>
		/// CSI G: cursor horizontal absolute.
		/// </summary>
		private const int CHA = 'G';
		/// <summary>
		/// CSI H: cursor position.
		/// </summary>
		private const int CUP = 'H';
		/// <summary>
		/// CSI J: erase display.
		/// </summary>
		private const int ED = 'J';
		/// <summary>
		/// CSI K: erase in line.
		/// </summary>
		private const int EL = 'K';
		/// <summary>
		/// CSI S: scroll up.
		/// </summary>
		private const int SU = 'S';
		/// <summary>
		/// CSI T: scroll down.
		/// </summary>
		private const int SD = 'T';
		/// <summary>
		/// CSI f: horizontal and vertical position.
		/// </summary>
		private const int HVP = 'f';
		/// <summary>
		/// CSI m: select graphic rendition.
		/// </summary>
		private const int SGR = 'm';
		/// <summary>
		/// CSI i: aux port off (4) / on (5).
		/// </summary>
		private const int AUX = 'i';
		/// <summary>
		/// CSI n: device status report (6).
		/// </summary>
		private const int DSR = 'n';
		/// <summary>
		/// CSI s: save cursor position.
		/// </summary>
		private const int SCP = 's';
		/// <summary>
		/// CSI u: restore cursor position.
		/// </summary>
		private const int RCP = 'u';
		/// <summary>
		/// CSI l: hide the cursor (?25)
		/// </summary>
		private const int DECTCEM0 = 'l';
		/// <summary>
		/// CSI h: show the cursor (?25)
		/// </summary>
		private const int DECTCEM1 = 'h';
		/// <summary>
		/// CSI final character begin (0x40-0x7E).
		/// </summary>
		private const int FINALB = '@';
		/// <summary>
		/// CSI final character end (0x40-0x7E).
		/// </summary>
		private const int FINALE = '~';


		// static data
		/// <summary>
		/// Stores saved cursor positions.
		/// </summary>
		private static List<int[]> CurPos = new List<int[]>();
		/// <summary>
		/// CSI code to function mapping.
		/// </summary>
		private static Dictionary<int, Action<IList<string>>> Fn = new Dictionary<int, Action<IList<string>>>() {
			{CUU, Cuu}, {CUD, Cud}, {CUF, Cuf}, {CUB, Cub}, {CNL, Cnl}, {CPL, Cpl}, {CHA, Cha}, {CUP, Cup}, {ED, Ed}, {EL, El},
			{SU, Su}, {SD, Sd}, {HVP, Hvp}, {SGR, Sgr}, {DSR, Dsr}, {SCP, Scp}, {RCP, Rcp}, {DECTCEM0, Dectcem0}, {DECTCEM1, Dectcem1}
		};


		// static method
		/// <summary>
		/// Process CSI control code.
		/// </summary>
		/// <param name="a">NA.</param>
		/// <returns>-ve if end of stream, else +ve.</returns>
		public static int Write(IList<string> a) {
			int c = 0;
			a = new List<string>();
			StringBuilder s = new StringBuilder();
			while(true) {
				c = Console.Read();
				if(c < 0) return -1;
				if(c >= FINALB && c <= FINALE) break;
				if(c != ';') s.Append((char)c);
				else { a.Add(s.ToString()); s.Clear(); }
			}
			a.Add(s.ToString());
			if(Fn.ContainsKey(c)) Fn[c](a);
			return 0;
		}

		/// <summary>
		/// Cursor up.
		/// </summary>
		/// <param name="a">n.</param>
		private static void Cuu(IList<string> a) {
			int n = ValExt.Int(a, 0, 1);
			Cmd.CursorWTop -= n;
		}

		/// <summary>
		/// Cursor down.
		/// </summary>
		/// <param name="a">n.</param>
		private static void Cud(IList<string> a) {
			int n = ValExt.Int(a, 0, 1);
			Cmd.CursorWTop += n;
		}

		/// <summary>
		/// Cursor forward.
		/// </summary>
		/// <param name="a">n.</param>
		private static void Cuf(IList<string> a) {
			int n = ValExt.Int(a, 0, 1);
			Cmd.CursorWLeft += n;
		}
		
		/// <summary>
		/// Cursor back.
		/// </summary>
		/// <param name="a">n.</param>
		private static void Cub(IList<string> a) {
			int n = ValExt.Int(a, 0, 1);
			Cmd.CursorWLeft -= n;
		}

		/// <summary>
		/// Cursor next line.
		/// </summary>
		/// <param name="a">n.</param>
		private static void Cnl(IList<string> a) {
			int n = ValExt.Int(a, 0, 1);
			Cmd.SetCursorPosition(0, Cmd.CursorTop + n);
		}

		/// <summary>
		/// Cursor previous line.
		/// </summary>
		/// <param name="a">n.</param>
		private static void Cpl(IList<string> a) {
			int n = ValExt.Int(a, 0, 1);
			Cmd.SetCursorPosition(0, Cmd.CursorTop - n);
		}

		/// <summary>
		/// Cursor horizontal absolute.
		/// </summary>
		/// <param name="a">c.</param>
		private static void Cha(IList<string> a) {
			int c = ValExt.Int(a, 0, 1) - 1;
			Cmd.CursorWLeft = c;
		}

		/// <summary>
		/// Cursor position.
		/// </summary>
		/// <param name="a">r,c.</param>
		private static void Cup(IList<string> a) {
			int r = ValExt.Int(a, 0, 1) - 1;
			int c = ValExt.Int(a, 1, 1) - 1;
			Cmd.SetCursorWPosition(c, r);
		}

		/// <summary>
		/// Erase display.
		/// </summary>
		/// <param name="a">n.</param>
		private static void Ed(IList<string> a) {
			int n = ValExt.Int(a, 0, 0);
			if(n == 0) Cmd.WriteRange(Console.CursorLeft, Console.CursorTop, 0, Console.BufferHeight);
			else if(n == 1) Cmd.WriteRange(0, 0, Console.CursorLeft, Console.CursorTop);
			else { Console.SetCursorPosition(0, 0);	Console.Clear(); }
		}

		/// <summary>
		/// Erase in line.
		/// </summary>
		/// <param name="a">n.</param>
		private static void El(IList<string> a) {
			int n = ValExt.Int(a, 0, 0);
			if(n == 0) Cmd.WriteRange(Console.CursorLeft, Console.CursorTop, Console.BufferWidth, Console.CursorTop);
			else if(n == 1) Cmd.WriteRange(0, Console.CursorTop, Console.CursorLeft, Console.CursorTop);
			else Cmd.WriteRange(0, Console.CursorTop, 0, Console.CursorTop + 1);
		}

		/// <summary>
		/// Scroll up.
		/// </summary>
		/// <param name="a">n.</param>
		private static void Su(IList<string> a) {
			int n = ValExt.Int(a, 0, 1);
			Cmd.WindowTop -= n;
		}

		/// <summary>
		/// Scroll down.
		/// </summary>
		/// <param name="a">n.</param>
		private static void Sd(IList<string> a) {
			int n = ValExt.Int(a, 0, 1);
			Cmd.WindowTop += n;
		}

		/// <summary>
		/// Horizontal and vertical position.
		/// </summary>
		/// <param name="a">.</param>
		private static void Hvp(IList<string> a) {
			Cup(a);
		}

		/// <summary>
		/// Select graphic rendition.
		/// </summary>
		/// <param name="a">?.</param>
		private static void Sgr(IList<string> a) {
      esc.Sgr.Write(a);
		}

		/// <summary>
		/// Device status report.
		/// </summary>
		/// <param name="a">.</param>
		private static void Dsr(IList<string> a) {
			Console.Write("\x1B[{0};{1}R", Console.CursorTop, Console.CursorLeft);
		}

		/// <summary>
		/// Save cursor position.
		/// </summary>
		/// <param name="a">.</param>
		private static void Scp(IList<string> a) {
			CurPos.Add(new int[] { Console.CursorLeft, Console.CursorTop });
		}

		/// <summary>
		/// Restore cursor position.
		/// </summary>
		/// <param name="a">.</param>
		private static void Rcp(IList<string> a) {
			int l = CurPos.Count;
			if(l == 0) return;
			int[] p = CurPos[l - 1];
			CurPos.RemoveAt(l - 1);
			Console.SetCursorPosition(p[0], p[1]);
		}

		/// <summary>
		/// Hides the cursor.
		/// </summary>
		/// <param name="a">.</param>
		private static void Dectcem0(IList<string> a) {
			Console.CursorVisible = false;
		}

		/// <summary>
		/// Shows the cursor.
		/// </summary>
		/// <param name="a">.</param>
		private static void Dectcem1(IList<string> a) {
			Console.CursorVisible = true;
		}
	}
}
