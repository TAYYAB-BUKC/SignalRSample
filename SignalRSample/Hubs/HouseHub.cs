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

				await Groups.AddToGroupAsync(Context.ConnectionId, houseName);
			}
		}

		public async Task LeaveHouse(string houseName)
		{
			var houseKey = $"{Context.ConnectionId}:{houseName}";
			if (HousesJoined.Contains(houseKey))
			{
				HousesJoined.Remove(houseKey);

				await Groups.RemoveFromGroupAsync(Context.ConnectionId, houseName);
			}
		}
	}
}