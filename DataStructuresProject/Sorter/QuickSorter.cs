using System;
using System.Collections.Generic;

namespace DataStructures.Sorter
{
	/**
	 * QuickSorter extends AbstractComparisonSorter to provide an implementation of the
	 * quick sort algorithm.
	 *  
	 * @author Zach Samuels
	 *
	 * @param <E> The generic data type to sort.
	 */
	public class QuickSorter<E> : AbstractComparisonSorter<E> 
		where E : IComparable<E>
	{
		/** Select the first element as the pivot  */
		public static readonly IPivotSelector FIRST_ELEMENT_SELECTOR = new FirstElementSelector();

		/** Select the last element as the pivot  */
		public static readonly IPivotSelector LAST_ELEMENT_SELECTOR = new LastElementSelector();

		/** Select the middle element as the pivot  */
		public static readonly IPivotSelector MIDDLE_ELEMENT_SELECTOR = new MiddleElementSelector();

		/** Select a random element as the pivot  */
		public static readonly IPivotSelector RANDOM_ELEMENT_SELECTOR = new RandomElementSelector();

		private IPivotSelector selector;

		/**
		 * QuickSorter constructor that sets the comparator used when sorting and
		 * the PivotSelector used to select the pivot index when sorting.
		 * 
		 * @param comparator The comparator object used to compare data elements.
		 * @param selector The PivotSelector object used to select the pivot.
		 */
		public QuickSorter(IComparer<E> comparator, IPivotSelector selector) : base(comparator)
		{
			SetSelector(selector);
		}

		/**
		 * QuickSorter constructor that sets the comparator used when sorting. The
		 * PivotSelector used is set to a default value. 
		 * 
		 * @param comparator The type of comparator to use when sorting.
		 */
		public QuickSorter(IComparer<E> comparator) : this(comparator, null)
		{			
		}

		/**
		 * QuickSorter constructor that sets the PivotSelector used to select the pivot
		 * index when sorting.
		 * 
		 * @param selector The PivotSelector object used to select the pivot.
		 */
		public QuickSorter(IPivotSelector selector) : this(null, selector)
		{			
		}

		/**
		 * Comparator and PivotSelector are set to default values.
		 */
		public QuickSorter() : this(null, null)
		{
		}

		/**
		 * Setter for the QuickSorter's PivotSelector.
		 * 
		 * @param selector The PivotSelector to use when sorting.
		 */
		private void SetSelector(IPivotSelector selector)
		{
			if (selector == null)
			{
				selector = new RandomElementSelector();
			}
			this.selector = selector;
		}

		/**
		 * Sorts entire array of the given data using quick sort.
		 * 
		 * @param data The array of data to sort.
		 */
	
		public override void Sort(E[] data)
		{
			QuickSort(data, 0, data.Length - 1);
		}

		/**
		 * Recursively sorts an array of data elements between the given indexes.
		 * 
		 * @param data The array of data to sort.
		 * @param low The lower bound of the array to sort.
		 * @param high The upper bound of the array to sort.
		 */
		private void QuickSort(E[] data, int low, int high)
		{
			if (low < high)
			{
				int pivotLocation = Partition(data, low, high);
				QuickSort(data, low, pivotLocation - 1);
				QuickSort(data, pivotLocation + 1, high);
			}
		}

		/**
		 * Partitions the given data array by selecting a pivot element and setting
		 * the index of the pivot element to the highest index in the array.
		 * 
		 * @param data The array of data to partition.
		 * @param low The lower bound of the array to partition.
		 * @param high The upper bound of the array to partition.
		 * 
		 * @return The index of the pivot element.
		 */
		private int Partition(E[] data, int low, int high)
		{
			// Select a Pivot element
			int pivotIndex = selector.SelectPivot(low, high);
			// Swap the pivot to be the last element in the array
			Swap(data, pivotIndex, high);

			return PartitionHelper(data, low, high);
		}

		/**
		 * Sorts elements in the data so that they are on the correct side of 
		 * the pivot and the pivot is in its final sorted location. 
		 * 
		 * @param data The array of data to partition.
		 * @param low The lower bound of the array to partition.
		 * @param high The upper bound of the array to partition.
		 * 
		 * @return The index of the pivot element.
		 */
		private int PartitionHelper(E[] data, int low, int high)
		{
			// The pivot will be in the last index location
			E pivot = data[high];
			int index = low; // index of smaller element
			for (int j = low; j < high; j++)
			{
				if (Compare(data[j], pivot) < 1)
				{
					if (index != j)
						Swap(data, index, j);
					index++;
				}
			}
			// swap the index with the pivot (T[high] is the pivot)
			if (index != high)
				Swap(data, index, high);

			// Return the index of the pivot element
			return index;
		}

		/**
		 * Swaps the elements at the given indexes in the given data array.
		 * 
		 * @param data The data array to swap.
		 * @param index1 The index of the first element to swap.
		 * @param index2 The index of the second element to swap.
		 */
		private void Swap(E[] data, int index1, int index2)
		{
			E temp = data[index1];
			data[index1] = data[index2];
			data[index2] = temp;
		}

		/**
		 * PivotSelector provides an interface for classes to provide an implementation
		 * of pivot selection functionality.
		 */
		public interface IPivotSelector
		{
			/**
			 * Returns the index of the selected pivot element
			 * @param low - the lowest index to consider
			 * @param high - the highest index to consider
			 * @return the index of the selected pivot element
			 */
			int SelectPivot(int low, int high);
		}

		/**
		 * FirstElementSelector implements the PivotSelector interface to 
		 * provide pivot selection functionality used in quick sorting.
		 */
		private class FirstElementSelector : IPivotSelector
		{

			/**
			 * Selects a pivot index at the beginning of the given index range.
			 * 
			 * @param low The low bound of the index range.
			 * @param high The high bound of the index range.
			 * 
			 * @return The selected pivot index.
			 */		
			public int SelectPivot(int low, int high)
			{
				return low;
			}

		}

		/**
		 * LastElementSelector implements the PivotSelector interface to 
		 * provide pivot selection functionality used in quick sorting.
		 */
		private class LastElementSelector : IPivotSelector
		{

			/**
			 * Selects a pivot index at the end of the given index range.
			 * 
			 * @param low The low bound of the index range.
			 * @param high The high bound of the index range.
			 * 
			 * @return The selected pivot index.
			 */	
			public int SelectPivot(int low, int high)
			{
				return high;
			}		
		}
	
		/**
			* MiddleElementSelector implements the PivotSelector interface to 
			* provide pivot selection functionality used in quick sorting.
			*/
		private class MiddleElementSelector : IPivotSelector
		{
			/**
			 * Selects a pivot index in the middle of the given index range.
			 * 
			 * @param low The low bound of the index range.
			 * @param high The high bound of the index range.
			 * 
			 * @return The selected pivot index.
			 */
	
			public int SelectPivot(int low, int high)
			{
				return (low + high) / 2;
			}
		
		}	
		/**
			* RandomElementSelector implements the PivotSelector interface to 
			* provide pivot random selection functionality used in quick sorting.
			*/
		private class RandomElementSelector : IPivotSelector
		{

			/**
			 * Randomly selects a pivot index between the given index range.
			 * 
			 * @param low The low bound of the index range.
			 * @param high The high bound of the index range.
			 * 
			 * @return The selected pivot index.
			 */
			public int SelectPivot(int low, int high)
			{
				Random random = new();
				return random.Next(high - low) + low;
			}		
		}
	}
}
