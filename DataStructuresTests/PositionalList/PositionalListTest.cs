using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures.PositionalList;
using DataStructures;
using System.Collections.Generic;
using System;

namespace TestProject2.PositionalList
{
    [TestClass]
    public class PositionalListTest
    {
		private PositionalLinkedList<string> list;

		/**
		 * Initializes a new list for testing.
		 */
		[TestInitialize]
		public void SetUp()
		{
			list = new PositionalLinkedList<string>();
		}

		/**
		 * Test for First() method.
		 */
		[TestMethod]
		public void TestFirst()
		{
			Assert.AreEqual(0, list.Size());
			Assert.IsTrue(list.IsEmpty());

			Assert.IsNull(list.First());

			IPosition<string> first = list.AddFirst("three");
			Assert.AreEqual(1, list.Size());
			Assert.AreEqual(first, list.First());

			first = list.AddFirst("two");
			Assert.AreEqual(2, list.Size());
			Assert.AreEqual(first, list.First());

			first = list.AddFirst("one");
			Assert.AreEqual(3, list.Size());
			Assert.AreEqual(first, list.First());
		}

		/**
		 * Test for Last() method.
		 */
		[TestMethod]
		public void TestLast()
		{
			Assert.AreEqual(0, list.Size());
			Assert.IsTrue(list.IsEmpty());

			// Test Last() on empty list
			Assert.IsNull(list.Last());

			IPosition<string> last = list.AddLast("one");
			Assert.AreEqual(1, list.Size());
			Assert.AreEqual(last, list.Last());

			last = list.AddLast("two");
			Assert.AreEqual(2, list.Size());
			Assert.AreEqual(last, list.Last());

			last = list.AddLast("three");
			Assert.AreEqual(3, list.Size());
			Assert.AreEqual(last, list.Last());
		}

		/**
		 * Test for AddFirst() method.
		 */
		[TestMethod]
		public void TestAddFirst()
		{
			Assert.AreEqual(0, list.Size());
			Assert.IsTrue(list.IsEmpty());
			IPosition<string> first = list.AddFirst("one");
			Assert.AreEqual(1, list.Size());
			Assert.IsFalse(list.IsEmpty());

			Assert.AreEqual(list.First(), first);

			IPosition<string> two = list.AddFirst("two");
			Assert.AreEqual(2, list.Size());
			Assert.AreEqual(list.First(), two);

			IPosition<string> three = list.AddFirst("three");
			Assert.AreEqual(3, list.Size());
			Assert.AreEqual(list.First(), three);

			IPosition<string> four = list.AddFirst("four");
			Assert.AreEqual(4, list.Size());
			Assert.AreEqual(list.First(), four);
		}

		/**
		 * Test for AddLast() method.
		 */
		[TestMethod]
		public void TestAddLast()
		{
			Assert.AreEqual(0, list.Size());
			Assert.IsTrue(list.IsEmpty());
			IPosition<string> first = list.AddLast("one");
			Assert.AreEqual(1, list.Size());
			Assert.AreEqual(list.Last(), first);

			IPosition<string> two = list.AddLast("two");
			Assert.AreEqual(2, list.Size());
			Assert.AreEqual(list.Last(), two);

			IPosition<string> three = list.AddLast("three");
			Assert.AreEqual(3, list.Size());
			Assert.AreEqual(list.Last(), three);

			IPosition<string> four = list.AddLast("four");
			Assert.AreEqual(4, list.Size());
			Assert.AreEqual(list.Last(), four);
		}

		/**
		 * Test for Before() method.
		 */
		[TestMethod]
		public void TestBefore()
		{
			Assert.AreEqual(0, list.Size());
			Assert.IsTrue(list.IsEmpty());

			IPosition<string> one = list.AddLast("one");

			// Test Before() on list with one element
			Assert.IsNull(list.Before(one));

			IPosition<string> two = list.AddLast("two");
			IPosition<string> three = list.AddLast("three");
			IPosition<string> four = list.AddLast("four");

			Assert.AreEqual(list.Before(four), three);
			Assert.AreEqual(list.Before(three), two);
			Assert.AreEqual(list.Before(two), one);
		}

		/**
		 * Test for After() method.
		 */
		[TestMethod]
		public void TestAfter()
		{
			Assert.AreEqual(0, list.Size());
			Assert.IsTrue(list.IsEmpty());

			IPosition<string> one = list.AddLast("one");

			// Test After() on list with one element
			Assert.IsNull(list.After(one));

			IPosition<string> two = list.AddLast("two");
			IPosition<string> three = list.AddLast("three");
			IPosition<string> four = list.AddLast("four");

			Assert.AreEqual(list.After(one), two);
			Assert.AreEqual(list.After(two), three);
			Assert.AreEqual(list.After(three), four);
		}

