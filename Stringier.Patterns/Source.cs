﻿using System;
using System.Collections.Generic;
using System.Text;

namespace System.Text.Patterns {
	public ref struct Source {
		private readonly ReadOnlySpan<Char> Buffer;

		public Source(String String) {
			Buffer = String.AsSpan();
			Position = 0;
		}

		public Int32 Length => Buffer.Length - Position;

		public Int32 Position { get; internal set; }

		public ReadOnlySpan<Char> Peek(Int32 Count) => Buffer.Slice(Position, Count);

		public ReadOnlySpan<Char> Read(Int32 Count) {
			ReadOnlySpan<Char> Result = Peek(Count);
			Position += Count;
			return Result;
		}

		public override String ToString() => Buffer.Slice(Position).ToString();
	}
}