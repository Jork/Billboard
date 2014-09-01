using Microsoft.Owin;
using Owin;

namespace Billboard.Web
{
	public class OwinStartup
	{
		public void Configuration(IAppBuilder app)
		{
			app.MapSignalR();
		}
	}
}