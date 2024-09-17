namespace reiv_server.Models {
    public class User {
        public int Id { get; set; } 
        public string? Username { get; set; }
        public ICollection<Reiv> Reivs { get; set; } = new List<Reiv>(); 
    }
}
