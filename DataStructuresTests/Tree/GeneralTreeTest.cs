using DataStructures;
using DataStructures.Tree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestProject2
{
    /**
     * Test class for GeneralTree.
     * 
     * @author Zach Samuels
     */
    [TestClass]
    public class GeneralTreeTest
    {

        private GeneralTree<string> tree;
        private GeneralTree<string> emptyTree;

        private IPosition<string> one;
        private IPosition<string> two;
        private IPosition<string> three;
        private IPosition<string> four;
        private IPosition<string> five;
        private IPosition<string> six;
        private IPosition<string> seven;
        private IPosition<string> eight;
        private IPosition<string> nine;
        private IPosition<string> ten;

        /**
         * Creates new Tree objects for testing.
         */
        [TestInitialize]
        public void AddChildUp()
        {
            tree = new GeneralTree<string>();
            emptyTree = new GeneralTree<string>();
        }

        /**
         * Helper method to construct a sample tree
         *
         * One
         * -> Two
         *   -> Six
         *   -> Five
         *   -> Ten
         *     -> Seven
         * -> Three
         *   -> Four
         *     -> Eight
         *     -> Nine
         *
         * Or, visually:
         *                 one
         *            /            \
         *         two                three
         *      /   |     \             |   
         *   six   five   ten          four
         *                  |         /    \
         *                seven     eight  nine              
         */
        [TestMethod]
        private void CreateTree()
        {
            one = tree.AddRoot("one");
            two = tree.AddChild(one, "two");
            three = tree.AddChild(one, "three");
            six = tree.AddChild(two, "six");
            five = tree.AddChild(two, "five");
            ten = tree.AddChild(two, "ten");
            seven = tree.AddChild(ten, "seven");
            four = tree.AddChild(three, "four");
            eight = tree.AddChild(four, "eight");
            nine = tree.AddChild(four, "nine");
        }

        /**
         * Test method for Set().
         */
        [TestMethod]
        public void TestSet()
        {
            one = tree.AddRoot("one");
            Assert.AreEqual("one", tree.Set(one, "ONE"));

            try
            {
                tree.Set(two, "invalid");
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentException);
            }

            Assert.AreEqual("ONE", tree.Set(one, "two"));
        }

        /**
         * Test method for the Size() method.
         */
        [TestMethod]
        public void TestSize()
        {
            Assert.IsTrue(tree.IsEmpty());
            CreateTree();

            Assert.AreEqual(10, tree.Size());
            Assert.IsFalse(tree.IsEmpty());

            tree.Remove(eight);
            Assert.AreEqual(9, tree.Size());
            tree.Remove(nine);
            Assert.AreEqual(8, tree.Size());
            tree.Remove(seven);
            Assert.AreEqual(7, tree.Size());
            tree.Remove(four);
            Assert.AreEqual(6, tree.Size());
            tree.Remove(three);
            Assert.AreEqual(5, tree.Size());
            tree.Remove(five);
            Assert.AreEqual(4, tree.Size());
            tree.Remove(six);
            Assert.AreEqual(3, tree.Size());
            tree.Remove(ten);
            Assert.AreEqual(2, tree.Size());
            tree.Remove(two);
            Assert.AreEqual(1, tree.Size());
            tree.Remove(one);
            Assert.AreEqual(0, tree.Size());

            Assert.IsTrue(tree.IsEmpty());
        }

        /**
         * Test method for NumChildren().
         */
        [TestMethod]
        public void TestNumChildren()
        {
            CreateTree();

            Assert.AreEqual(2, tree.NumChildren(one));
            Assert.AreEqual(3, tree.NumChildren(two));
            Assert.AreEqual(1, tree.NumChildren(three));
            Assert.AreEqual(2, tree.NumChildren(four));
            Assert.AreEqual(0, tree.NumChildren(five));
            Assert.AreEqual(0, tree.NumChildren(six));
            Assert.AreEqual(0, tree.NumChildren(seven));
            Assert.AreEqual(0, tree.NumChildren(eight));
            Assert.AreEqual(0, tree.NumChildren(nine));
            Assert.AreEqual(1, tree.NumChildren(ten));
        }

        /**
         * Test method for Parent().
         */
        [TestMethod]
        public void TestParent()
        {
            CreateTree();

            Assert.AreEqual(null, tree.Parent(one));
            Assert.AreEqual(one, tree.Parent(two));
            Assert.AreEqual(two, tree.Parent(six));
            Assert.AreEqual(two, tree.Parent(five));
            Assert.AreEqual(two, tree.Parent(ten));
            Assert.AreEqual(ten, tree.Parent(seven));
            Assert.AreEqual(one, tree.Parent(three));
            Assert.AreEqual(three, tree.Parent(four));
            Assert.AreEqual(four, tree.Parent(eight));
            Assert.AreEqual(four, tree.Parent(nine));
        }

        /**
         * Test method for IsInternal().
         */
        [TestMethod]
        public void TestIsInternal()
        {
            CreateTree();

            Assert.IsTrue(tree.IsInternal(one));
            Assert.IsTrue(tree.IsInternal(two));
            Assert.IsTrue(tree.IsInternal(three));
            Assert.IsTrue(tree.IsInternal(four));
            Assert.IsFalse(tree.IsInternal(five));
            Assert.IsFalse(tree.IsInternal(six));
            Assert.IsFalse(tree.IsInternal(seven));
            Assert.IsFalse(tree.IsInternal(eight));
            Assert.IsFalse(tree.IsInternal(nine));
            Assert.IsTrue(tree.IsInternal(ten));
        }

        /**
         * Test method for IsLeaf().
         */
        [TestMethod]
        public void IsLeaf()
        {
            CreateTree();

            Assert.IsFalse(tree.IsLeaf(one));
            Assert.IsFalse(tree.IsLeaf(two));
            Assert.IsFalse(tree.IsLeaf(three));
            Assert.IsFalse(tree.IsLeaf(four));
            Assert.IsTrue(tree.IsLeaf(five));
            Assert.IsTrue(tree.IsLeaf(six));
            Assert.IsTrue(tree.IsLeaf(seven));
            Assert.IsTrue(tree.IsLeaf(eight));
            Assert.IsTrue(tree.IsLeaf(nine));
            Assert.IsFalse(tree.IsLeaf(ten));
        }

        /**
         * Test method for IsRoot().
         */
        [TestMethod]
        public void IsRoot()
        {
            CreateTree();
            Assert.IsTrue(tree.IsRoot(one));
            Assert.IsFalse(tree.IsRoot(two));
            Assert.IsFalse(tree.IsRoot(three));
            Assert.IsFalse(tree.IsRoot(four));
            Assert.IsFalse(tree.IsRoot(five));
            Assert.IsFalse(tree.IsRoot(six));
            Assert.IsFalse(tree.IsRoot(seven));
            Assert.IsFalse(tree.IsRoot(eight));
            Assert.IsFalse(tree.IsRoot(nine));
            Assert.IsFalse(tree.IsRoot(ten));
        }

        /**
         * Test method for IsEmpty().
         */
        [TestMethod]
        public void TestIsEmpty()
        {
            Assert.IsTrue(emptyTree.IsEmpty());

            CreateTree();
            Assert.IsFalse(tree.IsEmpty());

            tree.Remove(eight);
            Assert.AreEqual(9, tree.Size());
            tree.Remove(nine);
            Assert.AreEqual(8, tree.Size());
            tree.Remove(seven);
            Assert.AreEqual(7, tree.Size());
            tree.Remove(four);
            Assert.AreEqual(6, tree.Size());
            tree.Remove(three);
            Assert.AreEqual(5, tree.Size());
            tree.Remove(five);
            Assert.AreEqual(4, tree.Size());
            tree.Remove(six);
            Assert.AreEqual(3, tree.Size());
            tree.Remove(ten);
            Assert.AreEqual(2, tree.Size());
            tree.Remove(two);
            Assert.AreEqual(1, tree.Size());
            tree.Remove(one);
            Assert.AreEqual(0, tree.Size());

            Assert.IsTrue(tree.IsEmpty());
        }

        /**
         * Test method for traversing a Tree in pre order.
         */
        [TestMethod]
        public void TestPreOrder()
        {
            CreateTree();

            IEnumerator<IPosition<string>> pre = tree.PreOrder().GetEnumerator();

            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual(one, pre.Current);
            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual(two, pre.Current);
            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual(six, pre.Current);
            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual(five, pre.Current);
            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual(ten, pre.Current);
            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual(seven, pre.Current);
            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual(three, pre.Current);
            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual(four, pre.Current);
            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual(eight, pre.Current);
            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual(nine, pre.Current);
        }

        /**
         * Test method for iterator().
         */
        [TestMethod]
        public void TestIterator()
        {
            CreateTree();
            IEnumerator<string> pre = tree.GetEnumerator();

            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual("one", pre.Current);
            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual("two", pre.Current);
            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual("six", pre.Current);
            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual("five", pre.Current);
            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual("ten", pre.Current);
            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual("seven", pre.Current);
            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual("three", pre.Current);
            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual("four", pre.Current);
            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual("eight", pre.Current);
            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual("nine", pre.Current);
            Assert.IsFalse(pre.MoveNext());
        }

        /**
         * Test method for traversing a Tree in post order.
         */
        [TestMethod]
        public void TestPostOrder()
        {
            CreateTree();
            IEnumerator<IPosition<string>> post = tree.PostOrder().GetEnumerator();

            Assert.IsTrue(post.MoveNext());
            Assert.AreEqual(six, post.Current);
            Assert.IsTrue(post.MoveNext());
            Assert.AreEqual(five, post.Current);
            Assert.IsTrue(post.MoveNext());
            Assert.AreEqual(seven, post.Current);
            Assert.IsTrue(post.MoveNext());
            Assert.AreEqual(ten, post.Current);
            Assert.IsTrue(post.MoveNext());
            Assert.AreEqual(two, post.Current);
            Assert.IsTrue(post.MoveNext());
            Assert.AreEqual(eight, post.Current);
            Assert.IsTrue(post.MoveNext());
            Assert.AreEqual(nine, post.Current);
            Assert.IsTrue(post.MoveNext());
            Assert.AreEqual(four, post.Current);
            Assert.IsTrue(post.MoveNext());
            Assert.AreEqual(three, post.Current);
            Assert.IsTrue(post.MoveNext());
            Assert.AreEqual(one, post.Current);
        }

        /**
         * Test method for traversing a Tree in level order.
         */
        [TestMethod]
        public void TestLevelOrder()
        {
            CreateTree();
            IEnumerator<IPosition<string>> level = tree.LevelOrder().GetEnumerator();

            Assert.IsTrue(level.MoveNext());
            Assert.AreEqual(one, level.Current);
            Assert.IsTrue(level.MoveNext());
            Assert.AreEqual(two, level.Current);
            Assert.IsTrue(level.MoveNext());
            Assert.AreEqual(three, level.Current);
            Assert.IsTrue(level.MoveNext());
            Assert.AreEqual(six, level.Current);
            Assert.IsTrue(level.MoveNext());
            Assert.AreEqual(five, level.Current);
            Assert.IsTrue(level.MoveNext());
            Assert.AreEqual(ten, level.Current);
            Assert.IsTrue(level.MoveNext());
            Assert.AreEqual(four, level.Current);
            Assert.IsTrue(level.MoveNext());
            Assert.AreEqual(seven, level.Current);
            Assert.IsTrue(level.MoveNext());
            Assert.AreEqual(eight, level.Current);
            Assert.IsTrue(level.MoveNext());
            Assert.AreEqual(nine, level.Current);
        }

        /**
         * Test method for AddChild().
         */
        [TestMethod]
        public void TestAddChild()
        {
            Assert.IsTrue(tree.IsEmpty());
            IPosition<string> newOne = tree.AddRoot("one");
            Assert.AreEqual(1, tree.Size());
            Assert.IsNull(tree.Parent(newOne));
            Assert.AreEqual("GeneralTree`1[\none\n]", tree.ToString());

        }

        /**
         * Test method for AddRoot().
         */
        [TestMethod]
        public void TestAddRoot()
        {
            Assert.IsTrue(tree.IsEmpty());
            tree.AddRoot("one");

            // Test adding root to Tree that already has a root
            try
            {
                tree.AddRoot("two");
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentException);
            }
        }

        /**
         * Test method for Remove().
         */
        [TestMethod]
        public void TestRemove()
        {
            CreateTree();
            Assert.AreEqual(10, tree.Size());
            Assert.AreEqual(2, tree.NumChildren(four));
            tree.Remove(nine);
            Assert.AreEqual("GeneralTree`1[\none\n two\n  six\n  five\n  ten\n   seven\n three\n  four\n   eight\n]", tree.ToString());
            Assert.AreEqual(9, tree.Size());
            Assert.AreEqual(1, tree.NumChildren(four));

            tree.Remove(three);
            Assert.AreEqual("GeneralTree`1[\none\n two\n  six\n  five\n  ten\n   seven\n four\n  eight\n]", tree.ToString());
            Assert.AreEqual(8, tree.Size());
            Assert.AreEqual(1, tree.NumChildren(four));
        }

        /**
         * Test method for empty trees.
         */
        [TestMethod]
        public void TestEmptyTree()
        {
            GeneralTree<string> newTree = new();
            Assert.IsTrue(newTree.IsEmpty());

            IPosition<string> newOne = newTree.AddRoot("one");
            Assert.IsFalse(newTree.IsEmpty());

            newTree.Remove(newOne);
            Assert.IsTrue(newTree.IsEmpty());
        }

    }
}
