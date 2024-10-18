﻿using System.ComponentModel.DataAnnotations;

namespace reiv_server.DTO {
    public class UserLoginDto {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = string.Empty; 

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty; 
    }
}
