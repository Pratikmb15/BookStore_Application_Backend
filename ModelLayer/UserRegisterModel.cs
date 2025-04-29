using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage = "Full name is required")]
        public string fullName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string password { get; set; }
        [Required(ErrorMessage = "Phone number is required"), Phone(ErrorMessage = "Invalid Phone Number")]
        public string mobileNum { get; set; }
        
    }
}
