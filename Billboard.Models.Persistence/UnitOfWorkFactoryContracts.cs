using System;
using System.Diagnostics.Contracts;

namespace Billboard.Models.Persistence
{
	/// <summary>
	/// Contract class for the <see cref="IUnitOfWorkFactory"/> interface.
	/// </summary>
	[ContractClassFor(typeof(IUnitOfWorkFactory))]
	abstract class UnitOfWorkFactoryContracts
		: IUnitOfWorkFactory
	{
		public IUnitOfWork BeginWork()
		{
			Contract.Ensures(Contract.Result<IUnitOfWork>() != null);
			throw new NotImplementedException();
		}
	}
}