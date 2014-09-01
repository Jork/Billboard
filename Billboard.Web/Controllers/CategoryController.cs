using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Mvc;
using Billboard.Models;
using Billboard.Models.Persistence;
using Billboard.Web.Models;
using Billboard.Web.Models.Converters;

namespace Billboard.Web.Controllers
{
	[Export]
	[PartCreationPolicy(CreationPolicy.NonShared)]
	public class CategoryController
		: Controller
	{
		private readonly Lazy<IUnitOfWork> _unitOfWork;
		private readonly ICategoryConverter _categoryConverter;

		[ImportingConstructor]
		public CategoryController(Lazy<IUnitOfWork> unitOfWork, ICategoryConverter categoryConverter)
		{
			Contract.Requires<ArgumentNullException>(unitOfWork != null);
			Contract.Requires<ArgumentNullException>(categoryConverter != null);

			_unitOfWork = unitOfWork;
			_categoryConverter = categoryConverter;
		}

		/// <summary>
		/// Gets all the categories to display as Tiles
		/// </summary>
		/// <returns>The model containing all the categories.</returns>
		[HttpGet]
		[Route("")]
		[Route("Category")]
		public ActionResult List()
		{
			// create unit of work
			using (IUnitOfWork work = _unitOfWork.Value)
			{

				// query db and convert to model
				IEnumerable<CategoryModel> model =
					work.Categories
						.Select(c => new { category = c, count = c.Notes.Count() })
						.ToList()
						.Select(cc => _categoryConverter.ToExternalModel(cc.category, cc.count));

				// return view using the model
				return View(model);
			}
		}

		/// <summary>
		/// Gets all the notes in the named category to display as Tiles
		/// </summary>
		/// <param name="name">The name of the category to display</param>
		/// <returns></returns>
		[HttpGet]
		[Route("Category/{name}")]
		public ActionResult ListContent(string name)
		{
			using (IUnitOfWork work = _unitOfWork.Value)
			{
				Category category = work.Categories.GetByName(name);
				if (category == null)
					return HttpNotFound();

				CategoryModel model = _categoryConverter.ToExternalModel(category, includeAllNotes: true);
				return View(model);
			}
		}

	}
}