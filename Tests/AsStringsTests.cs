﻿using System;
using System.Text;
using Stringier;
using Xunit;

namespace Tests {
	public class AsStringsTests {
		[Theory]
		[InlineData("hello", new[] { 0x68, 0x65, 0x6C, 0x6C, 0x6F })]
		[InlineData("привет", new[] { 0x43F, 0x440, 0x438, 0x432, 0x435, 0x442 })]
		[InlineData("𝄞", new[] { 0x1D11E })]
		public void AsString_Runes(String expected, Int32[] scalarValues) {
			Rune[] runes = new Rune[scalarValues.Length];
			for (Int32 i = 0; i < runes.Length; i++) {
				runes[i] = new Rune(scalarValues[i]);
			}
			Assert.Equal(expected, runes.AsString());
		}
	}
}
