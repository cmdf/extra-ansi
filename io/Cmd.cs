using System;
using System.Collections.Generic;
using App.data;

namespace App.io {
	class Cmd {

		// static data
		/// <summary>
		/// Console color to color number mapping.
		/// </summary>
		private static Dictionary<ConsoleColor, int> ClrNum = new Dictionary<ConsoleColor, int>() {
			{ConsoleColor.Black, 0}, {ConsoleColor.DarkRed, 1}, {ConsoleColor.DarkGreen, 2}, {ConsoleColor.DarkYellow, 3},
			{ConsoleColor.DarkBlue, 4}, {ConsoleColor.DarkMagenta, 5}, {ConsoleColor.DarkCyan, 6}, {ConsoleColor.Gray, 7},
			{ConsoleColor.DarkGray, 8}, {ConsoleColor.Red, 9}, {ConsoleColor.Green, 10}, {ConsoleColor.Yellow, 11},
			{ConsoleColor.Blue, 12}, {ConsoleColor.Magenta, 13}, {ConsoleColor.Cyan, 14}, {ConsoleColor.White, 15}
		};
		/// <summary>
		/// Color number to console color mapping.
		/// </summary>
		private static Dictionary<int, ConsoleColor> NumClr = new Dictionary<int, ConsoleColor>() {
			{0, ConsoleColor.Black}, {1, ConsoleColor.DarkRed}, {2, ConsoleColor.DarkGreen}, {3, ConsoleColor.DarkYellow},
			{4, ConsoleColor.DarkBlue}, {5, ConsoleColor.DarkMagenta}, {6, ConsoleColor.DarkCyan}, {7, ConsoleColor.Gray},
			{8, ConsoleColor.DarkGray}, {9, ConsoleColor.Red}, {10, ConsoleColor.Green}, {11, ConsoleColor.Yellow},
			{12, ConsoleColor.Blue}, {13, ConsoleColor.Magenta}, {14, ConsoleColor.Cyan}, {15, ConsoleColor.White}
		};


		// static property
		/// <summary>
		/// Get or set cursor left wrt buffer.
		/// </summary>
		public static int CursorLeft {
			get { return Console.CursorLeft; }
			set { Console.CursorLeft = MathExt.Limit(value, 0, Console.BufferWidth); }
		}
		/// <summary>
		/// Get or set cursor top wrt buffer.
		/// </summary>
		public static int CursorTop {
			get { return Console.CursorTop; }
			set { Console.CursorTop = MathExt.Limit(value, 0, Console.BufferHeight); }
		}
		/// <summary>
		/// Get or set cursor left wrt window.
		/// </summary>
		public static int CursorWLeft {
			get { return Console.CursorLeft - Console.WindowLeft; }
			set {
				int c = MathExt.Limit(value, 0, Console.WindowWidth);
				Console.CursorLeft = Console.WindowLeft + c;
			}
		}
		/// <summary>
		/// Get or set cursor top wrt window.
		/// </summary>
		public static int CursorWTop {
			get {	return Console.CursorTop - Console.WindowTop;	}
			set {
				int r = MathExt.Limit(value, 0, Console.WindowHeight);
				Console.CursorTop = Console.WindowTop + r;
			}
		}
		/// <summary>
		/// Get or set window left.
		/// </summary>
		public static int WindowLeft {
			get { return Console.WindowLeft; }
			set {
				int c = MathExt.Limit(value, 0, Console.BufferWidth - Console.WindowWidth + 1);
				Console.WindowLeft = c;
			}
		}
		/// <summary>
		/// Get or set console window top.
		/// </summary>
		public static int WindowTop {
			get { return Console.WindowTop; }
			set {
				int r = MathExt.Limit(value, 0, Console.BufferHeight - Console.WindowHeight + 1);
				Console.WindowTop = r;
			}
		}
		/// <summary>
		/// Get or set foreground color.
		/// </summary>
		public static int ForegroundColor {
			get { return ClrNum[Console.ForegroundColor]; }
			set {
				int c = MathExt.Limit(value, 0, 16);
				Console.ForegroundColor = NumClr[c];
			}
		}
		/// <summary>
		/// Get or set background color.
		/// </summary>
		public static int BackgroundColor {
			get { return ClrNum[Console.BackgroundColor]; }
			set {
				int c = MathExt.Limit(value, 0, 16);
				Console.BackgroundColor = NumClr[c];
			}
		}


		// static method
		/// <summary>
		/// Set cursor position wrt buffer.
		/// </summary>
		/// <param name="c">Column.</param>
		/// <param name="r">Row.</param>
		public static void SetCursorPosition(int c, int r) {
			CursorLeft = c;
			CursorTop = r;
		}

		/// <summary>
		/// Set cursor position wrt window.
		/// </summary>
		/// <param name="c">Column.</param>
		/// <param name="r">Row.</param>
		public static void SetCursorWPosition(int c, int r) {
			CursorWLeft = c;
			CursorWTop = r;
		}

		/// <summary>
		/// Get console color number from RGB color.
		/// </summary>
		/// <param name="r">Red value.</param>
		/// <param name="g">Green value.</param>
		/// <param name="b">Blue value.</param>
		/// <returns>Console color number.</returns>
		public static int RgbColor(int r, int g, int b) {
			int p = r > 0xC0 || g > 0xC0 || b > 0xC0? 0xC0 : 0x40;
			int v = (p == 0xC0? 8 : 0) | (r >= p ? 1 : 0) | (g >= p ? 2 : 0) | (b >= p ? 4 : 0);
			return (v & 7) == 7 ? v ^ 8 : v;
		}

		/// <summary>
		/// Write character to console.
		/// </summary>
		/// <param name="v">Character to write.</param>
		/// <returns>-ve if invalid character, else +ve.</returns>
		public static int Write(int v) {
			if(v < 0) return -1;
			Console.Write((char)v);
			return 0;
		}

		/// <summary>
		/// Writes character in specified buffer range.
		/// </summary>
		/// <param name="r0">Row begin (inc).</param>
		/// <param name="c0">Column begin (inc).</param>
		/// <param name="r1">Row end (inc).</param>
		/// <param name="c1">Column end (exc).</param>
		/// <param name="v">Value.</param>
		/// <returns>-ve if invalid character, else +ve.</returns>
		public static int WriteRange(int c0, int r0, int c1, int r1, int v = ' ') {
			if(v < 0) return -1;
			int r = Console.CursorTop, c = Console.CursorLeft;
			Console.SetCursorPosition(c0, r0);
			Console.Write(new string((char)v, (r1 - r0) * Console.BufferWidth + (c1 - c0)));
			Console.SetCursorPosition(c, r);
			return 0;
		}

		/// <summary>
		/// Writes character in specified window range.
		/// </summary>
		/// <param name="r0">Row begin (inc).</param>
		/// <param name="c0">Column begin (inc).</param>
		/// <param name="r1">Row end (inc).</param>
		/// <param name="c1">Column end (exc).</param>
		/// <param name="v">Value.</param>
		/// <returns>-ve if invalid character, else +ve.</returns>
		public static int WriteWRange(int c0, int r0, int c1, int r1, int v = ' ') {
			return WriteRange(Console.WindowLeft + c0, Console.WindowHeight + r0, Console.WindowLeft + c1, Console.WindowHeight + r1, v);
		}
	}
}
