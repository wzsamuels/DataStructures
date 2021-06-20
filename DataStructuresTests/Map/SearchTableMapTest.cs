using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures.Map;
using DataStructures.Data;
using System.Collections.Generic;

namespace TestProject2.Map
{
	/**
	* Test class for SearchTableMAp.
	* 
	* @author Zach Samuels
	*/
	[TestClass]
    public class SearchTableMapTest
    {
		private IMap<int, string> map;
		private IMap<Student, int> studentMap;

		/**
		 * Creates new Map objects for testing.
		 */
		[TestInitialize]
		public void SetUp()
		{
			map = new SearchTableMap<int, string>();
			studentMap = new SearchTableMap<Student, int>();
		}

		/**
		 * Test method for Put().
		 */
		[TestMethod]
		public void TestPut()
		{
			Assert.AreEqual(0, map.Size());
			Assert.IsTrue(map.IsEmpty());
			Assert.IsNull(map.Put(3, "string3"));
			Assert.AreEqual("SearchTableMap`2[3]", map.ToString());
			Assert.AreEqual(1, map.Size());
			Assert.IsFalse(map.IsEmpty());

			Assert.IsNull(map.Put(5, "string5"));
			Assert.AreEqual("SearchTableMap`2[3, 5]", map.ToString());
			Assert.AreEqual(2, map.Size());

			Assert.IsNull(map.Put(1, "string1"));
			Assert.AreEqual("SearchTableMap`2[1, 3, 5]", map.ToString());
			Assert.AreEqual(3, map.Size());

			Assert.AreEqual("string3", map.Put(3, "newstring3"));
			Assert.AreEqual("newstring3", map.GetValue(3));
			Assert.AreEqual("SearchTableMap`2[1, 3, 5]", map.ToString());
			Assert.AreEqual(3, map.Size());
		}

		/**
		 * Test method for get().
		 */
		public void TestGet()
		{
			Assert.IsTrue(map.IsEmpty());
			Assert.IsNull(map.Put(3, "string3"));
			Assert.IsNull(map.Put(5, "string5"));
			Assert.IsNull(map.Put(2, "string2"));
			Assert.IsNull(map.Put(4, "string4"));
			Assert.IsNull(map.Put(1, "string1"));
			Assert.IsFalse(map.IsEmpty());
			Assert.AreEqual("SearchTableMap`2[1, 2, 3, 4, 5]", map.ToString());

			Assert.AreEqual("string1", map.GetValue(1));
			Assert.AreEqual("SearchTableMap`2[1, 2, 3, 4, 5]", map.ToString());
			Assert.AreEqual("string5", map.GetValue(5));
			Assert.AreEqual("SearchTableMap`2[1, 2, 3, 4, 5]", map.ToString());

			Assert.IsNull(map.GetValue(6));
		}

		/**
		 * Test method for Remove().
		 */
		[TestMethod]
		public void TestRemove()
		{
			Assert.IsTrue(map.IsEmpty());
			Assert.IsNull(map.Put(3, "string3"));
			Assert.IsNull(map.Put(5, "string5"));
			Assert.IsNull(map.Put(2, "string2"));
			Assert.IsNull(map.Put(4, "string4"));
			Assert.IsNull(map.Put(1, "string1"));
			Assert.IsFalse(map.IsEmpty());
			Assert.AreEqual("SearchTableMap`2[1, 2, 3, 4, 5]", map.ToString());

			Assert.IsNull(map.Remove(6));
			Assert.AreEqual(5, map.Size());
			Assert.IsFalse(map.IsEmpty());
			Assert.AreEqual("SearchTableMap`2[1, 2, 3, 4, 5]", map.ToString());

			Assert.AreEqual("string3", map.Remove(3));
			Assert.AreEqual(4, map.Size());
			Assert.AreEqual("SearchTableMap`2[1, 2, 4, 5]", map.ToString());
		}

