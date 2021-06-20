using System.Collections.Generic;

namespace DataStructures.Set
{
    /**
    * The Set interface declares the methods needs to implement
    * the Set ADT.
    * 
    * @author Zach Samuels
    *
    * @param <E> The generic type of element contained in the Set.
    */
    public interface ISet<E> : IEnumerable<E>
    {
        /**
	     * Adds the given element to the Set.
	     * @param value The element to add to the Set.
	     */
        void Add(E value);

        /**
         * Determines if the given element is part of the Set.
         * @param value The element to find in the Set.
         * @return True if the element is in the Set, false otherwise.
         */
        bool Contains(E value);

        /**
         * Removes and returns the given element from the Set.
         * @param value The element to remove from the Set.
         * @return The removed element if it exists.
         */
        E Remove(E value);

        /**
         * Determines if the Set is empty.
         * @return True if the Set is empty, false otherwise.
         */
        bool IsEmpty();

        /**
         * Returns the number of elements in the Set.
         * @return The current size of the Set.
         */
        int Size();

        /**
         * Adds all elements of the given Set to the current Set (performs the union
         * set operation).
         * @param other The Set to add.
         */
        void AddAll(ISet<E> other);

        /**
         * Updates the current Set to only contain the elements in both it
         * and the given Set (performs the intersection set operation).
         * @param other The other set to intersect with the current one.
         */
        void RetainAll(ISet<E> other);

        /**
         * Updates the current set to remove all elements contained in it
         * and the given set (performs the subtraction set operation).
         * @param other The other Set to subtract from the current one.
         */
        void RemoveAll(ISet<E> other);
    }
}
