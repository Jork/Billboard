using System;
using System.ComponentModel.Composition;
using System.Linq;
using Billboard.Models;

namespace Billboard.Web.Models.Converters
{
	/// <summary>
	/// Converts between <see cref="CategoryModel"/> and <see cref="Category"/> entities.
	/// </summary>
	[Export(typeof(ICategoryConverter))]
	public sealed class CategoryConverter
		: ICategoryConverter
	{
		private readonly INoteConverter _noteConverter;

		/// <summary>
		/// Create a new <see cref="CategoryConverter"/>.
		/// </summary>
		/// <param name="noteConverter">The <see cref="INoteConverter"/> this CategoryConvert requires to add the notes to the category.</param>
		[ImportingConstructor]
		public CategoryConverter(INoteConverter noteConverter)
		{
			_noteConverter = noteConverter;
		}

		/// <summary>
		/// Create a new Category entity based upon the given Category model
		/// </summary>
		/// <param name="categoryModel">The external <see cref="CategoryModel"/> to convert to an internal <see cref="Category"/> entity</param>
		/// <returns>The external model converted to an entity</returns>
		public Category CreateNewEntity(CategoryModel categoryModel)
		{
			return new Category(categoryModel.Name);
		}

		/// <summary>
		/// Convert the internal entity
		/// </summary>
		/// <param name="category">The internal entity to conver to an external entity</param>
		/// <returns>The entity converted to an external model.</returns>
		public CategoryModel ToExternalModel(Category category)
		{
			return ToExternalModel(category, noteCount: 0, includeAllNotes: false);
		}

		/// <summary>
		/// Convert the internal <see cref="Category"/> entity to an external <see cref="CategoryModel"/>.
		/// </summary>
		/// <param name="category">The internal category to convert to an external CategoryModel</param>
		/// <param name="includeAllNotes">Set to <c>true</c> to include all the notes.</param>
		/// <returns>The entity converted to an external model.</returns>
		public CategoryModel ToExternalModel(Category category, bool includeAllNotes)
		{
			return ToExternalModel(category, noteCount: null, includeAllNotes: includeAllNotes);
		}

		/// <summary>
		/// Convert the internal <see cref="Category"/> entity to an external <see cref="CategoryModel"/> using a predefined number of notes.
		/// </summary>
		/// <param name="category">The internal category to convert to an external CategoryModel</param>
		/// <param name="noteCount">Set to the number of notes in the category.</param>
		/// <returns>The entity converted to an external model.</returns>
		public CategoryModel ToExternalModel(Category category, int noteCount)
		{
			return ToExternalModel(category, noteCount, false);
		}

		private CategoryModel ToExternalModel(Category category, int? noteCount, bool includeAllNotes)
		{
			var categoryModel =
				new CategoryModel()
				{
					Id = category.Id,
					Name = category.Name,
					NoteCount = noteCount ?? category.Notes.Count()
				};

			if (includeAllNotes)
			{
				categoryModel.Notes =
					category.Notes
						.Select(_noteConverter.ToClientNoteModel)
						.ToList();
			}

			return categoryModel;
		}


		/// <summary>
		/// Update the intenral entity with the data from the external model
		/// </summary>
		/// <param name="category">The entity to update.</param>
		/// <param name="categoryModel">The model to use to update the entity.</param>
		public void UpdateEntity(Category category, CategoryModel categoryModel)
		{
			throw new NotSupportedException("Renaming the category is currently not supported");
		}
	}
}