﻿using System;
using Xunit;

namespace Langly.DataStructures.Buffers {
	public class Bipartite1_Tests {
		[Fact]
		public void Constructor() {
			Bipartite<Int32> buffer = new Bipartite<Int32>();
		}

		[Theory]
		[InlineData(new Int32[] { })]
		[InlineData(new Int32[] { 1, 2, 3, 4, 5 })]
		public void Read(Int32[] values) {
			Bipartite<Int32> buffer = new Bipartite<Int32>().Write(values);
			foreach (Int32 expected in values) {
				_ = buffer.Read(out Int32 value);
				Assert.Equal(expected, value);
			}
		}

		[Theory]
		[InlineData(new Int32[] { }, new Int32[] { }, new Int32[] { })]
		[InlineData(new Int32[] { 1, 2, 3, 4, 5 }, new Int32[] { 1, 2, 3 }, new Int32[] { 4, 5 })]
		public void Write(Int32[] expected, Int32[] first, Int32[] second) {
			Bipartite<Int32> buffer = new Bipartite<Int32>();
			_ = buffer.Write(first).Write(second);
			Assert.Equal(expected, buffer);
		}
	}
}
