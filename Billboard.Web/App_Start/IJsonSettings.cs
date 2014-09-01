using System.Diagnostics.Contracts;
using Newtonsoft.Json;

namespace Billboard.Web
{
	/// <summary>
	/// Access to the JSON Serializer Settings.
	/// </summary>
	[ContractClass(typeof(JsonSettingsContracts))]
	public interface IJsonSettings
	{
		/// <summary>
		/// Gets the JSON Serializer Settings.
		/// </summary>
		/// <returns>The JSON Serializer Settings.</returns>
		JsonSerializerSettings GetSerializerSettings();
	}

	/// <summary>
	/// Code Contracts for the <see cref="IJsonSettings"/> interface.
	/// </summary>
	[ContractClassFor(typeof(IJsonSettings))]
	abstract class JsonSettingsContracts
		: IJsonSettings
	{
		public JsonSerializerSettings GetSerializerSettings()
		{
			Contract.Ensures(Contract.Result<JsonSerializerSettings>() != null);
			throw new System.NotImplementedException();
		}
	}

}