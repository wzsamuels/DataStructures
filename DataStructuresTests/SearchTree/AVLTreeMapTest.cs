using DataStructures;
using DataStructures.Map;
using DataStructures.SearchTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestProject2.SearchTree
{
	/**
	* Test class for AVLTreeMap.
	* 
	* @author Zach Samuels
	*
	*/
	[TestClass]
	public class AVLTreeMapTest
	{

		private BinarySearchTreeMap<int, string> tree;

		/**
		 * Create new AVLTreeMap for testing.
		 */
		[TestInitialize]
		public void SetUp()
		{
			tree = new AVLTreeMap<int, string>(null);
			tree = new AVLTreeMap<int, string>();
		}

		/**
		 * Test method for Put().
		 */
		[TestMethod]
		public void TestPut()
		{
			Assert.AreEqual(0, tree.Size());
			Assert.IsTrue(tree.IsEmpty());

			Assert.IsNull(tree.Put(5, "string5"));
			Assert.AreEqual(1, tree.Size());
			Assert.AreEqual(5, (int)tree.Root().GetElement().GetKey());
			Assert.IsNull(tree.Left(tree.Root()).GetElement());

			Assert.IsNull(tree.Put(10, "string10"));
			Assert.AreEqual(5, (int)tree.Root().GetElement().GetKey());
			Assert.IsNull(tree.Left(tree.Root()).GetElement());
			Assert.AreEqual(10, (int)tree.Right(tree.Root()).GetElement().GetKey());
			Assert.IsNull(tree.Left(tree.Right(tree.Root())).GetElement());
			Assert.IsNull(tree.Right(tree.Right(tree.Root())).GetElement());
			Assert.AreEqual(2, tree.Size());

			// Single rotation on Right side
			Assert.IsNull(tree.Put(15, "string15"));
			IEnumerator<IPosition<IMap<int, string>.IEntry>> it = tree.LevelOrder().GetEnumerator();
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(10, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(5, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(15, (int)it.Current.GetElement().GetKey());

			// Single rotation on Left side
			Assert.IsNull(tree.Put(3, "string3"));
			Assert.IsNull(tree.Put(2, "string2"));
			it = tree.LevelOrder().GetEnumerator();
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(10, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(3, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(15, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(2, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(5, (int)it.Current.GetElement().GetKey());

			// Double rotation on Left side
			tree = new AVLTreeMap<int, string>();
			Assert.IsNull(tree.Put(5, "string5"));
			Assert.IsNull(tree.Put(3, "string5"));
			Assert.IsNull(tree.Put(4, "string5"));
			it = tree.LevelOrder().GetEnumerator();
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(4, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(3, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(5, (int)it.Current.GetElement().GetKey());

			// Double rotation on Right side
			tree = new AVLTreeMap<int, string>();
			Assert.IsNull(tree.Put(5, "string5"));
			Assert.IsNull(tree.Put(10, "string5"));
			Assert.IsNull(tree.Put(8, "string5"));
			it = tree.LevelOrder().GetEnumerator();
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(8, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(5, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(10, (int)it.Current.GetElement().GetKey());

			tree = new AVLTreeMap<int, string>();
			Assert.IsNull(tree.Put(20, "string5"));
			Assert.IsNull(tree.Put(30, "string5"));
			Assert.IsNull(tree.Put(40, "string5"));
			Assert.IsNull(tree.Put(50, "string5"));
			Assert.IsNull(tree.Put(60, "string5"));
			Assert.IsNull(tree.Put(10, "string5"));
			Assert.IsNull(tree.Put(25, "string5"));
			Assert.IsNull(tree.Put(5, "string5"));
			Assert.IsNull(tree.Put(15, "string5"));
			Assert.IsNull(tree.Put(22, "string5"));
			Assert.IsNull(tree.Put(27, "string5"));
			Assert.IsNull(tree.Put(16, "string5"));

			Assert.IsNull(tree.Put(38, "string5"));
			Assert.IsNull(tree.Put(39, "string5"));
			Assert.IsNull(tree.Put(37, "string5"));
			Assert.IsNull(tree.Put(59, "string5"));
			Assert.IsNull(tree.Put(58, "string5"));
			Assert.IsNull(tree.Put(65, "string5"));
			Assert.IsNull(tree.Put(64, "string5"));
			Assert.IsNull(tree.Put(63, "string5"));
		}

		/**
		 * Test method for Get().
		 */
		[TestMethod]
		public void TestGet()
		{
			Assert.IsTrue(tree.IsEmpty());
			Assert.IsNull(tree.Put(3, "string3"));
			Assert.IsFalse(tree.IsEmpty());

			Assert.AreEqual("string3", tree.GetValue(3));
			Assert.AreEqual(null, tree.GetValue(6));
			Assert.AreEqual(null, tree.GetValue(0));

			Assert.IsNull(tree.Put(5, "string5"));
			Assert.IsNull(tree.Put(4, "string4"));
			Assert.IsNull(tree.Put(15, "string15"));

			Assert.AreEqual("string5", tree.GetValue(5));
			Assert.AreEqual("string4", tree.GetValue(4));
			Assert.AreEqual("string15", tree.GetValue(15));
		}

		/**
		 * Test method for Remove().
		 */
		[TestMethod]
		public void TestRemove()
		{
			tree = new AVLTreeMap<int, string>();

			Assert.IsNull(tree.Put(8, "8"));
			Assert.IsNull(tree.Put(3, "3"));
			Assert.IsNull(tree.Put(10, "10"));
			Assert.IsNull(tree.Put(11, "11"));
			Assert.IsNull(tree.Put(4, "4"));
			Assert.IsNull(tree.Put(6, "6"));
			Assert.IsNull(tree.Put(5, "5"));
			Assert.IsNull(tree.Put(7, "7"));
			Assert.IsNull(tree.Put(2, "2"));

			Assert.AreEqual("8", tree.Remove(8));

			tree = new AVLTreeMap<int, string>();
			Assert.IsTrue(tree.IsEmpty());

			Assert.IsNull(tree.Put(1, "one"));
			Assert.AreEqual(1, (int)tree.Root().GetElement().GetKey());

			Assert.IsNull(tree.Put(2, "two"));
			IEnumerator<IPosition<IMap<int, string>.IEntry>> it = tree.InOrder().GetEnumerator();
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(1, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(2, (int)it.Current.GetElement().GetKey());

			Assert.IsNull(tree.Put(3, "three"));
			it = tree.InOrder().GetEnumerator();
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(1, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(2, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(3, (int)it.Current.GetElement().GetKey());
			Assert.AreEqual(2, (int)tree.Root().GetElement().GetKey());

			Assert.IsNull(tree.Put(4, "four"));
			it = tree.InOrder().GetEnumerator();
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(1, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(2, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(3, (int)it.Current.GetElement().GetKey());
			Assert.IsTrue(it.MoveNext());
			Assert.AreEqual(4, (int)it.Current.GetElement().GetKey());

			Assert.AreEqual(2, (int)tree.Root().GetElement().GetKey());
			Assert.AreEqual(1, (int)tree.Left(tree.Root()).GetElement().GetKey());
			Assert.AreEqual(3, (int)tree.Right(tree.Root()).GetElement().GetKey());
			Assert.AreEqual(4, (int)tree.Right(tree.Right(tree.Root())).GetElement().GetKey());

			Assert.IsNull(tree.Put(5, "five"));
			it = tree.InOrder().GetEnumerator();
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

			Assert.AreEqual(2, (int)tree.Root().GetElement().GetKey());
			Assert.AreEqual(1, (int)tree.Left(tree.Root()).GetElement().GetKey());
			Assert.AreEqual(4, (int)tree.Right(tree.Root()).GetElement().GetKey());
			Assert.AreEqual(5, (int)tree.Right(tree.Right(tree.Root())).GetElement().GetKey());
			Assert.AreEqual(3, (int)tree.Left(tree.Right(tree.Root())).GetElement().GetKey());

			Assert.IsNull(tree.Put(6, "six"));
			Assert.IsNull(tree.Put(7, "seven"));
			Assert.AreEqual(7, tree.Size());
			Assert.IsFalse(tree.IsEmpty());

			Assert.AreEqual(4, (int)tree.Root().GetElement().GetKey());

			Assert.IsNull(tree.Remove(0));
			Assert.AreEqual(7, tree.Size());
			Assert.IsFalse(tree.IsEmpty());
			Assert.AreEqual(4, (int)tree.Root().GetElement().GetKey());
			Assert.AreEqual(2, (int)tree.Left(tree.Root()).GetElement().GetKey());
			Assert.AreEqual(1, (int)tree.Left(tree.Left(tree.Root())).GetElement().GetKey());
			Assert.AreEqual(3, (int)tree.Right(tree.Left(tree.Root())).GetElement().GetKey());
			Assert.AreEqual(6, (int)tree.Right(tree.Root()).GetElement().GetKey());
			Assert.AreEqual(5, (int)tree.Left(tree.Right(tree.Root())).GetElement().GetKey());
			Assert.AreEqual(7, (int)tree.Right(tree.Right(tree.Root())).GetElement().GetKey());

			tree = new AVLTreeMap<int, string>();
			Assert.IsNull(tree.Put(20, "string20"));
			Assert.IsNull(tree.Put(30, "string30"));
			Assert.IsNull(tree.Put(40, "string40"));
			Assert.IsNull(tree.Put(50, "string50"));
			Assert.IsNull(tree.Put(60, "string60"));
			Assert.IsNull(tree.Put(10, "string10"));
			Assert.IsNull(tree.Put(25, "string25"));
			Assert.IsNull(tree.Put(5, "string5"));
			Assert.IsNull(tree.Put(15, "string12"));
			Assert.IsNull(tree.Put(22, "string22"));
			Assert.IsNull(tree.Put(27, "string27"));
			Assert.IsNull(tree.Put(16, "string16"));

			Assert.IsNull(tree.Put(38, "string38"));
			Assert.IsNull(tree.Put(39, "string39"));
			Assert.IsNull(tree.Put(37, "string37"));
			Assert.IsNull(tree.Put(59, "string59"));
			Assert.IsNull(tree.Put(58, "string58"));
			Assert.IsNull(tree.Put(65, "string65"));
			Assert.IsNull(tree.Put(64, "string64"));
			Assert.IsNull(tree.Put(63, "string63"));

			// Remove Root
			Assert.AreEqual("string39", tree.Remove(39));

			// Remove one Right child
			Assert.AreEqual("string60", tree.Remove(60));

			// Remove leaf
			Assert.AreEqual("string65", tree.Remove(65));

			// Remove one Left child
			Assert.AreEqual("string64", tree.Remove(64));

			Assert.AreEqual("string59", tree.Remove(59));
			Assert.AreEqual("string20", tree.Remove(20));
			Assert.AreEqual("string22", tree.Remove(22));
		}
	}
}
