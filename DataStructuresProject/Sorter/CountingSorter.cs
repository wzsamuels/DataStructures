using DataStructures.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Sorter
{
    /**
     * CountingSorter uses the counting sort algorithm to sort data       
     * @author Zach Samuels
     *
     * @param <E> the generic type of data to sort
     */
    public class CountingSorter<E> : ISorter<E> 
        where E : IIdentifiable
    {
        /**
         * Sorts an array of generic data objects.
         * 
         * @param data The array of data to sort.
         */
        public void Sort(E[] data)
        {
            int min = data[0].GetId();
            int max = data[0].GetId();

            for (int i = 0; i <= data.Length - 1; i++)
            {
                min = Math.Min(data[i].GetId(), min);
                max = Math.Max(data[i].GetId(), max);
            }

            int range = max - min + 1;

            int[] bucket = new int[range];

            // Count the number of each data occurrence
            for (int i = 0; i <= data.Length - 1; i++)
                bucket[data[i].GetId() - min]++;

            // Accumulate frequencies
            for (int i = 1; i <= range - 1; i++)
                bucket[i] = bucket[i - 1] + bucket[i];

            // Create a new array for the sorted data
            Object[] sorted = new Object[data.Length];

            for (int i = 0; i <= data.Length - 1; i++)
            {
                sorted[bucket[data[i].GetId() - min] - 1] = (E)data[i];
                bucket[data[i].GetId() - min]--;
            }

            Array.Copy(sorted, 0, data, 0, data.Length);
        }
    }
}
