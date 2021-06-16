using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures.Map;
using System;
using System.Collections.Generic;

namespace TestProject2.Map
{
    /**
    * Test class for UnorderedLinkedMap.
    * 
    * @author Zach Samuels
    */
    [TestClass]
    public class UnorderedLinkedMapTest
    {
		private IMap<int, string> map;

		/**
		 * Creates a new Map for testing.
		 */
		[TestInitialize]
		public void SetUp()
		{
			map = new UnorderedLinkedMap<int, string>();
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
			Assert.AreEqual("UnorderedLinkedMap`2[3]", map.ToString());
			Assert.AreEqual(1, map.Size());
			Assert.IsNull(map.Put(5, "string5"));
			Assert.AreEqual("UnorderedLinkedMap`2[5, 3]", map.ToString());
			Assert.AreEqual(2, map.Size());

			Assert.AreEqual("string3", map.Put(3, "newstring3"));
			Assert.AreEqual("newstring3", map.GetValue(3));
			Assert.AreEqual("UnorderedLinkedMap`2[3, 5]", map.ToString());
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
			Assert.AreEqual("UnorderedLinkedMap`2[1, 4, 2, 5, 3]", map.ToString());

			Assert.AreEqual("string1", map.GetValue(1));
			Assert.AreEqual("UnorderedLinkedMap`2[1, 4, 2, 5, 3]", map.ToString());
			Assert.AreEqual("string3", map.GetValue(3));
			Assert.AreEqual("UnorderedLinkedMap`2[3, 1, 4, 2, 5]", map.ToString());
			Assert.AreEqual("string1", map.GetValue(1));
			Assert.AreEqual("UnorderedLinkedMap`2[1, 3, 4, 2, 5]", map.ToString());

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
			Assert.AreEqual("UnorderedLinkedMap`2[1, 4, 2, 5, 3]", map.ToString());

			Assert.IsNull(map.Remove(0));
			Assert.AreEqual(5, map.Size());
			Assert.IsFalse(map.IsEmpty());
			Assert.AreEqual("UnorderedLinkedMap`2[1, 4, 2, 5, 3]", map.ToString());

			Assert.AreEqual("string2", map.Remove(2));
			Assert.AreEqual(4, map.Size());
			Assert.AreEqual("UnorderedLinkedMap`2[1, 4, 5, 3]", map.ToString());
		}

		/**
		 * Test method for GetEnumerator().
		 */
		[TestMethod]
		public void TestIterator()
		{
			Assert.IsNull(map.Put(3, "string3"));
			Assert.IsNull(map.Put(5, "string5"));
			Assert.IsNull(map.Put(2, "string2"));
			Assert.IsNull(map.Put(4, "string4"));
			Assert.IsNull(map.Put(1, "string1"));

			IEnumerator<int> it = map.GetEnumerator();
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(1, (int)it.Current);
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(4, (int)it.Current);
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(2, (int)it.Current);
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(5, (int)it.Current);
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(3, (int)it.Current);
		}

		/**
		 * Test method for entrySet().
		 */
		[TestMethod]
		public void TestEntrySet()
		{
			Assert.IsNull(map.Put(3, "string3"));
			Assert.IsNull(map.Put(5, "string5"));
			Assert.IsNull(map.Put(2, "string2"));
			Assert.IsNull(map.Put(4, "string4"));
			Assert.IsNull(map.Put(1, "string1"));

			IEnumerator<IMap<int, string>.IEntry> it = map.EntryIterator().GetEnumerator();
			Assert.IsTrue(it.MoveNext());
			IMap<int, string>.IEntry entry = it.Current;
			Assert.AreEqual(1, entry.GetKey());
			Assert.AreEqual("string1", (String)(entry.GetValue()));

			Assert.IsTrue(it.MoveNext());
			entry = it.Current;
			Assert.AreEqual(4, (int)(entry.GetKey()));
			Assert.AreEqual("string4", (String)(entry.GetValue()));

			Assert.IsTrue(it.MoveNext());
			entry = it.Current;
			Assert.AreEqual(2, (int)(entry.GetKey()));
			Assert.AreEqual("string2", (String)(entry.GetValue()));

			Assert.IsTrue(it.MoveNext());
			entry = it.Current;
			Assert.AreEqual(5, (int)(entry.GetKey()));
			Assert.AreEqual("string5", (String)(entry.GetValue()));

			Assert.IsTrue(it.MoveNext());
			entry = it.Current;
			Assert.AreEqual(3, (int)(entry.GetKey()));
			Assert.AreEqual("string3", (String)(entry.GetValue()));
		}

		/**
		 * Test method for ValueIterator().
		 */
		[TestMethod]
		public void TestValues()
		{
			Assert.IsNull(map.Put(3, "string3"));
			Assert.IsNull(map.Put(5, "string5"));
			Assert.IsNull(map.Put(2, "string2"));
			Assert.IsNull(map.Put(4, "string4"));
			Assert.IsNull(map.Put(1, "string1"));

			IEnumerator<String> it = map.ValueIterator();
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual("string1", it.Current);
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual("string4", it.Current);
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual("string2", it.Current);
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual("string5", it.Current);
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual("string3", it.Current);		
		}
	}
}
