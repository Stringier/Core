using System;
using System.Diagnostics.CodeAnalysis;

namespace Collectathon.Stacks {
	public partial class Stack<TElement> {
		/// <summary>
		/// Represents a stack node.
		/// </summary>
		private sealed class Node {
			/// <summary>
			/// The next node of the stack.
			/// </summary>
			[AllowNull, MaybeNull]
			public Node Next;

			/// <summary>
			/// The element contained in this node.
			/// </summary>
			[AllowNull, MaybeNull]
			public readonly TElement Element;

			/// <summary>
			/// Initializes a new <see cref="Node"/>
			/// </summary>
			/// <param name="element">The element contained in this node.</param>
			public Node([AllowNull] TElement element) : this(element, null) { }

			/// <summary>
			/// Initializes a new <see cref="Node"/>
			/// </summary>
			/// <param name="element">The element contained in this node.</param>
			/// <param name="next">The next node of the stack.</param>
			public Node([AllowNull] TElement element, [AllowNull] Node next) {
				Element = element;
				Next = next;
			}

			/// <inheritdoc/>
			[return: NotNull]
			public override String ToString() => Element.ToString();
		}
	}
}
