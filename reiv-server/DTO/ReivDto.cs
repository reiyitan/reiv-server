namespace reiv_server.DTO {
    public class ReivDto {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public ReivCreatorDto Creator { get; set; } = null!;

        public ReivDto(string title, string content, ReivCreatorDto dto) {
            this.Title = title;
            this.Content = content;
            this.Creator = dto;
        }
    }
}
