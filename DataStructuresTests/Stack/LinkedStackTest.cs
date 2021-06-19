using System;
using DataStructures.Stack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject2.Stack
{
	/**
	* LinkedStackTest is a test class for LinkedStack.
	* 
	* @author Zach Samuels
	*
	*/
	[TestClass]
	public class LinkedStackTest
	{
		private IStack<string> stack;

		/**
		 * Initializes a new stack object for testing.
		 */
		[TestInitialize]
		public void SetUp()
		{
			stack = new LinkedStack<string>();
		}

		/**
		 * Test for Push() method.
		 */
		[TestMethod]
		public void TestPush()
		{
			Assert.AreEqual(0, stack.Size());
			Assert.IsTrue(stack.IsEmpty());

			stack.Push("one");
			Assert.AreEqual(1, stack.Size());
			Assert.IsFalse(stack.IsEmpty());

			stack.Push("two");
			Assert.AreEqual(2, stack.Size());
			stack.Push("three");
			Assert.AreEqual(3, stack.Size());
			stack.Push("four");
			Assert.AreEqual(4, stack.Size());
			stack.Push("five");
			Assert.AreEqual(5, stack.Size());
			stack.Push("six");
			Assert.AreEqual(6, stack.Size());
		}

		/**
		 * Test for Pop() method.
		 */
		[TestMethod]
		public void TestPop()
		{
			Assert.AreEqual(0, stack.Size());
			stack.Push("one");
			stack.Push("two");
			stack.Push("three");
			stack.Push("four");
			stack.Push("five");
			stack.Push("six");
			Assert.AreEqual(6, stack.Size());

			Assert.AreEqual("six", stack.Pop());
			Assert.AreEqual(5, stack.Size());

			Assert.AreEqual("five", stack.Pop());
			Assert.AreEqual(4, stack.Size());
			Assert.AreEqual("four", stack.Pop());
			Assert.AreEqual(3, stack.Size());
			Assert.AreEqual("three", stack.Pop());
			Assert.AreEqual(2, stack.Size());
			Assert.AreEqual("two", stack.Pop());
			Assert.AreEqual(1, stack.Size());
			Assert.AreEqual("one", stack.Pop());
			Assert.AreEqual(0, stack.Size());
			Assert.IsTrue(stack.IsEmpty());

			try
			{
				stack.Pop();
				Assert.Fail("EmptyStackException should have been thrown.");
			}
			catch (Exception e)
			{
				Assert.IsTrue(e is InvalidOperationException);
			}
		}

		/**
		 * Test for Top() method.
		 */
		[TestMethod]
		public void TestTop()
		{

			// Test calling Top() on empty stack
			Assert.AreEqual(0, stack.Size());
			try
			{
				stack.Top();
				Assert.Fail("EmptyStackException should have been thrown.");
			}
			catch (Exception e)
			{
				Assert.IsTrue(e is InvalidOperationException);
			}

			stack.Push("one");
			Assert.AreEqual("one", stack.Top());
			stack.Push("two");
			Assert.AreEqual("two", stack.Top());
			stack.Push("three");
			Assert.AreEqual("three", stack.Top());
			stack.Push("four");
			Assert.AreEqual("four", stack.Top());
			stack.Push("five");
			Assert.AreEqual("five", stack.Top());
			stack.Push("six");
			Assert.AreEqual("six", stack.Top());
		}
	}
}
