using ModelLayer;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ICartService
    {
        public GetAllCartItemModel<CartItem> GetAllCartItems(int userId);
        public GetAllCartItemModel<CartItem> GetCartItemById(int cartId);
        public Task<bool> AddItemToCart(int userId, CartItemModel cartModel);
        public Task<bool> UpdateCartItem(int cartId, CartItemModel cart);
        public Task<bool> DeleteCartItem(int cartItemId);
        public Task<int> PurchaseCartItems(int userId);
    }
}
