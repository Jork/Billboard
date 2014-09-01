using System;
using System.Diagnostics.Contracts;

namespace Billboard.Web
{
	/// <summary>
	/// All components that are registerd with this interface, will be called on startup to configure themselfs.
	/// </summary>
	[ContractClass(typeof(StartupConfigurationContracts))]
	public interface IStartupConfiguration
	{
		/// <summary>
		/// Configure the component during startup
		/// </summary>
		void Configure();
	}

	[ContractClassFor(typeof(IStartupConfiguration))]
	abstract class StartupConfigurationContracts
		: IStartupConfiguration
	{
		public void Configure()
		{
			throw new NotImplementedException();
		}
	}
}
