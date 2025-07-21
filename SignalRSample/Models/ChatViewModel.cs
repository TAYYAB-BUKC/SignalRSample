namespace SignalRSample.Models
{
	public class ChatViewModel
	{
		public ChatViewModel()
		{
			Rooms = new List<ChatRoom>();
			MaxRoomsAllowed = 4;
		}

		public int MaxRoomsAllowed { get; set; }
		public List<ChatRoom> Rooms { get; set; }
		public string? UserId { get; set; }
		public bool IsRoomAllowedToAdd => Rooms is null || Rooms.Count < MaxRoomsAllowed;
	}
}