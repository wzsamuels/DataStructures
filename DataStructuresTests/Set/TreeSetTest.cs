using DataStructures.Set;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestProject2.Set
{
    /**
    * Test class for TreeSet.
    * 
    * @author Zach Samuels
    */
    [TestClass]
    public class TreeSetTest
    {

        private TreeSet<int> set;

        /**
         * Creates a new TreeSet for testing.
         */
        [TestInitialize]
        public void SetUp()
        {
            set = new TreeSet<int>();
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
         * Test method for Remove().
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

        /**
         * Test iterating over a set.
         */
        [TestMethod]
        public void TestIterator()
        {
            Assert.IsTrue(set.IsEmpty());
            Assert.AreEqual(0, set.Size());
            set.Add(5);
            set.Add(10);
            set.Add(15);
            set.Add(20);
            set.Add(25);
            Assert.AreEqual(5, set.Size());

            // Iterator should give in-order traversal
            IEnumerator<int> it = set.GetEnumerator();

            Assert.IsTrue(it.MoveNext());
            Assert.AreEqual(5, (int)it.Current);
            Assert.IsTrue(it.MoveNext());
            Assert.AreEqual(10, (int)it.Current);
            Assert.IsTrue(it.MoveNext());
            Assert.AreEqual(15, (int)it.Current);
            Assert.IsTrue(it.MoveNext());
            Assert.AreEqual(20, (int)it.Current);
            Assert.IsTrue(it.MoveNext());
            Assert.AreEqual(25, (int)it.Current);

            Assert.IsFalse(it.MoveNext());
        }
    }
}
