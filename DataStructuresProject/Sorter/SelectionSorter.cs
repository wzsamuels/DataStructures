using System;
using System.Collections.Generic;

namespace DataStructures.Sorter
{
    /**
     * SelectionSorter uses the selection sort algorithm to sort data
     * 
     * @author Zach Samuels
     *
     * @param <E> the generic type of data to sort
     */
    public class SelectionSorter<E> : AbstractComparisonSorter<E>
        where E : IComparable<E>
    {

        /**
	     * SelectionSorter constructor with no parameters.
	     */
        public SelectionSorter() : this(null)
        {
        }

        /**
         * SelectionSorter constructor with one parameter.
         * 
         * @param comparator The type of comparator to use when sorting.
         */
        public SelectionSorter(IComparer<E> comparator) : base(comparator)
        {
        }

        /**
         * Sorts an array of generic data objects.
         * 
         * @param data The array of data to sort.
         */
        
        public override void Sort(E[] data)
        {
            for (int i = 0; i <= data.Length - 1; i++)
            {
                int min = i; // Assume the current element is the min
                             // Loop forward through the rest of the array
                for (int j = i + 1; j <= data.Length - 1; j++)
                {
                    //Check each element to see if it's the new min
                    if (Compare(data[j], data[min]) < 0)
                        min = j;
                }
                // If the current element isn't in the right place, swap it
                if (i != min)
                {
                    E x = data[i];
                    data[i] = data[min];
                    data[min] = x;
                }
            }
        }
    }
}
