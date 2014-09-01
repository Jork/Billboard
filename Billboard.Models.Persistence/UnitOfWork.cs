using System;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using Billboard.Models.Persistence.Configurations;
using Billboard.Models.Persistence.Migrations;

namespace Billboard.Models.Persistence
{
	/// <summary>
	/// Unit of Work for the Billboard domain
	/// </summary>
	[Export(typeof(IUnitOfWork))]
	[PartCreationPolicy(CreationPolicy.NonShared)]
	internal sealed class UnitOfWork
		: DbContext, IUnitOfWork
	{
		private readonly IDbContextModelConfiguration _configurator;

		/// <summary>
		/// Creates a new instance of the Unit of Work
		/// </summary>
		/// <param name="configurator">The configurator to use for configuring entity framework model.</param>
		[ImportingConstructor]
		public UnitOfWork(IDbContextModelConfiguration configurator)
			: base("Billboard")
		{
			Contract.Requires<ArgumentNullException>(configurator != null, "configurator");
			_configurator = configurator;

			Database.SetInitializer(new MigrateDatabaseToLatestVersion<UnitOfWork, Configuration>());
		}

		/// <summary>
		/// This method is called when the model for a derived context has been initialized, 
		/// but before the model has been locked down and used to initialize the context.
		/// </summary>
		/// <param name="modelBuilder">The builder that defines the model for the context being created. </param>
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			_configurator.ConfigureModel(GetType(), modelBuilder);
			base.OnModelCreating(modelBuilder);
		}

		/// <summary>
		/// Gets the repository of notes
		/// </summary>
		public IDbSet<Note> Notes
		{
			get { return Set<Note>(); }
		}

		/// <summary>
		/// Gets the repository of categories
		/// </summary>
		public IDbSet<Category> Categories
		{
			get { return Set<Category>(); }
		}

		/// <summary>
		/// Save/Persist the changes in this unit of work.
		/// </summary>
		void IUnitOfWork.SaveChanges()
		{
			base.SaveChanges();
		}
	}

	/// <summary>
	/// Contract class for the <see cref="IUnitOfWork"/> interface.
	/// </summary>
	[ContractClassFor(typeof(IUnitOfWork))]
	abstract class UnitOfWorkContracts
		: IUnitOfWork
	{
		public IDbSet<Note> Notes
		{
			get
			{
				Contract.Ensures(Contract.Result<IDbSet<Note>>() != null);
				throw new NotImplementedException();
			}
		}

		public IDbSet<Category> Categories
		{
			get
			{
				Contract.Ensures(Contract.Result<IDbSet<Category>>() != null);
				throw new NotImplementedException();
			}
		}

		public void SaveChanges()
		{
			throw new NotImplementedException();
		}

		#region IDisposable

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
