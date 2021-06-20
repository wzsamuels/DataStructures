using DataStructures;
using DataStructures.Map;
using DataStructures.SearchTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestProject2.SearchTree
{
    /**
    * Test class for SplayTreeMap.
    * @author Zach Samuels
    */
    [TestClass]
    public class SplayTreeMapTest
    {

        private BinarySearchTreeMap<int, string> tree;

        /**
         * Creates a new SplayTreeMap for testing.
         */
        [TestInitialize]
        public void SetUp()
        {
            tree = new SplayTreeMap<int, string>(null);
            tree = new SplayTreeMap<int, string>();
        }

        /**
         * Test method for Put().
         */
        [TestMethod]
        public void TestPut()
        {
            Assert.AreEqual(0, tree.Size());
            Assert.IsTrue(tree.IsEmpty());

            Assert.IsNull(tree.Put(10, "10"));
            Assert.AreEqual(1, tree.Size());
            Assert.AreEqual(10, (int)tree.Root().GetElement().GetKey());
            Assert.IsNull(tree.Put(20, "20"));
            Assert.AreEqual(2, tree.Size());
            Assert.IsNull(tree.Put(30, "30"));
            Assert.AreEqual(3, tree.Size());
            Assert.IsNull(tree.Put(40, "40"));
            Assert.AreEqual(4, tree.Size());
            Assert.IsNull(tree.Put(25, "25"));
            Assert.IsNull(tree.Put(50, "50"));
            Assert.IsNull(tree.Put(5, "5"));
            Assert.IsNull(tree.Put(15, "15"));
            Assert.AreEqual(tree.Size(), 8);

            IEnumerator<IPosition<IMap<int, string>.IEntry>> it = tree.LevelOrder().GetEnumerator();
            Assert.IsTrue(it.MoveNext());
            Assert.AreEqual(15, it.Current.GetElement().GetKey());
        }

        /**
         * Test method for Get().
         */
        [TestMethod]
        public void TestGet()
        {
            Assert.IsNull(tree.Put(10, "10"));
            Assert.IsNull(tree.Put(20, "20"));
            Assert.IsNull(tree.Put(30, "30"));
            Assert.IsNull(tree.Put(40, "40"));
            Assert.IsNull(tree.Put(25, "25"));
            Assert.IsNull(tree.Put(50, "50"));
            Assert.IsNull(tree.Put(5, "5"));
            Assert.IsNull(tree.Put(15, "15"));

            Assert.AreEqual("10", tree.GetValue(10));
            Assert.AreEqual(10, (int)tree.Root().GetElement().GetKey());
            Assert.AreEqual("20", tree.GetValue(20));
            Assert.AreEqual(20, (int)tree.Root().GetElement().GetKey());
            Assert.AreEqual("30", tree.GetValue(30));
            Assert.AreEqual(30, (int)tree.Root().GetElement().GetKey());
            Assert.AreEqual("40", tree.GetValue(40));
            Assert.AreEqual(40, (int)tree.Root().GetElement().GetKey());
            Assert.AreEqual("25", tree.GetValue(25));
            Assert.AreEqual(25, (int)tree.Root().GetElement().GetKey());

            // Get node that doesn't exist
            Assert.IsNull(tree.GetValue(250));
        }

        /**
         * Test method for Remove().
         */
        [TestMethod]
        public void TestRemove()
        {
            Assert.IsNull(tree.Put(10, "10"));
            Assert.IsNull(tree.Put(20, "20"));
            Assert.IsNull(tree.Put(30, "30"));
            Assert.IsNull(tree.Put(40, "40"));
            Assert.IsNull(tree.Put(25, "25"));
            Assert.IsNull(tree.Put(50, "50"));
            Assert.IsNull(tree.Put(5, "5"));
            Assert.IsNull(tree.Put(15, "15"));

            // Remove node with two children
            Assert.AreEqual("25", tree.Remove(25));

            // Remove node with one Left child
            Assert.AreEqual("30", tree.Remove(30));

            // Remove node with one Right child
            Assert.AreEqual("5", tree.Remove(5));

            // Remove Root node
            Assert.AreEqual("15", tree.Remove(15));
        }
    }
}
