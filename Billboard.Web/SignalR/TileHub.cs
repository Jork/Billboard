using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Billboard.Web.SignalR
{
	[HubName("tileHub")]
	public class TileHub
		: Hub
	{
		// currently no message that come back from the client
	}
}