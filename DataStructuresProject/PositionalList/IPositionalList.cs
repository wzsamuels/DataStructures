using System.Collections.Generic;

namespace DataStructures.PositionalList
{
	/**
	 * PositionalList provides an interface for implementing an iterable positional
	 * linked list data type.
	 *
	 * @author Zach Samuels
	 *
	 * @param <E> The generic data type contained in the positional list.
	*/
	public interface IPositionalList<E>	
    {
		/**
			* Inserts into the list a new element with the given value after the 
			* given position.
			* 
			* @param p The position to insert the new node after.
			* @param value The value of the new element to add.  
			* 
			* @return The new element as a PositionalNode.
			*/
		IPosition<E> AddAfter(IPosition<E> p, E value);
	
		/**
		 * Inserts a new element into the list with the given value before the 
		 * given position.
		 * 
		 * @param p The position to insert the new node before.
		 * @param value The value of the new element to add.  
		 * 
		 * @return The new element as a PositionalNode.
		 */
		IPosition<E> AddBefore(IPosition<E> p, E value);

		/**
			* Inserts a new element at the beginning of the list with the given value.
			* 
			* @param value The value of the new element to add.  
			* 
			* @return The new element as a PositionalNode.
			*/
		IPosition<E> AddFirst(E value);

		/**
			* Inserts a new element at the end of the list with the given value.
			* 
			* @param value The value of the new element to add.  
			* 
			* @return The new element as a PositionalNode.
			*/
		IPosition<E> AddLast(E value);

		/**
			* Gets the position after the given position.
			* 
			* @param p The position to find the position after.
			* @return The position after the given position as a PositionalNode.
			*/
		IPosition<E> After(IPosition<E> p);

		/**
			* Gets the position before the given position.
			* 
			* @param p The position to find the position before.
			* @return The position before the given position as a PositionalNode.
			*/
		IPosition<E> Before(IPosition<E> p);

		/**
			* Gets the first position of this PositionalLinkedList.
			* 
			* @return The first position as a PositionalNode.
			*/
		IPosition<E> First();

		/**
			* Determines if the this PositionalLinkedList is empty.
			* 
			* @return True if this list is empty, false if this list isn't empty.
		*/
		bool IsEmpty();

		/**
			* Gets the last position of this PositionalLinkedList.
			* 
			* @return The last position as a PositionalNode.
			*/
		IPosition<E> Last();

		/**
			* Creates a new positional iterator for this PositionalLinkedList.
			* 
			* @return An iterable object starting at the beginning of this linked list.
			*/
		IEnumerator<IPosition<E>> PositionIterator();

		public IEnumerator<E> ElementIterator();

		/**
			* Removes the given position from this list.
			* 
			* @param p The position to remove from the list.
			* 
			* @return The position that was removed from the list.
			*/
		E Remove(IPosition<E> p);

		/**
			* Sets the element at the given position to the given value.
			* 
			* @param p The position to set.
			* @param value The value to set the element to.
			* @return The element previously at the given positoin.
			*/
		E SetPosition(IPosition<E> p, E value);

		/**
			* Gets the number of elements in the positional list.
			* 
			* @return The size of positional list.
			*/
		int Size();
	}
}
