using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Billboard.Web.Models.Interfaces;

namespace Billboard.Web.Models
{
	public class CategoryModel
		: IModel
	{
		/// <summary>
		/// Gets or sets the id of the category
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the name of the category
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the list of notes in this Category
		/// </summary>
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public IList<NoteModel> Notes { get; set; }

		/// <summary>
		/// Gets or sets the number of notes in the Category
		/// </summary>
		public int? NoteCount { get; set; }

	}
}