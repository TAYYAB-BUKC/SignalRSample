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

				await Groups.RemoveFromGroupAsync(Context.ConnectionId, houseName);
			}
		}
	}
}