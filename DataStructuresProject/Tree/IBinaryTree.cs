using System.Collections.Generic;

namespace DataStructures.Tree
{

    /**
     * BinaryTree extextends the Tree interface to provide an interface
     * for the methods required by the BinaryTree ADT.
     * 
     * @author Zach Samuels
     *
     * @param <E> The generic type of element stored in the Tree.
     */
    public interface IBinaryTree<E> : ITree<E> {

        /**
         * Returns the left child of the given Node.
         * 
         * @param p The positional Node to find the child of.
         * @return The left child of the given Node.
         */
        IPosition<E> Left(IPosition<E> p);

        /**
         * Returns the right child of the given Node.
         * 
         * @param p The positional Node to find the child of.
         * @return The right child of the given Node.
         */
        IPosition<E> Right(IPosition<E> p);

        /**
         * Gets the sibling Node of the given Node.
         * 
         * @param p The Node to find the sibling of.
         * @return The given Node's sibling.
         */
        IPosition<E> Sibling(IPosition<E> p);

        /**
         * Provides an iterable list of the Tree's Nodes in order.
         * @return The in-order iterable list of the Tree's nodes.
         */
        IEnumerable<IPosition<E>> InOrder();
    }
}
