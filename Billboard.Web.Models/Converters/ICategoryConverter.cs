using System;
using System.Diagnostics.Contracts;
using Billboard.Models;

namespace Billboard.Web.Models.Converters
{
	/// <summary>
	/// Model Converter that can Convert between <see cref="Category"/> and <see cref="CategoryModel"/>.
	/// </summary>
	[ContractClass(typeof(CategoryConverterContracts))]
	public interface ICategoryConverter
	{
		/// <summary>
		/// Convert the external model to a new internal entity
		/// </summary>
		/// <param name="categoryModel">The external model to convert to an internal entity</param>
		/// <returns>The external model converted to an entity</returns>
		Category CreateNewEntity(CategoryModel categoryModel);

		/// <summary>
		/// Convert the internal entity
		/// </summary>
		/// <param name="category">The internal entity to conver to an external entity</param>
		/// <returns>The entity converted to an external model.</returns>
		CategoryModel ToExternalModel(Category category);

		/// <summary>
		/// Convert the internal <see cref="Category"/> entity to an external <see cref="CategoryModel"/>.
		/// </summary>
		/// <param name="category">The internal category to convert to an external CategoryModel</param>
		/// <param name="includeAllNotes">Set to <c>true</c> to include all the notes.</param>
		/// <returns>The entity converted to an external model.</returns>
		CategoryModel ToExternalModel(Category category, bool includeAllNotes);

		/// <summary>
		/// Convert the internal <see cref="Category"/> entity to an external <see cref="CategoryModel"/> using a predefined number of notes.
		/// </summary>
		/// <param name="category">The internal category to convert to an external CategoryModel</param>
		/// <param name="noteCount">Set to the number of notes in the category.</param>
		/// <returns>The entity converted to an external model.</returns>
		CategoryModel ToExternalModel(Category category, int noteCount);

		/// <summary>
		/// Update the intenral entity with the data from the external model
		/// </summary>
		/// <param name="category">The entity to update.</param>
		/// <param name="categoryModel">The model to use to update the entity.</param>
		void UpdateEntity(Category category, CategoryModel categoryModel);

	}

	[ContractClassFor(typeof(ICategoryConverter))]
	abstract class CategoryConverterContracts
		: ICategoryConverter
	{
		public Category CreateNewEntity(CategoryModel categoryModel)
		{
			Contract.Requires<ArgumentNullException>(categoryModel != null);
			Contract.Requires<ArgumentException>(categoryModel.Id == Guid.Empty, "Category should not have an id already.");
			Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(categoryModel.Name), "Category requires a name.");
			Contract.Ensures(Contract.Result<Category>() != null);
			throw new NotImplementedException();
		}

		public CategoryModel ToExternalModel(Category category)
		{
			Contract.Requires<ArgumentNullException>(category != null, "category");
			Contract.Ensures(Contract.Result<CategoryModel>() != null);
			Contract.Ensures(Contract.Result<CategoryModel>().Id == category.Id);
			throw new NotImplementedException();
		}

		public CategoryModel ToExternalModel(Category category, bool includeAllNotes)
		{
			Contract.Requires<ArgumentNullException>(category != null, "category");
			Contract.Ensures(Contract.Result<CategoryModel>() != null);
			Contract.Ensures(Contract.Result<CategoryModel>().Id == category.Id);
			throw new NotImplementedException();
		}

		public CategoryModel ToExternalModel(Category category, int noteCount)
		{
			Contract.Requires<ArgumentNullException>(category != null, "category");
			Contract.Ensures(Contract.Result<CategoryModel>() != null);
			Contract.Ensures(Contract.Result<CategoryModel>().Id == category.Id);
			Contract.Ensures(Contract.Result<CategoryModel>().NoteCount == noteCount);
			throw new NotImplementedException();
		}

		public void UpdateEntity(Category category, CategoryModel categoryModel)
		{
			Contract.Requires<ArgumentNullException>(category != null);
			Contract.Requires<ArgumentNullException>(categoryModel != null);
			Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(categoryModel.Name), "Category requires a name.");
			Contract.Requires<ArgumentException>(category.Id == categoryModel.Id, "Id's of category and category model should match.");
			throw new NotImplementedException();
		}
	}
}
