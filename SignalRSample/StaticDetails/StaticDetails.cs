namespace SignalRSample.StaticDetails
{
	public static class StaticDetails
	{
		static StaticDetails()
		{
			VoteCounts = new Dictionary<string, int>
			{
				{ VoterA, 0 },
				{ VoterB, 0 },
				{ VoterC, 0 }
			};
		}

		public static Dictionary<string, int> VoteCounts;
		public const string VoterA = "VoterA";
		public const string VoterB = "VoterB";
		public const string VoterC = "VoterC";
	}
}