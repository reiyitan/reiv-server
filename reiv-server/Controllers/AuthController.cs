using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using reiv_server.DTO;

namespace reiv_server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager; 

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) {
            this._userManager = userManager;
            this._signInManager = signInManager; 
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto dto) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            IdentityUser user = new IdentityUser {
                UserName = dto.Username,
                Email = dto.Email
            };

            IdentityResult result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded) {
                foreach (IdentityError e in result.Errors) {
                    ModelState.AddModelError(string.Empty, e.Description);
                }
                return BadRequest(ModelState);
            }
            
            await _signInManager.SignInAsync(user, false);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState); 
            }

            IdentityUser user = await _userManager.FindByNameAsync(dto.Username);
            if (user == null) {
                return NotFound("Invalid username"); 
            }

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, dto.Password, false, false);
            if (!result.Succeeded) {
                return Unauthorized("Invalid username or password"); 
            }

            return Ok(); 
        }
    }
}
