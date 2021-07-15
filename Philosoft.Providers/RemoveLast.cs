﻿using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Traits.Concepts {
	public static partial class Collection {
		/// <summary>
		/// Removes the last instance of the element from this object.
		/// </summary>
		/// <param name="collection">The elements in this collection.</param>
		/// <param name="count">The amount of elements in the collection; the amount currently in use.</param>
		/// <param name="element">The element to remove.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RemoveLast<TElement>([DisallowNull] TElement[] collection, ref Int32 count, [AllowNull] TElement element) => throw new NotImplementedException();
	}
}
