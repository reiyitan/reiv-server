using reiv_server.DTO;

namespace reiv_server.Models
{
    public class Post {

        public Post() { }
        public Post(CreatePostDto dto) {
            this.Content = dto.Content;
            this.CreatorId = dto.CreatorId;
        }

        public int Id { get; set; }
        public string? Content { get; set; }
        public int CreatorId { get; set; }
        public User Creator { get; set; } = null!;
    }
}
