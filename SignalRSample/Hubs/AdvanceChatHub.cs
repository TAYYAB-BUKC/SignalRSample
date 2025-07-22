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
				await Clients.Users(HubHelper.OnlineUsers()).SendAsync("UserConnected", userEmail);
				HubHelper.AddUserConnection(userId, Context.ConnectionId);
			}
			return base.OnConnectedAsync();
		}

		public override async Task<Task> OnDisconnectedAsync(Exception? exception)
		{
			var userId = Context?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
			if (!string.IsNullOrWhiteSpace(userId))
			{
				var userEmail = (await _dbContext.Users.FindAsync(userId))?.Email;
				if (HubHelper.HasUserConnection(userId, Context.ConnectionId))
				{
					var userConnections = HubHelper.Users[userId];
					userConnections.Remove(Context.ConnectionId);
				}
				await Clients.Users(HubHelper.OnlineUsers()).SendAsync("UserDisconnected", userEmail);
			}
			
			return base.OnDisconnectedAsync(exception);
		}

		public async Task NewRoomAdded(string roomName)
		{
			var userId = Context?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
			if (!string.IsNullOrWhiteSpace(userId))
			{
				var userEmail = (await _dbContext.Users.FindAsync(userId))?.Email;
				await Clients.All.SendAsync("NewRoomAdded", userEmail, roomName);
			}
		}

		public async Task RoomDeleted(string roomName)
		{
			var userId = Context?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
			if (!string.IsNullOrWhiteSpace(userId))
			{
				var userEmail = (await _dbContext.Users.FindAsync(userId))?.Email;
				await Clients.All.SendAsync("RoomDeleted", userEmail, roomName);
			}
		}

		public async Task SendPublicMessage(string roomName, string message)
		{
			var userId = Context?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
			if (!string.IsNullOrWhiteSpace(userId))
			{
				var userEmail = (await _dbContext.Users.FindAsync(userId))?.Email;
				await Clients.All.SendAsync("ReceivePublicMessage", userEmail, roomName, message);
			}
		}

		public async Task SendPrivateMessage(string receiverEmail, string message)
		{
			var userId = Context?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
			if (!string.IsNullOrWhiteSpace(userId))
			{
				var senderEmail = (await _dbContext.Users.FindAsync(userId))?.Email;
				await Clients.All.SendAsync("ReceivePrivateMessage", senderEmail, receiverEmail, message);
			}
		}
	}
}