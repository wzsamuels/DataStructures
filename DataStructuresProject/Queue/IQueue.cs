namespace DataStructures.Queue
{
    /**
     * Queue defines an interface for implementing the Queue ADT.
     * 
     * @author Zach Samuels
     *
     * @param <E> The generic data type contained in this Queue.
     */
    public interface IQueue<E>
    {

        /**
         * Adds the given value to the end of this Queue.
         * 
         * @param value The value to add to this Queue.
         */
        void Enqueue(E value);

        /**
         * Removes and returns the element from the front of this Queue.
         * 
         * @return The element removed from the front of this Queue.
         */
        E Dequeue();

        /**
         * Returns the element at the front of this Queue without removing it.
         * 
         * @return The element at the front of this Queue.
         */
        E Front();

        /**
         * Gets the number of elements currently in this queue.
         * 
         * @return The number of elements in this queue.
         */
        int Size();

        /**
         * Determines if this Queue is empty.
         * 
         * @return True if this Queue is empty, false otherwise.
         */
        bool IsEmpty();
    }
}
