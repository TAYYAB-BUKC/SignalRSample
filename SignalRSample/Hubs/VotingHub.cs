using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs
{
	public class VotingHub : Hub
	{
		public Dictionary<string, int> VotingStatus()
		{
			return StaticDetails.StaticDetails.VoteCounts;
		}
	}
}