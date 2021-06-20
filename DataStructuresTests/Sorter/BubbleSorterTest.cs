using DataStructures.Data;
using DataStructures.Sorter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject2.Sorter
{
	/**
	* Test class for BubbleSorter.
	*
	* @author Zach Samuels
	*/
	[TestClass]
	public class BubbleSorterTest
	{
		private readonly int[] dataAscending = { 1, 2, 3, 4, 5 };
		private readonly int[] dataDescending = { 5, 4, 3, 2, 1 };
		private readonly int[] dataRandom = { 4, 1, 5, 3, 2 };

		private readonly Student[] studentAscending = {
			new Student("Amy", "AmyLast", 1, 1, 1.0, "AUnityID"),
			new Student("Bob", "BobLast", 2, 2, 2.0, "BUnityID"),
			new Student("Carol", "CarolLast", 3, 3, 3.0, "CUnityID"),
			new Student("Dave", "DaveLast", 4, 4, 4.0, "DunityID") };

		private readonly Student[] studentDescending = {
			new Student("Dave", "DaveLast", 4, 4, 4.0, "DunityID"),
			new Student("Carol", "CarolLast", 3, 3, 3.0, "CUnityID"),
			new Student("Bob", "BobLast", 2, 2, 2.0, "BUnityID"),
			new Student("Amy", "AmyLast", 1, 1, 1.0, "AUnityID") };

		private BubbleSorter<int> intSorter;
		private BubbleSorter<Student> studentSorter;

		/**
		 * Initialize the sorter objects for testing.
		 */
		[TestInitialize]
		public void SetUp()
		{
			intSorter = new BubbleSorter<int>();
			studentSorter = new BubbleSorter<Student>();
		}

		/**
		 * Test sorting on int arrays.
		 */
		[TestMethod]
		public void TestSortints()
		{
			intSorter.Sort(dataAscending);
			Assert.AreEqual((int)1, dataAscending[0]);
			Assert.AreEqual((int)2, dataAscending[1]);
			Assert.AreEqual((int)3, dataAscending[2]);
			Assert.AreEqual((int)4, dataAscending[3]);
			Assert.AreEqual((int)5, dataAscending[4]);

			intSorter.Sort(dataDescending);
			Assert.AreEqual((int)1, dataDescending[0]);
			Assert.AreEqual((int)2, dataDescending[1]);
			Assert.AreEqual((int)3, dataDescending[2]);
			Assert.AreEqual((int)4, dataDescending[3]);
			Assert.AreEqual((int)5, dataDescending[4]);

			intSorter.Sort(dataRandom);
			Assert.AreEqual((int)1, dataRandom[0]);
			Assert.AreEqual((int)2, dataRandom[1]);
			Assert.AreEqual((int)3, dataRandom[2]);
			Assert.AreEqual((int)4, dataRandom[3]);
			Assert.AreEqual((int)5, dataRandom[4]);
		}

		/**
		 * Test sorting arrays of Students.
		 */
		[TestMethod]
		public void TestSortStudent()
		{
			studentSorter.Sort(studentAscending);
			Assert.AreEqual("Amy", studentAscending[0].GetFirst());
			Assert.AreEqual("Bob", studentAscending[1].GetFirst());
			Assert.AreEqual("Carol", studentAscending[2].GetFirst());
			Assert.AreEqual("Dave", studentAscending[3].GetFirst());

			studentSorter.Sort(studentDescending);
			Assert.AreEqual("Amy", studentAscending[0].GetFirst());
			Assert.AreEqual("Bob", studentAscending[1].GetFirst());
			Assert.AreEqual("Carol", studentAscending[2].GetFirst());
			Assert.AreEqual("Dave", studentAscending[3].GetFirst());
		}
	}

}
