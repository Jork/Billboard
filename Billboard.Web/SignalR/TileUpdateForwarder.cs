using System;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace Billboard.Web.SignalR
{
	/// <summary>
	/// Sends the message to the actual SignalR hub.
	/// </summary>
	[Export(typeof(ITileUpdates))]
	public class TileUpdateForwarder
		: ITileUpdates
	{
		private readonly Lazy<IHubContext> _context;

		[ImportingConstructor]
		public TileUpdateForwarder(IConnectionManager connectionManager)
		{
			Contract.Requires<ArgumentNullException>(connectionManager != null);
			_context = new Lazy<IHubContext>(connectionManager.GetHubContext<TileHub>);
		}

		/// <summary>
		/// The category with the given id has been updated.
		/// </summary>
		/// <param name="id">The id of the category that has been updated.</param>
		/// <param name="name">The (new) name of the category.</param>
		/// <param name="count">The (new) count on the tile of the category.</param>
		public void CategoryUpdate(Guid id, string name, int count)
		{
			IHubContext context = _context.Value;
			context.Clients.All.categoryUpdate(id, name, count);
		}

		/// <summary>
		/// The note in the given category with the given id has been updated.
		/// </summary>
		/// <param name="category">The id of the category the note is in.</param>
		/// <param name="id">The id of the note that has been updated.</param>
		/// <param name="title">The title of the note.</param>
		public void NoteUpdate(Guid category, Guid id, string title)
		{
			IHubContext context = _context.Value;
			context.Clients.All.noteUpdate(category, id, title);
		}
	}
}