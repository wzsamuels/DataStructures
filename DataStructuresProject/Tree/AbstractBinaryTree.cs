using DataStructures.List;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Tree
{
    /**
     * AbstractBinaryTree defines the methods shared by all implementations
     * of the Tree ADT using a linked list.
     * 
     * @author Zach Samuels
     *
     * @param <E> The generic type of element stored in the Tree.
     */
    public abstract class AbstractBinaryTree<E> : AbstractTree<E>, IBinaryTreeCollection<E> 
    {
        /**
         * Provides an iterable list of the Tree's Nodes in order.
         * @return The in-order iterable list of the Tree's nodes.
         */        
        public IEnumerable<IPosition<E>> InOrder()
        {
            ArrayBasedList<IPosition<E>> traversal = new();

            if (!IsEmpty())
            {
                InOrderHelper(Root(), traversal);
            }

            return traversal;
        }

        /**
         * Recursively builds a list of the Tree's Nodes.
         * @param node The Node to begin building the list.
         * @param traversal The list used to store the Tree's Nodes.
         */
        private void InOrderHelper(IPosition<E> node, ArrayBasedList<IPosition<E>> traversal)
        {
            if (Left(node) != null)
                InOrderHelper(Left(node), traversal);
            if (node.GetElement() != null)
                traversal.AddLast(node);
            if (Right(node) != null)
                InOrderHelper(Right(node), traversal);
        }

        /**
         * Gets the sibling Node of the given Node.
         * 
         * @param p The Node to find the sibling of.
         * @return The given Node's sibling.
         */
        
        public IPosition<E> Sibling(IPosition<E> p)
        {
            AbstractNode node = Validate(p);

            if (Parent(node) != null && NumChildren(Parent(node)) > 1)
            {
                if (Left(Parent(node)) == node)
                    return Right(Parent(node));
                else
                    return Left(Parent(node));

            }
            else
                return null;
        }

        /**
         * Determines if the given Position object is a valid Node object.
         * 
         * @param p The Position to validate.
         * @return The given Position cast to an AbstractNode if valid.
         */
        private static AbstractNode Validate(IPosition<E> p)
        {
            if (!(p is AbstractNode)) 
            {
                throw new ArgumentException("Position is not a valid binary tree node");
            }
            return (AbstractNode)(p);
        }

        /**
         * Returns the number of children of the given Node.
         * 
         * @param p The given Node to count the children of.
         * 
         * @return The number of children.
         */    
        public override int NumChildren(IPosition<E> p)
        {
            AbstractNode node = Validate(p);

            int count = 0;
            if (Left(node) != null)
                count++;
            if (Right(node) != null)
                count++;

            return count;

        }

        /**
         * Sets the given Node to have the given value.
         * 
         * @param p The Node to set the value of.
         * @param value The new value of the given Node.
         * @return The old value stored at the Node.
         */
    
        public E Set(IPosition<E> p, E value)
        {
            AbstractNode node = Validate(p);
            E original = node.GetElement();
            node.SetElement(value);
            return original;
        }

        /**
         * Creates an Iterator of the Tree's elements for in-order traversal.
         * 
         * @return The in-order Iterator.
         */
    
        public IEnumerator<E> GetEnumerator()
        {
            return new ElementIterator(InOrder().GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /**
         * Generates an iterable list of the given Node's children.
         * 
         * @param p The Node to find the children of.
         * @return The iterable list of children Nodes.
         */

        public override IEnumerable<IPosition<E>> Children(IPosition<E> p)
        {
            AbstractNode node = Validate(p);
                ArrayBasedList<IPosition<E>> ret = new();
            if (Left(node) != null)
            {
                ret.AddLast(Left(node));
            }
            if (Right(node) != null)
            {
                ret.AddLast(Right(node));
            }
            return ret;
        }

        public abstract IPosition<E> AddRoot(E value);
        public abstract IPosition<E> AddLeft(IPosition<E> p, E value);
        public abstract IPosition<E> AddRight(IPosition<E> p, E value);
        public abstract E Remove(IPosition<E> p);
        public abstract IPosition<E> Left(IPosition<E> p);
        public abstract IPosition<E> Right(IPosition<E> p);
    }
}
