using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class GetAllCartItemModel<T>
    {
        public List<T> cartItems { get; set; }
        public int totalPrice { get; set; }
    }
}
