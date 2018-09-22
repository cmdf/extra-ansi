using System;
using System.Collections.Generic;
using App.data;
using App.io;

namespace App.esc {
	/// <summary>
	/// Select Graphics Rendition.
	/// </summary>
	class Sgr {

		#region constant data
		/// <summary>
		/// SGR 0: reset / normal.
		/// </summary>
		private const int RST = 0;
		/// <summary>
		/// SGR 1: bold or increased intensity.
		/// </summary>
		private const int BLD1 = 1;
		/// <summary>
		/// SGR 2: faint (decreased intensity).
		/// </summary>
		private const int LGHT1 = 2;
		/// <summary>
		/// SGR 3: italic on.
		/// </summary>
		private const int ITLC1 = 3;
		/// <summary>
		/// SGR 4: underline single.
		/// </summary>
		private const int UNDRLN1 = 4;
		/// <summary>
		/// SGR 5: blink slow.
		/// </summary>
		private const int BLNKS1 = 5;
		/// <summary>
		/// SGR 6: blink rapid.
		/// </summary>
		private const int BLNKR1 = 6;
		/// <summary>
		/// SGR 7: image negative.
		/// </summary>
		private const int NGTV1 = 7;
		/// <summary>
		/// SGR 8: conceal.
		/// </summary>
		private const int CNCL1 = 8;
		/// <summary>
		/// SGR 9: crossed out.
		/// </summary>
		private const int CRSSD1 = 9;
		/// <summary>
		/// SGR 10: primary (default) font.
		/// </summary>
		private const int FNT0 = 10;
		/// <summary>
		/// SGR 11: nth-alternate font begin (11-19).
		/// </summary>
		private const int FNT1B = 11;
		/// <summary>
		/// SGR 19: nth-alternate font end (11-19).
		/// </summary>
		private const int FNT1E = 19;
		/// <summary>
		/// SGR 20: fraktur.
		/// </summary>
		private const int FRKTR = 20;
		/// <summary>
		/// SGR 21: bold off or underline double.
		/// </summary>
		private const int BLD0 = 21;
		/// <summary>
		/// SGR 22: normal color or intensity.
		/// </summary>
		private const int LGHT0 = 22;
		/// <summary>
		/// SGR 23: not italic, not fraktur.
		/// </summary>
		private const int ITLC0 = 23;
		/// <summary>
		/// SGR 24: underline none.
		/// </summary>
		private const int UNDRLN0 = 24;
		/// <summary>
		/// SGR 25: blink off.
		/// </summary>
		private const int BLNK0 = 25;
		/// <summary>
		/// SGR 26: reserved.
		/// </summary>
		private const int RSRVD0 = 26;
		/// <summary>
		/// SGR 27: image positive.
		/// </summary>
		private const int NGTV0 = 27;
		/// <summary>
		/// SGR 28: reveal.
		/// </summary>
		private const int CNCL0 = 28;
		/// <summary>
		/// SGR 29: not crossed out.
		/// </summary>
		private const int CRSSD0 = 29;
		/// <summary>
		/// SGR 30: set text color begin (foreground) (30-37).
		/// </summary>
		private const int FRCLR1B = 30;
		/// <summary>
		/// SGR 37: set text color end (foreground) (30-37).
		/// </summary>
		private const int FRCLR1E = 37;
		/// <summary>
		/// SGR 38: reserved for extended set foreground color.
		/// </summary>
		private const int FRCLR1EX = 38;
		/// <summary>
		/// SGR 39: default text color (foreground).
		/// </summary>
		private const int FRCLR0 = 39;
		/// <summary>
		/// SGR 40: set background color begin (40-47).
		/// </summary>
		private const int BCKCLR1B = 40;
		/// <summary>
		/// SGR 47: set background color end (40-47).
		/// </summary>
		private const int BCKCLR1E = 47;
		/// <summary>
		/// SGR 48: reserved for extended set background color.
		/// </summary>
		private const int BCKCLR1EX = 48;
		/// <summary>
		/// SGR 49: default background color.
		/// </summary>
		private const int BCKCLR0 = 49;
		/// <summary>
		/// SGR 50: reserved.
		/// </summary>
		private const int RSRVD1 = 50;
		/// <summary>
		/// SGR 51: framed.
		/// </summary>
		private const int BRDRF1 = 51;
		/// <summary>
		/// SGR 52: encircled.
		/// </summary>
		private const int BRDRC1 = 52;
		/// <summary>
		/// SGR 53: overlined.
		/// </summary>
		private const int OVRLND1 = 53;
		/// <summary>
		/// SGR 54: not framed or encirled.
		/// </summary>
		private const int BRDR0 = 54;
		/// <summary>
		/// SGR 55: not overlined.
		/// </summary>
		private const int OVRLND0 = 55;
		/// <summary>
		/// SGR 56: reserved.
		/// </summary>
		private const int RSRVD2 = 56;
		/// <summary>
		/// SGR 57: reserved.
		/// </summary>
		private const int RSRVD3 = 57;
		/// <summary>
		/// SGR 58: reserved.
		/// </summary>
		private const int RSRVD4 = 58;
		/// <summary>
		/// SGR 59: reserved.
		/// </summary>
		private const int RSRVD5 = 59;
		/// <summary>
		/// SGR 60: ideogram underline or right side line.
		/// </summary>
		private const int IUNDRLNSR = 60;
		/// <summary>
		/// SGR 61: ideogram double underline or double line on the right side.
		/// </summary>
		private const int IUNDRLNDR = 61;
		/// <summary>
		/// SGR 62: ideogram overline or left side line.
		/// </summary>
		private const int IOVRLNSL = 62;
		/// <summary>
		/// SGR 63: ideogram double overline or double line on the left side.
		/// </summary>
		private const int IOVRLNDL = 63;
		/// <summary>
		/// SGR 64: ideogram stress marking.
		/// </summary>
		private const int ISMRK = 64;
		/// <summary>
		/// SGR 65: ideogram attributes off.
		/// </summary>
		private const int IATTR0 = 65;
		/// <summary>
		/// SGR 90: set foreground text color, high intensity begin (90-97).
		/// </summary>
		private const int FRCLRH1B = 90;
		/// <summary>
		/// SGR 97: set foreground text color, high intensity end (90-97).
		/// </summary>
		private const int FRCLRH1E = 97;
		/// <summary>
		/// SGR 100: set background color, high intensity begin (100-107).
		/// </summary>
		private const int BCKCLRH1B = 100;
		/// <summary>
		/// SGR 107: set background color, high intensity end (100-107).
		/// </summary>
		private const int BCKCLRH1E = 107;
		#endregion


