using DataStructures.Map;
using System;
using System.Collections.Generic;

namespace DataStructures.SearchTree
{
    /**
	 * RedBlackTreeMap is a specific type of BinarySearchTreeMap that uses colored
	 * nodes to maintain balance and search order.
	 * @author Zach Samuels
	 *
	 * @param <K> The generic type used as the Map's Key.
	 * @param <V> The generic type used as the Map's Value.
	 */
    public class RedBlackTreeMap<TKey, TValue> : BinarySearchTreeMap<TKey, TValue> 
		where TKey : IComparable<TKey>
	{

		/**
		 * Creates a new RedBlackTreeMap with the default comparator.
		 */
		public RedBlackTreeMap() : base(null)
		{			
		}

		/**
		 * Creates a new RedBlackTreeMap with the given comparator.
		 * @param compare The comparator to use when ordering the map.
		 */
		public RedBlackTreeMap(IComparer<TKey> compare) : base(compare)
		{			
		}

		/**
		 *  Helper method to abstract the idea of black
		 * @param p The node to determine if black.
		 * @return True if the given node is black, false otherwise.
		 */
		private bool IsBlack(IPosition<IMap<TKey, TValue>.IEntry> p)
		{
			return GetProperty(p) == 0;
		}

		/**
		 * Helper method to abstract the idea of red
		 * @param p The node to determine if red.
		 * @return True if the given node is red, false otherwise.
		 */
		private bool IsRed(IPosition<IMap<TKey, TValue>.IEntry> p)
		{
			return GetProperty(p) == 1;
		}

		/**
		 * Set the color for a node to be black
		 * @param p The node to make black.
		 */
		private void MakeBlack(IPosition<IMap<TKey, TValue>.IEntry> p)
		{
			SetProperty(p, 0);
		}

		/**
		 * Set the color for a node to be red
		 * @param p The node to make red.
		 */
		private void MakeRed(IPosition<IMap<TKey, TValue>.IEntry> p)
		{
			SetProperty(p, 1);
		}

		/**
		 * Recolors the tree's nodes to fix the problem of a red node having a
		 * red parent or child.
		 * @param node The node to begin the fixing the problem at.
		 */
		private void ResolveRed(IPosition<IMap<TKey, TValue>.IEntry> node)
		{
			IPosition<IMap<TKey, TValue>.IEntry> parent = Parent(node);
			if (IsRed(parent))
			{
				IPosition<IMap<TKey, TValue>.IEntry> uncle = Sibling(parent);

				// CASE 1: the uncle (sibling of the parent) is black
				if (IsBlack(uncle))
				{
					IPosition<IMap<TKey, TValue>.IEntry> middle = Restructure(node);
					MakeBlack(middle);
					MakeRed(Left(middle));
					MakeRed(Right(middle));
				}
				else
				{
					// CASE 2: the uncle (sibling of the parent) is red
					MakeBlack(parent);
					MakeBlack(uncle);
					IPosition<IMap<TKey, TValue>.IEntry> grandparent = Parent(parent);
					if (!IsRoot(grandparent))
					{
						MakeRed(grandparent);
						ResolveRed(grandparent);
					}
				}
			}
		}

		/**
		 * Recolors the tree's nodes to fix a double black node problem.
		 * @param p The node to begin resolving.
		 */
		private void RemedyDoubleBlack(IPosition<IMap<TKey, TValue>.IEntry> p)
		{
			IPosition<IMap<TKey, TValue>.IEntry> node = p;
			IPosition<IMap<TKey, TValue>.IEntry> parent = Parent(node);
			IPosition<IMap<TKey, TValue>.IEntry> sibling = Sibling(node);

			if (IsBlack(sibling))
			{
				// CASE 1: trinode restructuring
				if (IsRed(Left(sibling)) || IsRed(Right(sibling)))
				{
					IPosition<IMap<TKey, TValue>.IEntry> temp;
					if (IsRed(Left(sibling)))
						temp = Left(sibling);
					else
						temp = Right(sibling);
					IPosition<IMap<TKey, TValue>.IEntry> middle = Restructure(temp);
					MakeBlack(Left(middle));
					MakeBlack(Right(middle));
				}
				else
				{
					// CASE 2: recoloring
					MakeRed(sibling);
					if (IsRed(parent))
						MakeBlack(parent);
					else if (!IsRoot(parent))
						RemedyDoubleBlack(parent);
				}
			}
			else
			{
				// CASE 3: Rotate
				Rotate(sibling);
				MakeBlack(sibling);
				MakeRed(parent);
				RemedyDoubleBlack(node);
			}
		}

		/**
		 * Inserts the given node into the tree and performs a splay.
		 * @param p The node to insert in the tree.
		 */
		
		protected override void ActionOnInsert(IPosition<IMap<TKey, TValue>.IEntry> p)
		{
			if (!IsRoot(p))
			{
				MakeRed(p);
				ResolveRed(p);
			}
		}

		/**
		 * Deletes the given node and performs a splay operation.
		 * @param p The node to delete from the tree.
		 */
		
		protected override void ActionOnDelete(IPosition<IMap<TKey, TValue>.IEntry> p)
		{
			if (IsRed(p))
			{
				MakeBlack(p);
			}
			else if (!IsRoot(p))
			{
				IPosition<IMap<TKey, TValue>.IEntry> sib = Sibling(p);
				if (IsInternal(sib) && (IsBlack(sib) || IsInternal(Left(sib))))
				{
					RemedyDoubleBlack(p);
				}
			}
		}
	}
}
