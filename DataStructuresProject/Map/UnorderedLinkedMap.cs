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
		private readonly IPositionalList<IMap<TKey, TValue>.IEntry> list;

		/**
		 * UnorderedLinkedMap constructor with no parameters. Creates a new, empty Map. 
		 */
		public UnorderedLinkedMap()
		{
			this.list = new PositionalLinkedList<IMap<TKey, TValue>.IEntry>();
		}

		/**
		 * Finds the Entry with the given Key in the Map.
		 * 
		 * @param key The Key of the Entry to find.
		 * 
		 * @return The Position of the Key if it exists, null otherwise.
		 */
		private IPosition<IMap<TKey, TValue>.IEntry> LookUp(TKey key)
		{
			IEnumerator<IPosition<IMap<TKey, TValue>.IEntry>> iterator = list.PositionIterator().GetEnumerator();

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
			IPosition<IMap<TKey, TValue>.IEntry> p = LookUp(key);

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
		private void MoveToFront(IPosition<IMap<TKey, TValue>.IEntry> position)
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
			IPosition<IMap<TKey, TValue>.IEntry> p = LookUp(key);

			if (p == null)
			{
				list.AddFirst(new MapEntry(key, value));
				return default;
			}
			else
			{
				IMap<TKey, TValue>.IEntry temp = list.SetPosition(p, new MapEntry(key, value));
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
			IPosition<IMap<TKey, TValue>.IEntry> p = LookUp(key);

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
		public override IEnumerable<IMap<TKey, TValue>.IEntry> EntryIterator()
		{
			IPositionalList<IMap<TKey, TValue>.IEntry> set = new PositionalLinkedList<IMap<TKey, TValue>.IEntry>();
			foreach (IMap<TKey, TValue>.IEntry m in list)
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
