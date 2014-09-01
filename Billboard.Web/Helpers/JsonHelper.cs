using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Billboard.Web.Helpers
{
	/// <summary>
	/// Helper for all kind of JSON serialization functions
	/// </summary>
	public static class JsonHelper
	{
		/// <summary>
		/// Serialize an object to a JSON string
		/// </summary>
		/// <param name="html">The <see cref="HtmlHelper"/> that supplies the context.</param>
		/// <param name="objectToSerialize">The object to serialize.</param>
		/// <returns>A JSON serialized representation of the given object</returns>
		[SuppressMessage( "Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "Source of JsonTextWriter shows that dispose does not dispose outer jsonSerializer aswell" )]
		[SuppressMessage( "Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "html", Justification = "While html paramater is not used, it is required to make this method a extension method on the HTML helper" )]
		[SuppressMessage( "Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "object", Justification = "I'm serializing .NET objects to JSON, so I think the name is suitable" )]
		public static MvcHtmlString ToJson( this HtmlHelper html, object objectToSerialize )
		{
			return MvcHtmlString.Create( ToJson( objectToSerialize  ) );
		}

		/// <summary>
		/// Stringifies a [JSON to LINQ] object tree
		/// </summary>
		/// <param name="html">The <see cref="HtmlHelper"/> that supplies the context</param>
		/// <param name="javaScriptObject">The java script object.</param>
		/// <returns>A Stringified version of the [JSON to LINQ] object tree</returns>
		[SuppressMessage( "Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "Source of JsonTextWriter shows that dispose does not dispose outer jsonSerializer aswell" )]
		[SuppressMessage( "Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "html", Justification = "While html paramater is not used, it is required to make this method a extension method on the HTML helper" )]
		public static MvcHtmlString ToJson( this HtmlHelper html, JObject javaScriptObject )
		{
			if( javaScriptObject == null )
				return MvcHtmlString.Create( "{}" );

			using( StringWriter stringWriter = new StringWriter( new StringBuilder( 256 ), CultureInfo.InvariantCulture ) )
			{
				using( JsonTextWriter jsonTextWriter = new JsonTextWriter( stringWriter ) )
				{
					jsonTextWriter.Formatting = Formatting.None;
					javaScriptObject.WriteTo( jsonTextWriter );
				}

				return MvcHtmlString.Create(
					stringWriter.ToString()
				);
			}
		}

		/// <summary>
		/// Serializes the given object to a JSON string
		/// </summary>
		/// <param name="objectToSerialize">The object to serialize.</param>
		/// <returns>A JSON serialized representation of the given object</returns>
		[SuppressMessage( "Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "Source of JsonTextWriter shows that dispose does not dispose outer jsonSerializer aswell" )]
		[SuppressMessage( "Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "object", Justification = "I'm serializing .NET objects to JSON, so I think the name is suitable" )]
		public static string ToJson( object objectToSerialize )
		{
			var jsonSettings = ServiceLocator.Current.GetInstance<IJsonSettings>().GetSerializerSettings();

			JsonSerializer jsonSerializer = JsonSerializer.Create(jsonSettings);
			using( StringWriter stringWriter = new StringWriter( new StringBuilder( 256 ), CultureInfo.InvariantCulture ) )
			{
				using( JsonTextWriter jsonTextWriter = new JsonTextWriter( stringWriter ) )
				{
					jsonTextWriter.Formatting = Formatting.None;
					jsonSerializer.Serialize( jsonTextWriter, objectToSerialize );
				}

				return stringWriter.ToString();
			}
		}

		/// <summary>
		/// Encodes the given string for use as a javascript string
		/// </summary>
		/// <param name="html">The <see cref="HtmlHelper"/> that supplies the context</param>
		/// <param name="item">The string to encode for use inside a javascript string</param>
		/// <returns>String encoded for use inside a javascript string</returns>
		[SuppressMessage( "Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "html", Justification = "While html paramater is not used, it is required to make this method a extension method on the HTML helper" )]
		public static MvcHtmlString JavaScriptEncode( this HtmlHelper html, string item )
		{
			return MvcHtmlString.Create( HttpUtility.JavaScriptStringEncode( item ) );
		}
	}
}