using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Web.Http;
using Billboard.Models;
using Billboard.Models.Persistence;
using Billboard.Web.Models;
using Billboard.Web.Models.Converters;

namespace Billboard.Web.ApiControllers
{
	[Export]
	[PartCreationPolicy(CreationPolicy.NonShared)]
	public class NoteController
		: ApiController
	{
		private readonly Lazy<IUnitOfWork> _unitOfWorkFactory;
		private readonly INoteConverter _noteConverter;

		[ImportingConstructor]
		public NoteController(Lazy<IUnitOfWork> unitOfWorkFactory, INoteConverter noteConverter)
		{
			Contract.Requires<ArgumentNullException>(unitOfWorkFactory != null);
			Contract.Requires<ArgumentNullException>(noteConverter != null);

			_unitOfWorkFactory = unitOfWorkFactory;
			_noteConverter = noteConverter;
		}

		public IEnumerable<NoteModel> Get()
		{
			using (IUnitOfWork work = _unitOfWorkFactory.Value)
			{
				return work.Notes
					.Select(_noteConverter.ToClientNoteModel)
					.ToList();
			}
		}

		public NoteModel Get(Guid id)
		{
			using (IUnitOfWork work = _unitOfWorkFactory.Value)
			{
				Note note = work.Notes.GetById(id);
				if (note == null)
					throw new HttpResponseException(HttpStatusCode.NotFound);

				return _noteConverter.ToClientNoteModel(note);
			}
		}

		public Guid Post([FromBody]NoteModel noteModel)
		{
			Contract.Requires<ArgumentNullException>(noteModel != null);
			Contract.Requires<ArgumentException>(noteModel.Id == Guid.Empty, "Note has already an id.");
			Contract.Requires<ArgumentException>(noteModel.CategoryId != Guid.Empty, "Missing category");
			Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(noteModel.Title), "Missing title");
			Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(noteModel.Message), "Missing message");
			Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(noteModel.Email), "Missing email address");

			using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Value)
			{
				Category category = unitOfWork.Categories.GetById(noteModel.CategoryId);
				if (category == null)
					throw new ArgumentException("Invalid Category.");

				Note n = _noteConverter.CreateNewNoteFromModel(noteModel);
				category.Notes.Add(n);

				unitOfWork.Notes.Add(n);
				unitOfWork.SaveChanges();

				return n.Id;
			}
		}

		public void Put(Guid id, [FromBody]NoteModel noteModel)
		{
			Contract.Requires<ArgumentNullException>(noteModel != null);
			Contract.Requires<ArgumentException>(noteModel.Id == id, "Id's do not match");
			Contract.Requires<ArgumentException>(noteModel.CategoryId != Guid.Empty, "Missing category");
			Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(noteModel.Title), "Missing title");
			Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(noteModel.Message), "Missing message");
			Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(noteModel.Email), "Missing email address");

			using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Value)
			{
				Note originalNote = unitOfWork.Notes.GetById(id);
				if (originalNote == null)
					throw new ArgumentException("Note with given id not found.");

				_noteConverter.UpdateNoteEntity(originalNote, noteModel);

				unitOfWork.SaveChanges();
			}
		}

		public bool Delete(Guid id)
		{
			using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Value)
			{
				Note originalNote = unitOfWork.Notes.GetById(id);
				if (originalNote == null)
					return false;

				unitOfWork.Notes.Remove(originalNote);

				unitOfWork.SaveChanges();
				return true;
			}
		}
	}
}