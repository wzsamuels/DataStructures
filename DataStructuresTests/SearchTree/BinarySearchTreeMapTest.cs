using DataStructures;
using DataStructures.Map;
using DataStructures.SearchTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestProject2.SearchTree
{
	/**
	* Test class for BinarySearchTreeMap.
	* 
	* @author Zach Samuels
	*/
	[TestClass]
	public class BinarySearchTreeMapTest
	{

		BinarySearchTreeMap<int, string> tree;

		/**
		 * Creates a new BinarySearchTreeMap for testing.
		 */
		[TestInitialize]
		public void SetUp()
		{
			tree = new BinarySearchTreeMap<int, string>();
		}

		/**
		 * Test method for Put().
		 */
		[TestMethod]
		public void TestPut()
		{
			Assert.AreEqual(0, tree.Size());
			Assert.IsTrue(tree.IsEmpty());
			Assert.AreEqual("BalanceableBinaryTree[\n\n]", tree.ToString());

			tree.Put(5, "five");
			Assert.AreEqual(1, tree.Size());
			Assert.IsFalse(tree.IsEmpty());

			tree.Put(8, "eight");
			Assert.AreEqual(2, tree.Size());

			tree.Put(2, "two");
			Assert.AreEqual(3, tree.Size());

			tree.Put(1, "one");
			tree.Put(3, "three");
			tree.Put(4, "four");
			tree.Put(9, "nine");
			tree.Put(7, "seven");
			tree.Put(10, "ten");
			tree.Put(11, "eleven");

			IEnumerator<IPosition<IMap<int, string>.IEntry>> it = tree.InOrder().GetEnumerator();
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(1, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(2, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(3, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(4, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(5, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(7, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(8, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(9, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(10, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(11, (int)it.Current.GetElement().GetKey());
			Assert.IsFalse(it.MoveNext());
		}

		/**
		 * Test method for Get().
		 */
		[TestMethod]
		public void TestGet()
		{
			tree.Put(7, "seven");
			tree.Put(1, "one");
			tree.Put(3, "three");
			tree.Put(4, "four");
			tree.Put(9, "nine");
			tree.Put(10, "ten");
			tree.Put(11, "eleven");

			Assert.IsTrue(tree.IsRoot(tree.Root()));
			Assert.AreEqual(7, tree.Size());
			Assert.AreEqual(2, tree.NumChildren(tree.Root()));

			Assert.AreEqual("one", tree.GetValue(1));
			Assert.AreEqual("three", tree.GetValue(3));
			Assert.AreEqual("four", tree.GetValue(4));
			Assert.AreEqual("nine", tree.GetValue(9));

			// Test iterators

			IEnumerator<IPosition<IMap<int, string>.IEntry>> it = tree.PreOrder().GetEnumerator();
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(7, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(1, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(3, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(4, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(9, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(10, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(11, (int)it.Current.GetElement().GetKey());
			Assert.IsFalse(it.MoveNext());

			it = tree.PostOrder().GetEnumerator();
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(4, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(3, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(1, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(11, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(10, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(9, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(7, (int)it.Current.GetElement().GetKey());
			Assert.IsFalse(it.MoveNext());

			it = tree.Children(tree.Root()).GetEnumerator();
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(1, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(9, (int)it.Current.GetElement().GetKey());

			IEnumerator<IMap<int, string>.IEntry> ite = tree.EntryIterator().GetEnumerator();
			Assert.IsTrue(ite.MoveNext());
			Assert.AreEqual(1, (int)ite.Current.GetKey());
			Assert.IsTrue(ite.MoveNext());
			Assert.AreEqual(3, (int)ite.Current.GetKey());
			Assert.IsTrue(ite.MoveNext());
			Assert.AreEqual(4, (int)ite.Current.GetKey());
			Assert.IsTrue(ite.MoveNext());
			Assert.AreEqual(7, (int)ite.Current.GetKey());
			Assert.IsTrue(ite.MoveNext());
			Assert.AreEqual(9, (int)ite.Current.GetKey());
			Assert.IsTrue(ite.MoveNext());
			Assert.AreEqual(10, (int)ite.Current.GetKey());
			Assert.IsTrue(ite.MoveNext());
			Assert.AreEqual(11, (int)ite.Current.GetKey());
			Assert.IsFalse(ite.MoveNext());
		}

		/**
		 * Test method for Remove().
		 */
		[TestMethod]
		public void TestRemove()
		{
			tree.Put(1, "one");
			Assert.AreEqual(1, tree.Size());

			Assert.IsNull(tree.Remove(10));
			Assert.AreEqual(1, tree.Size());

			Assert.AreEqual("one", tree.Remove(1));
			Assert.AreEqual(0, tree.Size());

			tree.Put(5, "five");
			tree.Put(8, "eight");
			tree.Put(2, "two");
			tree.Put(1, "one");
			tree.Put(3, "three");
			tree.Put(4, "four");
			tree.Put(9, "nine");
			tree.Put(7, "seven");
			tree.Put(10, "ten");
			tree.Put(11, "eleven");

			// Remove key with no children;
			tree.Remove(11);
			Assert.AreEqual(10, (int)tree.Right(tree.Right(tree.Right(tree.Root()))).GetElement().GetKey());

			// Remove key with one Right child
			tree.Remove(9);
			Assert.AreEqual(10, (int)tree.Right(tree.Right(tree.Root())).GetElement().GetKey());

			// Remove key with two children
			tree.Remove(8);
			Assert.AreEqual(10, (int)tree.Right(tree.Root()).GetElement().GetKey());

			// Remove key with one Left child
			tree.Remove(10);
			Assert.AreEqual(7, (int)tree.Right(tree.Root()).GetElement().GetKey());
		}
	}
}
