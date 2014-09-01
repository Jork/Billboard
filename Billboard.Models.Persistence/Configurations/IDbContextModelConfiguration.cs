using System;
using System.Data.Entity;
using System.Diagnostics.Contracts;

namespace Billboard.Models.Persistence.Configurations
{
	/// <summary>
	/// Configures a <see cref="DbContext"/> model.
	/// </summary>
	[ContractClass(typeof(DbContextModelConfigurationContracts))]
	internal interface IDbContextModelConfiguration
	{
		/// <summary>
		/// Configure the model fot the db context.
		/// </summary>
		/// <param name="dbContextType">The db context type to configure the model for</param>
		/// <param name="modelBuilder">The <see cref="DbModelBuilder"/> object to use for configuration.</param>
		void ConfigureModel(Type dbContextType, DbModelBuilder modelBuilder);
	}
}