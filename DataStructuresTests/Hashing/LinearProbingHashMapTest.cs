using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures.Map;
using System;
using System.Collections.Generic;
using DataStructures.Hashing;

namespace TestProject2.Hashing
{
	/**
	* Test class for LinearProbingHashMap.
	* @author Zach Samuels
	*
	*/
	[TestClass]
	public class LinearProbingHashMapTest
	{

		private IMap<int, string> map;

		/**
		 * Creates a new LinearProbingHashMap for testing.
		 */
		[TestInitialize]
		public void SetUp()
		{
			// Use the "true" flag to indicate we are testing.
			// Remember that (when testing) alpha = 1, beta = 1, and prime = 7
			// based on our AbstractHashMap constructor.
			// That means you can draw the hash table by hand
			// if you use integer keys, since Integer.hashCode() = the integer value, itself
			// Finally, apply compression. For example:
			// for key = 1: h(1) = ( (1 * 1 + 1) % 7) % 7 = 2
			// for key = 2: h(2) = ( (1 * 2 + 1) % 7) % 7 = 3
			// for key = 3: h(3) = ( (1 * 3 + 1) % 7) % 7 = 4
			// for key = 4: h(4) = ( (1 * 4 + 1) % 7) % 7 = 5
			// for key = 5: h(5) = ( (1 * 5 + 1) % 7) % 7 = 6
			// for key = 6: h(6) = ( (1 * 6 + 1) % 7) % 7 = 0
			// for key = 7: h(7) = ( (1 * 7 + 1) % 7) % 7 = 1
			// for key = 8: h(8) = ( (1 * 8 + 1) % 7) % 7 = 2
			// for key = 9: h(9) = ( (1 * 9 + 1) % 7) % 7 = 3
			// for key = 10: h(10) = ( (1 * 10 + 1) % 7) % 7 = 4
			// etc.
			map = new LinearProbingHashMap<int, string>();
			map = new LinearProbingHashMap<int, string>(true);
			map = new LinearProbingHashMap<int, string>(7);
			map = new LinearProbingHashMap<int, string>(7, true);
		}

		/**
		 * Test method for Put().
		 */
		[TestMethod]
		public void TestPut()
		{
			Assert.IsTrue(map.IsEmpty());
			Assert.IsNull(map.Put(1, "string1"));
			Assert.AreEqual(1, map.Size());
			Assert.IsNull(map.Put(2, "string2"));
			Assert.AreEqual(2, map.Size());
			Assert.IsNull(map.Put(3, "string3"));
			Assert.AreEqual(3, map.Size());
			Assert.IsNull(map.Put(4, "string4"));
			Assert.AreEqual(4, map.Size());
			Assert.IsNull(map.Put(5, "string5"));
			Assert.AreEqual(5, map.Size());
			Assert.IsNull(map.Put(6, "string6"));
			Assert.AreEqual(6, map.Size());
			Assert.IsNull(map.Put(7, "string7"));
			Assert.AreEqual(7, map.Size());
			Assert.IsNull(map.Put(8, "string8"));
			Assert.AreEqual(8, map.Size());
			Assert.IsNull(map.Put(9, "string9"));
			Assert.AreEqual(9, map.Size());
			Assert.IsNull(map.Put(10, "string10"));
			Assert.AreEqual(10, map.Size());
			Assert.IsFalse(map.IsEmpty());

			// Since our entrySet method returns the entries in the table
			// from left to right, we can use the entrySet to check
			// that our values are in the correct order in the hash table.
			// Alternatively, you could implement a tostring() method if you
			// want to check that the exact index of each entry is correct
			IEnumerator<IMap<int, string>.IEntry> it = map.EntryIterator().GetEnumerator();
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(6, (int)it.Current.GetKey()); // should be index 0
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(7, (int)it.Current.GetKey()); // should be index 1
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(1, (int)it.Current.GetKey()); // should be index 2
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(2, (int)it.Current.GetKey()); // should be index 3
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(3, (int)it.Current.GetKey()); // should be index 4
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(4, (int)it.Current.GetKey()); // should be index 5
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(5, (int)it.Current.GetKey()); // should be index 6
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(8, (int)it.Current.GetKey()); // should be index 7
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(9, (int)it.Current.GetKey()); // should be index 8
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(10, (int)it.Current.GetKey()); // should be index 9

			// Test overwriting keys
			Assert.AreEqual("string4", map.Put(4, "newstring4"));
			Assert.AreEqual(10, map.Size());
		}

