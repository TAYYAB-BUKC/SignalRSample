using Microsoft.AspNetCore.SignalR;
using SignalRSample.Services.Interfaces;

namespace SignalRSample.Hubs
{
	public class NotificationHub : Hub
	{
		private readonly INotificationTracker _notificationTracker;
		public NotificationHub(INotificationTracker notificationTracker)
		{
			_notificationTracker = notificationTracker;
		}

		public async Task AddNewNotification(string message)
		{
			_notificationTracker.NotificationsList.Add(message);
			await SentCurrentNotificationListToClients();
		}

		public async Task SentCurrentNotificationListToClients()
		{
			await Clients.All.SendAsync("UpdateNotificationListAndCount", _notificationTracker.NotificationsList);
		}
	}
}