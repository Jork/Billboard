using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using Microsoft.Practices.ServiceLocation;

namespace Billboard.Web
{
	/// <summary>
	/// Adapter for the ServiceLocator that used MEF to locate services.
	/// </summary>
	/// <remarks>
	/// The NuGet Package for the MefAdapter is unsigned. Created our own instead.
	/// </remarks>
	public class MefServiceLocatorAdapter
		: ServiceLocatorImplBase
	{
		private readonly ExportProvider _provider;

		public MefServiceLocatorAdapter(ExportProvider provider)
		{
			_provider = provider;
		}

		protected override object DoGetInstance(Type serviceType, string key)
		{
			if (string.IsNullOrEmpty(key))
				key = AttributedModelServices.GetContractName(serviceType);

			IEnumerable<Lazy<object>> exports = _provider.GetExports<object>(key);

			var export = exports.FirstOrDefault();
			if (export != null)
				return export.Value;

			throw new ActivationException(string.Format("Could not locate any instances of contract {0}", key));
		}

		protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
		{
			var exports = _provider.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
			return exports;
		}
	}
}