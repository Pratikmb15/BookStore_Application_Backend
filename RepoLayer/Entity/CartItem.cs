﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Entity
{
    public class CartItem
    {
        [Key]
        public int cartId { get; set; }
        [ForeignKey("User")]
        [Required(ErrorMessage = "User ID is required")]
        public int userId { get; set; }
        [ForeignKey("Book")]
        [Required(ErrorMessage = "Book ID is required")]
        public int bookId { get; set; }
        [Required(ErrorMessage = "Book quantity is required")]
        public int bookQuantity { get; set; } = 1;
        [Required(ErrorMessage = "Book price is required")]
        public int price { get; set; }
        [Required(ErrorMessage = "Book status is required")]
        public bool isPurchased { get; set; } = false;
        public  Book Book { get; set; } 

    }
}