		/**
		 * Test method for a Map of Student objects.
		 */	
		[TestMethod]
		public void TestStudentMap()
		{
			Student s1 = new("J", "K", 1, 0, 0, "jk");
			Student s2 = new("J", "S", 2, 0, 0, "js");
			Student s3 = new("S", "H", 3, 0, 0, "sh");
			Student s4 = new("J", "J", 4, 0, 0, "jj");
			Student s5 = new("L", "B", 5, 0, 0, "lb");

			studentMap.Put(s1, 100);
			Assert.AreEqual(1, studentMap.Size());

			studentMap.Put(s2, 200);
			studentMap.Put(s3, 300);
			studentMap.Put(s4, 400);
			studentMap.Put(s5, 500);

			// Test that keys are in sorted order
			IEnumerator<int> it = studentMap.ValueIterator();
			
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(500, it.Current);
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(300, it.Current);
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(400, it.Current);
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(100, it.Current);
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(200, it.Current);			
		}

		/**
		 * Test method for iterator().
		 */
		[TestMethod]
		public void TestIterator()
		{
			Assert.IsNull(map.Put(3, "string3"));
			Assert.IsNull(map.Put(5, "string5"));
			Assert.IsNull(map.Put(2, "string2"));
			Assert.IsNull(map.Put(4, "string4"));
			Assert.IsNull(map.Put(1, "string1"));

			// Test that keys are in sorted order
			IEnumerator<int> it = map.GetEnumerator();
			
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(1, it.Current);
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(2, it.Current);
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(3, it.Current);
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(4, it.Current);
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(5, it.Current);

			Assert.IsFalse(it.MoveNext());
			
		}

		/**
		 * Test method for entrySet().
		 */
		[TestMethod]
		public void testEntrySet()
		{
			Assert.IsNull(map.Put(3, "string3"));
			Assert.IsNull(map.Put(5, "string5"));
			Assert.IsNull(map.Put(2, "string2"));
			Assert.IsNull(map.Put(4, "string4"));
			Assert.IsNull(map.Put(1, "string1"));

			IEnumerator<IMap<int, string>.IEntry> it = map.EntryIterator().GetEnumerator();

			Assert.IsTrue(it.MoveNext());
			IMap<int, string>.IEntry entry = it.Current;
			
			Assert.AreEqual(1, (int)(entry.GetKey()));
			Assert.AreEqual("string1", (string)(entry.GetValue()));

			Assert.IsTrue(it.MoveNext());
			entry = it.Current;
			Assert.AreEqual(2, (int)(entry.GetKey()));
			Assert.AreEqual("string2", (string)(entry.GetValue()));

			Assert.IsTrue(it.MoveNext());
			entry = it.Current;
			Assert.AreEqual(3, (int)(entry.GetKey()));
			Assert.AreEqual("string3", (string)(entry.GetValue()));

			Assert.IsTrue(it.MoveNext());
			entry = it.Current;
			Assert.AreEqual(4, (int)(entry.GetKey()));
			Assert.AreEqual("string4", (string)(entry.GetValue()));

			Assert.IsTrue(it.MoveNext());
			entry = it.Current;
			Assert.AreEqual(5, (int)(entry.GetKey()));
			Assert.AreEqual("string5", (string)(entry.GetValue()));
			
		}

		/**
		 * Test method for values().
		 */
		[TestMethod]
		public void TestValues()
		{
			Assert.IsNull(map.Put(3, "string3"));
			Assert.IsNull(map.Put(5, "string5"));
			Assert.IsNull(map.Put(2, "string2"));
			Assert.IsNull(map.Put(4, "string4"));
			Assert.IsNull(map.Put(1, "string1"));

			IEnumerator<string> it = map.ValueIterator();
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual("string1", it.Current);
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual("string2", it.Current);
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual("string3", it.Current);
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual("string4", it.Current);
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual("string5", it.Current);

			Assert.IsFalse(it.MoveNext());
		}
	}
}
