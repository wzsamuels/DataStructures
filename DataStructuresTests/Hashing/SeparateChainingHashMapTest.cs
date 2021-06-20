using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures.Map;
using System;
using System.Collections.Generic;
using DataStructures.Hashing;

namespace TestProject2.Hashing
{
	/**
	* Test class for SeparateChainingHashMap.
	* @author Zach Samuels
	*/
	[TestClass]
	public class SeparateChainingHashMapTest
	{

		private IMap<int, string> map;

		/**
		 * Creates a new SeparateChainingHashMap for testing.
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
			// etc.
			// Remember that our secondary map (an AVL tree) is a search
			// tree, which means the entries should be sorted in order within
			// that tree
			map = new SeparateChainingHashMap<int, string>();
			map = new SeparateChainingHashMap<int, string>(true);
			map = new SeparateChainingHashMap<int, string>(7);
			map = new SeparateChainingHashMap<int, string>(7, true);
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

			// Since our entrySet method returns the entries in the table
			// from left to right, we can use the entrySet to check
			// that our values are in the correct order in the hash table.
			// Alternatively, you could implement a tostring() method if you
			// want to check that the exact index/map of each bucket is correct
			IEnumerator<IMap<int, string>.IEntry> it = map.EntryIterator().GetEnumerator();
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(3, (int)it.Current.GetKey()); // should be in a map in index 4


			Assert.IsNull(map.Put(4, "string4"));
			Assert.AreEqual(2, map.Size());
			Assert.IsFalse(map.IsEmpty());
			it = map.EntryIterator().GetEnumerator();
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(3, (int)it.Current.GetKey()); // should be in a map in index 4
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(4, (int)it.Current.GetKey()); // should be in a map in index 5

			// You should create some collisions to check that entries
			// are placed in the correct buckets

			// Test overwriting keys
			Assert.AreEqual("string4", map.Put(4, "newstring4"));
			Assert.AreEqual(2, map.Size());

			it = map.EntryIterator().GetEnumerator();
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(3, (int)it.Current.GetKey()); // should be index 4
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(4, (int)it.Current.GetKey()); // should be index 5
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
			Assert.IsNull(map.Put(3, "string3"));
			Assert.IsNull(map.Put(5, "string5"));
			Assert.IsNull(map.Put(2, "string2"));
			Assert.IsNull(map.Put(4, "string4"));
			Assert.IsNull(map.Put(1, "string1"));
			Assert.IsFalse(map.IsEmpty());

			// Test removing non-existent key
			Assert.IsNull(map.Remove(7));

			// Test removing keys
			Assert.AreEqual("string1", map.Remove(1));
			Assert.AreEqual("string2", map.Remove(2));
			Assert.AreEqual("string3", map.Remove(3));
			Assert.AreEqual("string4", map.Remove(4));
			Assert.AreEqual("string5", map.Remove(5));
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
		public void EntryIterator()
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
