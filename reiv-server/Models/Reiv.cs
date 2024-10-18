using Microsoft.AspNetCore.Identity;
using reiv_server.DTO;
using System.ComponentModel.DataAnnotations;

namespace reiv_server.Models {
    public class Reiv {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        public string CreatorId { get; set; } = string.Empty;

        [Required]
        public IdentityUser Creator { get; set; } = null!;

        public Reiv() { }
        public Reiv(CreateReivDto dto, string creatorId, IdentityUser creator) {
            this.Title = dto.Title ?? string.Empty; 
            this.Content = dto.Content ?? string.Empty;
            this.CreatorId = creatorId; 
            this.Creator = creator; 
        }
    }
}
