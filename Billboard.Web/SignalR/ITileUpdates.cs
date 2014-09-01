using System;

namespace Billboard.Web.SignalR
{
	/// <summary>
	/// Interface for sending tile updates to the client(s)
	/// </summary>
	interface ITileUpdates
	{
		/// <summary>
		/// The category with the given id has been updated.
		/// </summary>
		/// <param name="id">The id of the category that has been updated.</param>
		/// <param name="name">The (new) name of the category.</param>
		/// <param name="count">The (new) count on the tile of the category.</param>
		void CategoryUpdate( Guid id, string name, int count );

		/// <summary>
		/// The note in the given category with the given id has been updated.
		/// </summary>
		/// <param name="category">The id of the category the note is in.</param>
		/// <param name="id">The id of the note that has been updated.</param>
		/// <param name="title">The title of the note.</param>
		void NoteUpdate(Guid category, Guid id, string title);
	}
}
