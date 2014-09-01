using System;
using System.ComponentModel;
using System.ComponentModel.Composition;

namespace Billboard.Web
{
	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class ExportPriorityAttribute
		: Attribute, IExportPriority
	{
		public ExportPriorityAttribute(int priority)
		{
			Priority = priority;
		}

		/// <summary>
		/// Gets or sets the priority. The higher the number the sooner it will be called.
		/// </summary>
		public int Priority { get; set; }
	}

	public interface IExportPriority
	{
		[DefaultValue(0)]
		int Priority { get; }
	}
}