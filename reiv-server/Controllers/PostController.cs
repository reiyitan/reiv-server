using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reiv_server.DTO;
using reiv_server.Models;

namespace reiv_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase {
        private readonly ReivContext _context;

        public PostController(ReivContext context) {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts() {
            return await _context.Posts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id) {
            Post? post = await _context.Posts.FindAsync(id);
            if (post == null) {
                return NotFound();
            }
            return post;
        }

        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(CreatePostDto dto) {
            Post post = new Post(dto); 
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

    }
}
