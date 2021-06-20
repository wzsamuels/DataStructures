using DataStructures.List;
using DataStructures.Map;
using DataStructures.SearchTree;
using System;
using System.Collections.Generic;

namespace DataStructures.Hashing
{
    /**
     * SeparateChainingHashMap implements the hash map ADT using separate chaining
     * to deal with hashing collisions.
     * 
     * @author Zach Samuels
     *
     * @param <K> The generic type used as the entry's key.
     * @param <V> The generic type used as the entry's value.
     */
    public class SeparateChainingHashMap<TKey, TValue> : AbstractHashMap<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        private IMap<TKey, TValue>[] table;
        private int size;

        /**
         * Creates a new SeparateChainingHashMap default capacity and
         * non-test hashing.
         */
        public SeparateChainingHashMap() : this(AbstractHashMap<TKey, TValue>.DEFAULT_CAPACITY, false)
        {            
        }

        /**
         * Creates a new SeparateChainingHashMap with the default capacity and
         * the given testing value.
         * @param isTesting If true, test hashing should be used.
         */
        public SeparateChainingHashMap(bool isTesting) : this(AbstractHashMap<TKey, TValue>.DEFAULT_CAPACITY, isTesting)
        {            
        }

        /**
         * Creates a new SeparateChainingHashMap with the given capacity and
         * the non-test hashing.
         * @param capacity The capacity of the new hash map.
         */
        public SeparateChainingHashMap(int capacity) : this(capacity, false)
        {            
        }

        /**
         * Creates a new SeparateChainingHashMap with the given capacity and
         * the given testing value.
         * @param capacity The capacity of the new hash map.
         * @param isTesting If true, test hashing should be used.
         */
        public SeparateChainingHashMap(int capacity, bool isTesting) : base(capacity, isTesting)
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
                if (table[i] != null)
                {
                    // Each bucket contains a map, so include
                    // all entries in the entrySet for the map
                    // at the current bucket
                    foreach (IMap<TKey, TValue>.IEntry entry in table[i].EntryIterator())
                    {
                        list.AddLast(entry);
                    }
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
            // Example -- change this to whatever map you'd like        
            //table = new AVLTreeMap[capacity];
            table = new RedBlackTreeMap<TKey, TValue>[capacity];
            size = 0;
        }

        /**
         * Returns the value mapped to the given hash and key.
         * @param hash The hash value to find.
         * @param key The key to find.
         * @return The value mapped to the given key, null if the key doesn't exist.
         */
        public override TValue BucketGet(int hash, TKey key)
        {
            // Get the bucket at the specified index in the hash table      
            IMap<TKey, TValue> bucket = table[hash];
            // If there is no map in the bucket, then the entry does not exist      
            if (bucket == null)
            {
                return default;
            }
            // Otherwise, delegate to the existing map's get method to return the value     
            return bucket.GetValue(key);
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
            IMap<TKey, TValue> bucket = table[hash];

            if (bucket == null)
                bucket = new RedBlackTreeMap<TKey, TValue>();

            TValue temp = bucket.Put(key, value);

            if (temp == null)
                size++;

            table[hash] = bucket;
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
            IMap<TKey, TValue> bucket = table[hash];

            if (bucket == null)
                return default;

            TValue temp = bucket.Remove(key);

            if (temp != null)
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
    }
}
