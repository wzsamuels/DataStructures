using System;

namespace DataStructures.Tree
{
    /**
     * LinkedBinaryTree extends AbstractBinaryTree to create a concrete implementation of the
     * Binary Tree ADT using a linked list.
     * 
     * @author Zach Samuels
     *
     * @param <E> The generic type of element stored in the Tree.
     */
    public class LinkedBinaryTree<E> : AbstractBinaryTree<E> 
    {

        private Node root;
        private int size;

        /**
         * Creates a new, empty LinkedBinaryTree.
         */
        public LinkedBinaryTree()
        {
            root = null;
            size = 0;
        }

        /**
         * Determines if the given Position object is a valid linked binary node.
         * 
         * @param p The Position to check.
         * @return The Position object cast to a Node if it's valid.
         */
        protected Node Validate(IPosition<E> p)
        {
            if (!(p is Node)) {
                throw new ArgumentException("Position is not a valid linked binary node");
            }
            return (Node)p;
        }

        /**
         * Node extends AbstractNode to define a concrete implementation of a Node
         * that comprises a LinkedBinaryTree.
         *
         * @param <E> The generic type of element stored in the Tree.
         */
        public class Node : AbstractTree<E>.AbstractNode
        {
            private Node parent;
            private Node left;
            private Node right;

            /**
             * Creates a new Node with the given element and no parent.
             * @param element The element to store at the new Node.
             */
            public Node(E element) : this(element, null)
            {                
            }

            /**
             * Creates a new Node with the given element and the given parent Node.
             * @param element The element to store at the new Node.
             * @param parent The parent Node of the new Node.
             */
            public Node(E element, Node parent) : base(element)
            {
                SetParent(parent);
            }

            /**
             * Gets the left child of this Node.
             * @return The Node's left child Node.
             */
            public Node GetLeft()
            {
                return left;
            }

            /**
             * Gets the right child of this Node.
             * @return The Node's right child Node.
             */
            public Node GetRight()
            {
                return right;
            }

            /**
             * Sets the left child of this Node.
             * @param left The Node's new left child Node.
             */
            public void SetLeft(Node left)
            {
                this.left = left;
            }

            /**
             * Sets the right child of this Node.
             * @param right The Node's new right child Node.
             */
            public void SetRight(Node right)
            {
                this.right = right;
            }

            /**
             * Gets the Node's parent.
             * @return The Node's parent Node.
             */
            public Node GetParent()
            {
                return parent;
            }

            /**
             * Sets the Node's parent.
             * @param parent The Node's new parent Node.
             */
            public void SetParent(Node parent)
            {
                this.parent = parent;
            }
        }

        /**
         * Returns the left child of the given Node.
         * 
         * @param p The positional Node to find the child of.
         * @return The left child of the given Node.
         */
        public override IPosition<E> Left(IPosition<E> p)
        {
            Node node = Validate(p);
            return node.GetLeft();
        }

        /**
         * Returns the right child of the given Node.
         * 
         * @param p The positional Node to find the child of.
         * @return The right child of the given Node.
         */    
        public override IPosition<E> Right(IPosition<E> p)
        {
            Node node = Validate(p);
            return node.GetRight();
        }

        /**
         * Creates a new Node with the given value and sets it to the left child of the given
         * Node.
         * 
         * @param p The Node to add a left child to.
         * @param value The value to store in the new Node.
         * @throws IllegalArgumentException If the Node already has a left child.
         * @return The newly created Node.
         */   
        public override IPosition<E> AddLeft(IPosition<E> p, E value)
        {
            Node node = Validate(p);
            if (Left(node) != null)
            {
                throw new ArgumentException("Node already has a left child.");
            }

            Node newNode = CreateNode(value, node, null, null);
            node.SetLeft(newNode);
            size++;

            return newNode;
        }

