using System;
using System.Collections.Generic;
using System.Text;
using DataStructures.List;

namespace DataStructures.Map
{
    /**
     * SkipListMap extends AbstractSortMap to implement the Map ADT as an ordered 
     * skip linked list. Keys must be Comparable objects so that the Map Entries can
     * be sorted.
     * 
     * @author Zach Samuels
     *
     * @param <K> Generic type used for the Map's keys.
     * @param <V> Generic type stored as the Map's values.
     */
    public class SkipListMap<TKey, TValue> : AbstractSortedMap<TKey, TValue>
		where TKey : IComparable<TKey>
    {
		private readonly Random coinToss;
		private SkipListEntry start;
		private int size;
		private int height;

		/**
		 * Creates a new, empty SearchTableMap. The Map's Comparator is set to a
		 * default value.
		 */
		public SkipListMap() : this(null)
		{
			
		}

		/**
		 * Creates a new, empty SkipListMap that uses the given Comparator to sort
		 * its Entries.
		 * 
		 * @param compare The Comparator object used to sort the MAp.
		 */
		public SkipListMap(IComparer<TKey> compare) : base(compare)
		{
			coinToss = new Random();
			// Create a dummy head node for the left "-INFINITY" sentinel tower
			start = new SkipListEntry(default, default);
			// Create a dummy tail node for the right "+INFINITY" sentinel tower		
			start.SetNext(new SkipListEntry(default, default));
			// Set the +INFINITY tower's previous to be the "start" node
			start.GetNext().SetPrevious(start);
			size = 0;
			height = 0;
		}

		/**
		 * Helper method to determine if an entry is one of the sentinel
		 * -INFINITY or +INFINITY nodes (containing a null key).
		 * 
		 * @param entry The Entry to check for being sentinel.
		 * 
		 * @return True if the Entry is a sentinel, false otherwise.
		 */
		// 
		private static bool IsSentinel(SkipListEntry entry)
		{
			if (entry == null)
				return false;
			return entry.GetKey() == null || entry.GetKey().Equals(default(TKey));
		}

		/**
		 * Finds the Entry with the given Key in the Map.
		 * 
		 * @param key The Key of the Entry to find.
		 * 
		 * @return The Entry with the given Key if it exists, or the Entry just
		 * before where the given Entry should be inserted in the Map.
		 */
		private SkipListEntry LookUp(TKey key)
		{
			SkipListEntry current = start;
			while (current.GetBelow() != null)
			{
				current = current.GetBelow();				
				while (!IsSentinel(current.GetNext()) && key.CompareTo(current.GetNext().GetKey()) >= 0)
				{
					current = current.GetNext();
				}
			}
			return current;
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
			SkipListEntry temp = LookUp(key);

			if (temp != null && temp.GetKey() != null && temp.GetKey().Equals(key))
				return temp.GetValue();
			else
				return default;
		}

		/**
		 * Inserts a new SkipListEntry with the given Key and Value in the place
		 * above and after the given Entries.
		 * 
		 * @param prev The Entry before the insertion spot. 
		 * @param down The Entry below the insertion spot.
		 * @param key The new Entry's Key.
		 * @param value the new Entry's Value.
		 * 
		 * @return The new SkipListEntry object.
		 */
		private static SkipListEntry InsertAfterAbove(SkipListEntry prev,
			SkipListEntry down, TKey key, TValue value)
		{
			SkipListEntry newEntry = new(key, value);
			newEntry.SetBelow(down);
			newEntry.SetPrevious(prev);
			if (prev != null)
			{
				newEntry.SetNext(prev.GetNext());
				newEntry.GetPrevious().SetNext(newEntry);
			}
			if (newEntry.GetNext() != null)
				newEntry.GetNext().SetPrevious(newEntry);
			if (down != null)
				down.SetAbove(newEntry);
			return newEntry;
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
			SkipListEntry temp = LookUp(key);

			if (temp.GetKey() != null && temp.GetKey().CompareTo(key) == 0)
			{
				TValue originalValue = temp.GetValue();
				while (temp != null)
				{
					temp.SetValue(value);
					temp = temp.GetAbove();
				}
				return originalValue;
			}

			SkipListEntry q = null;
			int currentLevel = -1;
			do
			{
				currentLevel++;
				if (currentLevel >= height)
				{
					height++;
					SkipListEntry tail = start.GetNext();
					start = InsertAfterAbove(null, start, default, default);
                    InsertAfterAbove(start, tail, default, default);
				}
				q = InsertAfterAbove(temp, q, key, value);
				while (temp != null && temp.GetAbove() == null)
				{
					temp = temp.GetPrevious();
				}
				if (temp != null)
					temp = temp.GetAbove();
			} while (coinToss.Next() % 2 == 1);
			size++;
			return default;
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
			SkipListEntry temp = LookUp(key);

            TKey key1 = temp.GetKey();
            if (!EqualityComparer<TKey>.Default.Equals(key1, key))
				return default;
			else
			{
				// Save the Entry's Value to return to user
				TValue originalValue = temp.GetValue();

				// Remove all occurrences of the Entry, starting at the lowest level
				do
				{
					SkipListEntry prev = temp.GetPrevious();
					SkipListEntry next = temp.GetNext();
					SkipListEntry above = temp.GetAbove();
					SkipListEntry below = temp.GetBelow();

					if (prev != null)
						prev.SetNext(next);
					if (next != null)
						next.SetPrevious(prev);
					if (below != null)
						below.SetAbove(null);

					temp = above;
				} while (temp != null);

				// Check for empty levels, skipping over the top level
				SkipListEntry node = start.GetBelow();
				while (node != null)
				{
					// If we reach an empty level, unlink the sentinel nodes
					SkipListEntry next = node.GetNext();
					SkipListEntry nodeBelow = node.GetBelow();

					if (next != null && next.GetKey() == null)
					{
						SkipListEntry nextAbove = next.GetAbove();
						if (nextAbove != null)
							nextAbove.SetBelow(next.GetBelow());

						SkipListEntry nextBelow = next.GetBelow();
						if (nextBelow != null)
							nextBelow.SetAbove(next.GetAbove());

						SkipListEntry nodeAbove = node.GetAbove();
						if (nodeAbove != null)
							nodeAbove.SetBelow(node.GetBelow());

						if (nodeBelow != null)
							nodeBelow.SetAbove(node.GetAbove());
					}
					// Move down to the next level
					node = nodeBelow;
				}
				size--;
				return originalValue;
			}
		}

		/**
		 * Returns the number of entries currently in the Map.
		 * 
		 * @return The size of the Map.
		 */
		public override int Size()
		{
			return size;
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
			SkipListEntry current = start;
			while (current.GetBelow() != null)
			{
				current = current.GetBelow();
			}
			current = current.GetNext();
			while (!IsSentinel(current))
			{
				set.AddLast(current);
				current = current.GetNext();
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
			SkipListEntry cursor = start;
			while (cursor.GetBelow() != null)
			{
				cursor = cursor.GetBelow();
			}
			cursor = cursor.GetNext();
			while (cursor != null && cursor.GetKey() != null && !IsSentinel(cursor))
			{
				sb.Append(cursor.GetKey());
				if (!IsSentinel(cursor.GetNext()))
				{
					sb.Append(", ");
				}
				cursor = cursor.GetNext();
			}
			sb.Append(']');

			return sb.ToString();
		}

		// This method may be useful for testing or debugging.
		// You may comment-out this method instead of testing it, since
		// the full string will depend on the series of random coin flips
		// and will not have deterministic expected results.	
		/*
		public String toFullString() {
			StringBuilder sb = new StringBuilder(this.getClass().getSimpleName() + "[\n");
			SkipListEntry<K, V> cursor = start;
			SkipListEntry<K, V> firstInList = start;
			while( cursor != null) {
				firstInList = cursor;
				sb.append("-INF -> ");
				cursor = cursor.next;
				while(cursor != null && !isSentinel(cursor)) {
					sb.append(cursor.getKey() + " -> ");
					cursor = cursor.next;
				}
				sb.append("+INF\n");
				cursor = firstInList.below;
			}
			sb.append("]");
			return sb.toString();
		}
		*/

		/**
		 * SkipListEntry represents a single element in a SkipListMap.
		 * 
		 * @param <K> Generic type used for the Map's keys.
		 * @param <V> Generic type stored as the Map's values.
		 */
		private class SkipListEntry : MapEntry
		{
			private SkipListEntry above;
			private SkipListEntry below;
			private SkipListEntry prev;
			private SkipListEntry next;

			/**
			 * Creates a new SkipListEntry with the given Key and Value.
			 * 
			 * @param key The new Entry's Key.
			 * @param value The new Entry's Vakue.
			 */
			public SkipListEntry(TKey key, TValue value) :base(key, value)
			{				
				SetAbove(null);
				SetBelow(null);
				SetPrevious(null);
				SetNext(null);
			}

			/**
			 * Gets the SkipListEntry below the this Entry in the Map.
			 * 
			 * @return The Entry linked below this Entry.
			 */
			public SkipListEntry GetBelow()
			{
				return below;
			}

			/**
			 * Gets the SkipListEntry after this Entry in the Map
			 * 
			 * @return The Entry linked after this Entry.
			 */
			public SkipListEntry GetNext()
			{
				return next;
			}

			/**
			 * Gets the SkipListEntry before this Entry in the Map.
			 * 
			 * @return The Entry linked before this Entry.
			 */
			public SkipListEntry GetPrevious()
			{
				return prev;
			}

			/**
			 * Gets the SkipListEntry above this Entry in the Map.
			 * 
			 * @return The Entry linked above this Entry.
			 */
			public SkipListEntry GetAbove()
			{
				return above;
			}

			/**
			 * Sets the SkipListEntry below this Entry in the Map.
			 * 
			 * @param down The new Entry linked below this Entry.
			 */
			public void SetBelow(SkipListEntry down)
			{
				this.below = down;
			}

			/**
			 * Sets the SkipListEntry after this Entry in the Map.
			 * 
			 * @param next The new Entry linked after this Entry.
			 */
			public void SetNext(SkipListEntry next)
			{
				this.next = next;
			}

			/**
			 * Sets the SkipListEntry before this Entry in the Map.
			 * 
			 * @param prev The new Entry linked before this Entry.
			 */
			public void SetPrevious(SkipListEntry prev)
			{
				this.prev = prev;
			}

			/**
			 * Sets the SkipListEntry above this Entry in the Map.
			 * 
			 * @param up The Entry linked above this Entry.
			 */
			public void SetAbove(SkipListEntry up)
			{
				this.above = up;
			}
		}
	}
}
