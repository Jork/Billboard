using System;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using System.Web.Http;

namespace Billboard.Web
{
	[Export(typeof(IStartupConfiguration))]
	[ExportPriority(20)]
	public sealed class WebApiConfig
		: IStartupConfiguration
	{
		private readonly HttpConfiguration _httpConfiguration;

		[ImportingConstructor]
		public WebApiConfig(HttpConfiguration httpConfiguration)
		{
			Contract.Requires<ArgumentNullException>(httpConfiguration != null, "httpConfiguration");
			_httpConfiguration = httpConfiguration;
		}

		public void Configure()
		{
			_httpConfiguration.MapHttpAttributeRoutes();

			_httpConfiguration.Routes.MapHttpRoute(
			    name: "DefaultApi",
			    routeTemplate: "api/{controller}/{id}",
			    defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
