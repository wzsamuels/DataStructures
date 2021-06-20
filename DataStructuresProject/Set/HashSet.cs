using System.Collections.Generic;
using DataStructures.Hashing;
using DataStructures.Map;

namespace DataStructures.Set
{
    /**
    * Since our hash map uses linear probing, the entries are not ordered.
    * As a result, we do not restrict our hash set to use Comparable elements.
    * This also gives you an option if you need a set to manage elements
    * that are *NOT* Comparable (versus a TreeSet)
    * 
    * @author Zach Samuels
    *
    * @param <E> The generic type of element contained in the HashSet.
    */
    public class HashSet<E> : AbstractSet<E>
    {
        private readonly IMap<E, E> map;

        /**
         *  This constructor will use our "production version" of our hash map
         *  meaning random values for alpha and beta will be used
         */
        public HashSet() : this(false)
        {
            
        }

        /**
         * Creates a new HashSet with the given testing flag.
         * @param isTesting If isTesting is true, this constructor will use our 
         * "development version" of our hash map meaning alpha=1, beta=1, and prime=7
         */
        public HashSet(bool isTesting)
        {
            map = new LinearProbingHashMap<E, E>(isTesting);
        }

        /**
         * Returns an iterator for the HashSet.
         * @return An iterator over the elements of the HashSet.
         */
        public override IEnumerator<E> GetEnumerator()
        {
            return map.GetEnumerator();
        }

        /**
         * Adds the given element to the HashSet.
         * @param value The element to add to the HashSet.
         */
        public override void Add(E value)
        {
            if (EqualityComparer<E>.Default.Equals(map.GetValue(value), default))
                map.Put(value, value);
        }

        /**
         * Determines if the HashSet contains the given element.
         * @param value The element to find in the HashSet.
         * @return True if the element is in the set, false otherwise.
         */
        public override bool Contains(E value)
        {
            return !EqualityComparer<E>.Default.Equals(map.GetValue(value), default);
        }

        /**
         * Removes the given element from the HashSet.
         * @param value The element to remove.
         * @return The removed element if it exists in the set, null otherwise.
         */
        public override E Remove(E value)
        {
            return map.Remove(value);
        }

        /**
         * Returns the number of elements in the HashSet.
         * @return The current size of the HashSet.
         */
        public override int Size()
        {
            return map.Size();
        }
    }
}
