﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class ForgetPassModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
    }
}