		#region static data
		/// <summary>
		/// Bold bitwise-or flag.
		/// </summary>
		public static int Bld = 0x0;
		/// <summary>
		/// Faint bitwise-and flag.
		/// </summary>
		public static int Lght = 0xF;
		/// <summary>
		/// Currently selected foreground color.
		/// </summary>
		public static int FrClr;
		#endregion


		#region static method
		/// <summary>
		/// Process SGR control code.
		/// </summary>
		/// <param name="a">fn;args.</param>
		/// <returns>0.</returns>
		public static int Write(IList<string> a) {
			while(a.Count > 0) {
				int n = ValExt.Int(a, 0, RST);
				if(n == RST) Rst(a);
				else if(n == BLD1) Bld1(a);
				else if(n == BLD0) Bld0(a);
				else if(n == LGHT1) Lght1(a);
				else if(n == LGHT0) Lght0(a);
				else if(n >= FRCLR1B && n <= FRCLR1E) FrClr1(a);
				else if(n == FRCLR1EX) FrClr1Ex(a);
				else if(n == FRCLR0) FrClr0(a);
				else if(n >= BCKCLR1B && n <= BCKCLR1E) BckClr1(a);
				else if(n == BCKCLR1EX) BckClr1Ex(a);
				else if(n == BCKCLR0) BckClr0(a);
				else if(n >= FRCLRH1B && n <= FRCLRH1E) FrClrH1(a);
				else if(n >= BCKCLRH1B && n <= BCKCLRH1E) BckClrH1(a);
				a.RemoveAt(0);
			}
			return 0;
		}

