using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Billboard.Web
{

	/// <summary>
	/// Access to the dependecy injection container, for manual getting components
	/// </summary>
	[ContractClass(typeof(InjectionContainerContracts))]
	public interface IInjectionContainer
	{
		/// <summary>
		/// Manually register a component that was not automaticly registered using MEF.
		/// </summary>
		/// <typeparam name="TComponent">The type of the component to register.</typeparam>
		/// <param name="component">The component to register.</param>
		void Register<TComponent>(TComponent component)
			where TComponent : class;

		/// <summary>
		/// Gets the exported component
		/// </summary>
		/// <typeparam name="TComponent">The component to get.</typeparam>
		/// <returns>The exported component.</returns>
		TComponent GetExportedValue<TComponent>()
			where TComponent : class;

		/// <summary>
		/// Gets the exported component or when not found, <c>null</c>.
		/// </summary>
		/// <param name="exportType">The type of the component to get.</param>
		/// <returns>The exported component, or <c>null</c> when not found.</returns>
		object GetExportedValueOrDefault(Type exportType);

		/// <summary>
		/// Gets all the exported components.
		/// </summary>
		/// <typeparam name="TComponent">The interface of the components to get.</typeparam>
		/// <returns>Sequence of the exported components, or an empty sequence when none where found.</returns>
		IEnumerable<TComponent> GetExportedValues<TComponent>()
			where TComponent : class;
	}

	/// <summary>
	/// Code Contracts for the <see cref="IInjectionContainer"/> interface.
	/// </summary>
	[ContractClassFor(typeof(IInjectionContainer))]
	abstract class InjectionContainerContracts
		: IInjectionContainer
	{
		public void Register<TComponent>(TComponent component)
			where TComponent : class
		{
			Contract.Requires<ArgumentNullException>(component != null);
			throw new NotImplementedException();
		}

		public TComponent GetExportedValue<TComponent>()
			where TComponent : class
		{
			Contract.Ensures(Contract.Result<TComponent>() != null);
			throw new NotImplementedException();
		}

		public object GetExportedValueOrDefault(Type exportType)
		{
			Contract.Requires<ArgumentNullException>(exportType != null);
			throw new NotImplementedException();
		}

		public IEnumerable<TComponent> GetExportedValues<TComponent>()
			where TComponent : class
		{
			Contract.Ensures(Contract.Result<IEnumerable<TComponent>>() != null);
			Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<TComponent>>(), c => c != null));
			throw new NotImplementedException();
		}
	}
}