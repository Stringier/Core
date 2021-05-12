﻿using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Traits;
using Langly;

namespace Collectathon.Arrays {
	/// <summary>
	/// Represents any dynamically sized array.
	/// </summary>
	/// <typeparam name="TElement">The type of the elements in the array.</typeparam>
	/// <typeparam name="TSelf">The implementing type; itself.</typeparam>
	[DebuggerDisplay("{ToString(5),nq}")]
	public abstract partial class FlexibleArray<TElement, TSelf> :
		IAddSpan<TElement>,
		ICapacity,
		IClear,
		IEquatable<TSelf>, IEquatable<FlexibleArray<TElement, TSelf>>,
		IIndex<nint, TElement>,
		IInsertSpan<nint, TElement>,
		IPostpendSpan<TElement>,
		IPrependSpan<TElement>,
		IRemove<TElement>,
		IReplace<TElement>,
		ISequence<TElement, FlexibleArray<TElement, TSelf>.Enumerator>,
		IShift,
		ISlice<Memory<TElement>>
		where TSelf : FlexibleArray<TElement, TSelf> {
		/// <summary>
		/// The <see cref="Filter{TIndex, TElement}"/> being used.
		/// </summary>
		/// <remarks>
		/// This is never <see langword="null"/>; a sentinel is used by default.
		/// </remarks>
		[NotNull, DisallowNull]
		protected readonly Filter<nint, TElement> Filter;

		/// <summary>
		/// The backing array of this <see cref="FlexibleArray{TElement, TSelf}"/>.
		/// </summary>
		[DisallowNull, NotNull]
		protected TElement[] Memory;

		/// <summary>
		/// Initializes a new <see cref="FlexibleArray{TElement, TSelf}"/> with the given <paramref name="capacity"/>.
		/// </summary>
		/// <param name="capacity">The initial capacity.</param>
		/// <param name="count">The amount of elements in the array.</param>
		/// <param name="filter">The type of filter to use.</param>
		protected FlexibleArray(nint capacity, nint count, Filters filter) {
			Memory = new TElement[capacity];
			Count = count;
			Filter = Filter<nint, TElement>.Create(filter);
		}

		/// <summary>
		/// Initializes a new <see cref="FlexibleArray{TElement, TSelf}"/> with the given <paramref name="memory"/>.
		/// </summary>
		/// <param name="memory">The <see cref="Array"/> of <typeparamref name="TElement"/> to reuse.</param>
		/// <param name="count">The amount of elements in the array.</param>
		/// <param name="filter">The type of filter to use.</param>
		protected FlexibleArray([DisallowNull] TElement[] memory, nint count, Filters filter) {
			Memory = memory;
			Count = count;
			Filter = Filter<nint, TElement>.Create(filter);
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="memory">The <see cref="Array"/> of <typeparamref name="TElement"/> to reuse.</param>
		/// <param name="count">The amount of elements in the array.</param>
		/// <param name="filter">The <see cref="Filter{TIndex, TElement}"/> to reuse.</param>
		protected FlexibleArray([DisallowNull] TElement[] memory, nint count, [DisallowNull] Filter<nint, TElement> filter) {
			Memory = memory;
			Count = count;
			Filter = filter;
		}

		/// <inheritdoc/>
		public nint Capacity => Memory.Length;

		/// <inheritdoc/>
		public nint Count { get; protected set; }

		/// <inheritdoc/>
		[AllowNull, MaybeNull]
		public TElement this[nint index] {
			get => Memory[(Int32)index];
			set => Memory[(Int32)index] = value;
		}

#if !NETSTANDARD1_3
		/// <inheritdoc/>
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
		public void Add([AllowNull] params TElement[] elements) => Postpend(elements);

		/// <inheritdoc/>
		public void Add(Memory<TElement> elements) => Postpend(elements);

		/// <inheritdoc/>
		public void Add(ReadOnlyMemory<TElement> elements) => Postpend(elements);

		/// <inheritdoc/>
		public void Add(Span<TElement> elements) => Postpend(elements);

		/// <inheritdoc/>
		public void Add(ReadOnlySpan<TElement> elements) => Postpend(elements);

		/// <inheritdoc/>
		public void Clear() => Count = 0;

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
		public Enumerator GetEnumerator() => new Enumerator(Memory, Count);

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
		public virtual void Insert(nint index, [AllowNull] TElement element) {
			Memory.AsMemory((Int32)index, (Int32)Count - (Int32)index).CopyTo(Memory.AsMemory((Int32)index + 1));
			Memory[(Int32)index] = element;
			Count++;
		}

		/// <inheritdoc/>
		public void Insert(nint index, [AllowNull] params TElement[] elements) => Insert(index, elements.AsSpan());

		/// <inheritdoc/>
		public void Insert(nint index, Memory<TElement> elements) => Insert(index, elements.Span);

		/// <inheritdoc/>
		public void Insert(nint index, ReadOnlyMemory<TElement> elements) => Insert(index, elements.Span);

		/// <inheritdoc/>
		public void Insert(nint index, Span<TElement> elements) => Insert(index, (ReadOnlySpan<TElement>)elements);

		/// <inheritdoc/>
		public virtual void Insert(nint index, ReadOnlySpan<TElement> elements) {
			Memory.AsMemory((Int32)index, (Int32)Count - (Int32)index).CopyTo(Memory.AsMemory((Int32)index + elements.Length));
			elements.CopyTo(Memory.AsSpan((Int32)index));
			Count += elements.Length;
		}

		/// <inheritdoc/>
		public virtual void Postpend([AllowNull] TElement element) => Memory[Count++] = element;

		/// <inheritdoc/>
		public void Postpend([AllowNull] params TElement[] elements) => Postpend(elements.AsSpan());

		/// <inheritdoc/>
		public void Postpend(Memory<TElement> elements) => Postpend(elements.Span);

		/// <inheritdoc/>
		public void Postpend(ReadOnlyMemory<TElement> elements) => Postpend(elements.Span);

		/// <inheritdoc/>
		public void Postpend(Span<TElement> elements) => Postpend((ReadOnlySpan<TElement>)elements);

		/// <inheritdoc/>
		public virtual void Postpend(ReadOnlySpan<TElement> elements) {
			elements.CopyTo(Memory.AsSpan((Int32)Count));
			Count += elements.Length;
		}

		/// <inheritdoc/>
		public virtual void Prepend([AllowNull] TElement element) {
			ShiftRight();
			Memory[0] = element;
			Count++;
		}

		/// <inheritdoc/>
		public void Prepend([AllowNull] params TElement[] elements) => Prepend(elements.AsSpan());

		/// <inheritdoc/>
		public void Prepend(Memory<TElement> elements) => Prepend(elements.Span);

		/// <inheritdoc/>
		public void Prepend(ReadOnlyMemory<TElement> elements) => Prepend(elements.Span);

		/// <inheritdoc/>
		public void Prepend(Span<TElement> elements) => Prepend((ReadOnlySpan<TElement>)elements);

		/// <inheritdoc/>
		public virtual void Prepend(ReadOnlySpan<TElement> elements) {
			ShiftRight(elements.Length);
			elements.CopyTo(Memory);
			Count += elements.Length;
		}

		/// <inheritdoc/>
		public void Remove([AllowNull] TElement element) {
			for (nint i = 0; i < Count; i++) {
				if (Equals(Memory[i], element)) {
					Memory[i] = Memory[i + 1];
					i--;
					Count--;
				}
			}
		}

		/// <inheritdoc/>
		public void RemoveFirst([AllowNull] TElement element) {
			for (Int32 i = 0; i < Count; i++) {
				if (Equals(Memory[i], element)) {
					Memory.AsMemory(i).CopyTo(Memory.AsMemory(--i));
					Count--;
					return;
				}
			}
		}

		/// <inheritdoc/>
		public void RemoveLast([AllowNull] TElement element) => throw new NotImplementedException();

		/// <inheritdoc/>
		public void Replace([AllowNull] TElement search, [AllowNull] TElement replace) {
			for (Int32 i = 0; i < Count; i++) {
				if (Equals(Memory[i], search)) {
					Memory[i] = replace;
				}
			}
		}

		/// <inheritdoc/>
		public void ShiftLeft() => ShiftLeft(1);

		/// <inheritdoc/>
		public void ShiftLeft(nint amount) {
			if (amount != 0 && Count != 0) {
				Memory.AsMemory((Int32)amount).CopyTo(Memory);
				Memory.AsMemory((Int32)Capacity - (Int32)amount).Span.Clear();
			}
		}

		/// <inheritdoc/>
		public void ShiftRight() => ShiftRight(1);

		/// <inheritdoc/>
		public void ShiftRight(nint amount) {
			if (amount != 0 && Count != 0) {
				Memory.AsMemory(0, (Int32)Capacity - (Int32)amount).CopyTo(Memory.AsMemory((Int32)amount));
				Memory.AsMemory(0, (Int32)amount).Span.Clear();
			}
		}

		/// <inheritdoc/>
		public Memory<TElement> Slice() => Memory.AsMemory();

		/// <inheritdoc/>
		public Memory<TElement> Slice(nint start) => Memory.AsMemory((Int32)start);

		/// <inheritdoc/>
		public Memory<TElement> Slice(nint start, nint length) => Memory.AsMemory((Int32)start, (Int32)length);

		/// <inheritdoc/>
		[return: NotNull]
		public sealed override String ToString() => Collection.ToString(this);

		/// <inheritdoc/>
		[return: NotNull]
		public String ToString(nint amount) => Collection.ToString(this, amount);
	}
}
