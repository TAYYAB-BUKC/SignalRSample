using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs
{
	public class HouseHub : Hub
	{
		public List<string> HousesJoined { get; set; } = new List<string>();

		public async Task JoinHouse(string houseName)
		{
			var houseKey = $"{Context.ConnectionId}:{houseName}";
			if(!HousesJoined.Contains(houseKey))
			{
				HousesJoined.Add(houseKey);

				var clientHouses = GetClientHouses(Context.ConnectionId);
				await Clients.Caller.SendAsync("SubscriptionStatus", clientHouses, houseName, true);

				await Groups.AddToGroupAsync(Context.ConnectionId, houseName);
			}
		}

		private string GetClientHouses(string connectionId)
		{
			return String.Join(", ", HousesJoined.Where(h => h.Contains(connectionId))
				.ToList().Select(h => h.Split(":")[1]));
		}

		public async Task LeaveHouse(string houseName)
		{
			var houseKey = $"{Context.ConnectionId}:{houseName}";
			if (HousesJoined.Contains(houseKey))
			{
				HousesJoined.Remove(houseKey);

				var clientHouses = GetClientHouses(Context.ConnectionId);
				await Clients.Caller.SendAsync("SubscriptionStatus", clientHouses, houseName, false);

				await Groups.RemoveFromGroupAsync(Context.ConnectionId, houseName);
			}
		}
	}
}