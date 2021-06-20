using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Sorter
{
	/**
	 * InsertionSorter uses the insertion sort algorithm to sort data.
	 * 
	 * @param <E> The generic type of object being sorted. 
	 * 
	 * @author Zach Samuels
	 */
	public class InsertionSorter<E> : AbstractComparisonSorter<E>
		where E : IComparable<E>
	{
		/**
		 * InsertionSorter constructor with no parameters.
		 */
		public InsertionSorter() : this(null)
		{			
		}

		/**
		* InsertionSorter constructor with one parameter.
		* 
		* @param comparator The type of comparator to use when sorting.
		*/
		public InsertionSorter(IComparer<E> comparator) : base(comparator)
		{
		}

		/**
		 * Sorts an array of generic data objects.
		 * 
		 * @param data The array of data to sort.
		 */
	
		public override void Sort(E[] data)
		{
			for (int i = 1; i <= data.Length - 1; i++)
			{
				E x = data[i]; // element being compared
				int j = i - 1; // previous element to compare to
							   // Loop back through the array while x is out of order
				while (j >= 0 && Compare(data[j], x) > 0)
				{
					data[j + 1] = data[j]; // Shift the array to make room for x
					j--;
				}
				data[j + 1] = x; // Insert x in it's new ordered spot
			}
		}
	}
}
