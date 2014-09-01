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
	public class CategoryController
		: ApiController
	{
		private readonly Lazy<IUnitOfWork> _unitOfWorkFactory;
		private readonly ICategoryConverter _categoryConverter;

		[ImportingConstructor]
		public CategoryController(Lazy<IUnitOfWork> unitOfWorkFactory, ICategoryConverter categoryConverter)
		{
			Contract.Requires<ArgumentNullException>(unitOfWorkFactory != null);
			Contract.Requires<ArgumentNullException>(categoryConverter != null);

			_unitOfWorkFactory = unitOfWorkFactory;
			_categoryConverter = categoryConverter;
		}

		[HttpGet]
		public IEnumerable<CategoryModel> Get()
		{
			using (IUnitOfWork work = _unitOfWorkFactory.Value)
			{
				return work.Categories
					.Select( c => new { Category = c, NoteCount = c.Notes.Count() } )
					.ToList()
					.Select( cc => _categoryConverter.ToExternalModel(cc.Category, cc.NoteCount));
			}
		}

		[HttpGet]
		public CategoryModel Get(Guid id, bool withNotes = false)
		{
			using (IUnitOfWork work = _unitOfWorkFactory.Value)
			{
				Category category = work.Categories.GetById(id);
				if (category == null)
					throw new HttpResponseException(HttpStatusCode.NotFound);

				return _categoryConverter.ToExternalModel(category, includeAllNotes: withNotes);
			}
		}

		public Guid Post([FromBody]CategoryModel categoryModel)
		{
			Contract.Requires<ArgumentNullException>(categoryModel != null);
			Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(categoryModel.Name));

			using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Value)
			{
				Category category = unitOfWork.Categories.GetByName(categoryModel.Name);
				if (category != null)
					throw new InvalidOperationException("Category name already exists");

				category = _categoryConverter.CreateNewEntity(categoryModel);

				unitOfWork.Categories.Add(category);
				unitOfWork.SaveChanges();

				return category.Id;
			}
		}

		// PUT api/<controller>/5
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/<controller>/5
		public void Delete(int id)
		{
		}
	}
}