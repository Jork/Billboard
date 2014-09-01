using System;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using System.Web.Optimization;

namespace Billboard.Web
{
	[Export(typeof(IStartupConfiguration))]
	[ExportPriority(30)]
	public sealed class BundleConfig
		: IStartupConfiguration
	{
		private readonly BundleCollection _bundles;

		[ImportingConstructor]
		public BundleConfig(BundleCollection bundles)
		{
			Contract.Requires<ArgumentNullException>(bundles != null, "bundles");
			_bundles = bundles;
		}

		public void Configure()
		{
			_bundles.Add(
				new ScriptBundle("~/bundles/libraries")
					.Include("~/Scripts/modernizr-{version}.js")
					.Include("~/Scripts/jquery-{version}.js")
					.Include("~/Scripts/knockout-{version}.js")
					.Include("~/Scripts/jquery.transit.js")
					.Include("~/Scripts/jquery.signalR-{version}.js"));

			_bundles.Add(
				new ScriptBundle("~/bundles/billboard")
					.Include("~/Scripts/billboard.js"));

			_bundles.Add(
				new StyleBundle("~/bundles/style")
					.Include("~/Content/Site.css"));
		}
	}
}
