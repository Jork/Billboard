using System;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using System.Web.Mvc;
using System.Web.Routing;

namespace Billboard.Web
{
	[Export(typeof(IStartupConfiguration))]
	[ExportPriority(20)]
	public sealed class AttributeRoutingConfig
		: IStartupConfiguration
	{
		private readonly RouteCollection _routes;

		[ImportingConstructor]
		public AttributeRoutingConfig(RouteCollection routes)
		{
			Contract.Requires<ArgumentNullException>(routes != null, "routes");
			_routes = routes;
		}

		public void Configure()
		{
			_routes.MapMvcAttributeRoutes();
		}
	}
}
