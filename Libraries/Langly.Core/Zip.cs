﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Langly.DataStructures.Views;

namespace Langly {
	public static partial class CoreExtensions {
		/// <summary>
		/// Zips the two collections together.
		/// </summary>
		/// <typeparam name="TIndex">The type of the indicies.</typeparam>
		/// <typeparam name="TElement">The type of the elements.</typeparam>
		/// <param name="indicies">The collection to use as the indicies.</param>
		/// <param name="elements">The collection to use as the elements.</param>
		/// <returns>A <see cref="ZipView{TIndex, TElement, TIndexCollection, TElementCollection}"/> combining the two collections.</returns>
		public static ZipView<TIndex, TElement, TIndex[], TElement[]> Zip<TIndex, TElement>(this TIndex[] indicies, TElement[] elements) {
			if (indicies.Length != elements.Length) {
				throw new ArgumentException("Collection lengths must be the same in order to be zipped.");
			}
			return new ZipView<TIndex, TElement, TIndex[], TElement[]>(indicies, elements, indicies.Length);
		}
	}
}
