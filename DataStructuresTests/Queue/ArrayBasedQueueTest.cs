using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataStructures.Queue;

namespace TestProject2.Queue
{
    /**
     * ArrayBasedQueueTest is a test class for the ArrayBasedQueue class.
     * 
     * @author Zach Samuels
     * 
     */
    [TestClass]
    public class ArrayBasedQueueTest
    {

        private IQueue<string> queue;

        /**
         * Initialize an empty queue for testing.
         */
        [TestInitialize]
        public void SetUp()
        {
            queue = new ArrayBasedQueue<string>();
        }

        /**
         * Test for Enqueue() method.
         */
        [TestMethod]
        public void TestEnqueue()
        {
            Assert.AreEqual(0, queue.Size());
            Assert.IsTrue(queue.IsEmpty());

            queue.Enqueue("one");
            Assert.AreEqual(1, queue.Size());
            Assert.IsFalse(queue.IsEmpty());

            queue.Enqueue("two");
            Assert.AreEqual(2, queue.Size());
            queue.Enqueue("three");
            Assert.AreEqual(3, queue.Size());
            queue.Enqueue("four");
            Assert.AreEqual(4, queue.Size());
            queue.Enqueue("five");
            Assert.AreEqual(5, queue.Size());
            queue.Enqueue("six");
            Assert.AreEqual(6, queue.Size());
            queue.Enqueue("seven");
            Assert.AreEqual(7, queue.Size());
            queue.Enqueue("eight");
            Assert.AreEqual(8, queue.Size());
            queue.Enqueue("nine");
            Assert.AreEqual(9, queue.Size());
            queue.Enqueue("ten");
            Assert.AreEqual(10, queue.Size());
            queue.Enqueue("eleven");
            Assert.AreEqual(11, queue.Size());
            queue.Enqueue("twelve");
            Assert.AreEqual(12, queue.Size());
        }

        /**
         * Test for Dequeue() method.
         */
        [TestMethod]
        public void TestDequeue()
        {
            Assert.AreEqual(0, queue.Size());
            queue.Enqueue("one");
            queue.Enqueue("two");
            queue.Enqueue("three");
            queue.Enqueue("four");
            queue.Enqueue("five");
            queue.Enqueue("six");
            Assert.AreEqual(6, queue.Size());

            Assert.AreEqual("one", queue.Dequeue());
            Assert.AreEqual(5, queue.Size());

            Assert.AreEqual("two", queue.Dequeue());
            Assert.AreEqual(4, queue.Size());

            Assert.AreEqual("three", queue.Dequeue());
            Assert.AreEqual(3, queue.Size());

            Assert.AreEqual("four", queue.Dequeue());
            Assert.AreEqual(2, queue.Size());

            Assert.AreEqual("five", queue.Dequeue());
            Assert.AreEqual(1, queue.Size());

            Assert.AreEqual("six", queue.Dequeue());
            Assert.AreEqual(0, queue.Size());
            Assert.IsTrue(queue.IsEmpty());

            try
            {
                queue.Dequeue();
                Assert.Fail("InvalidOperationException should have been thrown.");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidOperationException);
            }
        }

        /**
         * Test for Front() method.
         */
        [TestMethod]
        public void TestFront()
        {
            Assert.AreEqual(0, queue.Size());

            try
            {
                queue.Front();
                Assert.Fail("InvalidOperationException should have been thrown.");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidOperationException);
            }

            queue.Enqueue("one");
            Assert.AreEqual("one", queue.Front());
            queue.Enqueue("two");
            Assert.AreEqual("one", queue.Front());
            queue.Enqueue("three");
            Assert.AreEqual("one", queue.Front());
            queue.Enqueue("four");
            Assert.AreEqual("one", queue.Front());
            queue.Enqueue("five");
            Assert.AreEqual("one", queue.Front());
            queue.Enqueue("six");
            Assert.AreEqual("one", queue.Front());
        }
    }
}