        /**
         * Creates a new Node with the given value and adds it as the right child
         * of the given Node.
         * 
         * @param p The Node to add a right child to.
         * @param value The value to store in the new Node.
         * @throws IllegalArgumentException If the Node already has a right child.
         * @return The newly created Node.
         */
        public override IPosition<E> AddRight(IPosition<E> p, E value)
        {
            Node node = Validate(p);
            if (Right(node) != null)
            {
                throw new ArgumentException("Node already has a right child.");
            }

            Node newNode = CreateNode(value, node, null, null);
            node.SetRight(newNode);
            size++;

            return newNode;
        }

        /**
         * Returns the current root of the Tree.
         * 
         * @return The Tree's root Node.
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
            Node node = Validate(p);
            return node.GetParent();
        }

        /**
         * Creates a new Node with the given value and adds it as the root of the
         * Tree.
         * 
         * @param value The value to store in the new root Node.
         * @throws IllegalArgumentException If the Tree already has a root Node.
         * @return The newly created root Node.
         */
    
        public override IPosition<E> AddRoot(E value)
        {
            if (Root() != null)
            {
                throw new ArgumentException("The tree already has a root.");
            }
            this.root = CreateNode(value, null, null, null);
            size++;
            return root;
        }

        /**
         * Removes and returns the given Node from the Tree. Only implemented to
         * remove Nodes with zero or one child.
         * 
         * @param p The Node to remove from the Tree.
         * @throws IllegalArgumentException If the Node has two children.
         * @return The Node removed from the Tree.
         */    
        public override E Remove(IPosition<E> p)
        {
            if (NumChildren(p) == 2)
            {
                throw new ArgumentException("The node has two children");
            }
            Node node = Validate(p);

            // Special case if the root is being removed
            if (IsRoot(node))
            {
                E original = node.GetElement();
                if (NumChildren(node) == 0)
                {
                    this.root = null;
                }
                else
                {
                    Node replacement;
                    if (Right(node) != null)
                        replacement = Validate(Right(node));
                    else
                        replacement = Validate(Left(node));
                    node.SetParent(null);
                    replacement.SetParent(null);
                    this.root = replacement;
                }
                size--;
                return original;
            }

            Node parent = Validate(Parent(p));
            if (NumChildren(node) == 1)
            {
                Node child;
                if (node.GetLeft() != null)
                {
                    child = Validate(Left(node));
                    child.SetParent(parent);
                    if (parent.GetLeft() == node)
                        parent.SetLeft(child);
                    else
                        parent.SetRight(child);

                }
                else
                {
                    child = Validate(Right(node));
                    child.SetParent(parent);
                    if (parent.GetLeft() == node)
                        parent.SetLeft(child);
                    else
                        parent.SetRight(child);
                }
            }
            else
            {
                if (Left(parent) == node)
                    parent.SetLeft(null);
                else
                    parent.SetRight(null);
            }
            size--;
            return node.GetElement();
        }

        /**
         * Returns the number of currently in the Tree.
         * 
         * @return The current size of the Tree.
         */    
        public override int Size()
        {
            return size;
        }

        /**
         * Creates and returns a new binary tree Node with the given element, parent, and children.
         * 
         * @param e The element stored at the new Node.
         * @param parent The parent of the new Node.
         * @param left The left child of the new Node.
         * @param right The right child of the new Node
         * 
         * @return The newly created Node.
         */
        protected  virtual Node CreateNode(E e, Node parent, Node left, Node right)
        {
            Node newNode = new(e);
            newNode.SetParent(parent);
            newNode.SetLeft(left);
            newNode.SetRight(right);

            return newNode;
        }

        /** 
         * setRoot is needed for a later lab...
         * ...but THIS DESIGN IS BAD! If a client arbitrarily changes
         * the root by using the method, the size may no longer be correct/valid.
         * Instead, the precondition for this method is that
         * it should *ONLY* be used when rotating nodes in 
         * balanced binary search trees. We could instead change
         * our rotation code to not need this setRoot method, but that
         * makes the rotation code messier. For the purpose of this lab,
         * we will sacrifice a stronger design for cleaner/less code.
         * 
         * @param p The Node to set as the root of the Tree.
         * @return The new root Node.
         */
        public IPosition<E> SetRoot(IPosition<E> p)
        {
            root = Validate(p);
            return root;
        }
    }
}
