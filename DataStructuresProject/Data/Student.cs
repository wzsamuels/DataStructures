using System;

namespace DataStructures.Data
{
    /**
	* A student is comparable and identifiable.
	* Students have a first name, last name, id number, 
	* number of credit hours, gpa, and unityID.
	* 
	* @author Dr. King
	* @author Zach Samuels
	*
	*/
    public class Student : IComparable<Student>, IIdentifiable
    {
		/** Student's first name */
		private String first;
		/** Student's last name */
		private String last;
		/** Student's numerical id */
		private int id;
		/** Student's credit hours */
		private int creditHours;
		/** Student's credit hours */
		private double gpa;
		/** Student's String unity ID */
		private String unityID;

		/**
		 * Constructor for Student
		 * 
		 * @param first Student's first name
		 * @param last Student's last name
		 * @param id Student's id
		 * @param creditHours Student's credit hours
		 * @param gpa Student's gpa
		 * @param unityID Student's unity ID
		 */
		public Student(String first, String last, int id, int creditHours, double gpa, String unityID)
		{
			this.first = first;
			this.last = last;
			this.id = id;
			this.creditHours = creditHours;
			this.gpa = gpa;
			this.unityID = unityID;
		}

		/**
		 * Getter for Student's first name.
		 * 
		 * @return The Student's first name.
		 */
		public String GetFirst()
		{
			return first;
		}

		/**
		 * Setter for the Student's first name.
		 * 
		 * @param first String to set the Student's first name to.
		 */
		public void SetFirst(String first)
		{
			this.first = first;
		}

		/**
		 * Getter for the Student's last name.
		 * 
		 * @return The Student's last name.
		 */
		public String GetLast()
		{
			return last;
		}

		/**
		 * Setter for the Student's last name.
		 * 
		 * @param last String to set the Student's last name to.
		 */
		public void SetLast(String last)
		{
			this.last = last;
		}

		/**
		 * Getter for the Student's ID.
		 * 
		 * @return The Student's ID.
		 */
		public int GetId()
		{
			return id;
		}

		/**
		 * Setter for the Student's ID.
		 * 
		 * @param id Integer to set the Student's ID.
		 */
		public void SetId(int id)
		{
			this.id = id;
		}

		/**
		 * Getter for the Student's credit hours.
		 * 
		 * @return The Student's credit hours.
		 */
		public int GetCreditHours()
		{
			return creditHours;
		}

		/**
		 * Setter for the Student's credit hours.
		 * 
		 * @param creditHours Integer to set the Student's credit hours.
		 */
		public void SetCreditHours(int creditHours)
		{
			this.creditHours = creditHours;
		}

		/**
		 * Getter for Student's GPA.
		 * 
		 * @return The Student's GPA.
		 */
		public double GetGpa()
		{
			return gpa;
		}

		/**
		 * Setter for Student's GPA.
		 * 
		 * @param gpa Double to set the Student's GPA.
		 */
		public void SetGpa(double gpa)
		{
			this.gpa = gpa;
		}

		/**
		 * Getter for Student's unity ID.
		 * 
		 * @return The Student's unity ID.
		 */
		public String GetUnityID()
		{
			return unityID;
		}

		/**
		 * Setter for Student's unity ID. 
		 * 
		 * @param unityID String to set the Student's unity ID.
		 */
		public void SetUnityID(String unityID)
		{
			this.unityID = unityID;
		}

		/**
		 * Generates a unique hash code for this Student object.
		 * 
		 * @return The generated hash code.
		 */
        public override int GetHashCode()
        {
            return HashCode.Combine(first, last, id);
        }

		/**
		 * Compares this Student object to another for equality.
		 * 
		 * @return True if the objects are equal or false if they aren't equal.
		 */
		 public override bool Equals(object obj)
        {
            return obj is Student student &&
                   first == student.first &&
                   last == student.last &&
                   id == student.id;
        }

		/**
		 * Compares this Student to another student for ordering based on:
		 * last name, then first name, then student id.
		 * 
		 * @param o The other student to compare.
		 * @return -1 if this Student is ordered first, 1 if ordered after, or 0 if they are the same Student.
		 */
		public int CompareTo(Student o)
		{
			if (last.CompareTo(o.last) < 0)
				return -1;
			else if (last.CompareTo(o.last) > 0)
				return 1;
			else
			{
				if (first.CompareTo(o.first) < 0)
					return -1;
				else if (first.CompareTo(o.first) > 0)
					return 1;
				else
				{
					if (id < o.id)
						return -1;
					else if (id > o.id)
						return 1;
					else
						return 0;
				}
			}
		}

        /**
		 * Displays a Student object as a string.
		 * 
		 * @return The Student's data as a string.
		 */
		public override String ToString()
		{
			return "Student [first=" + first + ", last=" + last + ", id=" + id + ", creditHours=" + creditHours + ", gpa="
					+ gpa + ", unityID=" + unityID + "]";
		}      
    }
}
