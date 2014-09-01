using System;
using Beaker.TechDays.Billboard.Models.Specs.Helpers;
using Billboard.Models.Persistence;
using Billboard.Web.Controllers;
using Billboard.Web.Models.Converters;

namespace Beaker.TechDays.Billboard.Web.Specs.Builders
{
	public sealed class CategoryControllerBuilder
	{
		private Maybe<IUnitOfWork> _unitOfWork;

		public CategoryControllerBuilder WithUnitOfWork(IUnitOfWork unitOfWork)
		{
			_unitOfWork = new Maybe<IUnitOfWork>(unitOfWork);
			return this;
		}

		public CategoryController Build()
		{
			IUnitOfWork unitOfWork = _unitOfWork.GetValueOrDefault(new UnitOfWorkMockBuilder().Build);

			// use actual implementations of converters during unit tests. 
			// Mocking them is to much work for such a simple conversion functionality
			ICategoryConverter converter = new CategoryConverter(new NoteConverter());

			return new CategoryController(new Lazy<IUnitOfWork>(() => unitOfWork), converter);
		}
	}
}