		/// <summary>
		/// Reset / Normal.
		/// </summary>
		/// <param name="a">.</param>
		private static void Rst(IList<string> a) {
			Console.ResetColor();
			FrClr = 7;
			Bld = 0x0;
			Lght = 0xF;
		}

		/// <summary>
		/// Bold or increased intensity.
		/// </summary>
		/// <param name="a">.</param>
		private static void Bld1(IList<string> a) {
			Cmd.ForegroundColor = (FrClr | (Bld = 0x8)) & Lght;
		}

		/// <summary>
		/// Bold off or underline double.
		/// </summary>
		/// <param name="a">.</param>
		private static void Bld0(IList<string> a) {
			Cmd.ForegroundColor = (FrClr | (Bld = 0x0)) & Lght;
		}

		/// <summary>
		/// Faint (decreased intensity).
		/// </summary>
		/// <param name="a">.</param>
		private static void Lght1(IList<string> a) {
			Cmd.ForegroundColor = (FrClr | Bld) & (Lght = 0x7);
		}

		/// <summary>
		/// Normal color or intensity.
		/// </summary>
		/// <param name="a">.</param>
		private static void Lght0(IList<string> a) {
			Cmd.ForegroundColor = (FrClr | (Bld = 0x0)) & (Lght = 0x0F);
		}

		/// <summary>
		/// Set text color (foreground).
		/// </summary>
		/// <param name="a">clr.</param>
		private static void FrClr1(IList<string> a) {
			FrClr = ValExt.Int(a, 0) - FRCLR1B;
			Cmd.ForegroundColor = (FrClr | Bld) & Lght;
		}

		/// <summary>
		/// Extended set foreground color.
		/// </summary>
		/// <param name="a">2;r;g;b | 5;clr.</param>
		private static void FrClr1Ex(IList<string> a) {
			int n = ValExt.Int(a, 1);
			int FrClr = n == 2 ? Cmd.RgbColor(ValExt.Int(a, 2), ValExt.Int(a, 3), ValExt.Int(a, 4)) : (ValExt.Int(a, 1) & 0xF);
			Cmd.ForegroundColor = (FrClr | Bld) & Lght;
			ListExt.ShiftN(a, 4);
		}

		/// <summary>
		/// Default text color (foreground).
		/// </summary>
		/// <param name="a">.</param>
		private static void FrClr0(IList<string> a) {
			Cmd.ForegroundColor = ((FrClr = 7) | Bld) & Lght;
		}

		/// <summary>
		/// Set background color.
		/// </summary>
		/// <param name="a">clr.</param>
		private static void BckClr1(IList<string> a) {
			int c = ValExt.Int(a, 0) - BCKCLR1B;
			Cmd.BackgroundColor = c;
		}

		/// <summary>
		/// Extended set background color.
		/// </summary>
		/// <param name="a">2;r;g;b | 5;clr.</param>
		private static void BckClr1Ex(IList<string> a) {
			int n = ValExt.Int(a, 1);
			int c = n == 2 ? Cmd.RgbColor(ValExt.Int(a, 2), ValExt.Int(a, 3), ValExt.Int(a, 4)) : (ValExt.Int(a, 2) & 0xF);
			Cmd.BackgroundColor = c;
			ListExt.ShiftN(a, 5);
		}

		/// <summary>
		/// Default background color.
		/// </summary>
		/// <param name="a">.</param>
		private static void BckClr0(IList<string> a) {
			Console.BackgroundColor = ConsoleColor.Black;
		}

		/// <summary>
		/// Set foreground text color, high intensity.
		/// </summary>
		/// <param name="a">clr.</param>
		private static void FrClrH1(IList<string> a) {
			FrClr = ValExt.Int(a, 0) - FRCLRH1B + 8;
			Cmd.ForegroundColor = (FrClr | Bld) & Lght;
		}

		/// <summary>
		/// Set background color, high intensity.
		/// </summary>
		/// <param name="a">clr.</param>
		private static void BckClrH1(IList<string> a) {
			int c = ValExt.Int(a, 0) - BCKCLRH1B + 8;
			Cmd.BackgroundColor = c;
		}
		#endregion
	}
}
