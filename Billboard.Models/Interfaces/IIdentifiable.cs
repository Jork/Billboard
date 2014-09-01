using System;
using System.Diagnostics.Contracts;

namespace Billboard.Models.Interfaces
{

	/// <summary>
	/// Implemented by objects/entities that are uniquely identifiable by a Guid.
	/// </summary>
	[ContractClass(typeof(IdentifiableContracts))]
	public interface IIdentifiable
	{
		/// <summary>
		/// Gets the unique identifier for the object/entity
		/// </summary>
		Guid Id { get; }
	}

	/// <summary>
	/// Code Contracts for the <see cref="IIdentifiable"/> interface.
	/// </summary>
	[ContractClassFor(typeof(IIdentifiable))]
	abstract class IdentifiableContracts
		: IIdentifiable
	{
		public Guid Id
		{
			get
			{
				Contract.Ensures(Contract.Result<Guid>() != Guid.Empty);
				throw new NotImplementedException();
			}
		}
	}

}
