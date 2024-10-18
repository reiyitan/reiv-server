using System.ComponentModel.DataAnnotations;

namespace reiv_server.DTO {
    public class UserRegistrationDto {
        [Required] 
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required] 
        public string Password { get; set; } = string.Empty;

        [Required] 
        [StringLength(50)]
        public string Username { get; set; } = string.Empty; 
    }
}
