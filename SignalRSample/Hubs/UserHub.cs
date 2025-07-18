using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs
{
	public class UserHub : Hub
	{
		public static int TotalViews { get; set; } = 0;
		public static int TotalUsers { get; set; } = 0;

		public override Task OnConnectedAsync()
		{
			TotalUsers++;
			return base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(Exception? exception)
		{
			TotalUsers--;
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