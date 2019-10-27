﻿using System;
using System.Collections.Generic;
using System.Text;

namespace System.Text.Patterns {
	/// <summary>
	/// Represents a <see cref="Pattern"/> node.
	/// </summary>
	internal abstract class Node : IEquatable<Node>, IEquatable<String> {
		/// <summary>
		/// Checks the first character in the <paramref name="Source"/> against the header of this pattern
		/// </summary>
		/// <remarks>
		/// This is primarily used to check whether a pattern may exist at the current position.
		/// </remarks>
		/// <param name="Source">The <see cref="Source"/> to check against</param>
		/// <returns><c>true</c> if this <see cref="Pattern"/> may be present, <c>false</c> if definately not.</returns>
		internal abstract Boolean CheckHeader(ref Source Source);

		/// <summary>
		/// Call the Consume parser of this <see cref="Node"/> on the <paramref name="Source"/> with the <paramref name="Result"/>
		/// </summary>
		/// <param name="Source">The <see cref="Source"/> to consume</param>
		/// <param name="Result">A <see cref="Result"/> containing whether a match occured and the captured <see cref="String"/></param>
		internal abstract void Consume(ref Source Source, ref Result Result);

		/// <summary>
		/// Call the Neglect parser of this <see cref="Node"/> on the <paramref name="Source"/> with the <paramref name="Result"/>.
		/// </summary>
		/// <param name="Source">The <see cref="Source"/> to consume</param>
		/// <param name="Result">A <see cref="Result"/> containing whether a match occured and the captured <see cref="String"/></param>
		internal abstract void Neglect(ref Source Source, ref Result Result);

		public static Boolean operator ==(Node Left, Node Right) => Left.Equals(Right);

		public static Boolean operator !=(Node Left, Node Right) => !Left.Equals(Right);

		/// <summary>
		/// Determines whether this instance and a specified object have the same value.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public sealed override Boolean Equals(Object? obj) {
			switch (obj) {
			case Node node:
				return Equals(node);
			case String @string:
				return Equals(@string);
			default:
				return false;
			}
		}

		/// <summary>
		/// Determines whether this instance and a specified object have the same value.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public abstract Boolean Equals(Node node);

		/// <summary>
		/// Determines whether the specified <see cref="ReadOnlySpan{T}"/> of <see cref="Char"/> can be represented by this <see cref="Pattern"/>.
		/// </summary>
		/// <param name="other">The <see cref="ReadOnlySpan{T}"/> of <see cref="Char"/> to check against this <see cref="Pattern"/>.</param>
		/// <returns><c>true</c> if representable; otherwise, <c>false</c>.</returns>
		public virtual Boolean Equals(ReadOnlySpan<Char> other) {
			Source Source = new Source(other);
			Result Result = new Result(ref Source);
			Consume(ref Source, ref Result);
			return Result && Source.Length == 0;
		}

		/// <summary>
		/// Determines whether the specified <see cref="String"/> can be represented by this <see cref="Pattern"/>.
		/// </summary>
		/// <param name="other">The <see cref="String"/> to check against this <see cref="Pattern"/>.</param>
		/// <returns><c>true</c> if representable; otherwise, <c>false</c>.</returns>
		public virtual Boolean Equals(String other) {
			Source Source = new Source(other);
			Result Result = new Result(ref Source);
			Consume(ref Source, ref Result);
			return Result && Source.Length == 0;
		}

		/// <summary>
		/// Returns the hash code for this <see cref="Pattern"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public abstract override Int32 GetHashCode();

		/// <summary>
		/// Returns a <see cref="String"/> that represents the current <see cref="Pattern"/>.
		/// </summary>
		/// <returns>A <see cref="String"/> that represents the current <see cref="Pattern"/>.</returns>
		public abstract override String ToString();

		internal virtual Node Alternate(Node Right) {
			if (Right is null) {
				throw new ArgumentNullException(nameof(Right));
			}
			return new Alternator(this, Right);
		}

		internal virtual Node Alternate(Char Right) => new Alternator(this, new CharLiteral(Right));

		internal virtual Node Alternate(String Right) {
			if (Right is null) {
				throw new ArgumentNullException(nameof(Right));
			}
			return new Alternator(this, new StringLiteral(Right));
		}

		internal Node Capture(out Capture Capture) => new Capturer(this, out Capture);

		internal virtual Node Concatenate(Node Right) {
			if (Right is null) {
				throw new ArgumentNullException(nameof(Right));
			}
			return new Concatenator(this, Right);
		}

		internal virtual Node Concatenate(Char Right) => new Concatenator(this, new CharLiteral(Right));

		internal virtual Node Concatenate(String Right) {
			if (Right is null) {
				throw new ArgumentNullException(nameof(Right));
			}
			return new Concatenator(this, new StringLiteral(Right));
		}

		internal virtual Node Negate() => new Negator(this);

		internal virtual Node Optional() => new Optor(this);

		internal virtual Node Repeat(Int32 Count) => new Repeater(this, Count);

		internal virtual Node Span() => new Spanner(this);

	}
}