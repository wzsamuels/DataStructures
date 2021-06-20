using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Set
{
    /**
    * AbstractSet defines the methods shared by all implementations
    * of the Set ADT regardless of what data structure is used.
    * 
    * @author Zach Samuels
    *
    * @param <E> The generic type of element contained in the Set.
    */
    public abstract class AbstractSet<E> : ISet<E>
    {
        /**
         * Adds all elements of the given Set to the current Set (performs the union
        * set operation).
        * @param other The Set to add.
        */
        public void AddAll(ISet<E> other)
        {
            foreach (E element in other)
            {
                Add(element);
            }
        }

        /**
         * Updates the current Set to only contain the elements in both it
         * and the given Set (performs the intersection set operation).
         * @param other The other set to intersect with the current one.
         */
        public void RetainAll(ISet<E> other)
        {
            foreach (E element in this)
            {
                if (!other.Contains(element))
                {
                    Remove(element);
                }
            }
        }

        /**
         * Updates the current set to remove all elements contained in it
         * and the given set (performs the subtraction set operation).
         * @param other The other Set to subtract from the current one.
         */
        public void RemoveAll(ISet<E> other)
        {
            foreach (E element in other)
            {
                Remove(element);
            }
        }

        /**
         * Determines if the Set is empty.
         * @return True if the Set is empty, false otherwise.
         */
        public bool IsEmpty()
        {
            return Size() == 0;
        }

        public abstract void Add(E value);
        public abstract bool Contains(E value);
        public abstract E Remove(E value);
        public abstract int Size();
        public abstract IEnumerator<E> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