		/**
		 * Test method for GetValue().
		 */
		[TestMethod]
		public void TestGetValue()
		{
			Assert.IsTrue(map.IsEmpty());
			Assert.IsNull(map.Put(3, "string3"));
			Assert.IsNull(map.Put(5, "string5"));
			Assert.IsNull(map.Put(2, "string2"));
			Assert.IsNull(map.Put(4, "string4"));
			Assert.IsNull(map.Put(1, "string1"));
			Assert.IsFalse(map.IsEmpty());

			Assert.AreEqual("string1", map.GetValue(1));
			Assert.AreEqual("string2", map.GetValue(2));
			Assert.AreEqual("string3", map.GetValue(3));
			Assert.AreEqual("string4", map.GetValue(4));
			Assert.AreEqual("string5", map.GetValue(5));

			// Test getting non-existent key
			Assert.IsNull(map.GetValue(7));
		}

		/**
		 * Test method for Remove().
		 */
		[TestMethod]
		public void TestRemove()
		{
			Assert.IsTrue(map.IsEmpty());
			Assert.IsNull(map.Put(1, "string1"));
			Assert.AreEqual(1, map.Size());
			Assert.IsNull(map.Put(2, "string2"));
			Assert.AreEqual(2, map.Size());
			Assert.IsNull(map.Put(3, "string3"));
			Assert.AreEqual(3, map.Size());
			Assert.IsNull(map.Put(4, "string4"));
			Assert.AreEqual(4, map.Size());
			Assert.IsNull(map.Put(5, "string5"));
			Assert.AreEqual(5, map.Size());
			Assert.IsNull(map.Put(6, "string6"));
			Assert.AreEqual(6, map.Size());
			Assert.IsNull(map.Put(7, "string7"));
			Assert.AreEqual(7, map.Size());
			Assert.IsNull(map.Put(8, "string8"));
			Assert.AreEqual(8, map.Size());
			Assert.IsNull(map.Put(9, "string9"));
			Assert.AreEqual(9, map.Size());
			Assert.IsNull(map.Put(10, "string10"));
			Assert.AreEqual(10, map.Size());
			Assert.IsFalse(map.IsEmpty());

			// Test removing non-existent key
			Assert.IsNull(map.Remove(101));
			Assert.AreEqual(10, map.Size());

			// Test removing and inserting same key
			Assert.AreEqual("string6", map.Remove(6));
			Assert.IsNull(map.Put(6, "string6"));
			Assert.AreEqual(10, map.Size());

			// Should overwrite old key index
			IEnumerator<IMap<int, string>.IEntry> it = map.EntryIterator().GetEnumerator();
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(6, (int)it.Current.GetKey()); // should be index 0

			Assert.AreEqual("string8", map.Remove(8));
			Assert.AreEqual(9, map.Size());
			Assert.AreEqual("string1", map.Remove(1));
			Assert.AreEqual(8, map.Size());
			Assert.AreEqual("string10", map.Remove(10));
			Assert.AreEqual(7, map.Size());
			Assert.AreEqual("string9", map.Remove(9));
			Assert.AreEqual(6, map.Size());
			Assert.AreEqual("string7", map.Remove(7));
			Assert.AreEqual(5, map.Size());
			Assert.AreEqual("string2", map.Remove(2));
			Assert.AreEqual(4, map.Size());
			Assert.AreEqual("string3", map.Remove(3));
			Assert.AreEqual(3, map.Size());
			Assert.AreEqual("string4", map.Remove(4));
			Assert.AreEqual(2, map.Size());
			Assert.AreEqual("string5", map.Remove(5));
			Assert.AreEqual(1, map.Size());
			Assert.AreEqual("string6", map.Remove(6));

			Assert.IsTrue(map.IsEmpty());
			Assert.IsNull(map.Put(1, "string1"));
			Assert.AreEqual(1, map.Size());
			Assert.IsNull(map.Put(2, "string2"));
			Assert.AreEqual(2, map.Size());
			Assert.IsNull(map.Put(3, "string3"));
			Assert.AreEqual(3, map.Size());
			Assert.IsNull(map.Put(4, "string4"));
			Assert.AreEqual(4, map.Size());
			Assert.IsNull(map.Put(5, "string5"));
			Assert.AreEqual(5, map.Size());
			Assert.IsNull(map.Put(6, "string6"));
			Assert.AreEqual(6, map.Size());
			Assert.IsNull(map.Put(7, "string7"));
			Assert.AreEqual(7, map.Size());
			Assert.IsNull(map.Put(8, "string8"));
			Assert.AreEqual(8, map.Size());
			Assert.IsNull(map.Put(9, "string9"));
			Assert.AreEqual(9, map.Size());
			Assert.IsNull(map.Put(10, "string10"));
			Assert.AreEqual(10, map.Size());
			Assert.IsFalse(map.IsEmpty());

			it = map.EntryIterator().GetEnumerator();
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(6, (int)it.Current.GetKey()); // should be index 0
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(7, (int)it.Current.GetKey()); // should be index 1
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(1, (int)it.Current.GetKey()); // should be index 2
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(2, (int)it.Current.GetKey()); // should be index 3
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(3, (int)it.Current.GetKey()); // should be index 4
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(4, (int)it.Current.GetKey()); // should be index 5
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(5, (int)it.Current.GetKey()); // should be index 6
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(8, (int)it.Current.GetKey()); // should be index 7
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(9, (int)it.Current.GetKey()); // should be index 8
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(10, (int)it.Current.GetKey()); // should be index 9
		}

