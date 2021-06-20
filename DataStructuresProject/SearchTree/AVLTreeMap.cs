using DataStructures.Map;
using System;
using System.Collections.Generic;

namespace DataStructures.SearchTree
{
	/**
	* AVLTreeMap is an implementation of the Map ADT and the Tree ADT that uses trinode
	* restructuring to maintain a balanced tree height.  
	* 
	* @author Zach Samuels
	*
	* @param <K> The generic type used as the Map's Key.
	* @param <V> The generic type used as the Map's Value.
	*/
	public class AVLTreeMap<TKey, TValue> : BinarySearchTreeMap<TKey, TValue> 
		where TKey : IComparable<TKey>
	{	
		/**
		 * Creates a new AVLTreeMap with a default Comparator.
		 */
		public AVLTreeMap() : base(null)
		{			
		}

		/**
		 * Creates a new AVLTreeMap with the given Comparator.
		 * 
		 * @param compare The type of Comparator to use when ordering the Map.
		 */
		public AVLTreeMap(IComparer<TKey> compare) : base(compare)
		{			
		}

		/**
		 * Helper method to trace upward from position p checking to make
		 * sure each node on the path is balanced. If a rebalance is necessary,
		 * trigger a trinode restructuring.
		 * 
		 * @param p The Node to rebalance if necessary.
		 */
		private void Rebalance(IPosition<IMap<TKey, TValue>.IEntry> p)
		{
			// Track the old height of the node x
			int oldHeight = 0;
			int newHeight;
			do
			{
				oldHeight = GetProperty(p);
				if (!IsBalanced(p))
				{
					// Find the child with the "taller" height
					IPosition<IMap<TKey, TValue>.IEntry> child = TallerChild(p);

					// Find the grandchild with the "taller" height
					IPosition<IMap<TKey, TValue>.IEntry> grandchild = TallerChild(child);

					// Perform trinode restructuring at the grandchild
					// save the root of the restructured subtree as x
					p = Restructure(grandchild);
					RecomputeHeight(Left(p));
					RecomputeHeight(Right(p));
				}
				RecomputeHeight(p);
				newHeight = GetProperty(p);

				// Move up to the parent
				p = Parent(p);
			} while (oldHeight != newHeight && p != null);
		}

		/**
		 * Returns the child of p that has the greater height
		 * If both children have the same height, use the child that 
		 * matches the parent's orientation.	
		 * 
		 * @param p The Node to check the children of.
		 * @return The child of p that has the greater height.
		 */
		private IPosition<IMap<TKey, TValue>.IEntry> TallerChild(IPosition<IMap<TKey, TValue>.IEntry> p)
		{
			if (GetProperty(Left(p)) > GetProperty(Right(p)))
				return Left(p);
			if (GetProperty(Left(p)) < GetProperty(Right(p)))
				return Right(p);
			if (IsRoot(p))
				return Left(p);
			if (p == Left(Parent(p)))
				return Left(p);
			else
				return Right(p);
		}

		/**
		 * Determines if the given Node has a balanced height.
		 * 
		 * @param p The Node to check for balance.
		 * @return True if the Node is  balanced, false otherwise.
		 */
		private bool IsBalanced(IPosition<IMap<TKey, TValue>.IEntry> p)
		{
			return Math.Abs(GetProperty(Left(p)) - GetProperty(Right(p))) <= 1;
		}

		/**
		 * Recompute and set the height of the given Node.
		 * @param p The Node to recompute the height of.
		 */
		private void RecomputeHeight(IPosition<IMap<TKey, TValue>.IEntry> p)
		{
			int h = 1 + Math.Max(GetProperty(Left(p)), GetProperty(Right(p)));
			SetProperty(p, h);
		}

		/**
		 * On insert a new Node, rebalance the Tree if needed. 
		 * @param node The Node to insert.
		 */
		
		protected override void ActionOnInsert(IPosition<IMap<TKey, TValue>.IEntry> node)
		{
			Rebalance(node);
		}

		/**
		 * On deleting a new Node, rebalance the Tree if needed.
		 * @param node The Node to insert.
		 */		
		protected override void ActionOnDelete(IPosition<IMap<TKey, TValue>.IEntry> node)
		{
			if (!IsRoot(node))
			{
				Rebalance(Parent(node));
			}
		}
	}
}
