using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
     * List provides an interface for iterable list ADTs.
 *
 * @author Zach Samuels
 *
 * @param <E> The generic data type contained in the list ADT.
 */
namespace DataStructures.List
{
    public interface IList<E>
    {
    
        /**
         * Adds the given element at the given index in the list.
         * 
         * @param index The index to add the new element.
         * @param value The element to add to the list.
         */
        void Add(int index, E value);

        /**
         * Adds the given element at the beginning of the list.
         * 
         * @param value The element to add to the list.
         */
        void AddFirst(E value);

        /**
         * Adds the given element to the end of the list.
         * 
         * @param value The element to add to the end of the list.
         */
        void AddLast(E value);

        /**
         * Gets the element at the beginning of list.
         * 
         * @return The element at the beginning of the list.
         */
        E First();

        /**
         * Gets the list element at the given index.
         * 
         * @param index The index of the element to get.
         * 
         * @return The element at the given index.
         */
        E GetIndex(int index);

        /**
         * Determines if the list is empty.
         * 
         * @return True if the list is empty, false if not.
         */
        bool IsEmpty();

        /**
         * Gets the value of the last element in the list.
         * 
         * @return The value at the end of the list.
         */
        E Last();

        /**
         * Removes and returns the element at the given index.
         * 
         * @param index The index of the element to remove.
         * @return The element previously at the given index.
         */
        E Remove(int index);

        /**
         * Removes and returns the element at the beginning of the list.
         * 
         * @return The element removed from the list.
         */
        E RemoveFirst();

        /**
         * Removes and returns the element at the end of the list.
         * 
         * @return The element removed from the list.
         */
        E RemoveLast();

        /**
         * Sets the element at the given index to the given value.
         * 
         * @param index The index to set.
         * @param value The value to set the element to.
         * @return The element previously at the given index.
         */
        E SetIndex(int index, E value);

        /**
         * Gets the number of elements in the array list.
         * 
         * @return The size of array list.
         */
        int Size();
    }
}
