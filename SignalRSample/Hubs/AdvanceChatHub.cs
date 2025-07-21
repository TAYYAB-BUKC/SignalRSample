using Microsoft.AspNetCore.SignalR;
using SignalRSample.Data;
using SignalRSample.Helpers;
using System.Security.Claims;

namespace SignalRSample.Hubs
{
	public class AdvanceChatHub : Hub
	{
		private readonly ApplicationDbContext _dbContext;
		public AdvanceChatHub(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public override async Task<Task> OnConnectedAsync()
		{
			var userId = Context?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
			if(!string.IsNullOrWhiteSpace(userId))
			{
				var userEmail = (await _dbContext.Users.FindAsync(userId))?.Email;
				await Clients.Users(HubHelper.OnlineUsers()).SendAsync("NewUserConnected", userId, userEmail);
				HubHelper.AddUserConnection(userId, Context.ConnectionId);
			}
			return base.OnConnectedAsync();
		}
	}
}