using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reiv_server.DTO;
using reiv_server.Models;

namespace reiv_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReivController : ControllerBase {
        private readonly ReivContext _context;

        public ReivController(ReivContext context) {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reiv>>> GetReivs() {
            List<Reiv> reivs = await _context.Reivs.ToListAsync();
            return Ok(reivs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reiv>> GetReiv(int id) {
            Reiv? reiv = await _context.Reivs.FindAsync(id);
            if (reiv == null) {
                return NotFound();
            }
            return Ok(reiv);
        }

        [HttpPost]
        public async Task<ActionResult<Reiv>> PostReiv(CreateReivDto dto) {
            Reiv reiv = new Reiv(dto);
            _context.Reivs.Add(reiv);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetReiv), new { id = reiv.Id }, reiv);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReiv(int id) {
            Reiv? reiv = await _context.Reivs.FindAsync(id);
            if (reiv == null) {
                return NotFound(); 
            }
            _context.Reivs.Remove(reiv);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
