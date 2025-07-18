using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs
{
	public class UserHub : Hub
	{
		public static int TotalViews { get; set; } = 0;
		public static int TotalUsers { get; set; } = 0;

		public override async Task<Task> OnConnectedAsync()
		{
			TotalUsers++;
			await Clients.All.SendAsync("UpdateTotalUsers", TotalUsers);
			return base.OnConnectedAsync();
		}

		public override async Task<Task> OnDisconnectedAsync(Exception? exception)
		{
			TotalUsers--;
			await Clients.All.SendAsync("UpdateTotalUsers", TotalUsers);
			return base.OnDisconnectedAsync(exception);
		}

		public async Task AddView()
		{
			TotalViews++;
			//send update to clients that view have been updated
			await Clients.All.SendAsync("UpdateTotalViews", TotalViews);
		}
	}
}