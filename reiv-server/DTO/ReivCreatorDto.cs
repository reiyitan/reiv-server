namespace reiv_server.DTO {
    public class ReivCreatorDto {
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;

        public ReivCreatorDto(string id, string username) {
            this.Id = id;
            this.Username = username; 
        }
    }
}
