using BusinessLayer.Interfaces;
using ModelLayer;
using RepoLayer.Entity;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepoService _cartRepoService;
        public CartService(ICartRepoService cartRepoService)
        {
            _cartRepoService = cartRepoService;
        }
        public Task<bool> AddItemToCart(int userId, CartItemModel cartModel)
        {
            return  _cartRepoService.AddItemToCart( userId, cartModel);
        }

        public Task<bool> DeleteCartItem(int cartItemId)
        {
            return _cartRepoService.DeleteCartItem(cartItemId);
        }

        public GetAllCartItemModel<CartItem> GetAllCartItems(int userId)
        {
            return _cartRepoService.GetAllCartItems(userId);
        }

        public int getCartId(int userId, int bookId)
        {
            return _cartRepoService.getCartId(userId, bookId);
        }

        public GetAllCartItemModel<CartItem> GetCartItemById(int cartId)
        {
            return _cartRepoService.GetCartItemById(cartId);
        }

        public async Task<int> PurchaseCartItems(int userId)
        {
            return await _cartRepoService.PurchaseCartItems(userId);
        }

        public Task<bool> UpdateCartItem(int cartId, UCartItemModel cart)
        {
            return _cartRepoService.UpdateCartItem(cartId, cart);
        }
    }
}
