using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalRSample.Data;
using SignalRSample.Models;
using System.Security.Claims;

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

		[HttpGet]
		[Route("/[controller]/GetChatUsers")]
		public async Task<ActionResult<object>> GetChatUsers()
		{
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var users = await _context.Users.ToListAsync();
            if(users is null)
            {
                return NotFound();
            }

			return users.Where(u => u.Id != userId).Select(u => new
            {
                u.Id,
                u.UserName
            }).ToList();
		}
	}
}
