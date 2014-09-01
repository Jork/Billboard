using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using Billboard.Models;

namespace Billboard.Web.Models.Converters
{
	[Export(typeof(INoteConverter))]
	public sealed class NoteConverter
		: INoteConverter
	{
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Validated by Code Contracts on Interface")]
		public Note CreateNewNoteFromModel(NoteModel noteModel)
		{
			return new Note(noteModel.Title, noteModel.Message, noteModel.Price, new EmailAddress(noteModel.Email));
		}

		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Validated by Code Contracts on Interface")]
		public NoteModel ToClientNoteModel(Note note)
		{
			return new NoteModel
			{
				CategoryId = note.Category.Id,
				Id = note.Id,
				Title = note.Title,
				Message = note.Message,
				Price = note.Price,
				Email = note.EmailAddress.Value
			};
		}

		/// <summary>
		/// Update the internal Note entity with the data from the external Note model
		/// </summary>
		/// <param name="note">The Note entity to update.</param>
		/// <param name="noteModel">The Note model to use to update the entity.</param>
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Validated by Code Contracts on Interface")]
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Justification = "Validated by Code Contracts on Interface")]
		public void UpdateNoteEntity(Note note, NoteModel noteModel)
		{
			note.Title = noteModel.Title;
			note.Message = noteModel.Message;
			note.Price = noteModel.Price;
			note.EmailAddress = new EmailAddress(noteModel.Email);
		}
	}
}
