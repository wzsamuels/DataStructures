using System.Collections.Generic;

namespace DataStructures.Tree
{
    /**
     * BinaryTreeCollection extends BinaryTree to define the interface for the
     * methods needed to implement a functional BinaryTree but are not strictly
     * part of the BinaryTree ADT.
     * 
     * @author Zach Samuels
     *
     * @param <E> The generic type of element stored in the Tree.
     */
    public interface IBinaryTreeCollection<E> : IBinaryTree<E>, IEnumerable<E>
    {
        /**
        * Creates a new Node with the given value and adds it as the root of the
        * Tree.
        * 
        * @param value The value to store in the new root Node.
        * @throws IllegalArgumentException If the Tree already has a root Node.
        * @return The newly created root Node.
        */
        IPosition<E> AddRoot(E value);

        /**
        * Creates a new Node with the given value and sets it to the left child of the given
        * Node.
        * 
        * @param p The Node to add a left child to.
        * @param value The value to store in the new Node.
        * @throws IllegalArgumentException If the Node already has a left child.
        * @return The newly created Node.
        */
        IPosition<E> AddLeft(IPosition<E> p, E value);

        /**
        * Creates a new Node with the given value and adds it as the right child
        * of the given Node.
        * 
        * @param p The Node to add a right child to.
        * @param value The value to store in the new Node.
        * @throws IllegalArgumentException If the Node already has a right child.
        * @return The newly created Node.
        */
        IPosition<E> AddRight(IPosition<E> p, E value);

        /**
        * Removes and returns the given Node from the Tree. Only implemented to
        * remove Nodes with zero or one child.
        * 
        * @param p The Node to remove from the Tree.
        * @throws IllegalArgumentException If the Node has two children.
        * @return The Node removed from the Tree.
        */
        E Remove(IPosition<E> p);

        /**
        * Sets the given Node to have the given value.
        * 
        * @param p The Node to set the value of.
        * @param value The new value of the given Node.
        * @return The old value stored at the Node.
        */
        E Set(IPosition<E> p, E value);
    }
}
