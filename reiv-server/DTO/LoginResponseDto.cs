using System.ComponentModel.DataAnnotations;

namespace reiv_server.DTO {
    public class LoginResponseDto {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty; 
    }
}
