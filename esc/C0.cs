using System;
using System.Collections.Generic;
using App.io;

namespace App.esc {
	/// <summary>
	/// C0 control character set.
	/// </summary>
	class C0 {

		// constant data
		/// <summary>
		/// ^@: null.
		/// </summary>
		private const int NUL = 0x00;
		/// <summary>
		/// ^A: start of heading.
		/// </summary>
		private const int SOH = 0x01;
		/// <summary>
		/// ^B: start of text.
		/// </summary>
		private const int STX = 0x02;
		/// <summary>
		/// ^C: end of text.
		/// </summary>
		private const int ETX = 0x03;
		/// <summary>
		/// ^D: end of transmission.
		/// </summary>
		private const int EOT = 0x04;
		/// <summary>
		/// ^E: enquiry.
		/// </summary>
		private const int ENQ = 0x05;
		/// <summary>
		/// ^F: acknowledge.
		/// </summary>
		private const int ACK = 0x06;
		/// <summary>
		/// ^G: bell.
		/// </summary>
		private const int BEL = 0x07;
		/// <summary>
		/// ^H: backspace.
		/// </summary>
		private const int BS = 0x08;
		/// <summary>
		/// ^I: character tabulation (horizontal tabulation).
		/// </summary>
		private const int HT = 0x09;
		/// <summary>
		/// ^J: line feed.
		/// </summary>
		private const int LF = 0x0A;
		/// <summary>
		/// ^K: line tabulation (vertical tabulation).
		/// </summary>
		private const int VT = 0x0B;
		/// <summary>
		/// ^L: form feed.
		/// </summary>
		private const int FF = 0x0C;
		/// <summary>
		/// ^M: carriage return.
		/// </summary>
		private const int CR = 0x0D;
		/// <summary>
		/// ^N: shift out.
		/// </summary>
		private const int SO = 0x0E;
		/// <summary>
		/// ^O: shift in.
		/// </summary>
		private const int SI = 0x0F;
		/// <summary>
		/// ^P: data link escape.
		/// </summary>
		private const int DLE = 0x10;
		/// <summary>
		/// ^Q: device control one (XON).
		/// </summary>
		private const int DC1 = 0x11;
		/// <summary>
		/// ^R: device control two.
		/// </summary>
		private const int DC2 = 0x12;
		/// <summary>
		/// ^S: device control three (XOFF).
		/// </summary>
		private const int DC3 = 0x13;
		/// <summary>
		/// ^T: device control four.
		/// </summary>
		private const int DC4 = 0x14;
		/// <summary>
		/// ^U: negative acknowledge.
		/// </summary>
		private const int NAK = 0x15;
		/// <summary>
		/// ^V: synchronous idle.
		/// </summary>
		private const int SYN = 0x16;
		/// <summary>
		/// ^W: end of transmission block.
		/// </summary>
		private const int ETB = 0x17;
		/// <summary>
		/// ^X: cancel.
		/// </summary>
		private const int CAN = 0x18;
		/// <summary>
		/// ^Y: end of medium.
		/// </summary>
		private const int EM = 0x19;
		/// <summary>
		/// ^Z: substitute.
		/// </summary>
		private const int SUB = 0x1A;
		/// <summary>
		/// ^[: escape.
		/// </summary>
		private const int ESC = 0x1B;
		/// <summary>
		/// ^\: file separator.
		/// </summary>
		private const int FS = 0x1C;
		/// <summary>
		/// ^]: group separator.
		/// </summary>
		private const int GS = 0x1D;
		/// <summary>
		/// ^^: record separator.
		/// </summary>
		private const int RS = 0x1E;
		/// <summary>
		/// ^_: unit separator.
		/// </summary>
		private const int US = 0x1F;
		

		// static data
		/// <summary>
		/// Previous character used.
		/// </summary>
		private static int Prev;


		// static method
		/// <summary>
		/// Process C0 control set character.
		/// </summary>
		/// <param name="a">NA.</param>
		/// <returns>-ve if end of stream, else +ve.</returns>
		public static int Write(IList<string> a) {
			int c = Console.Read();
			if(c == ESC) return C1.Write(a);
			if(!IsLn(c) || !IsLn(Prev) || c == Prev) return Cmd.Write(c);
			return (Prev = c);
		}

		/// <summary>
		/// Indicates if a character is new line.
		/// </summary>
		/// <param name="v">Character.</param>
		/// <returns>Whether character is new line.</returns>
		private static bool IsLn(int v) {
			return v == CR || v == LF;
		}
	}
}
