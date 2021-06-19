using System.Collections.Generic;

namespace DataStructures.Tree
{
    /**
     * GeneralTreeCollections defines the methods needed for the practical
     * implementation of a Tree but aren't strictly a part of the ADT.
     * 
     * @author Zach Samuels
     *
     * @param <E> The generic type of element stored in the Tree.
     */
    public interface IGeneralTreeCollection<E> : ITree<E>, IEnumerable<E>
    {
        /**
         * Creates a new Node with the given element and sets it to the root of 
         * the Tree.
         * 
         * @param value The element to store at the new root Node.
         * 
         * @return The newly created Node.
         */
        IPosition<E> AddRoot(E value);

        /**
         * Creates a new Node with the given element and sets it to the child of 
         * of the given Node in the Tree.
         * 
         * @param p The parent of the new Node.
         * @param value The element to store at the new root Node.
         * 
         * @return The newly created Node.
         */
        IPosition<E> AddChild(IPosition<E> p, E value);

        /**
         * Removes the given Node from the Tree.
         * 
         * @param p The given positional Node to remove.
         * 
         * @return The value of the element formerly stored at the Node.
         */
        E Remove(IPosition<E> p);

        /**
         * Sets the given Node to have the given value.
         * 
         * @param p The Node to set the value of.
         * @param value The new value to store at the given Node.
         * 
         * @return The value previously at the given Node.
         */
        E Set(IPosition<E> p, E value);
    }
}
