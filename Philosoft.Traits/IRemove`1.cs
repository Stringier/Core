﻿namespace System.Traits {
	/// <summary>
	/// Indicates the type can have elements removed from it.
	/// </summary>
	/// <typeparam name="TElement">The type of the elements.</typeparam>
	public interface IRemove<in TElement> {
		/// <summary>
		/// Removes all instances of the element from this object.
		/// </summary>
		/// <param name="element">The element to remove.</param>
		void Remove(TElement element);

		/// <summary>
		/// Removes the first instance of the element from this object.
		/// </summary>
		/// <param name="element">The element to remove.</param>
		void RemoveFirst(TElement element);

		/// <summary>
		/// Removes the last instance of the element from this object.
		/// </summary>
		/// <param name="element">The element to remove.</param>
		void RemoveLast(TElement element);
	}
}
