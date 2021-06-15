using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures.Map;
using System;
using System.Collections.Generic;

namespace TestProject2.Map
{
    /**
     * Test class for UnorderedArrayMap
     * 
     * @author Zach Samuels
     */
	[TestClass]
    public class UnorderedArrayMapTest
    {
		private IMap<int, string> map;

		/**
		 * Creates a new Map for testing.
		 */
		[TestInitialize]
		public void SetUp()
		{
			map = new UnorderedArrayMap<int, string>();
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
			Assert.AreEqual("UnorderedArrayMap`2[3]", map.ToString());
			Assert.AreEqual(1, map.Size());

			Assert.IsNull(map.Put(4, "string4"));
			Assert.AreEqual("UnorderedArrayMap`2[4, 3]", map.ToString());
			Assert.AreEqual(2, map.Size());

			Assert.AreEqual("string3", map.Put(3, "newstring3"));
			Assert.AreEqual("UnorderedArrayMap`2[3, 4]", map.ToString());
			Assert.AreEqual(2, map.Size());
			
		}

		/**
		 * Test method for get().
		 */
		[TestMethod]
		public void TestGet()
		{
			Assert.IsTrue(map.IsEmpty());
			Assert.IsNull(map.Put(3, "string3"));
			Assert.IsNull(map.Put(5, "string5"));
			Assert.IsNull(map.Put(2, "string2"));
			Assert.IsNull(map.Put(4, "string4"));
			Assert.IsNull(map.Put(1, "string1"));
			Assert.IsFalse(map.IsEmpty());
			Assert.AreEqual("UnorderedArrayMap`2[1, 4, 2, 5, 3]", map.ToString());

			Assert.AreEqual("string1", map.GetValue(1));
			Assert.AreEqual("UnorderedArrayMap`2[1, 4, 2, 5, 3]", map.ToString());

			Assert.AreEqual("string3", map.GetValue(3));
			Assert.AreEqual("UnorderedArrayMap`2[1, 4, 2, 3, 5]", map.ToString());

			Assert.AreEqual("string3", map.GetValue(3));
			Assert.AreEqual("UnorderedArrayMap`2[1, 4, 3, 2, 5]", map.ToString());

			Assert.AreEqual("string3", map.GetValue(3));
			Assert.AreEqual("UnorderedArrayMap`2[1, 3, 4, 2, 5]", map.ToString());

			Assert.AreEqual(null, map.GetValue(6));
		}

		/**
		 * Test method for remove().
		 */
		[TestMethod]
		public void testRemove()
		{
			Assert.IsTrue(map.IsEmpty());
			Assert.IsNull(map.Put(3, "string3"));
			Assert.IsNull(map.Put(5, "string5"));
			Assert.IsNull(map.Put(2, "string2"));
			Assert.IsNull(map.Put(4, "string4"));
			Assert.IsNull(map.Put(1, "string1"));
			Assert.IsFalse(map.IsEmpty());
			Assert.AreEqual("UnorderedArrayMap`2[1, 4, 2, 5, 3]", map.ToString());

			Assert.IsNull(map.Remove(0));
			Assert.AreEqual(5, map.Size());
			Assert.IsFalse(map.IsEmpty());
			Assert.AreEqual("UnorderedArrayMap`2[1, 4, 2, 5, 3]", map.ToString());

			Assert.AreEqual("string2", map.Remove(2));
			Assert.AreEqual(4, map.Size());
			Assert.AreEqual("UnorderedArrayMap`2[1, 4, 5, 3]", map.ToString());

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

			IEnumerator<int> it = map.GetEnumerator();
			it.MoveNext();
			Assert.AreEqual(1, (int)it.Current);
			it.MoveNext();
			Assert.AreEqual(4, (int)it.Current);
			it.MoveNext();
			Assert.AreEqual(2, (int)it.Current);
			it.MoveNext();
			Assert.AreEqual(5, (int)it.Current);
			it.MoveNext();
			Assert.AreEqual(3, (int)it.Current);

			Assert.IsFalse(it.MoveNext());		
		}

		/**
		 * Test method for EntryIterator().
		 */
		public void TestEntryIterator()
		{
			Assert.IsNull(map.Put(3, "string3"));
			Assert.IsNull(map.Put(5, "string5"));
			Assert.IsNull(map.Put(2, "string2"));
			Assert.IsNull(map.Put(4, "string4"));
			Assert.IsNull(map.Put(1, "string1"));

			IEnumerator<IEntry<int, string>> it = map.EntryIterator().GetEnumerator();
			Assert.IsTrue(it.MoveNext());
			IEntry<int, string> entry = it.Current;
			Assert.AreEqual(1, (int)(entry.GetKey()));
			Assert.AreEqual("string1", (String)(entry.GetValue()));

			it.MoveNext();
			entry = it.Current;
			Assert.AreEqual(4, (int)(entry.GetKey()));
			Assert.AreEqual("string4", (String)(entry.GetValue()));

			it.MoveNext();
			entry = it.Current;
			Assert.AreEqual(2, (int)(entry.GetKey()));
			Assert.AreEqual("string2", (String)(entry.GetValue()));

			it.MoveNext();
			entry = it.Current;
			Assert.AreEqual(5, (int)(entry.GetKey()));
			Assert.AreEqual("string5", (String)(entry.GetValue()));


			it.MoveNext();
			entry = it.Current;
			Assert.AreEqual(3, (int)(entry.GetKey()));
			Assert.AreEqual("string3", (String)(entry.GetValue()));
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
			it.MoveNext();
			Assert.AreEqual("string4", it.Current);
			it.MoveNext();
			Assert.AreEqual("string2", it.Current);
			it.MoveNext();
			Assert.AreEqual("string5", it.Current);
			it.MoveNext();
			Assert.AreEqual("string3", it.Current);		
		}
	}
}
