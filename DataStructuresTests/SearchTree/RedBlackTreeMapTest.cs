using DataStructures.SearchTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject2.SearchTree
{
    /**
    * Test class for RedBlackTreeMap.
    * @author Zach Samuels
    *
     */ 
    [TestClass]
    public class RedBlackTreeMapTest
    {

        private BinarySearchTreeMap<int, string> tree;

        /**
         * Creates a new RedBlackTreeMap for testing.
         */
        [TestInitialize]
        public void SetUp()
        {
            tree = new RedBlackTreeMap<int, string>(null);
            tree = new RedBlackTreeMap<int, string>();
        }

        /**
         * Test method for Put().
         */
        [TestMethod]
        public void TestPut()
        {
            Assert.AreEqual(0, tree.Size());
            Assert.IsTrue(tree.IsEmpty());

            Assert.IsNull(tree.Put(4, "four"));
            Assert.AreEqual(1, tree.Size());
            Assert.IsFalse(tree.IsEmpty());
            Assert.AreEqual(4, (int)tree.Root().GetElement().GetKey());

            Assert.IsNull(tree.Put(7, "seven"));
            Assert.AreEqual(2, tree.Size());
            Assert.AreEqual(4, (int)tree.Root().GetElement().GetKey());
            Assert.AreEqual(7, (int)tree.Right(tree.Root()).GetElement().GetKey());

            Assert.IsNull(tree.Put(12, "twelve"));
            Assert.AreEqual(3, tree.Size());
            Assert.AreEqual(7, (int)tree.Root().GetElement().GetKey());
            Assert.AreEqual(4, (int)tree.Left(tree.Root()).GetElement().GetKey());
            Assert.AreEqual(12, (int)tree.Right(tree.Root()).GetElement().GetKey());

            Assert.IsNull(tree.Put(15, "15"));
            Assert.IsNull(tree.Put(11, "11"));
            Assert.IsNull(tree.Put(2, "2"));
            Assert.IsNull(tree.Put(5, "5"));
            Assert.IsNull(tree.Put(16, "16"));
            Assert.IsNull(tree.Put(14, "14"));
            Assert.IsNull(tree.Put(6, "6"));

            // using Put to replace key
            Assert.AreEqual("seven", tree.Put(7, "new7"));
        }

        /**
         * Test method for Get().
         */
        [TestMethod]
        public void TestGet()
        {
            Assert.IsNull(tree.Put(15, "15"));
            Assert.IsNull(tree.Put(11, "11"));
            Assert.IsNull(tree.Put(2, "2"));
            Assert.IsNull(tree.Put(5, "5"));
            Assert.IsNull(tree.Put(16, "16"));
            Assert.IsNull(tree.Put(14, "14"));
            Assert.IsNull(tree.Put(6, "6"));
            Assert.IsNull(tree.Put(7, "7"));

            Assert.AreEqual("15", tree.GetValue(15));
            Assert.AreEqual("7", tree.GetValue(7));
            Assert.AreEqual("6", tree.GetValue(6));
            Assert.AreEqual("14", tree.GetValue(14));

            Assert.IsNull(tree.GetValue(102));
        }

        /**
         * Test method for Remove().
         */
        [TestMethod]
        public void TestRemove()
        {
            Assert.IsTrue(tree.IsEmpty());
            Assert.AreEqual(0, tree.Size());
            Assert.IsTrue(tree.IsEmpty());

            Assert.IsNull(tree.Remove(100));

            Assert.IsNull(tree.Put(15, "15"));
            Assert.IsNull(tree.Put(11, "11"));
            Assert.IsNull(tree.Put(2, "2"));
            Assert.IsNull(tree.Put(5, "5"));
            Assert.IsNull(tree.Put(16, "16"));
            Assert.IsNull(tree.Put(14, "14"));
            Assert.IsNull(tree.Put(6, "6"));
            Assert.IsNull(tree.Put(30, "30"));
            Assert.IsNull(tree.Put(50, "50"));

            Assert.AreEqual("15", tree.Remove(15));
            Assert.AreEqual("14", tree.Remove(14));
            Assert.AreEqual("5", tree.Remove(5));
            Assert.AreEqual("2", tree.Remove(2));
            Assert.AreEqual("6", tree.Remove(6));
            Assert.AreEqual("11", tree.Remove(11));
            Assert.AreEqual("30", tree.Remove(30));
            Assert.AreEqual("50", tree.Remove(50));
            Assert.AreEqual("16", tree.Remove(16));

            Assert.IsTrue(tree.IsEmpty());

            tree = new RedBlackTreeMap<int, string>();

            Assert.IsNull(tree.Put(20, "20"));
            Assert.IsNull(tree.Put(30, "30"));
            Assert.IsNull(tree.Put(40, "40"));
            Assert.IsNull(tree.Put(50, "50"));
            Assert.IsNull(tree.Put(10, "10"));
            Assert.IsNull(tree.Put(25, "25"));
            Assert.IsNull(tree.Put(45, "45"));
            Assert.IsNull(tree.Put(60, "60"));
            // Assert.IsNull(tree.Put(25, "25"));
            Assert.IsNull(tree.Put(28, "28"));

            Assert.AreEqual("28", tree.Remove(28));
            Assert.AreEqual("25", tree.Remove(25));
            Assert.AreEqual("20", tree.Remove(20));
            Assert.AreEqual("10", tree.Remove(10));
        }
    }
}
