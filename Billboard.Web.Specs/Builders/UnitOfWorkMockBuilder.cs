using System;
using System.Data.Entity;
using Beaker.TechDays.Billboard.Models.Specs.Helpers;
using Billboard.Models;
using Billboard.Models.Persistence;
using Moq;

namespace Beaker.TechDays.Billboard.Web.Specs.Builders
{
	public sealed class UnitOfWorkMockBuilder
	{
		private Maybe<IDbSet<Category>> _categories;
		private Maybe<IDbSet<Note>> _notes;

		public UnitOfWorkMockBuilder WithCategoryRepository(IDbSet<Category> categories)
		{
			_categories = new Maybe<IDbSet<Category>>(categories);
			return this;
		}

		public UnitOfWorkMockBuilder WithNoteRepository(IDbSet<Note> notes)
		{
			_notes = new Maybe<IDbSet<Note>>(notes);
			return this;
		}

		public IUnitOfWork Build()
		{
			IDbSet<Category> categories = _categories.GetValueOrDefault(new DbSetMockBuilder<Category>().Build);
			IDbSet<Note> notes = _notes.GetValueOrDefault(new DbSetMockBuilder<Note>().Build);

			var work = new Mock<IUnitOfWork>(MockBehavior.Strict);
			work.SetupGet(w => w.Categories).Returns(categories);
			work.SetupGet(w => w.Notes).Returns(notes);

			Mock<IDisposable> disposable = work.As<IDisposable>();
			disposable.Setup(d => d.Dispose());

			return work.Object;
		}
	}
}
