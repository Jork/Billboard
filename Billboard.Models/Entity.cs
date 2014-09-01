using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using Billboard.Models.Interfaces;

namespace Billboard.Models
{
	/// <summary>
	/// Base class for entities persisted in the entity framework
	/// </summary>
	public abstract class Entity
		: IEntity, IIdentifiable
	{
		private Guid _id;

		protected Entity()
		{
			Id = Guid.NewGuid();
		}

		/// <summary>
		/// The 'internal' id of the entity (required by EF)
		/// </summary>
		[Browsable(false)]
		public Guid Id
		{
			get
			{
				// via Interface: Contract.Ensures(Contract.Result<Guid>() != Guid.Empty);
				return _id;
			}
			protected internal set
			{
				Contract.Requires(value != Guid.Empty);
				_id = value;
			}
		}
	}
}
