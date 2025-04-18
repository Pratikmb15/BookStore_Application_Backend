﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class UserLoginModel
    {
        [Required, EmailAddress]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
