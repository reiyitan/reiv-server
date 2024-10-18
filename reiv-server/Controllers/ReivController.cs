using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reiv_server.DTO;
using reiv_server.Models;
using System.Security.Claims;

namespace reiv_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReivController : ControllerBase {
        private readonly ReivContext _context;
        private readonly UserManager<IdentityUser> _userManager; 

        public ReivController(ReivContext context, UserManager<IdentityUser> userManager) {
            this._context = context;
            this._userManager = userManager; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReivDto>>> GetReivs() {
            List<Reiv> reivs = await _context.Reivs.Include(reiv => reiv.Creator).ToListAsync();
            List<ReivDto> reivDtos = reivs.Select(reiv => 
                new ReivDto(
                    reiv.Title, 
                    reiv.Content, 
                    new ReivCreatorDto(reiv.CreatorId, reiv.Creator.UserName)
                )
            ).ToList(); 

            return Ok(reivDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReivDto>> GetReiv(int id) {
            Reiv? reiv = await _context.Reivs.Include(reiv => reiv.Creator).FirstOrDefaultAsync(reiv => reiv.Id == id);
            if (reiv == null) {
                return NotFound();
            }

            ReivDto reivDto = new ReivDto(reiv.Title, reiv.Content, new ReivCreatorDto(reiv.CreatorId, reiv.Creator.UserName)); 
            return Ok(reivDto);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ReivDto>> PostReiv(CreateReivDto dto) {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized(); 

            IdentityUser? creator = await _userManager.FindByIdAsync(userId);
            if (creator == null) return Unauthorized();

            Reiv reiv = new Reiv(dto, userId, creator);
            _context.Reivs.Add(reiv);
            await _context.SaveChangesAsync();

            ReivDto reivDto = new ReivDto(reiv.Title, reiv.Content, new ReivCreatorDto(reiv.CreatorId, reiv.Creator.UserName));
            return CreatedAtAction(nameof(GetReiv), new { id = reiv.Id }, reivDto);
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<ActionResult<ReivDto>> PatchReiv(int id, UpdateReivDto dto) {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Reiv reiv = await _context.Reivs.Include(reiv => reiv.Creator).FirstOrDefaultAsync(reiv => reiv.Id == id);
            if (reiv == null) {
                return NotFound();
            }

            if (userId == null || userId != reiv.CreatorId) {
                return Forbid();
            }

            if (dto.NewTitle != null) {
                reiv.Title = dto.NewTitle;
            }

            if (dto.NewContent != null) {
                reiv.Content = dto.NewContent;
            }

            await _context.SaveChangesAsync();
            ReivDto reivDto = new ReivDto(reiv.Title, reiv.Content, new ReivCreatorDto(reiv.CreatorId, reiv.Creator.UserName));
            return Ok(reivDto); 
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReiv(int id) {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            Reiv reiv = await _context.Reivs.FindAsync(id);
            if (reiv == null) {
                return NotFound(); 
            }

            if (userId == null || userId != reiv.CreatorId) {
                return Forbid(); 
            }

            _context.Reivs.Remove(reiv);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
