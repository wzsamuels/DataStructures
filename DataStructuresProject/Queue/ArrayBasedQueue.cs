using System;

namespace DataStructures.Queue
{
    /**
	 * ArrayBasedQueue extends the AbstractQueue class to implement the Queue ADT
	 * using a circular array of generic types.
	 * 
	 * @author Zach Samuels
	 *
	 * @param <E> The generic data type contained in this Queue.
	 */
    public class ArrayBasedQueue<E> : AbstractQueue<E> 
	{		
		/** A generic array holding the elements in the queue */
		private E[] data;
		/** The index of the front of the queue */
		private int front;
		/** The index of the end of the queue + 1 */
		private int rear;
		/** The current number of elements in the queue */
		private int size;
		/** The default starting capacity of the array */
		private const int DEFAULT_CAPACITY = 10;

		/**
		 * Constructor with one parameter. Creates a new queue with the given capacity.
		 * 
		 * @param initialCapacity The capacity of the new queue.
		 */
		public ArrayBasedQueue(int initialCapacity)
		{
			data = new E[initialCapacity];
			size = 0;
			front = 0;
			rear = 0;
		}

		/** 
		 * Constructor with no parameters. Creates a new Queue with default capacity.
		 */
		public ArrayBasedQueue() : this(DEFAULT_CAPACITY)
		{			
		}

		/**
		 * Adds the given value to the end of this Queue.
		 * 
		 * @param value The value to add to this Queue.
		 */
		public override void Enqueue(E value)
		{
			EnsureCapacity(size + 1);
			data[rear] = value;
			rear = (rear + 1) % data.Length;
			size++;
		}

		/**
		 * Removes and returns the element from the front of this Queue.
		 * 
		 * @return The element removed from the front of this Queue.
		 * @throws NoSuchElementException if this Queue is empty.
		 */
		public override E Dequeue()
		{
			if (size == 0)
				throw new InvalidOperationException(); 

			E temp = data[front];
			front = (front + 1) % data.Length;
			size--;

			return temp;
		}

		/**
		* Returns the element at the front of this Queue without removing it.
		* 
		* @return The element at the front of this Queue.
		* @throws NoSuchElementException if this Queue is empty.
		*/
		public override E Front()
		{
			if (size == 0)
				throw new InvalidOperationException();

			return data[front];
		}

		/**
		 * Gets the number of elements currently in this queue.
		 * 
		 * @return The number of elements in this queue.
		 */
		public override int Size()
		{
			return size;
		}

		/**
		 * Checks if this list has enough room for the given capacity. If not,
		 * the list is double in size.
		 * 
		 * @param minCapacity The new desired capacity of the list. 
		 */
		private void EnsureCapacity(int minCapacity)
		{
			int oldCapacity = data.Length;
			if (minCapacity > oldCapacity)
			{
				int newCapacity = oldCapacity * 2 + 1;
				if (newCapacity < minCapacity)
				{
					newCapacity = minCapacity;
				}
				// Create a new array with the new capacity
				E[] newData = new E[newCapacity];
				// Copy data from the old to new, but move front index to always
				// be index 0
				for (int i = 0; i < size; i++)
				{
					newData[i] = data[front];
					front++;
					if (front == size)
						front = 0;
				}
				// Do a deep copy of the new array to the old one
				data = new E[newCapacity];
				Array.Copy(newData, data, newCapacity);
				front = 0;
				rear = size;
			}
		}
	}
}
