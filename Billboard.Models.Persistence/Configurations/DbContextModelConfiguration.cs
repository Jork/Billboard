using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace Billboard.Models.Persistence.Configurations
{
	[Export(typeof(IDbContextModelConfiguration))]
	[PartCreationPolicy(CreationPolicy.NonShared)]
	internal sealed class DbContextModelConfiguration
		: IDbContextModelConfiguration
	{
		[ImportingConstructor]
		public DbContextModelConfiguration()
		{
		}

		public void ConfigureModel(Type dbContextType, DbModelBuilder modelBuilder)
		{
			ConfigureEntities(dbContextType, modelBuilder);
			ConfigurePrimaryKey(modelBuilder);

			// Map protected EF properties and ignore the public matches
			ConfigureEntityFrameworkProperties(modelBuilder);

			// our table names should not be pluralized.
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

		}

		/// <summary>
		/// Configures protected EF Properties to be used, and the matching public properties to be ignored.
		/// </summary>
		/// <param name="modelBuilder">The model builder to use for configuration.</param>
		private static void ConfigureEntityFrameworkProperties(DbModelBuilder modelBuilder)
		{
			Contract.Requires<ArgumentNullException>(modelBuilder != null, "modelBuilder");

			modelBuilder
				.Types()
				.Configure(ec =>
				{
					// if a property ends in EF, use that property (but column name is without EF)
					// and ignore the same property without the EF
					var propertyTwins =
						from efpi in ec.ClrType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic)
						where efpi.Name.EndsWith("EF", StringComparison.Ordinal)
						join pi in ec.ClrType.GetProperties(BindingFlags.Instance | BindingFlags.Public) on efpi.Name equals pi.Name + "EF"
						select new
						{
							EFProperty = efpi,
							PublicProperty = pi
						};

					foreach (var propertyTwin in propertyTwins)
					{
						ec.Property(propertyTwin.EFProperty).HasColumnName(propertyTwin.PublicProperty.Name);
						ec.Ignore(propertyTwin.PublicProperty);
					}
				});
		}

		private static void ConfigurePrimaryKey(DbModelBuilder modelBuilder)
		{
			Contract.Requires<ArgumentNullException>(modelBuilder != null, "modelBuilder");

			// the primary key is always the property with the exact name 'Id'
			modelBuilder
				.Properties()
				.Where(p => p.Name == "Id")
				.Configure(p => p.IsKey());
		}

		private static void ConfigureEntities(Type dbContextType, DbModelBuilder modelBuilder)
		{
			Contract.Requires<ArgumentNullException>(dbContextType != null, "dbContextType");
			Contract.Requires<ArgumentNullException>(modelBuilder != null, "modelBuilder");
			Contract.Assume(modelBuilder.Configurations !=  null, "EF Should always populate the Configurations property of the modelBuilder");

			// get all configurations
			IEnumerable<Type> configuredEntityTypes = GetConfigurations(dbContextType);

			// get all entities
			IEnumerable<Type> allEntityTypes = GetEntities(dbContextType);

			// determine unconfigured types
			IEnumerable<Type> unconfiguredEntityTypes =
				allEntityTypes.Except(configuredEntityTypes);

			// Let EF configure itself using the configurations
			modelBuilder.Configurations.AddFromAssembly(dbContextType.Assembly);

			// And do the remaining ourselfs
			MethodInfo genericMethod =
				modelBuilder.GetType().GetMethods()
					.Single(e => e.Name == "Entity" && e.IsGenericMethodDefinition);
			foreach (Type unconfiguredEntity in unconfiguredEntityTypes)
			{
				MethodInfo method = genericMethod.MakeGenericMethod(new[] { unconfiguredEntity });
				method.Invoke(modelBuilder, new object[0]);
			}
		}

		private static IEnumerable<Type> GetEntities(Type dbContextType)
		{
			Contract.Requires<ArgumentNullException>(dbContextType != null, "dbContextType");

			IEnumerable<Type> allEntityTypes =
				from type in dbContextType.Assembly.GetTypes()
				where !type.IsGenericType && !type.IsAbstract && type.IsClass
					&& typeof(Entity).IsAssignableFrom(type)
				select type;
			return allEntityTypes;
		}

		private static IEnumerable<Type> GetConfigurations(Type dbContextType)
		{
			Contract.Requires<ArgumentNullException>(dbContextType != null, "dbContextType");

			IEnumerable<Type> configuredEntityTypes =
				from type in dbContextType.Assembly.GetTypes()
				where !type.IsGenericType && !type.IsAbstract && type.IsClass
					&& type.BaseType != null && type.BaseType.IsGenericType
					&& type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)
				select type.BaseType.GetGenericArguments()[0];
			return configuredEntityTypes;
		}
	}
}