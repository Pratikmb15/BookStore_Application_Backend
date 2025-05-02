using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class WishListItemModel
    {
        [Required(ErrorMessage = "Book ID is required")]
        public int bookId { get; set; }
    }
}
