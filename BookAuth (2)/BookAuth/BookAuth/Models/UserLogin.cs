using System.ComponentModel.DataAnnotations;

namespace BookAuth.Models
{
    public class UserLogin
    {
            [Required, EmailAddress]
            public string Email { get; set; }

            [Required, MinLength(6, ErrorMessage = "Please enter a minimum of 6 characters")]
            public string Password { get; set; }
        
    }

}

