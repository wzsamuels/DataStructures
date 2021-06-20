using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DataStructures.Set;

namespace TestProject2.Set
{
    /**
    * Test class for HashSetTest.
    * 
    * @author Zach Samuels
    */
    [TestClass]
    public class HashSetTest
    {

        private DataStructures.Set.HashSet<int> set;
        private DataStructures.Set.HashSet<int> testSet;

        /**
         * Creates new HashSets for testing.
         */
        [TestInitialize]
        public void SetUp()
        {
            // Will use a hash map with random alpha, beta values
            set = new DataStructures.Set.HashSet<int>();

            // Will use our hash map for testing, which uses alpha=1, beta=1, prime=7
            testSet = new DataStructures.Set.HashSet<int>(true);
        }

        /**
         * Test method for Add().
         */
        [TestMethod]
        public void TestAdd()
        {
            Assert.IsTrue(set.IsEmpty());
            Assert.AreEqual(0, set.Size());

            set.Add(5);
            Assert.AreEqual(1, set.Size());
            Assert.IsFalse(set.IsEmpty());

            set.Add(5);
            Assert.AreEqual(1, set.Size());

            set.Add(10);
            Assert.AreEqual(2, set.Size());

            set.Add(15);
            Assert.AreEqual(3, set.Size());

            set.Add(20);
            Assert.AreEqual(4, set.Size());

            set.Add(10);
            Assert.AreEqual(4, set.Size());
        }

        /**
         * Test method for Contains().
         */
        [TestMethod]
        public void TestContains()
        {
            Assert.IsTrue(set.IsEmpty());
            Assert.AreEqual(0, set.Size());
            set.Add(5);
            set.Add(10);
            set.Add(15);
            set.Add(20);
            set.Add(25);
            Assert.AreEqual(5, set.Size());

            Assert.IsTrue(set.Contains(5));
            Assert.IsTrue(set.Contains(10));
            Assert.IsTrue(set.Contains(15));
            Assert.IsTrue(set.Contains(20));
            Assert.IsTrue(set.Contains(25));

            Assert.IsFalse(set.Contains(0));
            Assert.IsFalse(set.Contains(4));
            Assert.IsFalse(set.Contains(9));
            Assert.IsFalse(set.Contains(14));
        }

        /**
         * Test for Remove().
         */
        [TestMethod]
        public void TestRemove()
        {
            Assert.IsTrue(set.IsEmpty());
            Assert.AreEqual(0, set.Size());
            set.Add(5);
            set.Add(10);
            set.Add(15);
            set.Add(20);
            set.Add(25);
            Assert.AreEqual(5, set.Size());

            // Test removing non-existent element
            Assert.AreEqual(set.Remove(30), 0);

            Assert.AreEqual(5, (int)set.Remove(5));
            Assert.AreEqual(4, set.Size());

            Assert.AreEqual(10, (int)set.Remove(10));
            Assert.AreEqual(3, set.Size());

            Assert.AreEqual(15, (int)set.Remove(15));
            Assert.AreEqual(2, set.Size());

            Assert.AreEqual(20, (int)set.Remove(20));
            Assert.AreEqual(1, set.Size());

            Assert.AreEqual(25, (int)set.Remove(25));
            Assert.AreEqual(0, set.Size());

            Assert.AreEqual(set.Remove(25), 0);
            Assert.AreEqual(0, set.Size());
            Assert.IsTrue(set.IsEmpty());
        }

        /**
         * Test method for RetainAll().
         */
        [TestMethod]
        public void TestRetainAll()
        {
            Assert.IsTrue(set.IsEmpty());
            Assert.AreEqual(0, set.Size());
            set.Add(5);
            set.Add(10);
            set.Add(15);
            set.Add(20);
            set.Add(25);
            Assert.AreEqual(5, set.Size());

            TreeSet<int> other = new()
            {
                10,
                20,
                30
            };

            set.RetainAll(other);
            Assert.IsTrue(set.Contains(10));
            Assert.IsTrue(set.Contains(20));
            Assert.AreEqual(2, set.Size());
        }

        /**
         * Test method for RemoveAll().
         */
        [TestMethod]
        public void TestRemoveAll()
        {
            Assert.IsTrue(set.IsEmpty());
            Assert.AreEqual(0, set.Size());
            set.Add(5);
            set.Add(10);
            set.Add(15);
            set.Add(20);
            set.Add(25);
            Assert.AreEqual(5, set.Size());

            TreeSet<int> other = new()
            {
                10,
                20,
                30
            };

            set.RemoveAll(other);
            Assert.AreEqual(3, set.Size());
            Assert.IsTrue(set.Contains(5));
            Assert.IsTrue(set.Contains(15));
            Assert.IsTrue(set.Contains(25));
        }

        /**
         * Test method for AddAll().
         */
        [TestMethod]
        public void TestAddAll()
        {
            Assert.IsTrue(set.IsEmpty());
            Assert.AreEqual(0, set.Size());
            set.Add(5);
            set.Add(10);
            set.Add(15);
            set.Add(20);
            set.Add(25);
            Assert.AreEqual(5, set.Size());

            TreeSet<int> other = new()
            {
                30,
                40,
                50
            };

            set.AddAll(other);
            Assert.AreEqual(8, set.Size());
            Assert.IsTrue(set.Contains(30));
            Assert.IsTrue(set.Contains(40));
            Assert.IsTrue(set.Contains(50));
        }

        // Since our hash map uses random numbers, it can
        // be difficult to test that our hash set iterator returns
        // values in a certain order.
        // We could approach this problem with a few different options:
        // (1) instead of checking the specific order of entries
        //      visited by the iterator, we could save each
        //      iterator.next() into an array, then check that the 
        //      array *Contains* the entry, regardless of its exact location
        //
        // (2) implement an isTesting flag for HashSet, similar to HashtestSet. 
        //     This is the more appropriate option since we can control the
        //     entire execution and know exactly which values should appear
        //     in the set and in the correct expected order from using the iterator 
        //
        // Revisit your test cases for HashMap iterator -- those tests can be adapted
        //     to work here, too, since you have already worked out the details
        //     of where entries with certain keys will be stored and in what order
        //     they will be stored
        /**
         * Test method for iterating over a set.
         */
        [TestMethod]
        public void TestIterator()
        {
            Assert.IsTrue(testSet.IsEmpty());
            testSet.Add(3);
            testSet.Add(5);
            testSet.Add(2);
            testSet.Add(4);
            testSet.Add(1);
            testSet.Add(6);
            Assert.AreEqual(6, testSet.Size());
            Assert.IsFalse(testSet.IsEmpty());

            IEnumerator<int> it = testSet.GetEnumerator();
            Assert.IsTrue(it.MoveNext());
            Assert.AreEqual(6, (int)it.Current); // should be index 0
            Assert.IsTrue(it.MoveNext());
            Assert.AreEqual(1, (int)it.Current); // should be index 2
            Assert.IsTrue(it.MoveNext());
            Assert.AreEqual(2, (int)it.Current); // should be index 3
            Assert.IsTrue(it.MoveNext());
            Assert.AreEqual(3, (int)it.Current); // should be index 4
            Assert.IsTrue(it.MoveNext());
            Assert.AreEqual(4, (int)it.Current); // should be index 5
            Assert.IsTrue(it.MoveNext());
            Assert.AreEqual(5, (int)it.Current); // should be index 6   

            Assert.IsFalse(it.MoveNext());
        }
    }
}
