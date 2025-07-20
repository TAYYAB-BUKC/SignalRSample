using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs
{
	public class VotingHub : Hub
	{
		public Dictionary<string, int> VotingStatus()
		{
			return StaticDetails.StaticDetails.VoteCounts;
		}

		//public override async Task<Task> OnConnectedAsync()
		//{
		//	await Clients.Caller.SendAsync("UpdateVotingStatus",
		//	   StaticDetails.StaticDetails.VoteCounts[StaticDetails.StaticDetails.VoterA],
		//	   StaticDetails.StaticDetails.VoteCounts[StaticDetails.StaticDetails.VoterB],
		//	   StaticDetails.StaticDetails.VoteCounts[StaticDetails.StaticDetails.VoterC]
		//   );
		//	return base.OnConnectedAsync();
		//}
	}
}