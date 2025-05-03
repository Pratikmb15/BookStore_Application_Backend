using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Entity
{
    public class CustomerDetail
    {
        [Key]
        public int customerId { get; set; }
        [Required(ErrorMessage ="Customer Full name is required")]
        public string fullName { get; set; }
        [Required,Phone]
        public string mobileNumber { get; set; }
        [Required(ErrorMessage ="Address is required")]
        public string address { get; set; }
        [Required(ErrorMessage ="City is required")]
        public string city { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string state { get; set; }
        [ForeignKey("User")]
        public int userId { get; set; }


    }
}
