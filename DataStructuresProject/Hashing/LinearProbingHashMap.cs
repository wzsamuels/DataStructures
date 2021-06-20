using DataStructures.List;
using DataStructures.Map;
using System.Collections.Generic;

namespace DataStructures.Hashing
{
    /**
     * LinearProbingHashMap implements the hash map ADT using linear probing
     * to deal with hashing collisions.
     * 
     * @author Zach Samuels
     *
     * @param <K> The generic type used as the entry's key.
     * @param <V> The generic type used as the entry's value.
     */
    public class LinearProbingHashMap<TKey, TValue> : AbstractHashMap<TKey, TValue>
    {
        // This time, our array is an array of TableEntry objects
        private TableEntry[] table;
        private int size;

        /**
         * Creates a new LinearProbingHashMap with default capacity and
         * non-test hashing.
         */
        public LinearProbingHashMap() : this(AbstractHashMap<TKey, TValue>.DEFAULT_CAPACITY, false)
        {            
        }

        /**
         * Creates a new LinearProbingHashMap with the default capacity and
         * the given testing value.
         * @param isTesting If true, test hashing should be used.
         */
        public LinearProbingHashMap(bool isTesting) : this(AbstractHashMap<TKey, TValue>.DEFAULT_CAPACITY, isTesting)
        {            
        }

        /**
         * Creates a new LinearProbingHashMap with the given capacity and
         * the non-test hashing.
         * @param capacity The capacity of the new hash map.
         */
        public LinearProbingHashMap(int capacity) : this(capacity, false)
        {            
        }

        /**
         * Creates a new LinearProbingHashMap with the given capacity and
         * the given testing value.
         * @param capacity The capacity of the new hash map.
         * @param isTesting If true, test hashing should be used.
         */
        public LinearProbingHashMap(int capacity, bool isTesting) : base(capacity, isTesting)
        {            
            size = 0;
        }

        /**
         * Generates an iterable list containing the entries in the hash map.
         * @return The hash map's entries as an iterable list. 
         */
        public override IEnumerable<IMap<TKey, TValue>.IEntry> EntryIterator()
        {
            SinglyLinkedList<IMap<TKey, TValue>.IEntry> list = new();
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null && !table[i].IsDeleted())
                {
                    list.AddLast(table[i]);
                }
            }
            return list;
        }

        /**
         * Creates a new hash table with the given capacity.
         * @param capacity The capacity of the new hash table.
         */
       public override void CreateTable(int capacity)
        {
            table = new TableEntry[capacity];
            size = 0;
        }

        /**
         * Helper method to determine whether a bucket has an entry or not  
         * @param index The index to determine the availability of.
         * @return True if the given index is available.
         */
        private bool IsAvailable(int index)
        {
            return (table[index] == null || table[index].IsDeleted());
        }

        /**
         * Helper method to find the bucket for an entry;
         * If the entry *is* in the map, returns the index of the bucket
         * If the entry is *not* in the map, returns -(a + 1) to indicate 
         * that the entry should be added at index a
         * @param index The hash code of the key.
         * @param key The key to find.
         * @return The key's index or where to insert the key.
         */
        private int FindBucket(int index, TKey key)
        {
            int avail = -1;
            int j = index;
            do
            {
                if (IsAvailable(j))
                {
                    if (avail == -1)
                        avail = j;
                    if (table[j] == null)
                        return -(avail + 1);
                }
                else if (table[j].GetKey().Equals(key))
                    return j;
                j = (j + 1) % table.Length;
            } while (j != index);
            return -(avail + 1);
        }

        /**
         * Returns the value mapped to the given hash and key.
         * @param hash The hash value to find.
         * @param key The key to find.
         * @return The value mapped to the given key.
         */
        public override TValue BucketGet(int hash, TKey key)
        {
            int index = FindBucket(hash, key);

            if (index < 0 || table[index].IsDeleted())
                return default;

            return table[index].GetValue();
        }

        /**
         * Inserts or overwrites the given key with the given value.
         * @param hash The hash value for the key.
         * @param key The key to put into the hash map.
         * @param value The value mapped to the key.
         * @return The value previously mapped to the key or null if the key is new.
         */
        public override TValue BucketPut(int hash, TKey key, TValue value)
        {
            int index = FindBucket(hash, key);

            if (index < 0)
            {
                index = -(index + 1);
                table[index] = new TableEntry(key, value);
                size++;
                return default;
            }

            TValue temp = table[index].GetValue();
            table[index] = new TableEntry(key, value);
            return temp;
        }

        /**
         * Removes the given key from the hash map.
         * @param hash The key's hash value.
         * @param key The key to remove from the map.
         * @return The value mapped to the key or null if the key doesn't exist.
         */
        public override TValue BucketRemove(int hash, TKey key)
        {
            int index = FindBucket(hash, key);

            if (index < 0)
                return default;

            TValue temp = table[index].GetValue();
            table[index].SetDeleted(true);
            size--;
            return temp;
        }

        /**
         * Returns the number of entries in the hash map.
         * @return The size of the hash map.
         */
        public override int Size()
        {
            return size;
        }

        /**
         * Returns the current capacity of the hash table.
         * @return The capacity of the hash table
         */
        public override int Capacity()
        {
            return table.Length;
        }

        /**
         * TableEntry is a specialized type of map entry that contains a deleted
         * flag in addition to a key and value.
         *
         * @param <K> The generic type used as the entry's key.
         * @param <V> The generic type used as the entry's value.
         */
        private class TableEntry : MapEntry {

            private bool isDeleted;

            /**
             * Creates a new TableEntry with the given key and value.
             * @param key The TableEntry's key.
             * @param value The TableEntry's value.
             */
            public TableEntry(TKey key, TValue value) : base(key, value)
            {                
                SetDeleted(false);
            }

            /**
             * If the TableEntry has been deleted.
             * @return True if deleted, false otherwise.
             */
            public bool IsDeleted()
            {
                return isDeleted;
            }

            /**
             * Set the TableEntry to the given deleted value.
             * @param deleted If TableEntry has been deleted.
             */
            public void SetDeleted(bool deleted)
            {
                isDeleted = deleted;
            }
        }
    }
}
