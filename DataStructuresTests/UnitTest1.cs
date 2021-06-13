using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures.List;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject2
{
    [TestClass]
    public class UnitTest1
    {
		ArrayBasedList<string> list;
		ArrayBasedList<string> listRemove;

		[TestInitialize]
		public void SetUp()
        {
			list = new ArrayBasedList<string>();
            listRemove = new ArrayBasedList<string>
            {

                //Build a list for Remove testing
                { 0, "one" },
                { 1, "two" },
                { 2, "three" },
                { 3, "four" }
            };            
		}

		[TestMethod]
        public void TestAddIndex()
        {
            
            Assert.AreEqual(list.Size(), 0);
            Assert.IsTrue(list.IsEmpty());

            list.Add(0, "one");
            Assert.AreEqual(1, list.Size());
            Assert.AreEqual("one", list.GetIndex(0));
            Assert.IsFalse(list.IsEmpty());

            try
            {
                list.Add(15, "fifteen");
            }
            catch(Exception e)
            {
                Assert.IsTrue(e.GetType() == typeof(ArgumentOutOfRangeException));
            }
        }

		/**
		 * Test for AddLast() method.
		*/
		[TestMethod]
		public void testAddLast()
		{
			Assert.AreEqual(0, list.Size());
			Assert.IsTrue(list.IsEmpty());

			list.AddLast("one");
			Assert.AreEqual(1, list.Size());
			Assert.AreEqual("one", list.GetIndex(0));
			Assert.IsFalse(list.IsEmpty());
		}

		/**
		 * Test for Last() method.
		 */
		[TestMethod]
		public void testLast()
		{
			list.Add(0, "one");
			Assert.AreEqual("one", list.Last());
			list.Add(0, "two");
			Assert.AreEqual("one", list.Last());
			list.Add(0, "three");
			Assert.AreEqual("one", list.Last());
			Assert.AreEqual(3, list.Size());
		}

		/**
		 * Test for AddFirst() method.
		 */
		[TestMethod]
		public void testAddFirst()
		{
			list.AddFirst("one");
			Assert.AreEqual("one", list.First());
			list.AddFirst("two");
			Assert.AreEqual("two", list.First());
			list.AddFirst("three");
			Assert.AreEqual("three", list.First());
			list.AddFirst("four");
			Assert.AreEqual("four", list.First());
			list.AddFirst("five");
			Assert.AreEqual("five", list.First());
			list.AddFirst("six");
			Assert.AreEqual("six", list.First());
			list.AddFirst("seven");
			Assert.AreEqual("seven", list.First());
			list.AddFirst("eight");
			Assert.AreEqual("eight", list.First());
			list.AddFirst("nine");
			Assert.AreEqual("nine", list.First());
			list.AddFirst("ten");
			Assert.AreEqual("ten", list.First());
			list.AddFirst("eleven");
			Assert.AreEqual("eleven", list.First());
			list.AddFirst("twelve");
			Assert.AreEqual("twelve", list.First());

			Assert.AreEqual(12, list.Size());
		}

		/**
		 * Test for First() method.
		 */
		[TestMethod]
		public void testFirst()
		{
			list.Add(0, "one");
			Assert.AreEqual("one", list.First());
			list.Add(0, "two");
			Assert.AreEqual("two", list.First());
			list.Add(0, "three");
			Assert.AreEqual("three", list.First());
			Assert.AreEqual(3, list.Size());
		}

		/**
		 * Test for iterator() method.
		 */
		[TestMethod]
		public void testIterator()
		{
			// Start with an empty list
			Assert.AreEqual(0, list.Size());
			Assert.IsTrue(list.IsEmpty());

			// Try different operations to make sure they work
			// as expected for an empty list (at this point)

			// Now Add an element
			list.AddLast("one");

			// Use accessor methods to check that the list is correct
			Assert.AreEqual(1, list.Size());
			Assert.IsFalse(list.IsEmpty());
			Assert.AreEqual("one", list.GetIndex(0));

			// Create an iterator for the list that has 1 element
			// Create an iterator for the empty list

			// Try different iterator operations to make sure they work
			// as expected for a list that contains 1 element (at this point)
			//Assert.IsTrue(list.GetEnumerator().MoveNext());
			//list.GetEnumerator().MoveNext();
			//Assert.AreEqual("one", list.GetEnumerator().Current);
			
			list.AddLast("two");
			list.AddLast("three");
			list.AddLast("four");

			
		}

		/**
		 * Test for RemoveIndex() method.
		 */
		[TestMethod]
		public void testRemoveIndex()
		{
			// Test removing from empty list
			Assert.IsTrue(list.IsEmpty());
			try
			{
				list.Remove(0);
			}
			catch (ArgumentOutOfRangeException e)
			{
				Assert.AreEqual(e.Message,
						"Specified argument was out of the range of valid values. (Parameter 'Index is invalid: 0 (size=0)')");
			}
			Assert.IsTrue(list.IsEmpty());

			Assert.AreEqual("two", listRemove.GetIndex(1));
			listRemove.Remove(1);
			Assert.AreEqual("three", listRemove.GetIndex(1));
			listRemove.Remove(1);
			Assert.AreEqual("four", listRemove.GetIndex(1));
		}

		/**
		 * Test for RemoveFirst() method.
		 */
		[TestMethod]
		public void testRemoveFirst()
		{
			// Test removing from empty list
			Assert.IsTrue(list.IsEmpty());
			try
			{
				list.RemoveFirst();
			}
			catch (ArgumentOutOfRangeException e)
			{
				Assert.AreEqual(e.Message,
						"Specified argument was out of the range of valid values. (Parameter 'Index is invalid: 0 (size=0)')");
			}
			Assert.IsTrue(list.IsEmpty());

			Assert.AreEqual("one", listRemove.First());
			Assert.AreEqual("one", listRemove.RemoveFirst());
			Assert.AreEqual("two", listRemove.RemoveFirst());
			Assert.AreEqual("three", listRemove.RemoveFirst());
			Assert.AreEqual("four", listRemove.RemoveFirst());

			Assert.IsTrue(listRemove.IsEmpty());
		}

		/**
		 * Test for RemoveLast method().
		 */
		[TestMethod]
		public void testRemoveLast()
		{
			// Test removing from empty list
			Assert.IsTrue(list.IsEmpty());
			try
			{
				list.RemoveLast();
			}
			catch (ArgumentOutOfRangeException e)
			{
				Assert.AreEqual(e.Message,
						"Specified argument was out of the range of valid values. (Parameter 'Index is invalid: -1 (size=0)')");
			}
			Assert.IsTrue(list.IsEmpty());

			Assert.AreEqual("four", listRemove.RemoveLast());
			Assert.AreEqual("three", listRemove.RemoveLast());
			Assert.AreEqual("two", listRemove.RemoveLast());
			Assert.AreEqual("one", listRemove.RemoveLast());

			Assert.IsTrue(listRemove.IsEmpty());
		}

		/**
		 * Test for set() method.
		 */
		[TestMethod]
		public void testSet()
		{
			Assert.AreEqual("one", listRemove.SetIndex(0, "five"));
			Assert.AreEqual("two", listRemove.SetIndex(1, "six"));
			Assert.AreEqual("three", listRemove.SetIndex(2, "seven"));
			Assert.AreEqual("four", listRemove.SetIndex(3, "eight"));

			Assert.AreEqual("five", listRemove.GetIndex(0));
			Assert.AreEqual("six", listRemove.GetIndex(1));
			Assert.AreEqual("seven", listRemove.GetIndex(2));
			Assert.AreEqual("eight", listRemove.GetIndex(3));

			Assert.AreEqual(listRemove.Size(), 4);
		}
	}	
}
