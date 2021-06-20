namespace DataStructures.Data
{
    /**
 * Comparator to compare students based on id number
 * @author Dr. King
 * @author Zach Samuels
 */
    class StudentIDComparator
    {
		/**
	 * Compares students based on id number in ascending order
	 * 
	 * @param one The first Student to compare.
	 * @param two The second Student to compare.
	 * 
	 * @return -1 if Student one is ordered before two, 1 if two is ordered
	 * before one, or 0 if the Students are equal.
	 */
		public static int Compare(Student one, Student two)
		{
			if (one.GetId() < two.GetId())
				return -1;
			else if (one.GetId() > two.GetId())
				return 1;
			return 0;
		}
	}
}
