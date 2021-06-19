using DataStructures.List;
using DataStructures.PositionalList;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Tree
{
    /**
 * GeneralTree provides a non-specialized implementation of the Tree ADT.
 * 
 * @author Zach Samuels
 *
 * @param <E> The generic type of element stored in the Tree.
 */
    public class GeneralTree<E> : AbstractTree<E>, IGeneralTreeCollection<E> 
    {
        private Node root;
        private int size;

        /**
         * Creates a new, empty Tree.
         */
        public GeneralTree()
        {
            root = null;
            size = 0;
        }

        /**
         * Gets the root of the Tree.
         * 
         * @return The root Node of the Tree.
         */        
        public override IPosition<E> Root()
        {
            return root;
        }

        /**
         * Returns the parent of the given Node.
         * 
         * @param p The given Node to find the parent of as a Position.
         * 
         * @return The parent of the given Node as a Position.
         */
        
        public override IPosition<E> Parent(IPosition<E> p)
        {
            return Validate(p).GetParent();
        }

        /**
         * Returns a list of the the given Node's children.
         * 
         * @param p The given Node to find the child of as a Position.
         * 
         * @return An Iterable Position list of the given Node's children.
         */
        
        public override IEnumerable<IPosition<E>> Children(IPosition<E> p)
        {
            Node node = Validate(p);
            SinglyLinkedList<IPosition<E>> ret = new();
            // TODO: check this iterates correctly
            foreach (IPosition<Node> n in node.GetChildren().PositionIterator())
            {
                ret.AddLast(n.GetElement());
            }
            return ret;
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
            Node node = Validate(p);
            return node.GetChildren().Size();
        }

        /**
	     * Creates a new Node with the given element and sets it to the root of 
	     * the Tree.
	     * 
	     * @param value The element to store at the new root Node.
	     * 
	     * @return The newly created Node.
	     */
        
        public IPosition<E> AddRoot(E value)
        {
            if (root != null)
            {
                throw new ArgumentException("Tree already has a root");
            }
            this.root = CreateNode(value);
            size = 1;
            return root;
        }

        /**
	     * Creates a new Node with the given element and sets it to the child of 
	     * of the given Node in the Tree.
	     * 
	     * @param p The parent of the new Node.
	     * @param value The element to store at the new root Node.
	     * 
	     * @return The newly created Node.
	     */
        
        public IPosition<E> AddChild(IPosition<E> p, E value)
        {
            Node node = Validate(p);
            Node newNode = CreateNode(value);
            node.GetChildren().AddLast(newNode);
            newNode.SetParent(node);
            size++;
            return newNode;
        }

        /**
         * Sets the given Node to have the given value.
         * 
         * @param p The Node to set the value of.
         * @param value The new value to store at the given Node.
         * 
         * @return The value previously at the given Node.
         */
        
        public E Set(IPosition<E> p, E value)
        {
            Node node = Validate(p);
            E original = node.GetElement();
            node.SetElement(value);
            return original;
        }

        /**
         * Creates a new Node with the given element.
         * 
         * @param element The element stored at the new Node.
         * 
         * @return The newly created Node object.
         */
        public Node CreateNode(E element)
        {
            return new Node(element);
        }

        /**
         * Returns the number of elements in the Tree.
         * 
         * @return The size of the Tree.
         */
        
        public override int Size()
        {
            return size;
        }

        /**
         * Removes the given Node from the Tree.
         * 
         * @param p The given positional Node to remove.
         * 
         * @return The value of the element formerly stored at the Node.
         */
        
        public E Remove(IPosition<E> p)
        {
            // We will only support removal of a node that only has 1 child
            if (NumChildren(p) > 1)
            {
                throw new ArgumentException("The node has more than 1 child.");
            }
            // Handle special case if the node being removed is the root
            if (IsRoot(p))
            {
                E original = p.GetElement();
                if (NumChildren(p) == 0)
                {
                    this.root = null;
                }
                else
                {
                    Node replacement = Validate(p).GetChildren().First().GetElement();
                    replacement.SetParent(null);
                    this.root = replacement;
                }
                size--;
                return original;
            }
            // Handle the case where the node being removed is NOT the root
            Node node = Validate(p);
            Node parent = Validate(Parent(p));
            // Create an iterator over the parent node's children positions
            IEnumerator<IPosition<Node>> it = parent.GetChildren().PositionIterator().GetEnumerator();
            while (it.MoveNext())
            {
                // Find the position of the node to be removed
                IPosition<Node> current = it.Current;
                if (current.GetElement() == node)
                {
                    if (NumChildren(p) == 1)
                    {
                        // If the node being removed has 1 child, replace it
                        // in the parent's list of children
                        Node replacement = node.GetChildren().First().GetElement();
                        replacement.SetParent(parent);
                        parent.GetChildren().SetPosition(current, replacement);
                    }
                    else
                    {
                        // If the node being removed has 0 children, remove it
                        parent.GetChildren().Remove(current);
                    }
                }
            }
            size--;
            return node.GetElement();
        }

        /**
	     * Provides an iterator to traverse the Tree's elements in pre-order.
	     * 
	     * @return The iterator of the Tree's elements.
	     */

        public IEnumerator<E> GetEnumerator()
        {
            return new ElementIterator(PreOrder().GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /**
	     * Determines if the given Position is a valid Node object.
	     * 
	     * @param p The Position object to consider.
	     * 
	     * @return The Position object cast to a Node.
	     */
        private static Node Validate(IPosition<E> p)
        {
            if (!(p is Node)) {
                throw new ArgumentException("Position is not a legal general tree node");
            }
            return (Node)p;
        }

        /**
	     * Node extends AbstractNode to define the functionality of the nodes in a general
	     * tree.
	     *
	     * @param <E> The generic type of element stored in the Tree.
	     */
        public class Node : AbstractNode
        {

            private Node parent;

            // A general tree node needs to maintain a list of children
            private readonly IPositionalList<Node> children;

            /**
             * Creates a new Node with the given element and no parent.
             * 
             * @param element The element to store at the given Node.
             */
            public Node(E element) : this(element, null)
            {           
            }

            /**
             * Creates a Node with the given element and the given parent.
             * 
             * @param element The element to store at the given Node.
             * @param parent The parent of the new Node.
             */
            public Node(E element, Node parent) : base(element)
            {            
                SetParent(parent);
                children = new PositionalLinkedList<Node>();
            }

            /**
             * Sets the Node's parent node.
             * 
             * @param p The new parent node of this Node.
             */
            public void SetParent(Node p)
            {
                parent = p;
            }

            /**
             * Gets the parent of this Node.
             * 
             * @return The Node's parent.
             */
            public Node GetParent()
            {
                return parent;
            }

            /**
             * Returns a list of the Node's children.
             * 
             * @return The Node's children as a PostionalList.
             */
            public IPositionalList<Node> GetChildren()
            {
                return children;
            }
        }
    }
}
