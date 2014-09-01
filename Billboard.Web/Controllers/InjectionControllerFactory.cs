using System;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Billboard.Web.Controllers
{
	/// <summary>
	/// Controller factory that uses the IInjectionController to get the controllers
	/// </summary>
	[Export(typeof(IControllerFactory))]
	public sealed class InjectionControllerFactory
		: DefaultControllerFactory
	{
		private readonly IInjectionContainer _container;

		[ImportingConstructor]
		public InjectionControllerFactory(IInjectionContainer container)
		{
			Contract.Requires<ArgumentNullException>(container != null);
			_container = container;
		}

		protected override Type GetControllerType(RequestContext requestContext, string controllerName)
		{
			Type type = base.GetControllerType(requestContext, controllerName);

			if( type == null )
				throw new HttpException((int)HttpStatusCode.NotFound, "Page not found.");

			return type;
		}

		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			if (controllerType == null)
				return null;

			// ask injection container for the controller
			var controller = _container.GetExportedValueOrDefault(controllerType) as IController;

			if (controller == null)
				throw new HttpException((int)HttpStatusCode.NotFound, "Page not found.");

			return controller;
		}
	}
}