using DataStructures.Data;
using DataStructures.Sorter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject2.Sorter
{
	/**
	 * Test class for RadixSorter.
	*
	* @author Zach Samuels
	*/
	[TestClass]
	public class RadixSorterTest
	{

		private Student sOne;
		private Student sTwo;
		private Student sThree;
		private Student sFour;
		private Student sFive;

		private RadixSorter<Student> sorter;

		/**
		 * Set up Student and sorter objects for testing.
		 */
		[TestInitialize]
		public void SetUp()
		{
			sOne = new Student("OneFirst", "OneLast", 1, 1, 1.0, "oneUnityID");
			sTwo = new Student("TwoFirst", "TwoLast", 2, 2, 2.0, "twoUnityID");
			sThree = new Student("ThreeFirst", "ThreeLast", 3, 3, 3.0, "threeUnityID");
			sFour = new Student("FourFirst", "FourLast", 4, 4, 4.0, "fourUnityID");
			sFive = new Student("FiveFirst", "FiveLast", 5, 5, 5.0, "fiveUnityID");

			sorter = new RadixSorter<Student>();
		}

		/**
		 * Test sorting arrays of Students.
		 */
		[TestMethod]
		public void TestSortStudent()
		{
			Student[] original = { sTwo, sOne, sFour, sThree, sFive };
			sorter.Sort(original);
			Assert.AreEqual(sOne, original[0]);
			Assert.AreEqual(sTwo, original[1]);
			Assert.AreEqual(sThree, original[2]);
			Assert.AreEqual(sFour, original[3]);
			Assert.AreEqual(sFive, original[4]);
		}
	}

}
