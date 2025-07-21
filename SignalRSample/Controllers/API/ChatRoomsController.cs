using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalRSample.Data;
using SignalRSample.Models;

namespace SignalRSample.Controllers.API
{
	[ApiController]
    public class ChatRoomsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChatRoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ChatRooms
        [HttpGet]
		[Route("/[controller]/GetChatRooms")]
		public async Task<ActionResult<IEnumerable<ChatRoom>>> GetChatRooms()
        {
            return await _context.ChatRooms.ToListAsync();
        }

        // POST: api/ChatRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
		[Route("/[controller]/PostChatRoom")]
		public async Task<ActionResult<ChatRoom>> PostChatRoom(ChatRoom chatRoom)
        {
            _context.ChatRooms.Add(chatRoom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChatRoom", new { id = chatRoom.Id }, chatRoom);
        }

        // DELETE: api/ChatRooms/5
        [HttpDelete]
		[Route("/[controller]/DeleteChatRoom/{id}")]
		public async Task<IActionResult> DeleteChatRoom(int id)
        {
            var chatRoom = await _context.ChatRooms.FindAsync(id);
            if (chatRoom == null)
            {
                return NotFound();
            }

            _context.ChatRooms.Remove(chatRoom);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
