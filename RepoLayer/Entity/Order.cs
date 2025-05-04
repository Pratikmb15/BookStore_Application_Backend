using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Entity
{
    public class Order
    {
        [Key]
        public int orderId { get; set; }

        [ForeignKey("User")]
        public int userId { get; set; }

        [ForeignKey("Book")]
        public int bookId { get; set; }
        public int quantity { get; set; }
        public int totalPrice { get; set; }
        [Required(ErrorMessage = "Order Date is required")]
        public DateTime orderDate { get; set; }     
        public  Book Book { get; set; }
    }
}
