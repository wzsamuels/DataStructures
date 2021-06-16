using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Data
{
    /**
     * Comparator for comparing Students based on GPA
    * @author Zach Samuels
    */
    public class StudentGPAComparator : IComparer<Student>
    {
		/**
		 * Compares students based on GPA in descending order
		 * 
		 * @param one The first Student to compare.
		 * @param two The second Student to compare.
		 * 
		 * @return -1 if Student one is ordered before two, 1 if two is ordered
		 * before one, or 0 if the Students are equal.
		 */
		public int Compare(Student one, Student two)
		{
			if (one.GetGpa() < two.GetGpa())
				return 1;
			else if (one.GetGpa() > two.GetGpa())
				return -1;
			return 0;
		}
    }
}
