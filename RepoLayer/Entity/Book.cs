using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RepoLayer.Entity
{
    public class Book
    {
        [Key]  
        public int bookId { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string description { get; set; } = null!;
        [Required(ErrorMessage = "Discount Price is required")]
        public int discountPrice { get; set; }

        [Required(ErrorMessage = "Book Image is required")]
        public string bookImage { get; set; } = null!;
     
        [ForeignKey("userId")]
        public int userId { get; set; }

        [Required(ErrorMessage = "Book Name is required")]
        public string bookName { get; set; } = null!;
      
        [Required(ErrorMessage = "Author is required")]
        public string author { get; set; } = null!;
        [Required(ErrorMessage = "Quantity is required")]
        public int quantity { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public int price { get; set; }
       
        public DateTime createdAtDate { get; set; }
      
        public DateTime updatedAtDate { get; set; }
        
        
    }
}
