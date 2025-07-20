using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRSample.Data;

namespace SignalRSample.Hubs
{
	public class BasicChatHub : Hub
	{
		private readonly ApplicationDbContext _dbContext;
		public BasicChatHub(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task SendMessage(string sender, string message)
		{
			await Clients.All.SendAsync("NewMessageReceived", sender, message);
		}

		[Authorize]
		public async Task SendPrivateMessage(string sender, string receiver, string message)
		{
			var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == receiver.ToLower());
			if(user is not null)
			{
				await Clients.User(user.Id).SendAsync("NewMessageReceived", sender, message);
			}
		}
	}
}