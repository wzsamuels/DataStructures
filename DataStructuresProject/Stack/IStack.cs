namespace DataStructures.Stack
{
    /**
    * Stacks defines an interface for implementing the Stack ADT.
    * 
    * @author Zach Samuels
    *
    * @param <E> The generic type data held in this Stack.
    */
    public interface IStack<E>
    {

        /**
         * Adds the given value to the top of this Stack.
         * 
         * @param value The value to add to this Stack.
         */
        void Push(E value);

        /**
         * Removes and returns the value at the top of this Stack.
         * 
         * @return The value popped from this Stack
         */
        E Pop();

        /**
         * Gets the value currently at the top of this Stack without removing it.
         * 
         * @return The value at the top of this Stack.
         */
        E Top();

        /**
         * Gets the number of elements currently held in this Stack.
         * 
         * @return The number of elements in this Stack.
         */
        int Size();

        /**
         * Determines if this Stack is empty.
         * 
         * @return True if this Stack is empty, false otherwise.
         */
        bool IsEmpty();
    }
}
