﻿using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Traits {
	public static partial class Collection {
		/// <summary>
		/// Determines whether this collection contains the specified <paramref name="element"/>.
		/// </summary>
		/// <param name="head">The head node of this collection.</param>
		/// <param name="element">The element to attempt to find.</param>
		/// <returns><see langword="true"/> if <paramref name="element"/> is contained in this collection; otherwise, <see langword="false"/>.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean Contains<TNode, TElement>([DisallowNull] TNode head, [AllowNull] TElement element) where TNode : class, IContains<TElement>, INext<TNode> {
			TNode? N = head;
			while (N is not null) {
				if (N.Contains(element)) return true;
				N = N.Next;
			}
			return false;
		}
	}
}