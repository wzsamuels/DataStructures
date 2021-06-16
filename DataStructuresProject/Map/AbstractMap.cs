using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Map
{
	/**
    * AbstractMap defines the default behavior for methods required by the Map
    * interface that are shared by all implementations of the Map ADT.
    * 
    * @author Zach Samuels
    *
    * @param <K> Generic type used for the Map's keys.
    * @param <V> Generic type stored as the Map's values.
    */
	public abstract class AbstractMap<TKey, TValue> : IMap<TKey, TValue>, IEnumerable<TKey>
	{
		public abstract IEnumerable<IMap<TKey, TValue>.IEntry> EntryIterator();
		public abstract TValue GetValue(TKey key);
		public abstract TValue Put(TKey key, TValue value);
		public abstract TValue Remove(TKey key);
		public abstract int Size();
	
		/**
		* Determines if the Map is empty (has a size of zero).
		* 
		* @return True if the Map is empty, false otherwise.
		*/
		public bool IsEmpty()
		{
			return Size() == 0;
		}

		IEnumerator<TValue> IMap<TKey, TValue>.ValueIterator()
		{
			return new ValueIterator(EntryIterator().GetEnumerator());
		}

		/**
		* Creates a new Iterator for iterating over the Map's Keys.
		* 
		* @return An iterator of the Map's Keys.
		*/
		public IEnumerator<TKey> GetEnumerator()
		{
			return new KeyIterator(EntryIterator().GetEnumerator());

		}

		private IEnumerator GetEnumerator1()
		{
			return this.GetEnumerator();
		}


		IEnumerator IEnumerable.GetEnumerator()
        {
			return GetEnumerator1();
		}

        /**
		* MapEntry objects hold the Key/Value pairs that make up the Map.
		*
		* @param <K> Generic type used for the Map's keys.
		* @param <V> Generic type stored as the Map's values.
		*/
        protected class MapEntry : IMap<TKey, TValue>.IEntry
		{
			private TKey key;
			private TValue value;

			/**
			 * Constructs a new MapEntry object with the given Key and Value.
			 * 
			 * @param key The new MapEntry's Key. 
			 * @param value the new MapEntry's Value.
			 */
			public MapEntry(TKey key, TValue value)
			{
				SetKey(key);
				SetValue(value);
			}

			/**
			 * Retrieves the MapEntry's Key.
			 * 
			 * @return The MapEntry's Key.
			 */
			public TKey GetKey()
			{
				return key;
			}

			/**
			 * Retrieves the MapEntry's Value.
			 * 
			 * @return The MapEntry's Value;
			 */
			public TValue GetValue()
			{
				return value;
			}

			/**
			 * Sets the MapEntry's Key to the given Key.
			 * 
			 * @param key The new Key value.
			 * 
			 * @return The MapEntry's old Key.
			 */
			public TKey SetKey(TKey key)
			{
				this.key = key;
				return this.key;
			}

			/**
			 * Sets the MapEntry's Value to given Value.
			 * 
			 * @param value The new Value.
			 * 
			 * @return The MapEntry's old Value.
			 */
			public TValue SetValue(TValue value)
			{
				TValue original = this.value;
				this.value = value;
				return original;
			}

		}
		/**
		 * ValueIterator creates an Iterator for iterating over just the Map's Values.
		 */
		protected class ValueIterator : IEnumerator<TValue> {

			private readonly IEnumerator<IMap<TKey, TValue>.IEntry> it;

			/**
			 * Creates a new ValueIterator from the given Enumerator object.
			 * 
			 * @param iterator The Entry Enumerator to use as the new ValueIterator.
			 */
			public ValueIterator(IEnumerator<IMap<TKey, TValue>.IEntry> iterator)
			{
				it = iterator;
			}

            public TValue Current => it.Current.GetValue();

			object System.Collections.IEnumerator.Current => Current;

			public void Dispose()
            {
				it.Dispose();
            }

            /**
			 * Determines if the Iterator has another object left to be returned
			 * by next().
			 * 
			 * @return True if more elements exist in the Iterator, false otherwise.
			 */
            public bool MoveNext()
			{
				return it.MoveNext();
			}

            public void Reset()
            {
                it.Reset();
            }
        }

		/**
		 * KeyIterator creates an Iterator for iterating over just the Map's Keys.
		 */
		protected class KeyIterator : IEnumerator<TKey> {

			private readonly IEnumerator<IMap<TKey, TValue>.IEntry> it;
			public TKey Current => it.Current.GetKey();
			object IEnumerator.Current => Current;

            /**
			 * Creates a new KeyIterator capable of iterating over a Map with the
			 * given Entry type.
			 * @param iterator The Entry Iterator used to construct the KeyIterator.
			 */
            public KeyIterator(IEnumerator<IMap<TKey, TValue>.IEntry> iterator)
			{
				it = iterator;
			}		

            public bool MoveNext()
            {
				return it.MoveNext();
            }

            public void Reset()
            {
				it.Reset();
            }

            public void Dispose()
            {
				it.Dispose();
            }
        }

		public class ValueIterable : IEnumerable<TValue>
		{
			private readonly IEnumerator<IMap<TKey, TValue>.IEntry> it;

			public ValueIterable(IEnumerator<IMap<TKey, TValue>.IEntry> iterator)
			{
				it = iterator;
			}

			public IEnumerator<TValue> GetEnumerator()
            {
				return new ValueIterator(it);
			}

            /**
			 * Creates a new instance of the ValueIterator class as a ValueIterable
			 * object.
			 */
            IEnumerator IEnumerable.GetEnumerator()
            {
				return GetEnumerator();
            }
        }
}
}
