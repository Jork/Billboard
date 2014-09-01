using System;
using System.Data.Entity;
using System.Diagnostics.Contracts;

namespace Billboard.Models.Persistence
{
	/// <summary>
	/// Unit of Work for the Billboard domain
	/// </summary>
	[ContractClass(typeof(UnitOfWorkContracts))]
	public interface IUnitOfWork
		: IDisposable
	{
		/// <summary>
		/// Gets the repository of notes
		/// </summary>
		IDbSet<Note> Notes { get; }

		/// <summary>
		/// Gets the repository of categories.
		/// </summary>
		IDbSet<Category> Categories { get; }

		/// <summary>
		/// Save/Persist the changes in this unit of work.
		/// </summary>
		void SaveChanges();
	}
}