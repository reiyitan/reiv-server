﻿namespace reiv_server.Models {
    public class User {
        public int Id { get; set; } 
        public string? Username { get; set; }
        public ICollection<Post> Posts { get; set; } = new List<Post>(); 
    }
}
