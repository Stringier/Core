﻿using System.Runtime.CompilerServices;

namespace System.Traits.Concepts {
	public static partial class Collection {
		/// <summary>
		/// Insert an element into the collection at the specified index.
		/// </summary>
		/// <param name="collection">The elements of this collection.</param>
		/// <param name="count">The amount of elements in the collection; the amount currently in use.</param>
		/// <param name="index">The index at which <paramref name="element"/> should be inserted.</param>
		/// <param name="element">The element to insert.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Insert<TElement>(TElement[] collection, ref Int32 count, Int32 index, TElement element) {
			collection.AsSpan(index, count - index).CopyTo(collection.AsSpan(index + 1));
			collection[index] = element;
			count++;
		}

		/// <summary>
		/// Inserts the elements into the collection at the specified index, as a batch.
		/// </summary>
		/// <param name="collection">The elements of this collection.</param>
		/// <param name="count">The amount of elements in the collection; the amount currently in use.</param>
		/// <param name="index">The index at which the <paramref name="elements"/> should be inserted.</param>
		/// <param name="elements">The elements to insert.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Insert<TElement>(TElement[] collection, ref Int32 count, Int32 index, ReadOnlySpan<TElement> elements) {
			collection.AsSpan(index, count - index).CopyTo(collection.AsSpan(index + elements.Length));
			elements.CopyTo(collection.AsSpan(index));
			count += elements.Length;
		}
	}
}
