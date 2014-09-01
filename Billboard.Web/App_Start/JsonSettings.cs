using System.ComponentModel.Composition;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Billboard.Web
{
	[Export(typeof(IJsonSettings))]
	[PartCreationPolicy(CreationPolicy.Shared)]
	class JsonSettings
		: IJsonSettings
	{
		private readonly JsonSerializerSettings _settings;

		[ImportingConstructor]
		public JsonSettings()
		{
			_settings =
				new JsonSerializerSettings()
				{
					DateFormatHandling = DateFormatHandling.IsoDateFormat,
					MissingMemberHandling = MissingMemberHandling.Ignore,
					ReferenceLoopHandling = ReferenceLoopHandling.Error,
					NullValueHandling = NullValueHandling.Include,
					ObjectCreationHandling = ObjectCreationHandling.Reuse,
					TypeNameHandling = TypeNameHandling.None,
					ContractResolver = new CamelCasePropertyNamesContractResolver()
				};

			_settings.Converters.Add(new IsoDateTimeConverter());
		}

		public JsonSerializerSettings GetSerializerSettings()
		{
			return _settings;
		}
	}
}