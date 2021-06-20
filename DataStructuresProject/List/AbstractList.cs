using System;

/**
 * 
 *
 * @author Zach Samuels
 *
 * @param 
 */

namespace DataStructures.List
{
    /// <summary>
    /// AbstractList implements the functionality defined by the List interface that
    /// is shared by all list data types.
    /// </summary>
    /// <param name="E">The generic data type contained in the list ADT.</param>
    public abstract class AbstractList<E> : IList<E>
    {
        /**
     * Adds the given element at the beginning of the list.
     * 
     * @param value The element to add to the list.
     */
        public void AddFirst(E value)
        {
            Add(0, value);
        }

        /**
		 * Adds the given element to the end of the list.
		 * 
		 * @param value The element to add to the end of the list.
		 */
        public virtual void AddLast(E value)
        {
            Add(Size(), value);
        }

        /**
		 * Determines if the given index is within the bounds of the list.
		 * 
		 * @param index The index to check the validity of.
		 * @throws IndexOutOfBoundsException if the index is invalid.
		 */
        protected void CheckIndex(int index)
        {
            if (index < 0 || index >= Size())
            {
                throw new ArgumentOutOfRangeException("Index is invalid: "
                        + index + " (size=" + Size() + ")");
            }
        }

        /**
		 * Determines if the given index is within the bounds of the list in the
		 * context of adding to the list.
		 * 
		 * @param index The index to check the validity of.
		 * @throws IndexOutOfBoundsException if the index is invalid.
		 */
        protected void CheckIndexForAdd(int index)
        {
            if (index < 0 || index > Size())
            {
                throw new ArgumentOutOfRangeException("Index is invalid: "
                        + index + " (size=" + Size() + ")");
            }
        }

        /**
		 * Gets the first element in this List
		 * 
		 * @return The first element of the List.
		 */
        public E First()
        {
            return GetIndex(0);
        }

        /**
		 * Determines if the list is empty.
		 * 
		 * @return True if the list is empty, false if not.
		 */
        public bool IsEmpty()
        {
            return Size() == 0;
        }

        /**
		 * Gets the last element in this List.
		 * 
		 * @return The element last of the List.
		 */

        public virtual E Last()
        {
            return GetIndex(Size() - 1);
        }

        /**
		 * Removes and returns the first element in this List.
		 * 
		 * @return The previously first element in this List.
		 */
        public E RemoveFirst()
        {
            return Remove(0);
        }

        /**
		 * Removes and returns the element at the end of the list.
		 * 
		 * @return The element removed from the list.
		 */
        public E RemoveLast()
        {
            return Remove(Size() - 1);
        }

        public abstract void Add(int index, E value);
        public abstract E GetIndex(int index);
        public abstract E Remove(int index);
        public abstract E SetIndex(int index, E value);
        public abstract int Size();
    }
}
