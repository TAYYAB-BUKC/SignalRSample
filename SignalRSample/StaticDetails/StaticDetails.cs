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
		public const string VoterA = "Voter A";
		public const string VoterB = "Voter B";
		public const string VoterC = "Voter C";
	}
}