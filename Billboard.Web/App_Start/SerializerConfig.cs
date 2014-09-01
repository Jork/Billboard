using System;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using System.Web.Http;

namespace Billboard.Web
{
	[Export(typeof(IStartupConfiguration))]
	[PartCreationPolicy(CreationPolicy.Shared)]
	public class SerializerConfig
		: IStartupConfiguration
	{
		private readonly HttpConfiguration _httpConfiguration;
		private readonly IJsonSettings _jsonSettings;

		[ImportingConstructor]
		public SerializerConfig(HttpConfiguration httpConfiguration, IJsonSettings jsonSettings)
		{
			Contract.Requires<ArgumentNullException>(httpConfiguration != null);
			Contract.Requires<ArgumentNullException>(jsonSettings != null);

			_httpConfiguration = httpConfiguration;
			_jsonSettings = jsonSettings;
		}

		public void Configure()
		{
			_httpConfiguration.Formatters.JsonFormatter.SerializerSettings = _jsonSettings.GetSerializerSettings();

			//// replace json deserializer with JSON.NET deserializer
			//ValueProviderFactories.Factories.Remove(ValueProviderFactories.Factories.OfType<JsonValueProviderFactory>().FirstOrDefault());
			//ValueProviderFactories.Factories.Add(new JsonNetValueProviderFactory());

			//// add typeconverters that understand ISO Formatting for dates and durations
			//TypeDescriptor.AddAttributes(typeof(DateTime), new TypeConverterAttribute(typeof(JsonDateTimeConverter)));

			//ModelBinders.Binders.Add(typeof(DateTime), new DateTimeBinder());

			//// store so other processes might access same settings
			//JsonSettings.SetSettings(settings);
		}
	}
}