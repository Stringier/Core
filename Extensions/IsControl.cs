﻿using System;
#if !NETSTANDARD2_0
using System.Text;
#endif

namespace Stringier {
	public static partial class StringierExtensions {
		/// <summary>
		/// Indicates whether the specified Unicode character is categorized as a control character.
		/// </summary>
		/// <param name="char">The Unicode character to evaluate.</param>
		/// <returns><see langword="true"/> if <paramref name="char"/> is a control character; otherwise, <see langword="false"/>.</returns>
		public static Boolean IsControl(this Char @char) => Char.IsControl(@char);

#if !NETSTANDARD2_0
		/// <summary>
		/// Indicates whether the specified Unicode character is categorized as a control character.
		/// </summary>
		/// <param name="rune">The Unicode character to evaluate.</param>
		/// <returns><see langword="true"/> if <paramref name="rune"/> is a control character; otherwise, <see langword="false"/>.</returns>
		public static Boolean IsControl(this Rune rune) => Rune.IsControl(rune);
#endif
	}
}
