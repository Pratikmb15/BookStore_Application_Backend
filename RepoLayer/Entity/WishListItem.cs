using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Entity
{
    public class WishListItem
    {
        [Key]
        public int whishListId { get; set; }
        [ForeignKey("User")]
        [Required(ErrorMessage = "User ID is required")]
        public int userId { get; set; }
        [ForeignKey("Book")]
        [Required(ErrorMessage = "Book ID is required")]
        public int bookId { get; set; }
        public Book Book { get; set; }
    }
}
