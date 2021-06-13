using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.List
{
    public class SinglyLinkedList<T> : AbstractList<T>, IEnumerable<T> where T : class
    {
        /** The node at the front of the linked list */
        private readonly LinkedListNode front;
        /** The node at the end of the linked list */
        private LinkedListNode tail;
        /** The current number of elements in the linked list */
        private int size;

        /**
		 * Constructor with no parameters. Creates empty list with dummy front and null tail.
		 */
        public SinglyLinkedList()
        {
            front = new LinkedListNode(null);
            tail = null;
            size = 0;
        }

        /**
		 * Adds the given element at the given index in the list.
		 * 
		 * @param index The index to add the new element.
		 * @param value The element to add to the list.
		 */
        public override void Add(int index, T value)
        {
            CheckIndexForAdd(index);
            LinkedListNode current = front.GetNext();
            LinkedListNode previous = front;

            // Find the node at the index
            for (int i = 0; i < index; i++)
            {
                previous = current;
                current = current.GetNext();
            }
            previous.SetNext(new LinkedListNode(value, current));
            // If adding to the end, new node becomes the tail
            if (index == size)
                tail = previous.GetNext();
            size++;
        }

        /**
		 * Gets the value of the last element in the list.
		 * 
		 * @return The value at the end of the list.
		 */
        public override T Last()
        {
            return tail.GetElement();
        }

        /**
		 * Adds the given element to the end of the list.
		 * 
		 * @param value The element to add to the end of the list.
		 */
        public override void AddLast(T value)
        {
            // If the list is empty, the new node needs to be linked to the front
            if (size == 0)
            {
                tail = new LinkedListNode(value);
                front.SetNext(tail);
            }
            // If not empty, the new node becomes the tail
            else
            {
                tail.SetNext(new LinkedListNode(value));
                tail = tail.GetNext();
            }

            size++;
        }

        /**
		 * Gets the list element at the given index.
		 * 
		 * @param index The index of the element to get.
		 * 
		 * @return The element at the given index.
		 */
        public override T GetIndex(int index)
        {
            CheckIndex(index);
            LinkedListNode p = front.GetNext();
            for (int i = 0; i < index; i++)
                p = p.GetNext();
            return p.GetElement();
        }

        /**
		 * Removes and returns the element at the given index.
		 * 
		 * @param index The index of the element to remove.
		 * @return The element previously at the given index.
		 */
        public override T Remove(int index)
        {
            CheckIndex(index);
            LinkedListNode current = front.GetNext();
            LinkedListNode previous = front;
            for (int i = 0; i < index; i++)
            {
                previous = current;
                current = current.GetNext();
            }

            T temp = current.GetElement();
            previous.GetNext();
            previous.SetNext(current.GetNext());
            size--;

            return temp;
        }

        /**
		 * Sets the element at the given index to the given value.
		 * 
		 * @param index The index to set.
		 * @param value The value to set the element to.
		 * @return The element previously at the given index.
		 */
        public override T SetIndex(int index, T value)
        {
            CheckIndex(index);
            LinkedListNode p = front.GetNext();
            for (int i = 0; i < index; i++)
            {
                p = p.GetNext();
            }
            T temp = p.GetElement();
            p.SetElement(value);
            return temp;
        }

        /**
		 * Gets the number of elements in the array list.
		 * 
		 * @return The size of array list.
		 */
        public override int Size()
        {
            return size;
        }

        /**
		 * Creates a new iterator at the beginning of this SinglyLinkedList
		 * to iterate its the elements.
		 * 
		 * @return An iterator object for this SinglyLinkedList.
		 */

        public IEnumerator<T> GetEnumerator()
        {
            return new ElementIterator(this, front.GetNext());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /**
		 * LinkedListNode provides the functionality of a node in a linked list.
		 * Each node contains data and a link to the next node in the list.
		 *
		 * @param <E> The generic data type contained in the node.
		 */
        private class LinkedListNode
        {

            private T data;
            private LinkedListNode next;

            /**
			 * Constructor with one parameter. Sets the data contained in the node.
			 * 
			 * @param data The element stored in the node.
			 */
            public LinkedListNode(T data)
            {
                this.data = data;
                next = null;
            }

            /**
			 * Constructor with two parameters. Sets the data and the link to the
			 * next node.
			 * 
			 * @param data The element stored in the node.
			 * @param next The next node in the linked list.
			 */
            public LinkedListNode(T data, LinkedListNode next)
            {
                this.data = data;
                this.next = next;
            }

            /**
			 * Gets the next node in the linked list.
			 * 
			 * @return The node pointed to by this node's next field.
			 */
            public LinkedListNode GetNext()
            {
                return next;
            }

            /**
			 * Gets the element contained in this node.
			 * 
			 * @return The element at this node.
			 */
            public T GetElement()
            {
                return data;
            }

            /**
			 * Sets the link to the next node in the list.
			 * 
			 * @param next The node that follow this node in the list.
			 */
            public void SetNext(LinkedListNode next)
            {
                this.next = next;
            }

            /**
			 * Sets the element contained at this node.
			 * 
			 * @param data The element to store at this node.
			 */
            public void SetElement(T data)
            {
                this.data = data;
            }
        }

        /**
		* ElementIterator provides an iterator to iterate over the elements
		* of a singly linked list.
		*/
        private class ElementIterator : IEnumerator<T>
        {
            // Keep track of the next node that will be processed
            private LinkedListNode current;
            private int position = -1;
            private readonly SinglyLinkedList<T> parent;

            /**
             * Constructor with one parameter.
             * 
             * @param start The position to where iteration begins.
             */
            public ElementIterator(SinglyLinkedList<T> parent, LinkedListNode start)
            {
                this.parent = parent;
                current = start;
            }

            public T Current
            {
                get
                {
                    try
                    {
                        return current.GetElement();
                    }
                    catch (NullReferenceException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            public void Reset()
            {
                position = -1;
                current = parent.front;
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            /**
             * Advances the iterator and returns the next element in the list.
             * 
             * @return The next element in the list.
             * @throws NoSuchElementException if the no next element exists.
             */
            public bool MoveNext()
            {
                position++;
                current = current.GetNext();

                return position < parent.Size();
            }
        }
    }
}
