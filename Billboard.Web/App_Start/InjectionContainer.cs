using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;

namespace Billboard.Web
{
	sealed class InjectionContainer
		: IInjectionContainer, IDisposable
	{
		private readonly CompositionContainer _container;

		~InjectionContainer()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_container != null)
					_container.Dispose();
			}
		}

		public InjectionContainer()
		{
			Contract.Assume(HttpContext.Current != null, "Cannot create InjectionContainer when HttpContext.Current is not set.");
			Contract.Assume(ControllerBuilder.Current != null, "Cannot create controller builder when ControllerBuilder.Current is not set.");

			if (HttpContext.Current == null)
				throw new InvalidOperationException("Cannot create injection container when no HttpContext.Current has been set.");
			// MEF is used for finding components, but also as a DI framework.

			// Get all the components in the assemblies in the bin folder
			var binFolderCatalog = new DirectoryCatalog(HttpContext.Current.Server.MapPath("~/bin"));
			_container = new CompositionContainer(binFolderCatalog);

			// Compose all the parts
			_container.ComposeParts(this);

			// register our own container, so the constructor can use it as a component for injection
			_container.ComposeExportedValue<IInjectionContainer>(this);

			// configure factory for mvc controllers
			var controllerFactory = _container.GetExportedValue<IControllerFactory>();
			ControllerBuilder.Current.SetControllerFactory(controllerFactory);

			// configure factory for webapi controllers
			var apiControllerFactory = _container.GetExportedValue<IHttpControllerActivator>();
			GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), apiControllerFactory);

			// configure service locator
			var locator = new MefServiceLocatorAdapter(_container);
			ServiceLocator.SetLocatorProvider(() => locator);

		}

		/// <summary>
		/// Manually register a component that was not automaticly registered using MEF.
		/// </summary>
		/// <typeparam name="TComponent">The type of the component to register.</typeparam>
		/// <param name="component">The component to register.</param>
		public void Register<TComponent>(TComponent component)
			where TComponent : class
		{
			_container.ComposeExportedValue<TComponent>(component);
		}

		/// <summary>
		/// Gets the exported component
		/// </summary>
		/// <typeparam name="TComponent">The component to get.</typeparam>
		/// <returns>The exported component.</returns>
		public TComponent GetExportedValue<TComponent>()
			where TComponent : class
		{
			return _container.GetExportedValue<TComponent>();
		}

		/// <summary>
		/// Gets the exported component or when not found, <c>null</c>.
		/// </summary>
		/// <param name="exportType">The type of the component to get.</param>
		/// <returns>The exported component, or <c>null</c> when not found.</returns>
		public object GetExportedValueOrDefault(Type exportType)
		{
			IEnumerable<Lazy<object, object>> exports = _container.GetExports(exportType, null, null);
			Lazy<object, object> export = exports.SingleOrDefault();

			if (export == null)
				return null;

			return export.Value;
		}

		/// <summary>
		/// Gets all the exported components.
		/// </summary>
		/// <typeparam name="TComponent">The interface of the components to get.</typeparam>
		/// <returns>Sequence of the exported components, or an empty sequence when none where found.</returns>
		public IEnumerable<TComponent> GetExportedValues<TComponent>()
			where TComponent : class
		{
			IEnumerable<Lazy<TComponent, IExportPriority>> components = _container.GetExports<TComponent, IExportPriority>();
			foreach (Lazy<TComponent, IExportPriority> component in components.OrderByDescending(c => c.Metadata.Priority))
				yield return component.Value;
		}

	}
}