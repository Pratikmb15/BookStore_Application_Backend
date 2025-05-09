using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RepoLayer.Entity
{
    public class User
    {
        [Key]
        public int userId { get; set; }
        [Required(ErrorMessage = "Full name is required")]
        public string fullName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string password { get; set; }
        [Required(ErrorMessage = "Phone number is required"), Phone(ErrorMessage = "Invalid Phone Number")]
        public string mobileNum { get; set; }
        [Required(ErrorMessage = "Role is required")]
        public string role { get; set; }
        public string refreshToken { get; set; } = "empty";
        public DateTime refreshTokenExpiryTime { get; set; }
    }
}
