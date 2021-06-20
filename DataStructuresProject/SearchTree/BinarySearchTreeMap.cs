using DataStructures.Map;
using System;
using System.Collections.Generic;
using DataStructures.Tree;
using DataStructures.List;

namespace DataStructures.SearchTree
{
    /**
     * BinarySearchTreeMap implements the Map ADT and the Tree ADT to implement a
     * a sorted binary tree.
     * 
     * @author Zach Samuels
     *
     * @param <K> The generic type used as the Map's Key.
     * @param <V> The generic type used as the Map's Value.
     */
    public class BinarySearchTreeMap<TKey, TValue> : AbstractSortedMap<TKey, TValue>, IBinaryTree<IMap<TKey, TValue>.IEntry> 
        where TKey : IComparable<TKey>
    {

        // The BalanceableBinaryTree class is an inner class below
        private readonly BalanceableBinaryTree tree;

        /**
         * Creates a new BinarySEarchTreeMap with a default Comparator.
         */
        public BinarySearchTreeMap() : this(null)
        {
        }

        /**
         * Creates a new BinarySearchTreeMap with the given Comparator.
         * @param compare The Comparator to use when sorting the MAp.
         */
        public BinarySearchTreeMap(IComparer<TKey> compare) : base(compare)
        {
            tree = new BalanceableBinaryTree();
            tree.AddRoot(null);
        }

        /**
         * Returns the number of Nodes in the Tree.
         * @return The size of the Tree.
         */
        
        public override int Size()
        {
            // Our search trees will all use dummy/sentinel leaf nodes,
            // so the actual number of elements in the tree will be (size-1)/2      
            return (tree.Size() - 1) / 2;
        }

        /**
         * This method is used to add dummy/sentinel left and right children as leaves
         * @param p The node to add dummy leaves to.
         * @param entry The value to store at the given node.
         */
        private void ExpandLeaf(IPosition<IMap<TKey, TValue>.IEntry> p, IMap<TKey, TValue>.IEntry entry)
        {
            // initially, p is a dummy/sentinel node,
            // so replace the null entry with the new actual entry      
            tree.Set(p, entry);
            // Then add new dummy/sentinel children     
            tree.AddLeft(p, null);
            tree.AddRight(p, null);
        }

        /**
         * This helper method traces a path down the tree to locate the position
         * that contains an entry with the given key.
         * Think of "lookUp" as returning the last node visited when tracing
         * a path down the tree to find the given key.
         * @param p The node to starting looking at.
         * @param key The Key of the node to find.
         * @return The last node visited when finding the key.
         */
        private IPosition<IMap<TKey, TValue>.IEntry> LookUp(IPosition<IMap<TKey, TValue>.IEntry> p, TKey key)
        {
            // If we have reached a dummy/sentinel node (a leaf), return that sentinel node
            if (IsLeaf(p))
            {
                return p;
            }
            int comp = Compare(key, p.GetElement().GetKey());
            if (comp == 0)
            {
                // Return the position that contains the entry with the key         
                return p;
            }
            else if (comp < 0)
            {
                return LookUp(Left(p), key);
            }
            else
            {
                return LookUp(Right(p), key);
            }
        }

        /**
         * Gets the Value associated with the given Key.
         * 
         * @param key The Key to look for.
         * @return The Value stored at the Key.
         */
        
        public override TValue GetValue(TKey key)
        {
            IPosition<IMap<TKey, TValue>.IEntry> p = LookUp(tree.Root(), key);
            // ActionOnAccess is a "hook" for our AVL, Splay, and Red-Black Trees to use        
            ActionOnAccess(p);
            if (IsLeaf(p))
            {
                return default;
            }
            return p.GetElement().GetValue();
        }

        /**
         * Sets the Value associated with the given Key.
         * 
         * @param key The Key to set.
         * @param value The Value to assign.
         * @return The Value previously assigned to the given Key, null if the Key is new.
         */
        
        public override TValue Put(TKey key, TValue value)
        {
            // Create the new map entry     
            IMap<TKey, TValue>.IEntry newEntry = new MapEntry(key, value);
            // Get the last node visited when looking for the key       
            IPosition<IMap<TKey, TValue>.IEntry> p = LookUp(Root(), key);
            // If the last node visited is a dummy/sentinel node        
            if (IsLeaf(p))
            {
                ExpandLeaf(p, newEntry);
                // ActionOnInsert is a "hook" for our AVL, Splay, and Red-Black Trees to use            
                ActionOnInsert(p);
                return default;
            }
            else
            {
                TValue original = p.GetElement().GetValue();
                Set(p, newEntry);
                // ActionOnAccess is a "hook" for our AVL, Splay, and Red-Black Trees to use            
                ActionOnAccess(p);
                return original;
            }
        }

        /**
         * Removes the given Key from the Tree.
         * 
         * @param key The Key to remove.
         * @return The value previously stored at the Key.
         */
        
        public override TValue Remove(TKey key)
        {
            // Get the last node visited when looking for the key       
            IPosition<IMap<TKey, TValue>.IEntry> p = LookUp(Root(), key);
            // If p is a dummy/sentinel node        
            if (IsLeaf(p))
            {
                // ActionOnAccess is a "hook" for our AVL, Splay, and Red-Black Trees to use            
                ActionOnAccess(p);

                return default;
            }
            else
            {
                TValue original = p.GetElement().GetValue();
                // If the node has two children (that are not dummy/sentinel nodes)         
                if (IsInternal(Left(p)) && IsInternal(Right(p)))
                {
                    // Replace with the inorder successor               
                    IPosition<IMap<TKey, TValue>.IEntry> replacement = TreeMin(Right(p));
                    Set(p, replacement.GetElement());
                    // Move p to the replacement node in the right subtree              
                    p = replacement;
                }
                // Get the dummy/sentinel node (in case the node has an actual entry as a child)...         
                IPosition<IMap<TKey, TValue>.IEntry> leaf = (IsLeaf(Left(p)) ? Left(p) : Right(p));
                // ... then get its sibling (will be another sentinel or an actual entry node)          
                IPosition<IMap<TKey, TValue>.IEntry> sib = Sibling(leaf);
                // Remove the leaf NODE (this is your binary tree remove method)            
                Remove(leaf);
                // Remove the NODE (this is your binary tree remove method)
                // which will "promote" the sib node to replace p           
                Remove(p);
                // ActionOnDelete is a "hook" for our AVL, Splay, and Red-Black Trees to use            
                ActionOnDelete(sib);
                return original;
            }
        }

        /**
         * Returns the inorder successor (the minimum from the right subtree)
         * @param node The Node to find the successor of.
         * @return The inorder successor of the given Node.
         */
        private IPosition<IMap<TKey, TValue>.IEntry> TreeMin(IPosition<IMap<TKey, TValue>.IEntry> node)
        {
            IPosition<IMap<TKey, TValue>.IEntry> current = node;
            while (IsInternal(current))
            {
                current = Left(current);
            }
            return Parent(current);
        }

        /**
         * Creates an iterable list of all the entries in the map tree.
         * @return An iterable list of all the map's entries.
         */
        
        public override IEnumerable<IMap<TKey, TValue>.IEntry> EntryIterator()
        {
            ArrayBasedList<IMap<TKey, TValue>.IEntry> set = new(Size());
            foreach (IPosition<IMap<TKey, TValue>.IEntry> n in tree.InOrder())
            {
                set.AddLast(n.GetElement());
            }
            return set;
        }

        /**
         * Converts the tree to a string representation.
         * @return The tree as a string.
         */
        
        public override String ToString()
        {
            return tree.ToString();
        }

        /**
         * This is a "hook" method that will be overridden in 
         * your AVL, Splay, and Red-Black tree implementations
         * @param node The node to access.
         */
        protected virtual void ActionOnAccess(IPosition<IMap<TKey, TValue>.IEntry> node)
        {
            // Do nothing for BST
        }

        /**
         * This is a "hook" method that will be overridden in 
         * your AVL, Splay, and Red-Black tree implementations
         * @param node The node to insert in the tree.
         */
        protected virtual void ActionOnInsert(IPosition<IMap<TKey, TValue>.IEntry> node)
        {
            // Do nothing for BST
        }

        /**
         * This is a "hook" method that will be overridden in 
         * your AVL, Splay, and Red-Black tree implementations
         * @param node The node to delete from the tree.
         */
        protected virtual void ActionOnDelete(IPosition<IMap<TKey, TValue>.IEntry> node)
        {
            // Do nothing for BST
        }

        /**
         * BalanceableBinaryTree is a specific type of Binary Tree that must maintain balance as
         * nodes are added or deleted.
         *
         * @param <K> The generic type of Keys used in the map.
         * @param <V> The generic type of Values used in the map.
         */
        protected class BalanceableBinaryTree : LinkedBinaryTree<IMap<TKey, TValue>.IEntry>
        {

            /**
             * Relink is a helper method for trinode restructuring and rotations
             * @param parent The new parent of the child node.
             * @param child The child node to relink.
             * @param makeLeftChild If the child should be the left child of the parent.
             */
            private static void Relink(Node parent, Node child, bool makeLeftChild)
            {
                child.SetParent(parent);
                if (makeLeftChild)
                    parent.SetLeft(child);
                else
                    parent.SetRight(child);
            }

            /**
             * Rotate is a helper method for handling rotations and relinking nodes
             * @param p The node to rotate around.
             */
            public void Rotate(IPosition<IMap<TKey, TValue>.IEntry> p)
            {
                Node node = Validate(p);
                Node parent = Validate(Parent(node));

                Node grandparent = null;
                if (Parent(parent) != null)
                    grandparent = Validate(Parent(parent));

                // Check whether the rotation is a single rotation (no grandparent exists)
                if (grandparent == null)
                {
                    // Rotate the node to be the new root
                    SetRoot(node);
                    node.SetParent(null);
                }
                else
                {
                    // Otherwise, link the node as a child of the grandparent
                    if (parent == Left(grandparent))
                        Relink(grandparent, node, true);
                    else
                        Relink(grandparent, node, false);
                }
                // Regardless of whether a grandparent exists,
                // relink the parent and node and transfer node's subtree
                if (node == Left(parent))
                {
                    Relink(parent, Validate(Right(node)), true);
                    Relink(node, parent, false);
                }
                else
                {
                    Relink(parent, Validate(Left(node)), false);
                    Relink(node, parent, true);
                }
            }

            /**
             * Restructure is a helper method to perform a trinode restructuring
             * and trigger the appropriate rotations.
             * @param x The node to restructure.
             * @return The restructured node.
             */
            public IPosition<IMap<TKey, TValue>.IEntry> Restructure(IPosition<IMap<TKey, TValue>.IEntry> x)
            {
                Node node = Validate(x);
                Node parent = Validate(Parent(node));

                Node grandparent = null;
                if (Parent(parent) != null)
                    grandparent = Validate(Parent(parent));

                //if(node != null && parent != null && grandparent != null
                //	&& ((parent.GetLeft() == node && grandparent.GetLeft() == parent)
                //|| (parent.GetRight() == node && grandparent.GetRight() == parent))) {

                if ((parent.GetLeft() == node && grandparent.GetLeft() == parent) ||
                        (parent.GetRight() == node && grandparent.GetRight() == parent))
                {
                    Rotate(parent);
                    return parent;
                }
                else
                {
                    Rotate(node);
                    Rotate(node);
                    return node;
                }
            }

            /**
             * Creates a new node with the given value and family nodes.
             * @param element The map entry to store at the new node.
             * @param parent The parent node of the new node.
             * @param left The left child of the new node.
             * @param right The right child of the new node.
             * @return The newly created node object.
             */
            
            protected override Node CreateNode(IMap<TKey, TValue>.IEntry element, Node parent, Node left,
                    Node right)
            {
                BSTNode newNode = new(element);
                newNode.SetParent(parent);
                newNode.SetLeft(left);
                newNode.SetRight(right);
                newNode.SetProperty(0);
                return newNode;
            }

            /**
             * A binary search tree node needs to keep track of some property.
             * For example, for AVL trees the "property" is the height of the node.
             * For Red-Black trees, the "property" is the color of the node.
             *
             * @param <E> The generic type to store at the node.
             */
            protected class BSTNode : Node
            {

                private int property;

                /**
                 * Creates a new BSTNode that stores the given element.
                 * @param element The element to store at the node.
                 */
                public BSTNode(IMap<TKey, TValue>.IEntry element) : base(element)
                {                    
                    SetProperty(0);
                }

                /**
                 * Sets the node's property (height).
                 * @param height The new height of the node.
                 */
                public void SetProperty(int height)
                {
                    this.property = height;
                }
                /**
                 * Returns the node's property (height).
                 * @return The node's height.
                 */
                public int GetProperty()
                {
                    return property;
                }
            }

            /**
             * Returns the property of the given node.
             * @param p The node to find the property of.
             * @return The property of the node, or 0 if the node is null.
             */
            public int GetProperty(IPosition<IMap<TKey, TValue>.IEntry> p)
            {
                if (p == null)
                {
                    return 0;
                }
                BSTNode node = (BSTNode)p;
                return node.GetProperty();
            }

            /**
             * Sets the property of the given node.
             * @param p The node to set the property of.
             * @param value The node's new value.
             */
            public void SetProperty(IPosition<IMap<TKey, TValue>.IEntry> p, int value)
            {
                BSTNode node = (BSTNode)p;
                node.SetProperty(value);
            }
        }

        /////////////////////////////////////////////////////////////////////////////
        // All the methods below delegate to the BalanceableBinaryTree class, which extends 
        // your linked binary tree implementation
        /////////////////////////////////////////////////////////////////////////////

        /**
         * Returns the root node of the tree.
         * 
         * @return The node at the root of the tree.
         */
        public IPosition<IMap<TKey, TValue>.IEntry> Root()
        {
            return tree.Root();
        }

        /**
         * Returns the parent of the given node.
         * @param p The node to find the parent of.
         * @return The parent of the given node.
         */    
        public IPosition<IMap<TKey, TValue>.IEntry> Parent(IPosition<IMap<TKey, TValue>.IEntry> p)
        {
            return tree.Parent(p);
        }

        /**
         * Returns an iterable list of the given node's children.
         * @return An iterable list of the node's children.
         */ 
        public IEnumerable<IPosition<IMap<TKey, TValue>.IEntry>> Children(IPosition<IMap<TKey, TValue>.IEntry> p)
        {
            return tree.Children(p);
        }

        /**
         * Returns the number of children of the given node.
         * @param p The node to find the children of.
         * @return How many children the given node has.
         */
        public int NumChildren(IPosition<IMap<TKey, TValue>.IEntry> p)
        {
            return tree.NumChildren(p);
        }

        /**
         * Determines if the given node is internal.
         * @param p The node to find the internality of.
         * @return True if the node is internal, false otherwise.
         */    
        public bool IsInternal(IPosition<IMap<TKey, TValue>.IEntry> p)
        {
            return tree.IsInternal(p);
        }

        /**
         * Sets the given node to have the given map entry.
         * @param p The node to set.
         * @param entry The map entry to set at the given node.
         * @return The previous value stored at the given node.
         */
        public IMap<TKey, TValue>.IEntry Set(IPosition<IMap<TKey, TValue>.IEntry> p,
            IMap<TKey, TValue>.IEntry entry)
        {
            return tree.Set(p, entry);
        }

        /**
         * Determines if the given node is a leaf.
         * @param p The node to find if it's a leaf.
         * @return True if the node is a leaf, false otherwise.
         */
    
            public bool IsLeaf(IPosition<IMap<TKey, TValue>.IEntry> p)
        {
            return tree.IsLeaf(p);
        }

        /**
         * Determines if the given node is the root node.
         * @param p The node to determine as the root.
         * @return True if the node is the root, false otherwise.
         */
    
            public bool IsRoot(IPosition<IMap<TKey, TValue>.IEntry> p)
        {
            return tree.IsRoot(p);
        }

        /**
         * Returns an iterable list of the tree in preorder.
         * @return A preorder iterable list.
         */
    
            public IEnumerable<IPosition<IMap<TKey, TValue>.IEntry>> PreOrder()
        {
            return tree.PreOrder();
        }

        /**
         * Returns an iterable list of the tree in postorder.
         * @return A postorder iterable list.
         */
    
            public IEnumerable<IPosition<IMap<TKey, TValue>.IEntry>> PostOrder()
        {
            return tree.PostOrder();
        }

        /**
         * Returns an iterable list of the tree in level order.
         * @return A level order iterable list.
         */
    
            public IEnumerable<IPosition<IMap<TKey, TValue>.IEntry>> LevelOrder()
        {
            return tree.LevelOrder();
        }

        /**
         * Returns the left child of the given node.
         * @param p The node to find the left child of.
         * @return The left child node.
         */
    
            public IPosition<IMap<TKey, TValue>.IEntry> Left(IPosition<IMap<TKey, TValue>.IEntry> p)
        {
            return tree.Left(p);
        }

        /**
         * Removes the given node from the tree.
         * @param p The node to remove from the tree.
         * @return The map entry previously stored at the node.
         */
        protected IMap<TKey, TValue>.IEntry Remove(IPosition<IMap<TKey, TValue>.IEntry> p)
        {
            return tree.Remove(p);
        }

        /**
         * Returns the right child of the given node.
         * @param p The node to find the right child of.
         * @return The right child node.
         */
        public IPosition<IMap<TKey, TValue>.IEntry> Right(IPosition<IMap<TKey, TValue>.IEntry> p)
        {
            return tree.Right(p);
        }

        /**
         * Returns the sibling of the given node.
         * @param p The node to find the sibling of.
         * @return The sibling node.
         */
        public IPosition<IMap<TKey, TValue>.IEntry> Sibling(IPosition<IMap<TKey, TValue>.IEntry> p)
        {
            return tree.Sibling(p);
        }

        /**
         * Generates an iterable list of the tree inorder.
         * @return An inorder list of the trees entries.
         */
        public IEnumerable<IPosition<IMap<TKey, TValue>.IEntry>> InOrder()
        {
            return tree.InOrder();
        }

        /**
         * Performs a rotation around the given node.
         * @param p The node to 
         */
        protected void Rotate(IPosition<IMap<TKey, TValue>.IEntry> p)
        {
            tree.Rotate(p);
        }

        /**
         * Restructures the tree around the given node.
         * @param x The node to restructure.
         * @return The newly restructed node.
         */
        protected IPosition<IMap<TKey, TValue>.IEntry> Restructure(IPosition<IMap<TKey, TValue>.IEntry> x)
        {
            return tree.Restructure(x);
        }

        /**
         * Returns the property of the given node.
         * @param p The node to find the property of.
         * @return The property of the node.
         */
        public int GetProperty(IPosition<IMap<TKey, TValue>.IEntry> p)
        {
            return tree.GetProperty(p);
        }

        /**
         * Sets the property of the given node.
         * @param p The node to set the property of.
         * @param value The new property value of the node.
         */
        public void SetProperty(IPosition<IMap<TKey, TValue>.IEntry> p, int value)
        {
            tree.SetProperty(p, value);
        }
    }
}
