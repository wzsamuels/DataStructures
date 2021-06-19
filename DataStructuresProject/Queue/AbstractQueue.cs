namespace DataStructures.Queue
{
    /**
     * AbstractQueue defines functionality from the Queue interface for
     * methods shared by all Queue implementations.
     * 
     * @author Zach Samuels
     *
     * @param <E> The generic type data held in this Queue.
     */
    public abstract class AbstractQueue<E> : IQueue<E> 
    {
        public abstract E Dequeue();
        public abstract void Enqueue(E value);
        public abstract E Front();

        /**
         * Determines if this Queue is empty.
         * 
         * @return True if this Queue is empty, false otherwise.
         */
        public bool IsEmpty()
        {
            return Size() == 0;
        }

        public abstract int Size();
    }
}
