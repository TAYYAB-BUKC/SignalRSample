using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs
{
	public class BasicChatHub : Hub
	{
		public async Task SendMessage(string sender, string message)
		{
			await Clients.All.SendAsync("NewMessageReceived", sender, message);
		}
	}
}