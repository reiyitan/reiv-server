using System.ComponentModel.DataAnnotations;

namespace reiv_server.DTO {
    public class CreateReivDto {
        [Required] 
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty; 
    }
}
