namespace DataStructures.Stack
{
    /**
    * AbstractStack defines functionality from the Stack interface for
    * methods shared by all Stack implementations.
    * 
    * @author Zach Samuels
    *
    * @param <E> The generic type data held in this Stack.
    */
    public abstract class AbstractStack<E> : IStack<E> 
    {

        /**
         * Determines if this Stack is empty.
         * 
         * @return True if this Stack is empty, false otherwise.
         */
        public bool IsEmpty()
        {
            return Size() == 0;
        }

        public abstract E Pop();
        public abstract void Push(E value);
        public abstract int Size();
        public abstract E Top();
    }
}
