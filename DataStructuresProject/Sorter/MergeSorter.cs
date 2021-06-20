using System;
using System.Collections.Generic;

namespace DataStructures.Sorter
{
	/**
	 * MergerSorter extends AbstractComparisonSorter to provide an implementation of the
	 * merge sort algorithm. 
	 * 
	 * @author Zach Samuels
	 *
	 * @param <E> The generic data type to sort.
	 */
	public class MergeSorter<E> : AbstractComparisonSorter<E> 
		where E : IComparable<E>
	{	
		/**
		 * MergeSorter constructor that sets the comparator object to use when sorting.
		 * 
		 * @param comparator The type of comparator to use when sorting.
		 */
		public MergeSorter(Comparer<E> comparator) : base(comparator)
		{
		}

		/**
		 * MergeSorter with no parameters.
		 */
		public MergeSorter() : this(null)
		{			
		}

		/**
		 * Recursively sorts an array of data using the merge sort algorithm.
		 * 
		 * @param data The array of data elements to sort.
		 */
	
		public override void Sort(E[] data)
		{
			if (data.Length < 2)
				return;

			int mid = data.Length / 2;
			E[] left = new E[mid - 0];
			Array.Copy(data, 0, left, 0, mid);
			E[] right = new E[data.Length - mid];
			Array.Copy(data, mid, right, 0, data.Length - mid);

			Sort(left);
			Sort(right);

			Merge(left, right, data);

		}

		/**
		 * Helper function for sort(). Merges two arrays into a third
		 * destination array. 
		 * 
		 * @param left The first array to merge.
		 * @param right The second array to merge.
		 * @param data The destination array.
		 */
		private void Merge(E[] left, E[] right, E[] data)
		{
			int leftIndex = 0;
			int rightIndex = 0;
			while (leftIndex + rightIndex < data.Length)
			{
				if (rightIndex == right.Length || leftIndex < left.Length
						&& Compare(left[leftIndex], right[rightIndex]) < 0)
				{
					data[leftIndex + rightIndex] = left[leftIndex];
					leftIndex++;
				}
				else
				{
					data[leftIndex + rightIndex] = right[rightIndex];
					rightIndex++;
				}
			}
		}
	}

}
