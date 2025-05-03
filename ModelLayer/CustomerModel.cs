using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class CustomerModel
    {
        [Required(ErrorMessage = "Customer Full name is required")]
        public string fullName { get; set; }
        [Required, Phone]
        public string mobileNumber { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string address { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string city { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string state { get; set; }
    }
}
