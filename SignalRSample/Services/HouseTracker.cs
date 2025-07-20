using SignalRSample.Services.Interfaces;

namespace SignalRSample.Services
{
	public class HouseTracker : IHouseTracker
	{
		public List<string> HousesJoined { get; set; } = new List<string>();
	}
}