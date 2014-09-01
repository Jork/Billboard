using System;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using System.Web.Mvc;
using Billboard.Models.Persistence;
using Billboard.Web.Models;
using Billboard.Web.Models.Converters;

namespace Billboard.Web.Controllers
{
	[Export]
	[PartCreationPolicy(CreationPolicy.NonShared)]
	public class NoteController
		: Controller
	{
		private readonly Lazy<IUnitOfWork> _unitOfWork;
		private readonly INoteConverter _noteConverter;

		[ImportingConstructor]
		public NoteController(Lazy<IUnitOfWork> unitOfWork, INoteConverter noteConverter)
		{
			Contract.Requires<ArgumentNullException>(unitOfWork != null);
			Contract.Requires<ArgumentNullException>(noteConverter != null);

			_unitOfWork = unitOfWork;
			_noteConverter = noteConverter;
		}

		[Route("Note/{id:guid}")]
		[HttpGet]
		public ActionResult Details(Guid id)
		{
			// create unit of work
			using (IUnitOfWork work = _unitOfWork.Value)
			{
				// find note by id
				var note = work.Notes.GetById(id);
				if (note == null)
					return HttpNotFound();

				// convert to client model
				var model = _noteConverter.ToClientNoteModel(note);

				// return view using the model
				return View(model);
			}
		}

		[Route("Note")]
		[HttpGet]
		public ActionResult New(Guid catId)
		{
			// create unit of work
			using (IUnitOfWork work = _unitOfWork.Value)
			{
				// find category by id
				var category = work.Categories.GetById(catId);
				if (category == null)
					return HttpNotFound();

				// convert to client model
				var model = new NoteModel
				{
					CategoryId = catId,
					Title = "Nieuwe notitie"
				};

				// return the details view using the new, empty model
				return View("Details", model);
			}
		}
	}
}