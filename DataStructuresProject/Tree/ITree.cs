using System.Collections.Generic;

namespace DataStructures.Tree
{
    /**
    * Tree declares the methods required to implement the Tree ADT.
    *  
    * @author Zach Samuels
    *
    * @param <E> The generic type of element stored in the Tree.
    */
    public interface ITree<E>
    {
        /**
         * Returns the the root of the Tree. 
         * 
         * @return The root of the Tree as a Position.
         */
        IPosition<E> Root();

        /**
         * Returns the parent of the given Node.
         * 
         * @param p The given Node to find the parent of as a Position.
         * 
         * @return The parent of the given Node as a Position.
         */
        IPosition<E> Parent(IPosition<E> p);

        /**
         * Returns a list of the the given Node's children.
         * 
         * @param p The given Node to find the child of as a Position.
         * 
         * @return An Iterable Position list of the given Node's children.
         */
        IEnumerable<IPosition<E>> Children(IPosition<E> p);

        /**
         * Returns the number of children of the given Node.
         * 
         * @param p The given Node to count the children of.
         * 
         * @return The number of children.
         */
        int NumChildren(IPosition<E> p);

        /**
         * Determines if the given Node is internal (not a leaf).
         * 
         * @param p The given Node to find the internality of.
         * 
         * @return True if the Node is internal, false otherwise. 
         */
        bool IsInternal(IPosition<E> p);

        /**
         * Determines if the given Node is a leaf.
         * 
         * @param p The given Node to consider.
         * 
         * @return True is the Node is a leaf, false otherwise.
         */
        bool IsLeaf(IPosition<E> p);

        /**
         * Determines if the given Node is the root of the Tree.
         * 
         * @param p The Node to check.
         * 
         * @return True if the Node is the root, false otherwise.
         */
        bool IsRoot(IPosition<E> p);

        /**
         * Returns the number of elements in the Tree.
         * 
         * @return The size of the Tree.
         */
        int Size();

        /**
         * Determines if the Tree is empty.
         * 
         * @return True if the Tree is empty, false otherwise.
         */
        bool IsEmpty();

        /**
         * Creates an Iterable Iterator of the Tree's Nodes for pre-order traversal.
         * 
         * @return The pre-order Iterable Iterator.
         */
        IEnumerable<IPosition<E>> PreOrder();

        /**
         * Creates an Iterable Iterator of the Tree's Nodes for post-order traversal.
         * 
         * @return The post-order Iterable Iterator.
         */
        IEnumerable<IPosition<E>> PostOrder();

        /**
         * Creates an Iterable Iterator of the Tree's Nodes for level-order traversal.
         * 
         * @return The level-order Iterable Iterator.
         */
        IEnumerable<IPosition<E>> LevelOrder();
    }
}
