using System;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using System.Web.Mvc;
using System.Web.Routing;

namespace Billboard.Web
{
	[Export(typeof(IStartupConfiguration))]
	[ExportPriority(10)]
	[PartCreationPolicy(CreationPolicy.NonShared)]
	public class RouteConfig
		: IStartupConfiguration
	{
		private readonly RouteCollection _routes;

		[ImportingConstructor]
		public RouteConfig(RouteCollection routes)
		{
			Contract.Requires<ArgumentNullException>(routes != null, "routes");
			_routes = routes;
		}

		/// <summary>
		/// Configure the routes
		/// </summary>
		public void Configure()
		{
			_routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			_routes.MapMvcAttributeRoutes();
			
			AreaRegistration.RegisterAllAreas();

			/* We're working with attribute routing, disable default controller routes
			_routes.MapRoute(
			    name: "Default",
			    url: "{controller}/{action}/{id}",
			    defaults: new { controller = "Category", action = "List", id = UrlParameter.Optional }
			);
			 * */
		}
	}
}
