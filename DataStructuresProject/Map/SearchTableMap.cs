using System;
using System.Collections.Generic;
using System.Text;
using DataStructures.List;

namespace DataStructures.Map
{
    /**
     * SearchTableMap extends AbstractSortMap to implement the Map ADT as an ordered 
     * array-based list. Keys must be Comparable objects so that the Map Entries can
     * be sorted.
     * 
     * @author Zach Samuels
     *
     * @param <K> Generic type used for the Map's keys.
     * @param <V> Generic type stored as the Map's values.
     */
    public class SearchTableMap<TKey, TValue> : AbstractSortedMap<TKey, TValue> where TKey : IComparable<TKey>
	{
		private readonly ArrayBasedList<IMap<TKey, TValue>.IEntry> list;

		/**
		 * SearchTableMap with no parameters. The Map's Comparator is set to a
		 * default value.
		 */
		public SearchTableMap() : this(null)
		{
		}

		/**
		 * SearchTableMap with one parameter. Sets the Map's Comparator to the
		 * given Comparator object.
		 * 
		 * @param compare The Comparator to use when sorting the Map.
		 */
		public SearchTableMap(IComparer<TKey> compare) : base(compare)
		{			
			list = new ArrayBasedList<IMap<TKey, TValue>.IEntry>();
		}

		/**
		 * Finds the Entry with the given Key in the Map.
		 * 
		 * @param key The Key of the Entry to find.
		 * 
		 * @return If the given Key exists in the Map, its index is return. If not,
		 * a negative index representing where it belongs in sorted order is returned.
		 */
		private int LookUp(TKey key)
		{
			return BinarySearchHelper(0, list.Size() - 1, key);
		}

		/**
		 * Uses a binary search to find the index of the Entry with the given
		 * Key in the Map.
		 * 
		 * @param min The lower bound of indexes to search. 
		 * @param max The upper bound of indexes to search.
		 * @param key The Key to search for.
		 * @return The index of the Entry in the Map.
		 */
		private int BinarySearchHelper(int min, int max, TKey key)
		{
			if (min > max)
				return -1 * (min + 1);

			int mid = (max + min) / 2;
			if (list.GetIndex(mid).GetKey().CompareTo(key) == 0)
				return mid;
			else if (list.GetIndex(mid).GetKey().CompareTo(key) > 0)
				return BinarySearchHelper(min, mid - 1, key);
			else
				return BinarySearchHelper(mid + 1, max, key);
		}

		/**
		 * Returns the number of entries currently in the Map.
		 * 
		 * @return The size of the Map.
		 */
		public override int Size()
		{
			return list.Size();
		}

		/**
		 * Gets the Value associated with the given Key.
		 * 
		 * @param key The Key to look up in the Map.
		 * 
		 * @return The Value associated with the Key or null if no such Key exists.
		 */
		public override TValue GetValue(TKey key)
		{
			int index = LookUp(key);

			if (index < 0)
				return default;
			else
				return list.GetIndex(index).GetValue();
		}

		/**
		 * Generates an object that can be iterated over to retrieve the Entries in
		 * the Map.
		 * 
		 * @return An Iterable object for all Entries in the Map.
		 */
		public override IEnumerable<IMap<TKey, TValue>.IEntry> EntryIterator()
		{
			ArrayBasedList<IMap<TKey, TValue>.IEntry> set = new();
			foreach (IMap<TKey, TValue>.IEntry m in list)
			{
				set.AddLast(m);
			}
			return set;
		}

		/**
		 * Adds a Key associated with the given Value to the Map if no such Key exists, or replaces
		 * the Value if the Key already exists. 
		 * 
		 * @param key The Key to add or set in the Map.
		 * @param value The Value to add or replace.
		 * 
		 * @return The old Value associated with the Key or null if the Key is new.
		 */
		public override TValue Put(TKey key, TValue value)
		{
			int index = LookUp(key);

			if (index < 0)
			{
				list.Add(-1 * (index + 1), new MapEntry(key, value));
				return default;
			}
			else
			{
				return list.SetIndex(index, new MapEntry(key, value)).GetValue();
			}
		}

		/**
		 * Removes and returns the Value associated with the given from the Map.
		 * 
		 * @param key The Key to remove from the Map.
		 * 
		 * @return The Value associated with the Key or null if no such Key exists.
		 */
		public override TValue Remove(TKey key)
		{
			int index = LookUp(key);

			if (index < 0)
				return default;
			else
			{
				return list.Remove(index).GetValue();
			}
		}

		/**
		 * Generates a String representation of the Map listing its Keys in their internally
		 * stored order.
		 * 
		 * @return The Map's Keys as a String.
		 */
		public override string ToString()
		{
			StringBuilder sb = new(this.GetType().Name + "[");
			IEnumerator<IMap<TKey, TValue>.IEntry> it = list.GetEnumerator();

			if (it.MoveNext())
			{
				sb.Append(it.Current.GetKey());
			}
			while (it.MoveNext())
			{
				sb.Append(", ");
				sb.Append(it.Current.GetKey());
			}

			sb.Append(']');
			return sb.ToString();
		}
	}
}
