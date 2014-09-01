namespace Billboard.Models.Interfaces
{
	/// <summary>
	/// Represent an item with a unique name (within its repository or collection)
	/// </summary>
	public interface INamed
	{
		/// <summary>
		/// Gets the name of the item.
		/// </summary>
		string Name { get; }
	}
}
