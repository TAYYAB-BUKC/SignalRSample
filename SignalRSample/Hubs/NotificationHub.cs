using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs
{
	public class NotificationHub : Hub
	{
		public List<string> Notifications { get; set; } = new List<string>();
		
		public async Task AddNewNotification(string message)
		{
			Notifications.Add(message);
			await SentCurrentNotificationListToClients();
		}

		public async Task SentCurrentNotificationListToClients()
		{
			await Clients.All.SendAsync("UpdateNotificationListAndCount", Notifications);
		}
	}
}