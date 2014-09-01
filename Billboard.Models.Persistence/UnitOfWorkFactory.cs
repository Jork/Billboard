using System;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using Billboard.Models.Persistence.Configurations;

namespace Billboard.Models.Persistence
{
	[Export(typeof(IUnitOfWorkFactory))]
	[PartCreationPolicy(CreationPolicy.NonShared)]
	sealed class UnitOfWorkFactory
		: IUnitOfWorkFactory
	{
		private readonly IDbContextModelConfiguration _configurator;

		/// <summary>
		/// Creates a new instance of the Unit of Work Factory
		/// </summary>
		/// <param name="configurator">The configurator to use for configuring entity framework model.</param>
		[ImportingConstructor]
		public UnitOfWorkFactory(IDbContextModelConfiguration configurator)
		{
			Contract.Requires<ArgumentNullException>(configurator != null);
			_configurator = configurator;
		}

		/// <summary>
		/// Begin a new unit of work.
		/// </summary>
		/// <returns>The new unit of work that was started.</returns>
		public IUnitOfWork BeginWork()
		{
			return new UnitOfWork(_configurator);
		}
	}
}