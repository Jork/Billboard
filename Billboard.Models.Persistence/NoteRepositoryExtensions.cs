using System;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Billboard.Models.Persistence
{
	public static class NoteRepositoryExtensions
	{
		/// <summary>
		/// Gets the number of notes in the given category.
		/// </summary>
		/// <param name="notes">The notes repository to query.</param>
		/// <param name="category">The category the notes should belong to.</param>
		/// <returns>The number of notes in the category.</returns>
		public static int GetCountForCategory(this IDbSet<Note> notes, Category category)
		{
			Contract.Requires<ArgumentNullException>(notes != null, "notes");
			Contract.Requires<ArgumentNullException>(category != null, "category");

			Guid id = category.Id;
			return notes.Count(n => n.Category.Id == id);
		}
	}
}