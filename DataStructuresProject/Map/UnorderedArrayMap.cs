using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using DataStructures.List;

namespace DataStructures.Map
{
    /**
    * UnorderedArrayMap extends AbstractMap to implement the Map ADT as an unordered
    * array-based list. The Map uses a transpose heuristic to improve the performance
    * of lookup operations.
    * 
    * @author Zach Samuels
    *
    * @param <K> Generic type used for the Map's keys.
    * @param <V> Generic type stored as the Map's values.
    */
    public class UnorderedArrayMap<TKey, TValue> : AbstractMap<TKey, TValue> where TValue : class
    {
		// Use the adapter pattern to delegate to our existing
		// array-based list implementation
		private ArrayBasedList<IEntry<TKey, TValue>> list;

		/**
		 * Default Constructor for UnorderedArrayMap. Creates a new, empty Map.
		 */
		public UnorderedArrayMap()
		{
			this.list = new ArrayBasedList<IEntry<TKey, TValue>>();
		}

		/**
		 * Finds the Entry with the given Key in the Map.
		 * 
		 * @param key The Key of the Entry to find.
		 * 
		 * @return The index of the Key if it exists, -1 otherwise.
		 */
		private int LookUp(TKey key)
		{
			int index = 0;

			foreach(TKey entry in list)
			{
				if (entry.Equals(key))
					return index;
				index++;
			}

			return -1;
		}

		/**
		 * Gets the Value associated with the given Key. The Key is then transposed with the 
		 * previous Key in the Map.
		 * 
		 * @param key The Key to look up in the Map.
		 * 
		 * @return The Value associated with the Key or null if no such Key exists.
		 */
		public override TValue GetValue(TKey key)
		{
			int index = LookUp(key);

			if (index == -1)
				return null;
			else
				return Transpose(index);
		}

		/**
		 * Adds a Key associated with the given Value to the Map if no such Key
		 * exists, or replaces the Value if the Key already exists. If the Key is
		 * new, it's added to the front of the list. If not, the Key is transposed
		 * with the previous Key in the Map. 
		 * 
		 * @param key The Key to add or set in the Map.
		 * @param value The Value to add or replace.
		 * 
		 * @return The old Value associated with the Key or null if the Key is new.
		 */
		public override TValue Put(TKey key, TValue value)
		{
			int index = LookUp(key);

			if (index == -1)
			{
				list.AddFirst(new MapEntry<TKey, TValue>(key, value));
				return null;
			}
			else
			{
				TValue temp = list.SetIndex(index, new MapEntry<TKey, TValue>(key, value)).GetValue();
				Transpose(index);
				return temp;
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
			IEntry<TKey, TValue> temp = null;

			try
			{
				temp = list.GetIndex(index);
				list.Remove(index);
			}
			catch (ArgumentOutOfRangeException)
			{
				return null;
			}

			return temp.GetValue();
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
		 * Switches Entry with the given index with the Entry at index - 1. Does
		 * nothing if the Entry is already at the front of the Map.
		 * 
		 * @param index The index of the Entry to move forward.
		 * 
		 * @return The Value of the transposed Entry. 
		 */
		private TValue Transpose(int index)
		{
			if (index != 0)
			{
				IEntry<TKey, TValue> indexEntry = list.GetIndex(index);
				IEntry<TKey, TValue> prevEntry = list.GetIndex(index - 1);
				list.SetIndex(index - 1, indexEntry);
				list.SetIndex(index, prevEntry);

				return indexEntry.GetValue();
			}
			// Don't transpose if the index is already 0
			else
				return list.GetIndex(index).GetValue();
		}

		/**
		 * Generates an object that can be iterated over to retrieve the Entries in
		 * the Map.
		 * 
		 * @return An Iterable object for all Entries in the Map.
		 */
		public override IEnumerable<IEntry<TKey, TValue>> EntryIterator()
		{
			return list;
		}

		/**
		 * Generates a String representation of the Map listing its Keys in their internally
		 * stored order.
		 * 
		 * @return The Map's Keys as a String.
		 */
		public override string ToString()
		{
			StringBuilder sb = new(TypeDescriptor.GetClassName(this) + "[");
			IEnumerator<IEntry<TKey, TValue>> it = list.GetEnumerator();

			while(it.MoveNext())
            {
				sb.Append(it.Current.GetKey());
				sb.Append(", ");
			}
			
			sb.Append("]");
			return sb.ToString();
		}
	}
}
