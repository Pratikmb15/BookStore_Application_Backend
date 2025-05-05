using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class UCartItemModel
    {
        [Required(ErrorMessage = "Book ID is required")]
        public int bookId { get; set; }
        [Required(ErrorMessage = "Book quantity is required")]
        public int bookQuantity { get; set; } = 1;
    }
}
