using Microsoft.AspNetCore.SignalR;
using SignalRSample.Services.Interfaces;

namespace SignalRSample.Hubs
{
	public class HouseHub : Hub
	{
		private readonly IHouseTracker _houseTracker;
		public HouseHub(IHouseTracker houseTracker)
		{
			_houseTracker = houseTracker;
		}

		public async Task JoinHouse(string houseName)
		{
			var houseKey = $"{Context.ConnectionId}:{houseName}";
			if(!_houseTracker.HousesJoined.Contains(houseKey))
			{
				_houseTracker.HousesJoined.Add(houseKey);

				var clientHouses = GetClientHouses(Context.ConnectionId);
				await Clients.Caller.SendAsync("SubscriptionStatus", clientHouses, houseName, true);
				//await Clients.AllExcept(Context.ConnectionId).SendAsync("NewSubscriptionMessage", $"Member have subscribed to {houseName} successfully");
				await Clients.Others.SendAsync("NewSubscriptionMessage", $"Member have subscribed to {houseName} successfully");

				await Groups.AddToGroupAsync(Context.ConnectionId, houseName);
			}
		}

		private string GetClientHouses(string connectionId)
		{
			return String.Join(", ", _houseTracker.HousesJoined.Where(h => h.Contains(connectionId))
				.ToList().Select(h => h.Split(":")[1]));
		}

		public async Task LeaveHouse(string houseName)
		{
			var houseKey = $"{Context.ConnectionId}:{houseName}";
			if (_houseTracker.HousesJoined.Contains(houseKey))
			{
				_houseTracker.HousesJoined.Remove(houseKey);

				var clientHouses = GetClientHouses(Context.ConnectionId);
				await Clients.Caller.SendAsync("SubscriptionStatus", clientHouses, houseName, false);

				//await Clients.AllExcept(Context.ConnectionId).SendAsync("UnsubscriptionMessage", $"Member have unsubscribed to {houseName} successfully");
				await Clients.Others.SendAsync("UnsubscriptionMessage", $"Member have unsubscribed to {houseName} successfully");

				await Groups.RemoveFromGroupAsync(Context.ConnectionId, houseName);
			}
		}
	}
}