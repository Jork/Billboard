using System;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.UI;

namespace Billboard.Web.ApiControllers
{
	[Export(typeof(IHttpControllerActivator))]
	public sealed class ApiControllerFactory
		: IHttpControllerActivator
	{
		private readonly IInjectionContainer _container;

		[ImportingConstructor]
		public ApiControllerFactory(IInjectionContainer container)
		{
			Contract.Requires<ArgumentNullException>(container != null, "container");
			_container = container;
		}

		public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
		{
			var controller = _container.GetExportedValueOrDefault(controllerType) as IHttpController;
			return controller;
		}
	}
}