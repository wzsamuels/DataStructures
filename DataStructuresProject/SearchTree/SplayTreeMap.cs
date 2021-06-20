using DataStructures.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.SearchTree
{
	/**
	* SplayTreeMap is a specific implementation of a BinarySearchTreeMap that
	* maintains balance 
	* 
	* @author Zach Samuels
	*
	* @param <K> The generic type used as the Map's Key.
	* @param <V> The generic type used as the Map's Value. ]
	*/
	public class SplayTreeMap<TKey, TValue> : BinarySearchTreeMap<TKey, TValue> 
		where TKey : IComparable<TKey>
	{
		/**
		 * Creates a new SplayTreeMap with the default comparator.
		 */
		public SplayTreeMap() : base(null)
		{
		}

		/**
		 * Creates a new SplayTreeMap with the 
		 * @param compare The comparator to use when ordering the tree map.
		 */
		public SplayTreeMap(IComparer<TKey> compare) : base(compare)
		{			
		}

		/**
		 * Performs the splay operation on the given node and the needed
		 * trinode rotations.
		 * @param p The node to splay.
		 */
		private void Splay(IPosition<IMap<TKey, TValue>.IEntry> p)
		{
			IPosition<IMap<TKey, TValue>.IEntry> node = p;

			// Continue until node is the root
			while (!IsRoot(node))
			{
				// Track the parent and grandparent nodes
				IPosition<IMap<TKey, TValue>.IEntry> parent = Parent(node);
				IPosition<IMap<TKey, TValue>.IEntry> grandparent = Parent(parent);

				if (grandparent == null)
				{
					// ZIG
					// Perform a single rotation if there is no grandparent
					Rotate(node);
				}
				else if ((Left(parent) == node && Left(grandparent) == parent) ||
							(Right(parent) == node && Right(grandparent) == parent))
				{
					// ZIG-ZIG
					// Rotate the parent around grandparent first
					Rotate(parent);
					// Then Rotate the node around the parent
					Rotate(node);
				}
				else
				{
					// ZIG-ZAG
					// Rotate node around parent
					Rotate(node);
					// Then Rotate node around grandparent
					Rotate(node);
				}
			}
		}

		/**
		 * Access the given node and performs the Splay operation on it.
		 * @param p The node to access.
		 */
	
		protected override void ActionOnAccess(IPosition<IMap<TKey, TValue>.IEntry> p)
		{
			// If p is a dummy/sentinel node, move to the parent
			if (IsLeaf(p))
			{
				p = Parent(p);
			}
			if (p != null)
			{
				Splay(p);
			}
		}

		/**
		 * Inserts the given node in the tree and performs the Splay operation
		 * on it.
		 * @param node The node to insert in the tree.
		 */
	
		protected override void ActionOnInsert(IPosition<IMap<TKey, TValue>.IEntry> node)
		{
			Splay(node);
		}

		/**
		 * Deletes the given node from the tree and performs the Splay operation.
		 * @param p The node to remove from the tree.
		 */
	
		protected override void ActionOnDelete(IPosition<IMap<TKey, TValue>.IEntry> p)
		{
			if (!IsRoot(p))
			{
				Splay(Parent(p));
			}
		}
	}
}
