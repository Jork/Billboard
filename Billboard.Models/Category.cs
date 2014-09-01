using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using Billboard.Models.Interfaces;

namespace Billboard.Models
{
	public class Category
		: Entity, INamed
	{
		private string _name;
		private readonly IEntityCollection<Note> _notes;

		public Category(string name)
			: this()
		{
			Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(name));
			Contract.Requires<ArgumentOutOfRangeException>(name.Length <= 50, "Name has a maximum length of 50.");

			_name = name;
		}

		internal protected Category()
			: base()
		{
			_notes = new EntityCollection<Note>(n => n.Category = this, n => n.Category = null);
		}

		/// <summary>
		/// Gets the name of the Category
		/// </summary>
		[StringLength(50)]
		[Required]
		public string Name
		{
			get
			{
				Contract.Ensures(!String.IsNullOrEmpty(Contract.Result<string>()));
				Contract.Ensures(Contract.Result<string>().Length <= 50);
				return _name;
			}
			internal set
			{
				Contract.Requires(!String.IsNullOrEmpty(value));
				Contract.Requires(value.Length <= 50);
				_name = value;
			}
		}

		/// <summary>
		/// Gets the set of all the notes in the category
		/// </summary>
		public virtual IEntityCollection<Note> Notes
		{
			get
			{
				Contract.Ensures(Contract.Result<ICollection<Note>>() != null);
				return _notes;
			}
		}
	}
}
