using DataStructures.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Stack
{
	/**
	* LinkedStack extends AbstractStack in order to implement the Stack ADT. 
	* 
	* @author Zach Samuels
	*
	* @param <E> The generic type data held in this Stack.
	*/
	public class LinkedStack<E> : AbstractStack<E>
		where E : class
	{
		private readonly SinglyLinkedList<E> list;

		/**
		 * Constructor with no parameters. Creates a new empty Stack.
		 */
		public LinkedStack()
		{
			list = new SinglyLinkedList<E>();
		}

		/**
		 * Adds the given value to the top of this Stack.
		 * 
		 * @param value The value to add to this Stack.
		 */
		
		public override void Push(E value)
		{
			list.AddFirst(value);
		}

		/**
		 * Removes and returns the value at the top of this Stack.
		 * 
		 * @return The value popped from this Stack
		 */
		
		public override E Pop()
		{
			if (list.IsEmpty())
				throw new InvalidOperationException();
			return list.RemoveFirst();
		}

		/**
		 * Gets the value currently at the top of this Stack without removing it.
		 * 
		 * @return The value at the top of this Stack.
		 */
		
		public override E Top()
		{
			if (list.IsEmpty())
				throw new InvalidOperationException();
			return list.First();
		}

		/**
		 * Gets the number of elements currently held in this Stack.
		 * 
		 * @return The number of elements in this Stack.
		 */
		
		public override int Size()
		{
			return list.Size();
		}
	}
}
