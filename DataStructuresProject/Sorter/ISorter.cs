using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Sorter
{

	/**
	 * Interface that defines the sorting behavior
	 *
	 * @author Zach Samuels
	 *
	 * @param<E> The generic type of object to sort.
	 */
	public interface ISorter<E>
	{
		/**
		 * Method to sort an array of generic objects.
		 * 
		 * @param data The array of objects to sort.
		 */
		void Sort(E[] data);
	}

}
