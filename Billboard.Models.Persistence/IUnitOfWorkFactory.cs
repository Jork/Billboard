using System.Diagnostics.Contracts;

namespace Billboard.Models.Persistence
{
	/// <summary>
	/// Factory for starting new units of work
	/// </summary>
	[ContractClass(typeof(UnitOfWorkFactoryContracts))]
	public interface IUnitOfWorkFactory
	{
		/// <summary>
		/// Begin a new unit of work.
		/// </summary>
		/// <returns>The new unit of work that was started.</returns>
		IUnitOfWork BeginWork();
	}
}
