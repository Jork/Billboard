using System;
using System.Diagnostics.Contracts;
using Billboard.Models;

namespace Billboard.Web.Models.Converters
{
	/// <summary>
	/// Converts between <see cref="NoteModel"/> and <see cref="Note"/> entities.
	/// </summary>
	[ContractClass(typeof(NoteConverterContracts))]
	public interface INoteConverter
	{
		Note CreateNewNoteFromModel(NoteModel noteModel);
		NoteModel ToClientNoteModel(Note note);
		void UpdateNoteEntity(Note note, NoteModel noteModel);
	}

	/// <summary>
	/// Code Contracts for the <see cref="INoteConverter"/> interface.
	/// </summary>
	[ContractClassFor(typeof(INoteConverter))]
	abstract class NoteConverterContracts
		: INoteConverter
	{
		public Note CreateNewNoteFromModel(NoteModel noteModel)
		{
			Contract.Requires<ArgumentNullException>(noteModel != null);
			Contract.Requires<ArgumentException>(noteModel.CategoryId != Guid.Empty, "Note requires a category to belong to.");
			Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(noteModel.Title), "Note requires a title.");
			Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(noteModel.Message), "Note requires a message body.");
			Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(noteModel.Email), "Note requires an email address.");
			Contract.Ensures(Contract.Result<Note>() != null);
			throw new NotImplementedException();
		}

		public NoteModel ToClientNoteModel(Note note)
		{
			Contract.Requires<ArgumentNullException>(note != null);
			Contract.Ensures(Contract.Result<NoteModel>() != null);
			throw new NotImplementedException();
		}

		public void UpdateNoteEntity(Note note, NoteModel noteModel)
		{
			Contract.Requires<ArgumentNullException>(note != null);
			Contract.Requires<ArgumentNullException>(noteModel != null);
			Contract.Requires<ArgumentException>(noteModel.CategoryId != Guid.Empty, "Note requires a category to belong to.");
			Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(noteModel.Title), "Note requires a title.");
			Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(noteModel.Message), "Note requires a message body.");
			Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(noteModel.Email), "Note requires an email address.");
			Contract.Requires<ArgumentException>(note.Id == noteModel.Id, "Id's of note and note model should match.");
			throw new NotImplementedException();
		}
	}

}
