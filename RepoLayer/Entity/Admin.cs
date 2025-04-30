using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RepoLayer.Entity
{
    public class Admin
    {
        [Key]
        public int userId { get; set; }
        [Required(ErrorMessage = "FullName is required")]
        public string fullName { get; set; }
        [Required(ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Invalid Email")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }
        [Required(ErrorMessage = "Phone number is required"), Phone(ErrorMessage = "Invalid Phone Number")]
        public string mobileNum { get; set; }
        [Required(ErrorMessage = "Role is required")]
        public string role { get; set; }

        public string refreshToken { get; set; }
        public DateTime refreshTokenExpiryTime { get; set; }
    }
}
