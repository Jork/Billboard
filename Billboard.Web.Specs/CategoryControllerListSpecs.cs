using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Beaker.TechDays.Billboard.Models.Specs.Builders;
using Beaker.TechDays.Billboard.Web.Specs.Builders;
using Billboard.Models;
using Billboard.Models.Persistence;
using Billboard.Web.Controllers;
using Billboard.Web.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Beaker.TechDays.Billboard.Web.Specs
{
	[TestClass]
	public class CategoryControllerListSpecs
	{
		[TestMethod]
		public void WhenNoCategoriesThenEmptyModelShouldBeReturned()
		{
			//! given: unit of work with no categories.
			IUnitOfWork unitOfWork = new UnitOfWorkMockBuilder()
				.Build();

			//! when: controller is asked for the list.
			CategoryController subject = new CategoryControllerBuilder()
				.WithUnitOfWork(unitOfWork)
				.Build();

			var viewResult = (ViewResult)subject.List();
			var model = (IEnumerable<CategoryModel>)viewResult.ViewData.Model;

			//! then: a model with empty categories should be returned.
			model.Should().NotBeNull();
			model.Should().BeEmpty();
		}

		[TestMethod]
		public void WhenSomeCategoriesThenThoseCategoriesShouldBeReturned()
		{
			//! given: unit of work with 3 categories.
			var cat1 = new Category("Foo");
			var cat2 = new Category("Bar");
			var cat3 = new Category("Spinazie");
			IDbSet<Category> categories = new DbSetMockBuilder<Category>()
				.ContainingEntity(cat1)
				.ContainingEntity(cat2)
				.ContainingEntity(cat3)
				.Build();
			IUnitOfWork unitOfWork = new UnitOfWorkMockBuilder()
				.WithCategoryRepository( categories )
				.Build();

			//! when: controller is asked for the list.
			CategoryController subject = new CategoryControllerBuilder()
				.WithUnitOfWork(unitOfWork)
				.Build();

			var viewResult = (ViewResult)subject.List();
			var model = (IEnumerable<CategoryModel>)viewResult.ViewData.Model;

			//! then: a model with empty categories should be returned.
			model.Should().NotBeNull();
			model.Count().Should().Be(3);
			model.Should().OnlyContain(c => new[] { cat1, cat2, cat3 }.Any(cat => cat.Id == c.Id));
		}

		[TestMethod]
		public void WhenTheCategoryContains3NotesThenNoteCountShouldBe3()
		{
			Category cat = new CategoryBuilder().Build();
			IDbSet<Category> categories = new DbSetMockBuilder<Category>()
				.ContainingEntity(cat)
				.Build();
			IDbSet<Note> notes = new DbSetMockBuilder<Note>()
				.ContainingEntity(new NoteBuilder().InCategory(cat).Build())
				.ContainingEntity(new NoteBuilder().InCategory(cat).Build())
				.ContainingEntity(new NoteBuilder().InCategory(cat).Build())
				.Build();
			IUnitOfWork unitOfWork = new UnitOfWorkMockBuilder()
				.WithCategoryRepository(categories)
				.WithNoteRepository(notes)
				.Build();

			//! when: controller is asked for the list.
			CategoryController subject = new CategoryControllerBuilder()
				.WithUnitOfWork(unitOfWork)
				.Build();

			var viewResult = (ViewResult)subject.List();
			var model = (IEnumerable<CategoryModel>)viewResult.ViewData.Model;

			//! then: the single category should have a note count of 3
			CategoryModel category = model.Single();
			category.NoteCount.Should().Be(3);
		}
	}
}
