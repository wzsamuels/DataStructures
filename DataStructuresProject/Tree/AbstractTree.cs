using DataStructures.List;
using DataStructures.Queue;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Tree
{
    /**
     * AbstractTree defines the methods that have identical implementations
     * for all types of Trees.
     * 
     * @author Zach Samuels
     *
     * @param <E> The generic type of element stored in the Tree.
     */
    public abstract class AbstractTree<E> : ITree<E>
    {

        /**
         * Determines if the given Position is an internal Node.
         * 
         * @param p The Position object to check.
         * @return True if the Position is an internal Node, false otherwise.
         */
        public bool IsInternal(IPosition<E> p)
        {
            return NumChildren(p) > 0;
        }

        /**
         * Determines if the given Node is a leaf.
         * 
         * @param p The given Node to consider.
         * @return True is the Node is a leaf, false otherwise.
         */
        public bool IsLeaf(IPosition<E> p)
        {
            return NumChildren(p) == 0;
        }

        /**
         * Determines if the given Node is the root of the Tree.
         * 
         * @param p The Node to check.
         * @return True if the Node is the root, false otherwise.
         */
        public bool IsRoot(IPosition<E> p)
        {
            return p == Root();
        }

        /**
         * Determines if the Tree is empty.
         * 
         * @return True if the Tree is empty, false otherwise.
         */
        public bool IsEmpty()
        {
            return Size() == 0;
        }

        /**
         * Creates an Iterable Iterator of the Tree's Nodes for pre-order traversal.
         * 
         * @return The pre-order Iterable Iterator.
         */
        public IEnumerable<IPosition<E>> PreOrder()
        {
            // You can use any list data structure here that supports
            // O(1) addLast
            ArrayBasedList<IPosition<E>> traversal = new();
            if (!IsEmpty())
            {
                PreOrderHelper(Root(), traversal);
            }
            return traversal;
        }

        /**
         * Helper method for preOrde() that recursively builds a list of
         * of the given's Positions children.
         * 
         * @param p The Position to begin building the list.
         * @param traversal The list to build.
         */
        private void PreOrderHelper(IPosition<E> p, ArrayBasedList<IPosition<E>> traversal)
        {
            // Don't add null leaves
            if (p.GetElement() != null)
                traversal.AddLast(p);
            foreach (IPosition<E> c in Children(p))
            {
                PreOrderHelper(c, traversal);
            }
        }

        /**
         * Creates an Iterable Iterator of the Tree's Nodes for post-order traversal.
         * 
         * @return The post-order Iterable Iterator.
         */
        public IEnumerable<IPosition<E>> PostOrder()
        {
            // You can use any list data structure here that supports
            // O(1) addLast
            ArrayBasedList<IPosition<E>> list = new();
            if (!IsEmpty())
            {
                PostOrderHelper(Root(), list);
            }
            return list;
        }

        /**
         * Helper method for postOrder() that recursively builds a list of
         * of the given's Positions children.
         * 
         * @param p The Position to begin building the list.
         * @param list The list to build.
         */
        private void PostOrderHelper(IPosition<E> p, ArrayBasedList<IPosition<E>> list)
        {
            foreach (IPosition<E> c in Children(p))
            {
                PostOrderHelper(c, list);
            }
            // Don't add null leaves
            if (p.GetElement() != null)
                list.AddLast(p);
        }

        /**
         * Creates an Iterable Iterator of the Tree's Nodes for level-order traversal.
         * 
         * @return The level-order Iterable Iterator.
         */
        public IEnumerable<IPosition<E>> LevelOrder()
        {
            ArrayBasedList<IPosition<E>> list = new();
            IQueue<IPosition<E>> queue = new ArrayBasedQueue<IPosition<E>>();

            if (!IsEmpty())
            {
                queue.Enqueue(Root());
                while (!queue.IsEmpty())
                {
                    IPosition<E> q = queue.Dequeue();
                    // Don't add null leaves
                    if (q.GetElement() != null)
                        list.AddLast(q);
                    foreach (IPosition<E> c in Children(q))
                    {
                        queue.Enqueue(c);
                    }
                }
            }

            return list;
        }

        /**
         * ElementIterator converts an Iterator of Tree Nodes to an Iterator of
         * the elements stored in the Nodes.
         */
        protected class ElementIterator : IEnumerator<E>
        {
            private readonly IEnumerator<IPosition<E>> it;

            /**
             * Creates a new ElementIterator from the given Position Iterator.
             * 
             * @param iterator The Iterator to convert to an ElementIterator
             */
            public ElementIterator(IEnumerator<IPosition<E>> iterator)
            {
                it = iterator;
            }

            /**
             * Determines if there is another element to be returned by Next().
             * 
             * @return True if there's another element in the iterator, false otherwise.
             */
            public bool MoveNext()
            {
                return it.MoveNext();
            }

            public void Reset()
            {
                it.Reset();
            }

            public void Dispose()
            {
                it.Dispose();
            }

            /**
             * Returns the next element in the Iterator and moves the Iterator 
             * cursor forward.
             * 
             * @return The next element in the Iterator.
             */
            public E Current => it.Current.GetElement();

            object IEnumerator.Current => Current;
        }

        /**
         * AbstractNode provides the Tree Node methods needed for Nodes in 
         * all types of Trees.
         *
         * @param <E> The generic type of element stored in the Node.
         */
        public abstract class AbstractNode : IPosition<E>
        {
            private E element;

            /**
             * Creates a new Node with the given element.
             * 
             * @param element The element stored at the Node.
             */
            public AbstractNode(E element)
            {
                SetElement(element);
            }

            /**
             * Gets the Node's element.
             * @return The element stored at this Node.
             */
            public E GetElement()
            {
                return element;
            }

            /**
             * Sets the Node's element.
             * @param element The new element to store at this Node.
             */
            public void SetElement(E element)
            {
                this.element = element;
            }
        }

        /**
         * Returns a String representation of the current Tree.
         * 
         * @return The Tree and it's Nodes as a String.
         */
        public override String ToString()
        {
            StringBuilder sb = new(this.GetType().Name + "[\n");
            ToStringHelper(sb, "", Root());
            sb.Append(']');
            return sb.ToString();
        }

        /**
         * Recursively builds a String representative of the Tree with the given
         * root node.
         * 
         * @param sb StringBuilder object used to build the String.
         * @param indent The String used as indentation.
         * @param root The root Node of the Tree to build a String of.
         */
        private void ToStringHelper(StringBuilder sb, String indent, IPosition<E> root)
        {
            if (root == null)
            {
                return;
            }
            sb.Append(indent).Append(root.GetElement()).Append('\n');
            foreach (IPosition<E> child in Children(root))
            {
                ToStringHelper(sb, indent + " ", child);
            }
        }
        public abstract IPosition<E> Root();
        public abstract IPosition<E> Parent(IPosition<E> p);
        public abstract IEnumerable<IPosition<E>> Children(IPosition<E> p);
        public abstract int NumChildren(IPosition<E> p);
        public abstract int Size();
    }
}        
