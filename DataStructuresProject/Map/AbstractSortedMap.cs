using System;
using System.Collections.Generic;

namespace DataStructures.Map
{
	/**
     * AbstractSortedMap defines the methods shared by all classes that implement the
     * Map ADT as a sorted Map. Sorted Maps require that their Keys are Comparable for
     * the purpose of sorting. 
     * 
     * @author Zach Samuels
     *
     * @param <K> Generic type used as the Map's keys.
     * @param <V> Generic type stored as the Map's values.
     */
	public abstract class AbstractSortedMap<TKey, TValue> : AbstractMap<TKey, TValue> where TKey : IComparable<TKey>
	{
		private readonly IComparer<TKey> compare;

		/**
		 * AbstractSortedMap construct with one constructor. Sets the Comparator
		 * used to sort the Map by key. Sorting is done by natural order if
		 * null is passed.
		 * 
		 * @param compare Comparator object used for ordering keys.
		 */
		public AbstractSortedMap(IComparer<TKey> compare)
		{
			if (compare == null)
			{
				this.compare = new NaturalOrder();
			}
			else
			{
				this.compare = compare;
			}
		}

		/**
		 * Compares two keys for order based on the Map's Comparator.
		 * 
		 * @param key1 The first key to compare.
		 * @param key2 The second key to compare.
		 * 
		 * @return 0 if the keys are equal, -1 if key1 is ordered first, 1 if key2
		 * is ordered first.
		 */
		public int Compare(TKey first, TKey second)
		{
			return compare.Compare(first, second);
		}

		/**
		 * NaturalOrder implements the Comparator interface to define a natural ordering method
		 * for sorting the Map's Keys.
		 */
		private class NaturalOrder : IComparer<TKey>
		{	
            public int Compare(TKey first, TKey second)
            {
				return ((IComparable<TKey>)first).CompareTo(second);
            }
        }	
	}      
}