		/**
		 * Test method for GetEnumerator().
		 */
		[TestMethod]
		public void GetEnumerator()
		{
			Assert.IsNull(map.Put(3, "string3"));
			Assert.IsNull(map.Put(5, "string5"));
			Assert.IsNull(map.Put(2, "string2"));
			Assert.IsNull(map.Put(4, "string4"));
			Assert.IsNull(map.Put(1, "string1"));
			Assert.IsNull(map.Put(6, "string6"));

			IEnumerator<int> it = map.GetEnumerator();
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(6, (int)it.Current);
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(1, (int)it.Current);
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(2, (int)it.Current);
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(3, (int)it.Current);
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(4, (int)it.Current);
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(5, (int)it.Current);
		}

		/**
		 * Test method for EntryIterator().
		 */
		[TestMethod]
		public void TestEntryIterator()
		{
			Assert.IsNull(map.Put(3, "string3"));
			Assert.IsNull(map.Put(5, "string5"));
			Assert.IsNull(map.Put(2, "string2"));
			Assert.IsNull(map.Put(4, "string4"));
			Assert.IsNull(map.Put(1, "string1"));
			Assert.IsNull(map.Put(6, "string6"));

			IEnumerator<IMap<int, string>.IEntry> it = map.EntryIterator().GetEnumerator();
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(6, (int)it.Current.GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(1, (int)it.Current.GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(2, (int)it.Current.GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(3, (int)it.Current.GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(4, (int)it.Current.GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(5, (int)it.Current.GetKey());
		}

		/**
		 * Test method for ValueIterator().
		 */
		[TestMethod]
		public void TestValueIterator()
		{
			Assert.IsNull(map.Put(3, "string3"));
			Assert.IsNull(map.Put(5, "string5"));
			Assert.IsNull(map.Put(2, "string2"));
			Assert.IsNull(map.Put(4, "string4"));
			Assert.IsNull(map.Put(1, "string1"));
			Assert.IsNull(map.Put(6, "string6"));

			IEnumerator<string> it = map.ValueIterator();
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual("string6", it.Current);
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
		}
	}
}
