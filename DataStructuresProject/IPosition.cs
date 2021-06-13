namespace DataStructures
{
	/**
	* Position provides an interface for elements of a 
	* positional ADTs.
	*
	* @author Zach Samuels
	* 
	* @param <E> The generic data type contained in the ADT.
   */
	public interface IPosition<E> 
    {
		/**
		 * Gets the element at this Position.
		 * 
		 * @return The element at this position.
		 */
		E GetElement();
	}
}
