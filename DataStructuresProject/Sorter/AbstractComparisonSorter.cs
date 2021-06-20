using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Sorter
{
    /**
     * Abstract parent class for all sorting classes that use a comparison in their algorithms.
     * 
     * @author Zach Samuels
     *
     * @param <E> The generic type of object to comparison sort.
     */
    public abstract class AbstractComparisonSorter<E> : ISorter<E>
        where E : IComparable<E>
    {

        /** The type of comparator to use when ordering data */
        private IComparer<E> comparator;

        /**
         * AbstractComparisonSorter constructor with one parameter.
         * 
         * @param comparator The type of comparator to use when sorting.
         */
        public AbstractComparisonSorter(IComparer<E> comparator)
        {
            SetComparator(comparator);
        }

        /**
         * Sets the comparator used when sorting.
         * 
         * @param comparator If null, the comparator is set to natural ordering.
         */
        private void SetComparator(IComparer<E> comparator)
        {
            if (comparator == null)
            {
                comparator = new NaturalOrder();
            }
            this.comparator = comparator;
        }

        /**
         * NaturalOrder compares two objects for order using their natural ordering.
         */
        public class NaturalOrder : IComparer<E> {

            /**
    	     * Compares two generic objects for order using their natural ordering.
    	     * 
    	     * @param first The first object to compare.
    	     * @param second The second object to compare.
    	     * 
             * @return -1 if this Student is ordered first, 1 if ordered after, or 0 if they are the same Student.
    	     */
            public int Compare(E first, E second)
            {
                return first.CompareTo(second);
            }
        }

        /**
         * Compares two generic objects for order using the comparator. 
         * 
         * @param data1 The first object to compare.
         * @param data2 The second object to compare.
         * 
         * @return -1 if this Student is ordered first, 1 if ordered after, or 0 if they are the same Student.
         */
        public int Compare(E data1, E data2)
        {
            return comparator.Compare(data1, data2);
        }

        public abstract void Sort(E[] data);
    }
}
