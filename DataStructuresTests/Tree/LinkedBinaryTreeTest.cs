using DataStructures;
using DataStructures.Tree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestProject2.Tree
{
    /**
    * Test class for LinkedBinaryTree.
    * 
    * @author Zach Samuels
    */
    [TestClass]
    public class LinkedBinaryTreeTest
    {

        private LinkedBinaryTree<String> tree;
        private IPosition<String> one;
        private IPosition<String> two;
        private IPosition<String> three;
        private IPosition<String> four;
        private IPosition<String> five;
        private IPosition<String> six;
        private IPosition<String> seven;
        private IPosition<String> eight;
        private IPosition<String> nine;
        private IPosition<String> ten;


        /**
         * Creates new LinkedBinaryTree for testing.
         */
        [TestInitialize]
        public void SetUp()
        {
            tree = new LinkedBinaryTree<String>();
        }

        /**
         * Sample tree to help with testing
         *
         * One
         * -> Two
         *   -> Six
         *   -> Ten
         *     -> Seven
         *     -> Five
         * -> Three
         *   -> Four
         *     -> Eight
         *     -> Nine
         * 
         * Or, visually:
         *                    one
         *                /        \
         *             two          three
         *            /   \            /
         *         six   ten          four
         *              /   \        /     \
         *            seven  five  eight nine    
         */
        private void CreateTree()
        {
            one = tree.AddRoot("one");
            two = tree.AddLeft(one, "two");
            three = tree.AddRight(one, "three");
            six = tree.AddLeft(two, "six");
            ten = tree.AddRight(two, "ten");
            four = tree.AddLeft(three, "four");
            seven = tree.AddLeft(ten, "seven");
            five = tree.AddRight(ten, "five");
            eight = tree.AddLeft(four, "eight");
            nine = tree.AddRight(four, "nine");
        }

        /**
         * Test method for validate().
         */
        [TestMethod]
        public void TestValidate()
        {
            Assert.IsTrue(tree.IsEmpty());
            one = tree.AddRoot("one");
            Assert.AreEqual("LinkedBinaryTree`1[\none\n]", tree.ToString());

            try
            {
                two = tree.AddLeft(two, "one");
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentException);
            }
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
         * Test method for Size().
         */
        [TestMethod]
        public void TestSize()
        {
            Assert.IsTrue(tree.IsEmpty());
            CreateTree();

            Assert.AreEqual(10, tree.Size());
        }

        /**
         * Test method for NumChildren().
         */
        [TestMethod]
        public void TestNumChildren()
        {
            CreateTree();

            Assert.AreEqual(2, tree.NumChildren(one));
            Assert.AreEqual(2, tree.NumChildren(two));
            Assert.AreEqual(1, tree.NumChildren(three));
            Assert.AreEqual(2, tree.NumChildren(four));
            Assert.AreEqual(0, tree.NumChildren(five));
            Assert.AreEqual(0, tree.NumChildren(six));
            Assert.AreEqual(0, tree.NumChildren(seven));
            Assert.AreEqual(0, tree.NumChildren(eight));
            Assert.AreEqual(0, tree.NumChildren(nine));
            Assert.AreEqual(2, tree.NumChildren(ten));
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
            Assert.AreEqual(one, tree.Parent(three));
            Assert.AreEqual(three, tree.Parent(four));
            Assert.AreEqual(ten, tree.Parent(five));
            Assert.AreEqual(two, tree.Parent(six));
            Assert.AreEqual(ten, tree.Parent(seven));
            Assert.AreEqual(four, tree.Parent(eight));
            Assert.AreEqual(four, tree.Parent(nine));
            Assert.AreEqual(two, tree.Parent(ten));
        }

        /**
         * Test method for Iterator().
         */
        [TestMethod]
        public void TestIterator()
        {
            CreateTree();

            IEnumerator<String> inorder = tree.GetEnumerator();          

            Assert.IsTrue(inorder.MoveNext());
            Assert.AreEqual("six", inorder.Current);
            Assert.IsTrue(inorder.MoveNext());
            Assert.AreEqual("two", inorder.Current);
            Assert.IsTrue(inorder.MoveNext());
            Assert.AreEqual("seven", inorder.Current);
            Assert.IsTrue(inorder.MoveNext());
            Assert.AreEqual("ten", inorder.Current);
            Assert.IsTrue(inorder.MoveNext());
            Assert.AreEqual("five", inorder.Current);
            Assert.IsTrue(inorder.MoveNext());
            Assert.AreEqual("one", inorder.Current);
            Assert.IsTrue(inorder.MoveNext());
            Assert.AreEqual("eight", inorder.Current);
            Assert.IsTrue(inorder.MoveNext());
            Assert.AreEqual("four", inorder.Current);
            Assert.IsTrue(inorder.MoveNext());
            Assert.AreEqual("nine", inorder.Current);
            Assert.IsTrue(inorder.MoveNext());
            Assert.AreEqual("three", inorder.Current);

            Assert.IsFalse(inorder.MoveNext());
        }

        /**
         * Test method for Sibling().
         */
        [TestMethod]
        public void TestSibling()
        {
            CreateTree();

            Assert.AreEqual(null, tree.Sibling(one));
            Assert.AreEqual(three, tree.Sibling(two));
            Assert.AreEqual(two, tree.Sibling(three));
            Assert.AreEqual(null, tree.Sibling(four));
            Assert.AreEqual(seven, tree.Sibling(five));
            Assert.AreEqual(ten, tree.Sibling(six));
            Assert.AreEqual(five, tree.Sibling(seven));
            Assert.AreEqual(nine, tree.Sibling(eight));
            Assert.AreEqual(eight, tree.Sibling(nine));
            Assert.AreEqual(six, tree.Sibling(ten));
        }

        /**
         * Test for IsInternal().
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
         * Test method for preOrder().
         */
        [TestMethod]
        public void TestPreOrder()
        {
            CreateTree();

            IEnumerator<IPosition<String>> pre = tree.PreOrder().GetEnumerator();

            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual(one, pre.Current);
            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual(two, pre.Current);
            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual(six, pre.Current);
            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual(ten, pre.Current);
            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual(seven, pre.Current);
            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual(five, pre.Current);
            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual(three, pre.Current);
            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual(four, pre.Current);
            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual(eight, pre.Current);
            Assert.IsTrue(pre.MoveNext());
            Assert.AreEqual(nine, pre.Current);

            Assert.IsFalse(pre.MoveNext());
        }

        /**
         * Test method for postOrder().
         */
        [TestMethod]
        public void TestPostOrder()
        {
            CreateTree();

            IEnumerator<IPosition<String>> post = tree.PostOrder().GetEnumerator();

            Assert.IsTrue(post.MoveNext());
            Assert.AreEqual(six, post.Current);
            Assert.IsTrue(post.MoveNext());
            Assert.AreEqual(seven, post.Current);
            Assert.IsTrue(post.MoveNext());
            Assert.AreEqual(five, post.Current);
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

            Assert.IsFalse(post.MoveNext());
        }

        /**
         * Test method for inOrder().
         */
        [TestMethod]
        public void TestInOrder()
        {
            CreateTree();

            IEnumerator<IPosition<String>> inorder = tree.InOrder().GetEnumerator();

            Assert.IsTrue(inorder.MoveNext());
            Assert.AreEqual(six, inorder.Current);
            Assert.IsTrue(inorder.MoveNext());
            Assert.AreEqual(two, inorder.Current);
            Assert.IsTrue(inorder.MoveNext());
            Assert.AreEqual(seven, inorder.Current);
            Assert.IsTrue(inorder.MoveNext());
            Assert.AreEqual(ten, inorder.Current);
            Assert.IsTrue(inorder.MoveNext());
            Assert.AreEqual(five, inorder.Current);
            Assert.IsTrue(inorder.MoveNext());
            Assert.AreEqual(one, inorder.Current);
            Assert.IsTrue(inorder.MoveNext());
            Assert.AreEqual(eight, inorder.Current);
            Assert.IsTrue(inorder.MoveNext());
            Assert.AreEqual(four, inorder.Current);
            Assert.IsTrue(inorder.MoveNext());
            Assert.AreEqual(nine, inorder.Current);
            Assert.IsTrue(inorder.MoveNext());
            Assert.AreEqual(three, inorder.Current);

            Assert.IsFalse(inorder.MoveNext());
        }

        /**
         * Test method for SetRoot() and AddRoot().
         */
        [TestMethod]
        public void TestRoot()
        {
            one = tree.AddRoot("one");
            two = tree.AddLeft(one, "two");
            two = tree.SetRoot(two);

            try
            {
                tree.AddRoot("three");
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentException);
            }
        }

        /**
         * Test method for IsEmpty().
         */
        [TestMethod]
        public void TestEmptyTree()
        {
            Assert.IsTrue(tree.IsEmpty());
        }

        /**
         * Test method for levelOrder().
         */
        [TestMethod]
        public void TestLevelOrder()
        {
            CreateTree();

            IEnumerator<IPosition<String>> level = tree.LevelOrder().GetEnumerator();

            Assert.IsTrue(level.MoveNext());
            Assert.AreEqual(one, level.Current);
            Assert.IsTrue(level.MoveNext());
            Assert.AreEqual(two, level.Current);
            Assert.IsTrue(level.MoveNext());
            Assert.AreEqual(three, level.Current);
            Assert.IsTrue(level.MoveNext());
            Assert.AreEqual(six, level.Current);
            Assert.IsTrue(level.MoveNext());
            Assert.AreEqual(ten, level.Current);
            Assert.IsTrue(level.MoveNext());
            Assert.AreEqual(four, level.Current);
            Assert.IsTrue(level.MoveNext());
            Assert.AreEqual(seven, level.Current);
            Assert.IsTrue(level.MoveNext());
            Assert.AreEqual(five, level.Current);
            Assert.IsTrue(level.MoveNext());
            Assert.AreEqual(eight, level.Current);
            Assert.IsTrue(level.MoveNext());
            Assert.AreEqual(nine, level.Current);

            Assert.IsFalse(level.MoveNext());
        }

        /**
         * Test method for AddLeft() and AddRight().
         */
        [TestMethod]
        public void TestAddChildren()
        {
            Assert.IsTrue(tree.IsEmpty());
            one = tree.AddRoot("one");
            Assert.AreEqual("LinkedBinaryTree`1[\none\n]", tree.ToString());

            two = tree.AddLeft(one, "two");
            Assert.AreEqual("LinkedBinaryTree`1[\none\n two\n]", tree.ToString());

            three = tree.AddRight(one, "three");
            Assert.AreEqual("LinkedBinaryTree`1[\none\n two\n three\n]", tree.ToString());

            try
            {
                tree.AddLeft(one, "four");
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentException);
            }

            try
            {
                tree.AddRight(one, "five");
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

            try
            {
                tree.Remove(one);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentException);
            }

            Assert.AreEqual("LinkedBinaryTree`1[\none\n two\n  six\n  ten\n   seven\n   five\n three\n  four\n   eight\n   nine\n]", tree.ToString());

            tree.Remove(nine);
            Assert.AreEqual("LinkedBinaryTree`1[\none\n two\n  six\n  ten\n   seven\n   five\n three\n  four\n   eight\n]", tree.ToString());

            tree.Remove(eight);
            Assert.AreEqual("LinkedBinaryTree`1[\none\n two\n  six\n  ten\n   seven\n   five\n three\n  four\n]", tree.ToString());

            tree.Remove(three);
            Assert.AreEqual("LinkedBinaryTree`1[\none\n two\n  six\n  ten\n   seven\n   five\n four\n]", tree.ToString());

            tree.Remove(seven);
            Assert.AreEqual("LinkedBinaryTree`1[\none\n two\n  six\n  ten\n   five\n four\n]", tree.ToString());

            tree.Remove(five);
            Assert.AreEqual("LinkedBinaryTree`1[\none\n two\n  six\n  ten\n four\n]", tree.ToString());

            tree.Remove(six);
            Assert.AreEqual("LinkedBinaryTree`1[\none\n two\n  ten\n four\n]", tree.ToString());

            tree.Remove(two);
            Assert.AreEqual("LinkedBinaryTree`1[\none\n ten\n four\n]", tree.ToString());

            tree.Remove(ten);
            Assert.AreEqual("LinkedBinaryTree`1[\none\n four\n]", tree.ToString());

            tree.Remove(four);
            Assert.AreEqual("LinkedBinaryTree`1[\none\n]", tree.ToString());

            tree.Remove(one);
            Assert.AreEqual("LinkedBinaryTree`1[\n]", tree.ToString());

            Assert.IsTrue(tree.IsEmpty());

            one = tree.AddRoot("one");
            two = tree.AddRight(one, "two");
            three = tree.AddRight(three, "three");

            tree.Remove(one);
        }
    }
}