		/**
		 * Test for AddBefore() method.
		 */
		[TestMethod]
		public void TestAddBefore()
		{
			Assert.AreEqual(0, list.Size());
			Assert.IsTrue(list.IsEmpty());
			IPosition<string> last = list.AddFirst("three");
			Assert.AreEqual(1, list.Size());
			Assert.IsFalse(list.IsEmpty());

			IPosition<string> middle = list.AddBefore(last, "two");
			Assert.AreEqual(2, list.Size());
			Assert.AreEqual(middle, list.First());
			Assert.AreEqual(middle.GetElement(), "two");
			Assert.AreEqual(list.After(middle).GetElement(), "three");

			IPosition<string> first = list.AddBefore(middle, "one");
			Assert.AreEqual(3, list.Size());
			Assert.AreEqual(first, list.First());
			Assert.AreEqual(first.GetElement(), "one");
			Assert.AreEqual(list.After(first).GetElement(), "two");

			first = list.First();
			middle = list.After(first);
			last = list.After(middle);

			Assert.AreEqual(first, list.First());
			Assert.AreEqual(list.After(first), middle);
			Assert.AreEqual(list.After(middle), last);
			Assert.AreEqual(list.Before(last), middle);
			Assert.AreEqual(list.Before(middle), first);
			Assert.AreEqual(last, list.Last());
		}

		/**
		 * Test for AddAfter() method.
		 */
		[TestMethod]
		public void TestAddAfter()
		{
			IPosition<string> one = list.AddLast("one");

			IPosition<string> two = list.AddAfter(one, "two");
			IPosition<string> three = list.AddAfter(two, "three");
			IPosition<string> four = list.AddAfter(three, "four");

			Assert.AreEqual(list.After(one), two);
			Assert.AreEqual(list.After(two), three);
			Assert.AreEqual(list.After(three), four);

			Assert.AreEqual(list.Before(four), three);
			Assert.AreEqual(list.Before(three), two);
			Assert.AreEqual(list.Before(two), one);
		}

		/**
		 * Test for SetPosition() method.
		 */
		[TestMethod]
		public void TestSetPosition()
		{
			IPosition<string> one = list.AddLast("one");
			IPosition<string> two = list.AddAfter(one, "two");
			IPosition<string> three = list.AddAfter(two, "three");
			IPosition<string> four = list.AddAfter(three, "four");
			Assert.AreEqual(list.Size(), 4);

			Assert.AreEqual(list.SetPosition(four, "five"), "four");
			Assert.AreEqual(list.Size(), 4);
			Assert.AreEqual(list.SetPosition(three, "six"), "three");
			Assert.AreEqual(list.Size(), 4);
			Assert.AreEqual(list.SetPosition(two, "seven"), "two");
			Assert.AreEqual(list.Size(), 4);
			Assert.AreEqual(list.SetPosition(one, "eight"), "one");
			Assert.AreEqual(list.Size(), 4);
		}

		/**
		 * Test for Remove() method.
		 */
		[TestMethod]
		public void TestRemove()
		{
			IPosition<string> one = list.AddLast("one");
			IPosition<string> two = list.AddAfter(one, "two");
			IPosition<string> three = list.AddAfter(two, "three");
			IPosition<string> four = list.AddAfter(three, "four");
			Assert.AreEqual(list.Size(), 4);

			Assert.AreEqual(list.Remove(four), "four");
			Assert.AreEqual(list.Size(), 3);
			Assert.AreEqual(list.Remove(three), "three");
			Assert.AreEqual(list.Size(), 2);
			Assert.AreEqual(list.Remove(two), "two");
			Assert.AreEqual(list.Size(), 1);
			Assert.AreEqual(list.Remove(one), "one");
			Assert.AreEqual(list.Size(), 0);
		}

		/**
		 * Test for iterator() method.
		 */
		[TestMethod]
		public void TestIterator()
		{
			// Start with an empty list
			Assert.AreEqual(0, list.Size());
			Assert.IsTrue(list.IsEmpty());

			// Create an iterator for the empty list
			IEnumerator<IPosition<string>> it = list.PositionIterator().GetEnumerator();
			Assert.IsFalse(it.MoveNext());

			// Trying to get the next element from an empty list should throw a NoSuchElementException
			try
			{
				it.MoveNext();
			}
			catch (Exception e)
			{
				Assert.IsTrue(e is InvalidOperationException);
			}

			Assert.IsNull(it.Current);

			// Now add an element
			list.AddLast("one");
			list.AddLast("two");
			list.AddLast("three");
			list.AddLast("four");

			// Use accessor methods to check that the list is correct
			Assert.AreEqual(4, list.Size());
			Assert.IsFalse(list.IsEmpty());
			Assert.AreEqual("one", list.First().GetElement());
			Assert.AreEqual("four", list.Last().GetElement());

			// Create an iterator for the list that has 1 element
			it = list.PositionIterator().GetEnumerator();

			// Try different iterator operations to make sure they work
			// as expected for a list that contains 4 elements
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual("one", it.Current.GetElement());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual("two", it.Current.GetElement());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual("three", it.Current.GetElement());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual("four", it.Current.GetElement());
			Assert.IsFalse(it.MoveNext());
			try
			{
				it.MoveNext();
			}
			catch (Exception e)
			{
				Assert.IsTrue(e is InvalidOperationException);
			}
		}

		/**
		 * Test for position iterators.
		 */
		public void PositionIterator()
		{
			Assert.AreEqual(0, list.Size());
			IPosition<string> first = list.AddFirst("one");
			IPosition<string> second = list.AddLast("two");
			IPosition<string> third = list.AddLast("three");
			Assert.AreEqual(3, list.Size());

			IEnumerator<IPosition<string>> it = list.PositionIterator().GetEnumerator();
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(first, it.Current);
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(second, it.Current);
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(third, it.Current);

			Assert.IsFalse(it.MoveNext());
		}		
	}
}
