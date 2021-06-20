using System;
using System.Collections.Generic;
using DataStructures.Map;
using DataStructures.SearchTree;

namespace DataStructures.Set
{
    /**
    * TreeSet implements the Set ADT using a binary search tree to store the
    * elements of the set.
    * Remember that search trees are ordered, so our elements must be Comparable.
    * 
    * @author Zach Samuels
    *
    * @param <E> The generic type of element contained in the TreeSet.
    */
    public class TreeSet<E> : AbstractSet<E> where E : IComparable<E>
    {
        private readonly IMap<E, E> tree;

        /**
         * Creates a new TreeSet.
         */
        public TreeSet()
        {
            tree = new RedBlackTreeMap<E, E>();
        }

        /**
         * Returns an iterator for the TreeSet.
         * @return An iterator over the elements of the TreeSet.
         */
        public override IEnumerator<E> GetEnumerator()
        {
            return tree.GetEnumerator();
        }

        /**
         * Adds the given element to the TreeSet.
         * @param value The element to add to the TreeSet.
         */
        public override void Add(E value)
        {
            if (EqualityComparer<E>.Default.Equals(tree.GetValue(value), default))
            {
                tree.Put(value, value);
            }
        }

        /**
         * Determines if the TreeSet contains the given element.
         * @param value The element to find in the TreeSet.
         * @return True if the element is in the set, false otherwise.
         */
        public override bool Contains(E value)
        {
           return !EqualityComparer<E>.Default.Equals(tree.GetValue(value), default);
        }

        /**
         * Removes the given element from the TreeSet.
         * @param value The element to remove.
         * @return The removed element if it exists in the set, null otherwise.
         */
        public override E Remove(E value)
        {
            return tree.Remove(value);
        }

        /**
         * Returns the number of elements in the TreeSet.
         * @return The current size of the TreeSet.
         */
        public override int Size()
        {
            return tree.Size();
        }
    }
}
