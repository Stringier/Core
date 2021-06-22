﻿using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Traits;
using Collectathon.Enumerators;

namespace Collectathon.Arrays {
	/// <summary>
	/// Represents any dynamically sized array.
	/// </summary>
	/// <typeparam name="TElement">The type of the elements in the array.</typeparam>
	/// <typeparam name="TSelf">The implementing type; itself.</typeparam>
	[DebuggerDisplay("{ToString(5),nq}")]
	public abstract class FlexibleArray<TElement, TSelf> :
		IAddSpan<TElement>,
		ICapacity,
		IClear,
		IContains<TElement>,
		IEquatable<TSelf>, IEquatable<FlexibleArray<TElement, TSelf>>,
		IIndex<Int32, TElement>,
		IInsertSpan<Int32, TElement>,
		IPostpendSpan<TElement>,
		IPrependSpan<TElement>,
		IRemove<TElement>,
		IReplace<TElement>,
		ISequence<TElement, ArrayEnumerator<TElement>>,
		IShift,
		ISlice<Memory<TElement>>
		where TSelf : FlexibleArray<TElement, TSelf> {
		/// <summary>
		/// The backing array of this <see cref="FlexibleArray{TElement, TSelf}"/>.
		/// </summary>
		[DisallowNull, NotNull]
		protected TElement?[] Memory;

		/// <summary>
		/// The amount of elements contained in this collection.
		/// </summary>
		private Int32 count;

		/// <summary>
		/// Initializes a new <see cref="FlexibleArray{TElement, TSelf}"/> with the given <paramref name="capacity"/>.
		/// </summary>
		/// <param name="capacity">The initial capacity.</param>
		/// <param name="count">The amount of elements in the array.</param>
		protected FlexibleArray(Int32 capacity, Int32 count) {
			Memory = new TElement[capacity];
			Count = count;
		}

		/// <summary>
		/// Initializes a new <see cref="FlexibleArray{TElement, TSelf}"/> with the given <paramref name="memory"/>.
		/// </summary>
		/// <param name="memory">The <see cref="Array"/> of <typeparamref name="TElement"/> to reuse.</param>
		/// <param name="count">The amount of elements in the array.</param>
		protected FlexibleArray([DisallowNull] TElement?[] memory, Int32 count) {
			Memory = memory;
			Count = count;
		}

		/// <inheritdoc/>
		[O(1), Ω(1), Θ(1)]
		public Int32 Capacity => Memory.Length;

		/// <inheritdoc/>
		[O(1), Ω(1), Θ(1)]
		public Int32 Count {
			get => count;
			protected set => count = value;
		}

		/// <inheritdoc/>
		[O(1), Ω(1), Θ(1)]
		[AllowNull, MaybeNull]
		public TElement this[Int32 index] {
			get => Memory[index];
			set => Memory[index] = value;
		}

#if !NETSTANDARD1_3
		/// <inheritdoc/>
		[O(1), Ω(1), Θ(1)]
		public Memory<TElement> this[Range range] {
			get {
				(Int32 offset, Int32 length) = range.GetOffsetAndLength((Int32)Count);
				return ((ISlice<Memory<TElement>>)this).Slice(offset, length);
			}
		}
#endif

		/// <summary>
		/// Shifts the <paramref name="array"/> left by <paramref name="amount"/>.
		/// </summary>
		/// <param name="array">The <see cref="FlexibleArray{TElement, TSelf}"/> to shift.</param>
		/// <param name="amount">The amount of positions to shift.</param>
		[return: MaybeNull, NotNullIfNotNull("array")]
		public static TSelf operator <<([AllowNull] FlexibleArray<TElement, TSelf> array, Int32 amount) {
			array?.ShiftLeft(amount);
			return (TSelf)array;
		}

		/// <summary>
		/// Shifts the <paramref name="array"/> right by <paramref name="amount"/>.
		/// </summary>
		/// <param name="array">The <see cref="FlexibleArray{TElement, TSelf}"/> to shift.</param>
		/// <param name="amount">The amount of positions to shift.</param>
		[return: MaybeNull, NotNullIfNotNull("array")]
		public static TSelf operator >>([AllowNull] FlexibleArray<TElement, TSelf> array, Int32 amount) {
			array?.ShiftRight(amount);
			return (TSelf)array;
		}

		/// <inheritdoc/>
		public void Add([AllowNull] TElement element) => Postpend(element);

		/// <inheritdoc/>
		public void Add([AllowNull] params TElement?[] elements) => Postpend(elements);

		/// <inheritdoc/>
		public void Add(ArraySegment<TElement?> elements) => Postpend(elements);

		/// <inheritdoc/>
		public void Add(Memory<TElement?> elements) => Postpend(elements);

		/// <inheritdoc/>
		public void Add(ReadOnlyMemory<TElement?> elements) => Postpend(elements);

		/// <inheritdoc/>
		public void Add(Span<TElement?> elements) => Postpend(elements);

		/// <inheritdoc/>
		public void Add(ReadOnlySpan<TElement?> elements) => Postpend(elements);

		/// <inheritdoc/>
		[O(1), Ω(1), Θ(1)]
		public void Clear() => Count = 0;

		/// <inheritdoc/>
		[O(Complexity.n), Ω(Complexity.n), Θ(Complexity.n)]
		public Boolean Contains([AllowNull] TElement element) => Collection.Contains(Memory, Count, element);

		/// <summary>
		/// Determines if the two values are equal.
		/// </summary>
		/// <param name="obj">The other object.</param>
		/// <returns><see langword="true"/> if the two values are equal; otherwise, <see langword="false"/>.</returns>
		public sealed override Boolean Equals([AllowNull] Object obj) {
			switch (obj) {
			case TSelf other:
				return Equals(other);
			case FlexibleArray<TElement, TSelf> other:
				return Equals(other);
			case System.Collections.Generic.IEnumerable<TElement> other:
				return Equals(other);
			default:
				return false;
			}
		}

		/// <inheritdoc/>
		public Boolean Equals([AllowNull] TSelf other) => Collection.Equals(this, other);

		/// <inheritdoc/>
		public Boolean Equals([AllowNull] FlexibleArray<TElement, TSelf> other) => Collection.Equals(this, other);

		/// <inheritdoc/>
		public Boolean Equals([AllowNull] System.Collections.Generic.IEnumerable<TElement> other) => Collection.Equals(this, other);

		/// <inheritdoc/>
		public ArrayEnumerator<TElement> GetEnumerator() => new ArrayEnumerator<TElement>(Memory, Count);

		/// <inheritdoc/>
		[return: NotNull]
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <inheritdoc/>
		[return: NotNull]
		System.Collections.Generic.IEnumerator<TElement> System.Collections.Generic.IEnumerable<TElement>.GetEnumerator() => GetEnumerator();

		/// <inheritdoc/>
		[SuppressMessage("Major Bug", "S3249:Classes directly extending \"object\" should not call \"base\" in \"GetHashCode\" or \"Equals\"", Justification = "I'm literally enforcing correct behavior by ensuring downstream doesn't violate what this analyzer is trying to enforce...")]
		public sealed override Int32 GetHashCode() => base.GetHashCode();

		/// <inheritdoc/>
		public virtual void Insert(Int32 index, [AllowNull] TElement element) => Collection.Insert(Memory, ref count, index, element);

		/// <inheritdoc/>
		public void Insert(Int32 index, [AllowNull] params TElement?[] elements) => Insert(index, elements.AsSpan());

		/// <inheritdoc/>
		public void Insert([DisallowNull] Int32 index, ArraySegment<TElement?> elements) => Insert(index, elements.AsSpan());

		/// <inheritdoc/>
		public void Insert(Int32 index, Memory<TElement?> elements) => Insert(index, elements.Span);

		/// <inheritdoc/>
		public void Insert(Int32 index, ReadOnlyMemory<TElement?> elements) => Insert(index, elements.Span);

		/// <inheritdoc/>
		public void Insert(Int32 index, Span<TElement?> elements) => Insert(index, (ReadOnlySpan<TElement>)elements);

		/// <inheritdoc/>
		public virtual void Insert(Int32 index, ReadOnlySpan<TElement?> elements) => Collection.Insert(Memory, ref count, index, elements);

		/// <inheritdoc/>
		[O(Complexity.n_plus_k), Ω(Complexity.n)]
		public virtual void Postpend([AllowNull] TElement element) => Collection.Postpend(Memory, ref count, element);

		/// <inheritdoc/>
		[O(Complexity.n_plus_k), Ω(Complexity.n)]
		public void Postpend([AllowNull] params TElement?[] elements) => Postpend(elements.AsSpan());

		/// <inheritdoc/>
		[O(Complexity.n_plus_k), Ω(Complexity.n)]
		public void Postpend(ArraySegment<TElement?> elements) => Postpend(elements.AsSpan());

		/// <inheritdoc/>
		[O(Complexity.n_plus_k), Ω(Complexity.n)]
		public void Postpend(Memory<TElement?> elements) => Postpend(elements.Span);

		/// <inheritdoc/>
		[O(Complexity.n_plus_k), Ω(Complexity.n)]
		public void Postpend(ReadOnlyMemory<TElement> elements) => Postpend(elements.Span);

		/// <inheritdoc/>
		[O(Complexity.n_plus_k), Ω(Complexity.n)]
		public void Postpend(Span<TElement> elements) => Postpend((ReadOnlySpan<TElement>)elements);

		/// <inheritdoc/>
		[O(Complexity.n_plus_k), Ω(Complexity.n)]
		public virtual void Postpend(ReadOnlySpan<TElement> elements) => Collection.Postpend(Memory, ref count, elements);

		/// <inheritdoc/>
		[O(Complexity.n_plus_k), Ω(Complexity.n)]
		public virtual void Prepend([AllowNull] TElement element) => Collection.Prepend(Memory, ref count, element);

		/// <inheritdoc/>
		[O(Complexity.n_plus_k), Ω(Complexity.n)]
		public void Prepend([AllowNull] params TElement?[] elements) => Prepend(elements.AsSpan());

		/// <inheritdoc/>
		[O(Complexity.n_plus_k), Ω(Complexity.n)]
		public void Prepend(ArraySegment<TElement?> elements) => Postpend(elements.AsSpan());

		/// <inheritdoc/>
		[O(Complexity.n_plus_k), Ω(Complexity.n)]
		public void Prepend(Memory<TElement?> elements) => Prepend(elements.Span);

		/// <inheritdoc/>
		[O(Complexity.n_plus_k), Ω(Complexity.n)]
		public void Prepend(ReadOnlyMemory<TElement?> elements) => Prepend(elements.Span);

		/// <inheritdoc/>
		[O(Complexity.n_plus_k), Ω(Complexity.n)]
		public void Prepend(Span<TElement?> elements) => Prepend((ReadOnlySpan<TElement>)elements);

		/// <inheritdoc/>
		[O(Complexity.n_plus_k), Ω(Complexity.n)]
		public virtual void Prepend(ReadOnlySpan<TElement?> elements) => Collection.Prepend(Memory, ref count, elements);

		/// <inheritdoc/>
		[O(Complexity.n_plus_k), Ω(Complexity.n_plus_k)]
		public void Remove([AllowNull] TElement element) => Collection.Remove(Memory, ref count, element);

		/// <inheritdoc/>
		[O(Complexity.n_plus_k), Ω(Complexity.n_plus_k)]
		public void RemoveFirst([AllowNull] TElement element) => Collection.RemoveFirst(Memory, ref count, element);

		/// <inheritdoc/>
		[O(Complexity.n_plus_k), Ω(Complexity.n_plus_k)]
		public void RemoveLast([AllowNull] TElement element) => Collection.RemoveLast(Memory, ref count, element);

		/// <inheritdoc/>
		[O(Complexity.n), Ω(Complexity.n), Θ(Complexity.n)]
		public void Replace([AllowNull] TElement search, [AllowNull] TElement replace) => Collection.Replace(Memory, Count, search, replace);

		/// <inheritdoc/>
		public void ShiftLeft() => Collection.ShiftLeft(Memory, Count, 1);

		/// <inheritdoc/>
		public void ShiftLeft(Int32 amount) => Collection.ShiftLeft(Memory, Count, amount);

		/// <inheritdoc/>
		public void ShiftRight() => Collection.ShiftRight(Memory, Count, 1);

		/// <inheritdoc/>
		public void ShiftRight(Int32 amount) => Collection.ShiftRight(Memory, Count, amount);

		/// <inheritdoc/>
		[O(1), Ω(1), Θ(1)]
		public Memory<TElement> Slice() => Memory.AsMemory();

		/// <inheritdoc/>
		[O(1), Ω(1), Θ(1)]
		public Memory<TElement> Slice(Int32 start) => Memory.AsMemory(start);

		/// <inheritdoc/>
		[O(1), Ω(1), Θ(1)]
		public Memory<TElement> Slice(Int32 start, Int32 length) => Memory.AsMemory(start, length);

		/// <inheritdoc/>
		[O(Complexity.n), Ω(Complexity.n), Θ(Complexity.n)]
		[return: NotNull]
		public sealed override String ToString() => Collection.ToString(Memory);

		/// <inheritdoc/>
		[O(Complexity.n), Ω(Complexity.n), Θ(Complexity.n)]
		[return: NotNull]
		public String ToString(Int32 amount) => Collection.ToString(Memory, amount);
	}
}
