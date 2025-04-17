using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    class UserRegisterModel
    {
        public string fullName { get; set; }
        [Required, EmailAddress]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required, Phone]
        public string mobileNum { get; set; }
        [Required]
        public string? role { get; set; } = "User";
    }
}
