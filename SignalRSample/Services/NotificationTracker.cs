using SignalRSample.Services.Interfaces;

namespace SignalRSample.Services
{
	public class NotificationTracker : INotificationTracker
	{
		public List<string> NotificationsList { get; set; } = new List<string>();
	}
}