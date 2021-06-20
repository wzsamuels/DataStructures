using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Sorter
{
	/**
	* BubbleSorter uses the bubble sort algorithm to sort data.
	* 
	* @author Zach Samuels
	*
	* @param <E> The generic type of data to sort.
	*/
	public class BubbleSorter<E> : AbstractComparisonSorter<E>
		where E : IComparable<E>
	{

		/**
		 * BubbleSorter Constructor with no parameters.
		 */
		public BubbleSorter() : this(null)
		{			
		}

		/**
		 * BubbleSorter constructor with one parameter.
		 * 
		 * @param comparator The type of comparator to use when sorting.
		 */
		public BubbleSorter(IComparer<E> comparator) : base(comparator)
		{			
		}

		/**
		 * Sorts an array of generic data objects.
		 * 
		 * @param data The array of data to sort.
		 */
		public override void Sort(E[] data)
		{
			bool repeat = true;
			while (repeat)
			{
				repeat = false;
				for (int i = 1; i <= data.Length - 1; i++)
				{
					if (Compare(data[i], data[i - 1]) < 0)
					{
						E x = data[i - 1];
						data[i - 1] = data[i];
						data[i] = x;
						repeat = true;
					}

				}
			}
		}
	}
}
