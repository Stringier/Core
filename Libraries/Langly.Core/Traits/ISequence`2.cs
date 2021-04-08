﻿using System;
using System.Diagnostics.CodeAnalysis;
using Langly.Traits;

namespace Langly.Traits {
	/// <summary>
	/// Indicates the type is a sequence of <typeparamref name="TElement"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of elements in the sequence.</typeparam>
	/// <typeparam name="TEnumerator">The type of the enumerator for this sequence.</typeparam>
	/// <remarks>
	/// This interface devirtualizes the enumerator, and simplifies numerous parts of the interface through well defined defaults.
	/// </remarks>
	public interface ISequence<TElement, out TEnumerator> :
		IContains<TElement>,
		ICount,
		System.Collections.Generic.IEnumerable<TElement>
		where TEnumerator : IEnumerator<TElement> {
		/// <inheritdoc/>
		Boolean IContains<TElement>.Contains([AllowNull] TElement element) {
			foreach (TElement item in this) {
				if (item is null && element is null) {
					return true;
				} else if (element?.Equals(item) ?? false) {
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Folds the collection into a single element as described by <paramref name="func"/>.
		/// </summary>
		/// <param name="func">The function describing the folding operation. This is a magma.</param>
		/// <param name="identity">The identity value for <paramref name="func"/>.</param>
		/// <returns>A single element after folding the entire collection.</returns>
		/// <remarks>
		/// <para><paramref name="func"/> is a magma, so associativity like left-fold and right-fold are completely irrelevant.</para>
		/// <para><paramref name="identity"/> is required as a start point for the fold. It needs to be the identity of the <paramref name="func"/> to function properly. For example, the identity of addition is <c>0</c>, and the identity of multiplication is <c>1</c>. Without an appropriate identity, the results will be wrong.</para>
		/// </remarks>
		TElement Fold([DisallowNull] Func<TElement, TElement, TElement> func, [AllowNull] TElement identity) {
			TElement result = identity;
			if (func is null) {
				goto Result;
			}
			foreach (TElement item in this) {
				result = func(result, item);
			}
		Result:
			return result;
		}

		/// <summary>
		/// Returns an enumerator that iterates through the sequence.
		/// </summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		[return: NotNull]
		new TEnumerator GetEnumerator();

		/// <inheritdoc/>
		[return: NotNull]
		System.Collections.Generic.IEnumerator<TElement> System.Collections.Generic.IEnumerable<TElement>.GetEnumerator() => GetEnumerator();

		/// <inheritdoc/>
		[return: NotNull]
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>
		/// Count all occurrences of <paramref name="element"/> in the collection.
		/// </summary>
		/// <param name="element">The element to count.</param>
		/// <returns>The amount of occurrences found.</returns>
		[SuppressMessage("Performance", "HAA0601:Value type to reference type conversion causing boxing allocation", Justification = "There's nothing I can do about this.")]
		nint Occurrences([AllowNull] TElement element) {
			nint count = 0;
			foreach (TElement item in this) {
				if (item is null && element is null) {
					count++;
				} else if (element?.Equals(item) ?? false) {
					count++;
				}
			}
			return count;
		}

		/// <summary>
		/// Count all occurrences of <paramref name="element"/> in the collection.
		/// </summary>
		/// <param name="element">The element to count.</param>
		/// <returns>The amount of occurrences found.</returns>
		nint Occurrences([AllowNull] IEquals<TElement> element) {
			nint count = 0;
			foreach (TElement item in this) {
				if (item is null && element is null) {
					count++;
				} else if (element?.Equals(item) ?? false) {
					count++;
				}
			}
			return count;
		}

		/// <summary>
		/// Count all occurrences of elements that match the provided predicate in the collection.
		/// </summary>
		/// <param name="predicate">The <see cref="Predicate{T}"/> describing a match of the elements to count.</param>
		/// <returns>The amount of occurrences found.</returns>
		nint Occurrences([AllowNull] Predicate<TElement> predicate) {
			nint count = 0;
			foreach (TElement item in this) {
				if (item is null && predicate is null) {
					count++;
				} else if (predicate(item)) {
					count++;
				}
			}
			return count;
		}
	}
}

namespace Langly {
	public static partial class TraitExtensions {
		#region Fold()
		/// <summary>
		/// Folds the collection into a single element as described by the <paramref name="func"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the collection.</typeparam>
		/// <typeparam name="TEnumerator">The enumerator for the collection.</typeparam>
		/// <param name="collection">This collection.</param>
		/// <param name="func">The function describing the folding operation. This is a magma.</param>
		/// <param name="identity">The identity value for <paramref name="func"/>.</param>
		/// <returns>A single element after folding the entire <paramref name="collection"/>. If <paramref name="collection"/> or <paramref name="func"/> are <see langword="null"/> this function returns <paramref name="identity"/>.</returns>
		/// <remarks>
		/// <para><paramref name="func"/> is a magma, so associativity like left-fold and right-fold are completely irrelevant.</para>
		/// <para><paramref name="identity"/> is required as a start point for the fold. It needs to be the identity of the <paramref name="func"/> to function properly. For example, the identity of addition is <c>0</c>, and the identity of multiplication is <c>1</c>. Without an appropriate identity, the results will be wrong.</para>
		/// </remarks>
		[return: MaybeNull]
		public static TElement Fold<TElement, TEnumerator>([AllowNull] this ISequence<TElement, TEnumerator> collection, [DisallowNull] Func<TElement, TElement, TElement> func, [AllowNull] TElement identity) where TEnumerator : IEnumerator<TElement> => collection is not null ? collection.Fold(func, identity) : identity;

		/// <summary>
		/// Folds the collection into a single element as described by the <paramref name="func"/>.
		/// </summary>
		/// <param name="collection">This collection.</param>
		/// <param name="func">The function describing the folding operation. This is a magma.</param>
		/// <param name="identity">The identity value for <paramref name="func"/>.</param>
		/// <returns>A single element after folding the entire <paramref name="collection"/>. If <paramref name="collection"/> or <paramref name="func"/> are <see langword="null"/> this function returns <paramref name="identity"/>.</returns>
		/// <remarks>
		/// <para><paramref name="func"/> is a magma, so associativity like left-fold and right-fold are completely irrelevant.</para>
		/// <para><paramref name="identity"/> is required as a start point for the fold. It needs to be the identity of the <paramref name="func"/> to function properly. For example, the identity of addition is <c>0</c>, and the identity of multiplication is <c>1</c>. Without an appropriate identity, the results will be wrong.</para>
		/// </remarks>
		[return: MaybeNull]
		public static Char Fold([AllowNull] this String collection, [DisallowNull] Func<Char, Char, Char> func, Char identity) => collection is not null ? Fold(collection.AsMemory(), func, identity) : identity;

		/// <summary>
		/// Folds the collection into a single element as described by the <paramref name="func"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the collection.</typeparam>
		/// <param name="collection">This collection.</param>
		/// <param name="func">The function describing the folding operation. This is a magma.</param>
		/// <param name="identity">The identity value for <paramref name="func"/>.</param>
		/// <returns>A single element after folding the entire <paramref name="collection"/>. If <paramref name="collection"/> or <paramref name="func"/> are <see langword="null"/> this function returns <paramref name="identity"/>.</returns>
		/// <remarks>
		/// <para><paramref name="func"/> is a magma, so associativity like left-fold and right-fold are completely irrelevant.</para>
		/// <para><paramref name="identity"/> is required as a start point for the fold. It needs to be the identity of the <paramref name="func"/> to function properly. For example, the identity of addition is <c>0</c>, and the identity of multiplication is <c>1</c>. Without an appropriate identity, the results will be wrong.</para>
		/// </remarks>
		[return: MaybeNull]
		public static TElement Fold<TElement>([AllowNull] this TElement[] collection, [DisallowNull] Func<TElement, TElement, TElement> func, [AllowNull] TElement identity) => collection is not null ? Fold(collection.AsMemory(), func, identity) : identity;

		/// <summary>
		/// Folds the collection into a single element as described by the <paramref name="func"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the collection.</typeparam>
		/// <param name="collection">This collection.</param>
		/// <param name="func">The function describing the folding operation. This is a magma.</param>
		/// <param name="identity">The identity value for <paramref name="func"/>.</param>
		/// <returns>A single element after folding the entire <paramref name="collection"/>. If <paramref name="collection"/> or <paramref name="func"/> are <see langword="null"/> this function returns <paramref name="identity"/>.</returns>
		/// <remarks>
		/// <para><paramref name="func"/> is a magma, so associativity like left-fold and right-fold are completely irrelevant.</para>
		/// <para><paramref name="identity"/> is required as a start point for the fold. It needs to be the identity of the <paramref name="func"/> to function properly. For example, the identity of addition is <c>0</c>, and the identity of multiplication is <c>1</c>. Without an appropriate identity, the results will be wrong.</para>
		/// </remarks>
		[return: MaybeNull]
		public static TElement Fold<TElement>(this Memory<TElement> collection, [DisallowNull] Func<TElement, TElement, TElement> func, [AllowNull] TElement identity) => Fold(collection.Span, func, identity);

		/// <summary>
		/// Folds the collection into a single element as described by the <paramref name="func"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the collection.</typeparam>
		/// <param name="collection">This collection.</param>
		/// <param name="func">The function describing the folding operation. This is a magma.</param>
		/// <param name="identity">The identity value for <paramref name="func"/>.</param>
		/// <returns>A single element after folding the entire <paramref name="collection"/>. If <paramref name="collection"/> or <paramref name="func"/> are <see langword="null"/> this function returns <paramref name="identity"/>.</returns>
		/// <remarks>
		/// <para><paramref name="func"/> is a magma, so associativity like left-fold and right-fold are completely irrelevant.</para>
		/// <para><paramref name="identity"/> is required as a start point for the fold. It needs to be the identity of the <paramref name="func"/> to function properly. For example, the identity of addition is <c>0</c>, and the identity of multiplication is <c>1</c>. Without an appropriate identity, the results will be wrong.</para>
		/// </remarks>
		[return: MaybeNull]
		public static TElement Fold<TElement>(this ReadOnlyMemory<TElement> collection, [DisallowNull] Func<TElement, TElement, TElement> func, [AllowNull] TElement identity) => Fold(collection.Span, func, identity);

		/// <summary>
		/// Folds the collection into a single element as described by the <paramref name="func"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the collection.</typeparam>
		/// <param name="collection">This collection.</param>
		/// <param name="func">The function describing the folding operation. This is a magma.</param>
		/// <param name="identity">The identity value for <paramref name="func"/>.</param>
		/// <returns>A single element after folding the entire <paramref name="collection"/>. If <paramref name="collection"/> or <paramref name="func"/> are <see langword="null"/> this function returns <paramref name="identity"/>.</returns>
		/// <remarks>
		/// <para><paramref name="func"/> is a magma, so associativity like left-fold and right-fold are completely irrelevant.</para>
		/// <para><paramref name="identity"/> is required as a start point for the fold. It needs to be the identity of the <paramref name="func"/> to function properly. For example, the identity of addition is <c>0</c>, and the identity of multiplication is <c>1</c>. Without an appropriate identity, the results will be wrong.</para>
		/// </remarks>
		[return: MaybeNull]
		public static TElement Fold<TElement>(this Span<TElement> collection, [DisallowNull] Func<TElement, TElement, TElement> func, [AllowNull] TElement identity) => Fold((ReadOnlySpan<TElement>)collection, func, identity);

		/// <summary>
		/// Folds the collection into a single element as described by the <paramref name="func"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the collection.</typeparam>
		/// <param name="collection">This collection.</param>
		/// <param name="func">The function describing the folding operation. This is a magma.</param>
		/// <param name="identity">The identity value for <paramref name="func"/>.</param>
		/// <returns>A single element after folding the entire <paramref name="collection"/>. If <paramref name="collection"/> or <paramref name="func"/> are <see langword="null"/> this function returns <paramref name="identity"/>.</returns>
		/// <remarks>
		/// <para><paramref name="func"/> is a magma, so associativity like left-fold and right-fold are completely irrelevant.</para>
		/// <para><paramref name="identity"/> is required as a start point for the fold. It needs to be the identity of the <paramref name="func"/> to function properly. For example, the identity of addition is <c>0</c>, and the identity of multiplication is <c>1</c>. Without an appropriate identity, the results will be wrong.</para>
		/// </remarks>
		[return: MaybeNull]
		public static TElement Fold<TElement>(this ReadOnlySpan<TElement> collection, [DisallowNull] Func<TElement, TElement, TElement> func, [AllowNull] TElement identity) {
			TElement result = identity;
			if (func is not null) {
				if (func is null) {
					goto Result;
				}
				foreach (TElement item in collection) {
					result = func(result, item);
				}
			}
		Result:
			return result;
		}
		#endregion

		#region Occurrences(Collection, TElement)
		/// <summary>
		/// Count all occurrences of <paramref name="element"/> in the collection.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the collection.</typeparam>
		/// <typeparam name="TEnumerator">The type of the enumerator of the collection.</typeparam>
		/// <param name="collection">This collection.</param>
		/// <param name="element">The element to count.</param>
		/// <returns>The amount of occurrences found.</returns>
		public static nint Occurrences<TElement, TEnumerator>([AllowNull] this ISequence<TElement, TEnumerator> collection, [AllowNull] TElement element) where TEnumerator : IEnumerator<TElement> {
			if (collection is null) {
				return 0;
			} else if (element is null) {
				return NullOccurrences(collection);
			}
			return collection.Occurrences(element);
		}

		/// <summary>
		/// Count all occurrences of <paramref name="element"/> in the collection.
		/// </summary>
		/// <param name="collection">This collection.</param>
		/// <param name="element">The element to count.</param>
		/// <returns>The amount of occurrences found.</returns>
		public static nint Occurrences([AllowNull] this String collection, Char element) => collection is not null ? Occurrences(collection.AsMemory(), element) : 0;

		/// <summary>
		/// Count all occurrences of <paramref name="element"/> in the collection.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the collection.</typeparam>
		/// <param name="collection">This collection.</param>
		/// <param name="element">The element to count.</param>
		/// <returns>The amount of occurrences found.</returns>
		public static nint Occurrences<TElement>([AllowNull] this TElement[] collection, [AllowNull] TElement element) => collection is not null ? Occurrences(collection.AsMemory(), element) : 0;

		/// <summary>
		/// Count all occurrences of <paramref name="element"/> in the collection.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the collection.</typeparam>
		/// <param name="collection">This collection.</param>
		/// <param name="element">The element to count.</param>
		/// <returns>The amount of occurrences found.</returns>
		public static nint Occurrences<TElement>(this Memory<TElement> collection, [AllowNull] TElement element) => Occurrences(collection.Span, element);

		/// <summary>
		/// Count all occurrences of <paramref name="element"/> in the collection.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the collection.</typeparam>
		/// <param name="collection">This collection.</param>
		/// <param name="element">The element to count.</param>
		/// <returns>The amount of occurrences found.</returns>
		public static nint Occurrences<TElement>(this ReadOnlyMemory<TElement> collection, [AllowNull] TElement element) => Occurrences(collection.Span, element);

		/// <summary>
		/// Count all occurrences of <paramref name="element"/> in the collection.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the collection.</typeparam>
		/// <param name="collection">This collection.</param>
		/// <param name="element">The element to count.</param>
		/// <returns>The amount of occurrences found.</returns>
		public static nint Occurrences<TElement>(this Span<TElement> collection, [AllowNull] TElement element) => Occurrences((ReadOnlySpan<TElement>)collection, element);

		/// <summary>
		/// Count all occurrences of <paramref name="element"/> in the collection.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the collection.</typeparam>
		/// <param name="collection">This collection.</param>
		/// <param name="element">The element to count.</param>
		/// <returns>The amount of occurrences found.</returns>
		public static nint Occurrences<TElement>(this ReadOnlySpan<TElement> collection, [AllowNull] TElement element) {
			if (collection.Length == 0) {
				return 0;
			} else if (element is null) {
				return NullOccurrences(collection);
			}
			nint count = 0;
			foreach (TElement item in collection) {
				if (item is null && element is null) {
					count++;
				} else if (element?.Equals(item) ?? false) {
					count++;
				}
			}
			return count;
		}
		#endregion

		#region Occurrences(Collection, IEquals<TElement>)
		/// <summary>
		/// Count all occurrences of <paramref name="element"/> in the collection.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the collection.</typeparam>
		/// <typeparam name="TEnumerator">The type of the enumerator of the collection.</typeparam>
		/// <param name="collection">This collection.</param>
		/// <param name="element">The element to count.</param>
		/// <returns>The amount of occurrences found.</returns>
		public static nint Occurrences<TElement, TEnumerator>([AllowNull] this ISequence<TElement, TEnumerator> collection, [AllowNull] IEquals<TElement> element) where TEnumerator : IEnumerator<TElement> {
			if (collection is null) {
				return 0;
			} else if (element is null) {
				return NullOccurrences(collection);
			}
			return collection.Occurrences(element);
		}

		/// <summary>
		/// Count all occurrences of <paramref name="element"/> in the collection.
		/// </summary>
		/// <param name="collection">This collection.</param>
		/// <param name="element">The element to count.</param>
		/// <returns>The amount of occurrences found.</returns>
		public static nint Occurrences([AllowNull] this String collection, [AllowNull] IEquals<Char> element) => Occurrences(collection.AsMemory(), element);

		/// <summary>
		/// Count all occurrences of <paramref name="element"/> in the collection.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the collection.</typeparam>
		/// <param name="collection">This collection.</param>
		/// <param name="element">The element to count.</param>
		/// <returns>The amount of occurrences found.</returns>
		public static nint Occurrences<TElement>([AllowNull] this TElement[] collection, [AllowNull] IEquals<TElement> element) => Occurrences(collection.AsMemory(), element);

		/// <summary>
		/// Count all occurrences of <paramref name="element"/> in the collection.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the collection.</typeparam>
		/// <param name="collection">This collection.</param>
		/// <param name="element">The element to count.</param>
		/// <returns>The amount of occurrences found.</returns>
		public static nint Occurrences<TElement>(this Memory<TElement> collection, [AllowNull] IEquals<TElement> element) => Occurrences(collection.Span, element);

		/// <summary>
		/// Count all occurrences of <paramref name="element"/> in the collection.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the collection.</typeparam>
		/// <param name="collection">This collection.</param>
		/// <param name="element">The element to count.</param>
		/// <returns>The amount of occurrences found.</returns>
		public static nint Occurrences<TElement>(this ReadOnlyMemory<TElement> collection, [AllowNull] IEquals<TElement> element) => Occurrences(collection.Span, element);

		/// <summary>
		/// Count all occurrences of <paramref name="element"/> in the collection.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the collection.</typeparam>
		/// <param name="collection">This collection.</param>
		/// <param name="element">The element to count.</param>
		/// <returns>The amount of occurrences found.</returns>
		public static nint Occurrences<TElement>(this Span<TElement> collection, [AllowNull] IEquals<TElement> element) => Occurrences((ReadOnlySpan<TElement>)collection, element);

		/// <summary>
		/// Count all occurrences of <paramref name="element"/> in the collection.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the collection.</typeparam>
		/// <param name="collection">This collection.</param>
		/// <param name="element">The element to count.</param>
		/// <returns>The amount of occurrences found.</returns>
		public static nint Occurrences<TElement>(this ReadOnlySpan<TElement> collection, [AllowNull] IEquals<TElement> element) {
			if (collection.Length == 0) {
				return 0;
			} else if (element is null) {
				return NullOccurrences(collection);
			}
			nint count = 0;
			foreach (TElement item in collection) {
				if (item is null && element is null) {
					count++;
				} else if (element?.Equals(item) ?? false) {
					count++;
				}
			}
			return count;
		}
		#endregion

		#region Occurrences(Collection, Predicate<TElement>)
		/// <summary>
		/// Count all occurrences of elements that match the provided predicate in the collection.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the collection.</typeparam>
		/// <typeparam name="TEnumerator">The type of the enumerator of the collection.</typeparam>
		/// <param name="collection">This collection.</param>
		/// <param name="predicate">The <see cref="Predicate{T}"/> describing a match of the elements to count.</param>
		/// <returns>The amount of occurrences found.</returns>
		public static nint Occurrences<TElement, TEnumerator>([AllowNull] this ISequence<TElement, TEnumerator> collection, [AllowNull] Predicate<TElement> predicate) where TEnumerator : IEnumerator<TElement> {
			if (collection is null) {
				return 0;
			} else if (predicate is null) {
				return NullOccurrences(collection);
			}
			return collection.Occurrences(predicate);
		}

		/// <summary>
		/// Count all occurrences of elements that match the provided predicate in the collection.
		/// </summary>
		/// <param name="collection">This collection.</param>
		/// <param name="predicate">The <see cref="Predicate{T}"/> describing a match of the elements to count.</param>
		/// <returns>The amount of occurrences found.</returns>
		public static nint Occurrences([AllowNull] this String collection, [AllowNull] Predicate<Char> predicate) => collection is not null ? Occurrences(collection.AsMemory(), predicate) : 0;

		/// <summary>
		/// Count all occurrences of elements that match the provided predicate in the collection.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the collection.</typeparam>
		/// <param name="collection">This collection.</param>
		/// <param name="predicate">The <see cref="Predicate{T}"/> describing a match of the elements to count.</param>
		/// <returns>The amount of occurrences found.</returns>
		public static nint Occurrences<TElement>([AllowNull] this TElement[] collection, [AllowNull] Predicate<TElement> predicate) => collection is not null ? Occurrences(collection.AsMemory(), predicate) : 0;

		/// <summary>
		/// Count all occurrences of elements that match the provided predicate in the collection.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the collection.</typeparam>
		/// <param name="collection">This collection.</param>
		/// <param name="predicate">The <see cref="Predicate{T}"/> describing a match of the elements to count.</param>
		/// <returns>The amount of occurrences found.</returns>
		public static nint Occurrences<TElement>(this Memory<TElement> collection, [AllowNull] Predicate<TElement> predicate) => Occurrences(collection.Span, predicate);

		/// <summary>
		/// Count all occurrences of elements that match the provided predicate in the collection.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the collection.</typeparam>
		/// <param name="collection">This collection.</param>
		/// <param name="predicate">The <see cref="Predicate{T}"/> describing a match of the elements to count.</param>
		/// <returns>The amount of occurrences found.</returns>
		public static nint Occurrences<TElement>(this ReadOnlyMemory<TElement> collection, [AllowNull] Predicate<TElement> predicate) => Occurrences(collection.Span, predicate);

		/// <summary>
		/// Count all occurrences of elements that match the provided predicate in the collection.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the collection.</typeparam>
		/// <param name="collection">This collection.</param>
		/// <param name="predicate">The <see cref="Predicate{T}"/> describing a match of the elements to count.</param>
		/// <returns>The amount of occurrences found.</returns>
		public static nint Occurrences<TElement>(this Span<TElement> collection, [AllowNull] Predicate<TElement> predicate) => Occurrences((ReadOnlySpan<TElement>)collection, predicate);

		/// <summary>
		/// Count all occurrences of elements that match the provided predicate in the collection.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the collection.</typeparam>
		/// <param name="collection">This collection.</param>
		/// <param name="predicate">The <see cref="Predicate{T}"/> describing a match of the elements to count.</param>
		/// <returns>The amount of occurrences found.</returns>
		public static nint Occurrences<TElement>(this ReadOnlySpan<TElement> collection, [AllowNull] Predicate<TElement> predicate) {
			if (collection.Length == 0) {
				return 0;
			} else if (predicate is null) {
				return NullOccurrences(collection);
			}
			nint count = 0;
			foreach (TElement item in collection) {
				if (item is null && predicate is null) {
					count++;
				} else if (predicate(item)) {
					count++;
				}
			}
			return count;
		}
		#endregion

		private static nint NullOccurrences<TElement, TEnumerator>([DisallowNull] ISequence<TElement, TEnumerator> collection) where TEnumerator : IEnumerator<TElement> {
			nint count = 0;
			foreach (TElement item in collection) {
				if (item is null) {
					count++;
				}
			}
			return count;
		}

		private static nint NullOccurrences<TElement>([DisallowNull] ReadOnlySpan<TElement> collection) {
			nint count = 0;
			foreach (TElement item in collection) {
				if (item is null) {
					count++;
				}
			}
			return count;
		}
	}
}
