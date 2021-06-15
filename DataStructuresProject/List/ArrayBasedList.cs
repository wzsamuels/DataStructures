using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.List
{
    public class ArrayBasedList<E> : AbstractList<E>, IEnumerable<E>
    {/** The default size of a new array list */
        private const int DEFAULT_CAPACITY = 16;
        /** The list's data elements */
        private E[] data;
        /** The current size of the list */
        private int size;

        /**
		 * Constructor with no parameters. Sets the list's size to the default 
		 * capacity.
		 */
        public ArrayBasedList() : this(DEFAULT_CAPACITY)
        {

        }

        /**
		 * Constructor with one parameter. 
		 * Warning suppressed in order to cast to generic data type E.
		 * 
		 * @param capacity The size of the new list.
		 */

        public ArrayBasedList(int capacity)
        {
            data = new E[capacity];
            size = 0;
        }

        /**
		 * Adds a new element to the list at the given index with the given value.
		 * 
		 * @param index The position to add the new element.
		 * @param value The value of the new element in the list.
		 */
        public override void Add(int index, E value)
        {
            //CheckIndex(index);

            // Check array capacity and expand if needed
            EnsureCapacity(size + 1);
            // Shift array to make room for new element
            for (int i = size; i > index; i--)
            {
                data[i] = data[i - 1];
            }

            data[index] = value;
            size++;
        }

        /**
		 * Gets the element at the given index.
		 * 
		 * @param index The index to retrieve.
		 * @throws IndexOutOfBoundsException If index is out of bounds of the array.
		 * @return The element at the given index.
		 */
        public override E GetIndex(int index)
        {
            CheckIndex(index);

            return data[index];
        }

        /**
		 * Removes and returns the element at the given index.
		 * 
		 * @param index The index of the element to remove.
		 * @return The element previously at the given index.
		 */
        public override E Remove(int index)
        {
            CheckIndex(index);

            E temp = data[index];
            //Shift array to remove element
            for (int i = index; i < size - 1; i++)
            {
                data[i] = data[i + 1];
            }

            size--;

            return temp;
        }

        /**
		 * Sets the element at the given index to the given value.
		 * 
		 * @param index The index to set.
		 * @param value The value to set the element to.
		 * @return The element previously at the given index.
		 */
        public override E SetIndex(int index, E value)
        {
            CheckIndex(index);

            E temp = data[index];
            data[index] = value;

            return temp;
        }

        /**
		 * Gets the number of elements in the array list.
		 * 
		 * @return The size of array list.
		 */
        public override int Size()
        {
            return size;
        }

        public IEnumerator<E> GetEnumerator()
        {
            for(int i = 0; i < size; i++)
            {
                yield return data[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /**
		 * Creates an iterator to iterator over the elements in the list.
		 * 
		 * @return An iterator object for this ArrayList.
		 */


        /**
		 * Checks if this list has enough room for the given capacity. If not,
		 * the list is double in size.
		 * 
		 * @param minCapacity The new desired capacity of the list. 
		 */
        private void EnsureCapacity(int minCapacity)
        {
            int oldCapacity = data.Length;
            if (minCapacity > oldCapacity)
            {
                int newCapacity = oldCapacity * 2 + 1;
                if (newCapacity < minCapacity)
                {
                    newCapacity = minCapacity;
                }
                Array.Resize(ref data, newCapacity);
            }
        }
    }
}
