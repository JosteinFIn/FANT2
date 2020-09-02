using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FANT2.Data;
using FANT2.Models;
using FANT2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FANT2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public MessagesApiController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
	        _context = context;
	        _userManager = userManager;
        }

        
      
        // POST: api/MessagesApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Message>> PostMessage(CreateMessage message)
        {

	        var user = await _userManager.GetUserAsync(User);
	        var model = new Message()
	        {
		        AnnonseId = message.AnnonseId,
		        Melding = message.Melding,
                FromUserId = user.Id

	        };
            _context.Melding.Add(model);
            await _context.SaveChangesAsync();

            return Ok(message);
        }

        // DELETE: api/MessagesApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Message>> DeleteMessage(int id)
        {
            var message = await _context.Melding.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            _context.Melding.Remove(message);
            await _context.SaveChangesAsync();

            return message;
        }

        private bool MessageExists(int id)
        {
            return _context.Melding.Any(e => e.Id == id);
        }
    }
}
