using System.Data.Entity.Infrastructure;
using System.Diagnostics.CodeAnalysis;
using Billboard.Models.Persistence.Configurations;

namespace Billboard.Models.Persistence.Migrations
{
	[SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Called my migrations to create an instance of our UnitOfWork class")]
	sealed class MigrationsContextFactory
		: IDbContextFactory<UnitOfWork>
	{
		public UnitOfWork Create()
		{
			var configurator = new DbContextModelConfiguration();
			return new UnitOfWork(configurator);
		}
	}
}
