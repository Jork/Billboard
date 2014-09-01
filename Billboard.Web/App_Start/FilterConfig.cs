using System;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Web.Mvc;

namespace Billboard.Web
{
	[Export(typeof(IStartupConfiguration))]
	public sealed class FilterConfig
		: IStartupConfiguration
	{
		private readonly GlobalFilterCollection _filters;

		[ImportingConstructor]
		public FilterConfig(GlobalFilterCollection filters)
		{
			Contract.Requires<ArgumentNullException>(filters != null, "filters");
			_filters = filters;
		}

		[ContractInvariantMethod]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
		private void ObjectInvariant()
		{
			Contract.Invariant(_filters != null);
		}

		/// <summary>
		/// Configure the filters.
		/// </summary>
		public void Configure()
		{
			_filters.Add(new HandleErrorAttribute());
		}
	}
}
