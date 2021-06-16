using System;
using System.Collections.Generic;
using System.Text;
using DataStructures.PositionalList;

namespace DataStructures.Map
{
    /**
    * UnorderedLinkedMap extends AbstractMap to define the functionality for
    * the Map ADT implemented as positional linked list.
    * 
    * @author Zach Samuels
    *
    * @param <K> Generic type used for the Map's keys.
    * @param <V> Generic type stored as the Map's values.
    */
    public class UnorderedLinkedMap<TKey, TValue> : AbstractMap<TKey, TValue>
    {
		private IPositionalList<IEntry<TKey, TValue>> list;

		/**
		 * UnorderedLinkedMap constructor with no parameters. Creates a new, empty Map. 
		 */
		public UnorderedLinkedMap()
		{
			this.list = new PositionalLinkedList<IEntry<TKey, TValue>>();
		}

		/**
		 * Finds the Entry with the given Key in the Map.
		 * 
		 * @param key The Key of the Entry to find.
		 * 
		 * @return The Position of the Key if it exists, null otherwise.
		 */
		private IPosition<IEntry<TKey, TValue>> LookUp(TKey key)
		{
			IEnumerator<IPosition<IEntry<TKey, TValue>>> iterator = list.PositionIterator();

			while(iterator.MoveNext())
			{
				if (iterator.Current.GetElement().GetKey().Equals(key))
					return iterator.Current;
			}

			return null;

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
			IPosition<IEntry<TKey, TValue>> p = LookUp(key);

			if (p != null && p.GetElement() != null)
			{
				MoveToFront(p);
				return p.GetElement().GetValue();
			}
			else
				return default;
		}

		/**
		 * Moves the given Position to the front of the Map.
		 * 
		 * @param position The Position to move to the front of the Map.
		 */
		private void MoveToFront(IPosition<IEntry<TKey, TValue>> position)
		{
			list.Remove(position);
			list.AddFirst(position.GetElement());
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
			IPosition<IEntry<TKey, TValue>> p = LookUp(key);

			if (p == null)
			{
				list.AddFirst(new MapEntry<TKey, TValue>(key, value));
				return default;
			}
			else
			{
				IEntry<TKey, TValue> temp = list.SetPosition(p, new MapEntry<TKey, TValue>(key, value));
				MoveToFront(p);
				return temp.GetValue();
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
			IPosition<IEntry<TKey, TValue>> p = LookUp(key);

			if (p == null)
				return default;
			else
				return list.Remove(p).GetValue();
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
		 * Generates an object that can be iterated over to retrieve the Entries in
		 * the Map.
		 * 
		 * @return An Iterable object for all Entries in the Map.
		 */
		public override IEnumerable<IEntry<TKey, TValue>> EntryIterator()
		{
			IPositionalList<IEntry<TKey, TValue>> set = new PositionalLinkedList<IEntry<TKey, TValue>>();
			foreach (IEntry<TKey, TValue> m in list)
			{
				set.AddLast(m);
			}
			return set;
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
			IEnumerator<IEntry<TKey, TValue>> it = list.GetEnumerator();

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
