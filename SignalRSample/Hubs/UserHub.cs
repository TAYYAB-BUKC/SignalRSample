using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs
{
	public class UserHub : Hub
	{
		public static int TotalViews { get; set; } = 0;

		public async Task AddView()
		{
			TotalViews++;
			//send update to clients that view have been updated
			await Clients.All.SendAsync("UpdateTotalViews", TotalViews);
		}
	}
}