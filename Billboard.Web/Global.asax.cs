using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;

namespace Billboard.Web
{
	public class MvcApplication
		: HttpApplication
	{
		private IInjectionContainer _container;

		protected void Application_Start()
		{
			_container = new InjectionContainer();

			// register some MVC objects required for configuration
			_container.Register(GlobalFilters.Filters);
			_container.Register(RouteTable.Routes);
			_container.Register(BundleTable.Bundles);
			_container.Register(GlobalConfiguration.Configuration);
			_container.Register(GlobalHost.ConnectionManager);

			// AreaRegistration.RegisterAllAreas();

			// let all configurable items, configure themselfs.
			var configurables = _container.GetExportedValues<IStartupConfiguration>();
			foreach (IStartupConfiguration configurable in configurables)
				configurable.Configure();

			GlobalConfiguration.Configuration.EnsureInitialized();
		}

	}
}
