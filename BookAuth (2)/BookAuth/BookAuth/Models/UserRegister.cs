﻿using System.ComponentModel.DataAnnotations;


namespace BookAuth.Models
{
    public class UserRegister
    {
        public int Id { get; set; }
        [Required]
        public string? FirstName { get; set; } = string.Empty;
        [Required]
        public string? LastName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(6, ErrorMessage = "Please enter minimun 6 character")]
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = "User";
    }
}
