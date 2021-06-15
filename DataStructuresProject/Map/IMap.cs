using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Map
{
    /**
	 * The Map interface declares the methods that are implemented by all classes that use
	 * the Map ADT. 
	 *  
	 * @author Zach Samuels
	 *
	 * @param <K> Generic type used for the Map's keys.
	 * @param <V> Generic type stored as the Map's values.
	 */
    public interface IMap<TKey, TValue> :  IEnumerable<TKey>
    {
        /**
		 * Generates an object that can be iterated over to retrieve the Entries in
		 * the Map.
		 * 
		 * @return An Iterable object for all Entries in the Map.
		 */
        IEnumerable<IEntry<TKey, TValue>> EntryIterator();

        /**
        * Generates an object that can be iterated over to retrieve the Values in
        * the Map.
        * 
        * @return An Iterable object for all Values in the Map.
        */
        IEnumerator<TValue> ValueIterator();

        /**
		 * Gets the Value associated with the given Key.
		 * 
		 * @param key The Key to look up in the Map.
		 * 
		 * @return The Value associated with the Key or null if no such Key exists.
		 */
        TValue GetValue(TKey key);

        /**
		 * Determines if the Map is empty (has a size of zero).
		 * 
		 * @return True if the Map is empty, false otherwise.
		 */
        bool IsEmpty();

        /**
		 * Adds a Key associated with the given Value to the Map if no such Key exists, or replaces
		 * the Value if the Key already exists. 
		 * 
		 * @param key The Key to add or set in the Map.
		 * @param value The Value to add or replace.
		 * 
		 * @return The old Value associated with the Key or null if the Key is new.
		 */
        TValue Put(TKey key, TValue value);

        /**
		 * Removes and returns the Value associated with the given from the Map.
		 * 
		 * @param key The Key to remove from the Map.
		 * 
		 * @return The Value associated with the Key or null if no such Key exists.
		 */
        TValue Remove(TKey key);

        /**
		 * Returns the number of entries currently in the Map.
		 * 
		 * @return The size of the Map.
		 */
        int Size();              
    }

    /**
    * Entry objects hold the Key/Value pairs that make up the Map. This interface
    * declares the methods needed for such functionality.   
    *
    * @param <K> Generic type used for the Map's keys.
    * @param <V> Generic type stored as the Map's values.
    */
    public interface IEntry<TKey, TValue>
    {
        /**
         * Retrieves the Entry's Key.
         * 
         * @return The Entry's Key.
         */
        TKey GetKey();

        /**
         * Retrieves the Entry's Value.
         * 
         * @return The Entry's Value;
         */
        TValue GetValue();

        /**
         * Sets the Entry's Key to the given Key.
         * 
         * @param key The new Key value.
         * 
         * @return The Entry's old Key.
         */
        TKey SetKey(TKey key);

        /**
         * Sets the Entry's Value to given Value.
         * 
         * @param value The new Value.
         * 
         * @return The Entry's old Value.
         */
        TValue SetValue(TValue value);
    }
}
