using System;
using System.Data.Entity;
using System.Diagnostics.Contracts;

namespace Billboard.Models.Persistence.Configurations
{
	[ContractClassFor(typeof(IDbContextModelConfiguration))]
	abstract class DbContextModelConfigurationContracts
		: IDbContextModelConfiguration
	{
		public void ConfigureModel(Type dbContextType, DbModelBuilder modelBuilder)
		{
			Contract.Requires<ArgumentNullException>(dbContextType != null);
			Contract.Requires<ArgumentNullException>(modelBuilder != null);

			throw new NotImplementedException();
		}
	}
}