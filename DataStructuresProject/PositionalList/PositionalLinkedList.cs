using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.PositionalList
{
	/**
	 * PositionalLinkedList implements the Position and PositionalList classes
	 * to provided the functionality for a positional linked list.
	 *
	 * @author Zach Samuels
	 *
	 * @param <E> The generic data type stored by the list.
	 */
	class PositionalLinkedList<E> where E : class, IPositionalList<E>, IEnumerable<E>
	{
		/** The node at the beginning of the list */
		private PositionalNode<E> front;
		/** The node at the end of the list */
		private PositionalNode<E> tail;
		/** The current number of elements in the list */
		private int size;

		/**
		 * Constructor with no parameters
		 */
		public PositionalLinkedList()
		{
			front = new PositionalNode<E>(null);
			tail = new PositionalNode<E>(null, null, front);
			front.SetNext(tail);
			size = 0;
		}

		/**
		 * Creates a new iterator at the beginning of this PositionalLinkedList
		 * to iterate its the elements.
		 * 
		 * @return An iterator object for this PositionalLinkedList.
		 */
		public IEnumerator<E> iterator()
		{
			// we start at front.GetNext() because front is a dummy/sentinel node
			return new ElementIterator(front.GetNext());
		}

		/**
		 * Inserts into the list a new element with the given value after the 
		 * given position.
		 * 
		 * @param p The position to insert the new node after.
		 * @param value The value of the new element to add.  
		 * 
		 * @return The new element as a PositionalNode.
		 */
		public IPosition<E> AddAfter(IPosition<E> p, E value)
		{
			PositionalNode<E> node = Validate(p);
			return AddBetween(value, node.GetNext(), node);
		}

		/**
		 * Inserts a new element into the list with the given value before the 
		 * given position.
		 * 
		 * @param p The position to insert the new node before.
		 * @param value The value of the new element to add.  
		 * 
		 * @return The new element as a PositionalNode.
		 */
		public IPosition<E> AddBefore(IPosition<E> p, E value)
		{
			PositionalNode<E> node = Validate(p);
			return AddBetween(value, node, node.GetPrevious());
		}

		/**
		 * Inserts a new element at the beginning of the list with the given value.
		 * 
		 * @param value The value of the new element to add.  
		 * 
		 * @return The new element as a PositionalNode.
		 */
		public IPosition<E> AddFirst(E value)
		{
			return AddBetween(value, front.GetNext(), front);
		}

		/**
		 * Inserts a new element at the end of the list with the given value.
		 * 
		 * @param value The value of the new element to add.  
		 * 
		 * @return The new element as a PositionalNode.
		 */
		public IPosition<E> AddLast(E value)
		{
			return AddBetween(value, tail, tail.GetPrevious());
		}

		/**
		 * Gets the position after the given position.
		 * 
		 * @param p The position to find the position after.
		 * @return The position after the given position as a PositionalNode.
		 */
		public IPosition<E> After(IPosition<E> p)
		{
			PositionalNode<E> node = Validate(p);
			if (node.GetNext() == tail)
				return null;

			return node.GetNext();
		}

		/**
		 * Gets the position before the given position.
		 * 
		 * @param p The position to find the position before.
		 * @return The position before the given position as a PositionalNode.
		 */
		public IPosition<E> Before(IPosition<E> p)
		{
			PositionalNode<E> node = Validate(p);
			if (node.GetPrevious() == front)
				return null;

			return node.GetPrevious();
		}

		/**
		 * Gets the first position of this PositionalLinkedList.
		 * 
		 * @return The first position as a PositionalNode.
		 */
		public IPosition<E> First()
		{
			// If front is linked to tail, the list is empty
			if (front.GetNext() == tail)
				return null;

			return front.GetNext();
		}

		/**
		 * Determines if the this PositionalLinkedList is empty.
		 * 
		 * @return True if this list is empty, false if this list isn't empty.
		 */
		public bool IsEmpty()
		{
			return size == 0;
		}

		/**
		 * Gets the last position of this PositionalLinkedList.
		 * 
		 * @return The last position as a PositionalNode.
		 */
		public IPosition<E> Last()
		{
			// If front is linked to tail, the list is empty
			if (front.GetNext() == tail)
				return null;

			return tail.GetPrevious();
		}

		/**
		 * Creates a new positional iterator for this PositionalLinkedList.
		 * 
		 * @return An iterable object starting at the beginning of this linked list.
		 */
		public IEnumerator<IPosition<E>> positions()
		{
			return new PositionIterable();
		}

		/**
		 * Removes the given position from this list.
		 * 
		 * @param p The position to remove from the list.
		 * 
		 * @return The position that was removed from the list.
		 */
		public E Remove(IPosition<E> p)
		{
			PositionalNode<E> node = Validate(p);

			PositionalNode<E> prev = node.GetPrevious();
			PositionalNode<E> next = node.GetNext();
			prev.SetNext(next);
			next.SetPrevious(prev);
			size--;

			return node.GetElement();
		}

		/**
		 * Sets the element at the given position to the given value.
		 * 
		 * @param p The position to set.
		 * @param value The value to set the element to.
		 * @return The element previously at the given position.
		 */
		public E Set(IPosition<E> p, E value)
		{
			PositionalNode<E> node = Validate(p);

			E temp = node.GetElement();
			node.SetElement(value);

			return temp;
		}

		/**
		 * Gets the number of elements in the positional list.
		 * 
		 * @return The size of positional list.
		 */
		public int Size()
		{
			return size;
		}

		/**
		 * Determines if the given Position is a valid PositionalNode object.
		 * 
		 * @param p The Position to validate.
		 * @return The Position cast to a PositionalNode.
		 */
		private PositionalNode<E> Validate(IPosition<E> p)
		{
			if (p.GetType() == typeof(PositionalNode<E>)) {
				return (PositionalNode<E>)p;
			}
			throw new ArgumentException("Position is not a valid positional list node.");
		}

		/**
		 * Adds the given element to this list between the given positions.
		 * 
		 * @param value The element to add to the list.
		 * @param next The position after the new position.
		 * @param prev The position before the new position.
		 * @return The new positional node.
		 */
		private IPosition<E> AddBetween(E value, PositionalNode<E> next, PositionalNode<E> prev)
		{
			PositionalNode<E> newNode = new PositionalNode<E>(value, next, prev);
			// Link the new node into the list
			next.SetPrevious(newNode);
			prev.SetNext(newNode);
			size++;

			return newNode;
		}

		/**
		 * PositionalNode represents the individual nodes that make up a PositionalLinkedList.
		 *	 
		 * @param <E> The generic data type stored in each node.
		 */
		private class PositionalNode<E> : IPosition<E> {

			private E element;
			private PositionalNode<E> next;
			private PositionalNode<E> previous;

			/**
			 * Constructor with one parameter. Value is set, next and previous are null.
			 * 
			 * @param value The value to store in the new node.
			 */
			public PositionalNode(E value) : this(value, null)
			{
			}

			/**
			 * Constructor with two parameters. Value and next are set,
			 * previous is null.
			 * 
			 * @param value The value to store in the new node.
			 * @param next The link to the following node in the list.
			 */
			public PositionalNode(E value, PositionalNode<E> next) : this(value, next, null)
			{				
			}

			/**
			 * Constructor with two parameters. Value, next and previous
			 * are set.
			 * 
			 * @param value The value to store in the new node.
			 * @param next The link to the following node in the list.
			 * @param prev The link to the previous node in the list.
			 */
			public PositionalNode(E value, PositionalNode<E> next, PositionalNode<E> prev)
			{
				SetElement(value);
				SetNext(next);
				SetPrevious(prev);
			}

			/**
			 * Setter for this PositionalNode's previous node.
			 * 
			 * @param prev The new link to the node before this PositionalNode.
			 */
			public void SetPrevious(PositionalNode<E> prev)
			{
				previous = prev;
			}

			/**
			 * Getter for this PositionalNode's previous node.
			 * 
			 * @return The previous node before this PositionalNode.
			 */
			public PositionalNode<E> GetPrevious()
			{
				return previous;
			}

			/**
			 * Setter for this PositionalNode's next node.
			 * 
			 * @param next The new link to the node after this PositionalNode.
			 */
			public void SetNext(PositionalNode<E> next)
			{
				this.next = next;
			}

			/**
			 * Getter for this PositionalNode's next node.
			 * 
			 * @return The next node after this PositionalNode.
			 */
			public PositionalNode<E> GetNext()
			{
				return next;
			}

			/**
			 * Getter for this PositionalNode's element.
			 * 
			 * @return This node's element
			 */
			public E GetElement()
			{
				return element;
			}

			/**
			 * Setter for this PositionalNode's element.
			 * 
			 * @param element This node's new element.
			 */
			public void SetElement(E element)
			{
				this.element = element;
			}
		}
	}
}
