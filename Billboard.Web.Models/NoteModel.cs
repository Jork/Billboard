using System;
using Billboard.Web.Models.Interfaces;

namespace Billboard.Web.Models
{
	public sealed class NoteModel
		: IModel
	{
		/// <summary>
		/// Gets or sets the category this note belongs to.
		/// </summary>
		public Guid CategoryId { get; set; }

		/// <summary>
		/// Gets or sets the id of the note
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the title of the note
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the message of the note
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Gets or sets the optional price of the note
		/// </summary>
		public decimal? Price { get; set; }

		/// <summary>
		/// Gets or sets the email address
		/// </summary>
		public string Email { get; set; }

	}
}