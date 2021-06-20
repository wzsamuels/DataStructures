using System;
using DataStructures.List;
using DataStructures.Map;

namespace DataStructures.Hashing
{
    /**
    * AbstractHashMap extends AbstractMap to define the methods shared
    * by all hash map implementations.
    * 
    * @author Zach Samuels
    *
    * @param <K> The generic type used as the entry's key.
    * @param <V> The generic type used as the entry's value.
    */
    public abstract class AbstractHashMap<TKey, TValue> : AbstractMap<TKey, TValue>
    {/** An initial capacity for the hash table */
        protected const int DEFAULT_CAPACITY = 17;

        // From our discussion in class, the expected number of probes
        // for separate chaining remains relatively the same no matter
        // what the load factor may be. However, for linear probing, to
        // reduce the chance of having large clusters, we will resize
        // when the load factor reaches 0.5
        private const double MAX_LOAD_FACTOR = 0.5;

        /** Prime number to use in hashing */
        protected const int DEFAULT_PRIME = 109345121;

        // Alpha and Beta values for MAD compression
        // This implementation uses a variation of the MAD method
        // where h(k) = ( (alpha * f(k) + beta) % prime) % capacity
        private readonly long alpha;
        private readonly long beta;

        // The prime number to use for compression strategy
        private readonly int prime;

        /**
         * You can use the isTesting flag (set to true) to control
         * the testing environment and avoid random numbers when testing
         * @param capacity The capacity of the hash map.
         * @param isTesting If true, constant values should be used to hash
         */
        public AbstractHashMap(int capacity, bool isTesting)
        {
            if (isTesting)
            {
                alpha = 1;
                beta = 1;
                prime = 7;
            }
            else
            {
                Random rand = new();
                alpha = rand.Next(DEFAULT_PRIME - 1) + 1;
                beta = rand.Next(DEFAULT_PRIME);
                prime = DEFAULT_PRIME;
            }
            CreateTable(capacity);
        }

        /**
         * Compress the given key to a hash value.
         * @param key The key to compress.
         * @return The compressed hash value.
         */
        private int Compress(TKey key)
        {
            return (int)((Math.Abs(key.GetHashCode() * alpha + beta) % prime) % Capacity());
        }

        /**
         * Inserts or overwrites the given key with the given value.
         * @param key The key to insert.
         * @param value The value to insert.
         * @return The value formerly stored at the key or null if the key is new. 
         */
        public override TValue Put(TKey key, TValue value)
        {
            TValue ret = BucketPut(Compress(key), key, value);
            if ((double)Size() / Capacity() > MAX_LOAD_FACTOR)
            {
                Resize(2 * Capacity() + 1);
            }
            return ret;
        }

        /**
         * Returns the value mapped to the given key.
         * @param key The key to lookup.
         * @return The value associated with the key.
         */
        public override TValue GetValue(TKey key)
        {
            return BucketGet(Compress(key), key);
        }

        /**
         * Removes the given key from the hash map.
         * @param key The key to remove from the hash map.
         * @return The value mapped to the remove key or null if the key isn't
         * in the map.
         */
        public override TValue Remove(TKey key)
        {
            return BucketRemove(Compress(key), key);
        }

        /**
         * Resizes the hash map with the given capacity.
         * @param newCapacity The new capacity of the hash map.
         */
        private void Resize(int newCapacity)
        {
            ArrayBasedList<IMap<TKey, TValue>.IEntry> list = new();
            foreach (IMap<TKey, TValue>.IEntry entry in EntryIterator())
            {
                list.AddLast(entry);
            }
            CreateTable(newCapacity);
            foreach (IMap<TKey, TValue>.IEntry entry in list)
            {
                Put(entry.GetKey(), entry.GetValue());
            }
        }

        /**
         * Returns the current capacity of the hash table.
         * @return The capacity of the hash table
         */
        public abstract int Capacity();

        /**
         * Creates a new hash table with the given capacity.
         * @param capacity The capacity of the new hash table.
         */
        public abstract void CreateTable(int capacity);

        /**
         * Returns the value mapped to the given hash and key.
         * @param hash The hash value to find.
         * @param key The key to find.
         * @return The value mapped to the given key.
         */
        public abstract TValue BucketGet(int hash, TKey key);

        /**
         * Inserts or overwrites the given key with the given value.
         * @param hash The hash value for the key.
         * @param key The key to put into the hash map.
         * @param value The value mapped to the key.
         * @return The value previously mapped to the key or null if the key is new.
         */
        public abstract TValue BucketPut(int hash, TKey key, TValue value);

        /**
         * Removes the given key from the hash map.
         * @param hash The key's hash value.
         * @param key The key to remove from the map.
         * @return The value mapped to the key or null if the key doesn't exist.
         */
        public abstract TValue BucketRemove(int hash, TKey key);
    }
}
