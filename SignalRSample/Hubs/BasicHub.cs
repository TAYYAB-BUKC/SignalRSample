using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs
{
	public class BasicHub : Hub
	{
		public override async Task<Task> OnConnectedAsync()
		{
			// If you need to send notification to each client
			//await Clients.All.SendAsync("MethodName", parameter);

			// If you need to send notification back to the request Initiator
			//await Clients.Caller.SendAsync("MethodName", parameter);

			// If you need to send notification to all the client except request Initiator
			//await Clients.Others.SendAsync("MethodName", parameter);

			// If you need to send notification to a particular client
			//await Clients.Client("ConnectionIdOfClient").SendAsync("MethodName", parameter);

			// If you need to send notification to particular clients
			//await Clients.Clients("ConnectionIdOfClient", "ConnectionIdOfClient").SendAsync("MethodName", parameter);

			// If you need to send notification to all clients except particular clients
			//await Clients.AllExcept("ConnectionIdOfClient", "ConnectionIdOfClient").SendAsync("MethodName", parameter);

			// If you need to send notification to all tabs of one client
			//await Clients.User("UserIdOfClient").SendAsync("MethodName", parameter);

			// If you need to send notification to particular users
			//await Clients.Users("UserIdOfClient", "UserIdOfClient").SendAsync("MethodName", parameter);

			// If you need to add client to a group
			//await Groups.AddToGroupAsync("Context.ConnectionId", "GroupName");

			return base.OnConnectedAsync();
		}
	}
}