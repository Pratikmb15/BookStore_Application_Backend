using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class ResetPasswordModel
    {
        [Required (ErrorMessage ="Password is required")]
        public string NewPassWord { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        public string ConfirmPassWord { get; set; }
    }
}
