using DataStructures.Data;
using System;

namespace DataStructures.Sorter
{
    /**
    * RadixSorter uses the radix sort algorithm to sort data
    *
    * @param <E> the generic type of data to sort
    */
    public class RadixSorter<E> : ISorter<E>
        where E : IIdentifiable
    {
        /**
         * Sorts an array of generic data objects.
         * 
         * @param data The array of data to sort.
         */
        public void Sort(E[] data)
        {
            int max = 0;
            for (int i = 0; i <= data.Length - 1; i++)
            {
                max = Math.Max(data[i].GetId(), max);
            }

            // The number of digits in the largest value
            int range = (int)Math.Ceiling(Math.Log10(max + 1));
            // The current digit
            int place = 1;

            for (int j = 1; j <= range; j++)
            {
                // Holds data frequencies
                int[] bucket = new int[10];
                // Count the number of each value occurrence
                for (int i = 0; i <= data.Length - 1; i++)
                    bucket[(data[i].GetId() / place) % 10]++;

                // Accumulate frequencies
                for (int i = 1; i <= 9; i++)
                    bucket[i] = bucket[i - 1] + bucket[i];

                // Create a new array for the sorted data
                Object[] sorted = new Object[data.Length];

                for (int i = data.Length - 1; i >= 0; i--)
                {
                    sorted[bucket[(data[i].GetId() / place) % 10] - 1] = (E)data[i];
                    bucket[(data[i].GetId() / place) % 10] = bucket[(data[i].GetId() / place) % 10] - 1;
                }

                Array.Copy(sorted, 0, data, 0, data.Length);
                place *= 10; // Move to the next digit
            }
        }
    }
}
