using ModelLayer;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Services
{
    public class CartRepoService: ICartRepoService
    {
        private readonly IBookRepoService _bookRepoService;
        private readonly AppDbContext _context;
        public CartRepoService(IBookRepoService bookRepoService, AppDbContext context)
        {
            _bookRepoService = bookRepoService;
            _context = context;
        }
        public List<CartItem> GetAllCartItems(int userId)
        {
            var cartItems = _context.Cart.Where(n => n.userId == userId).ToList();
            if (cartItems == null || cartItems.Count == 0)
            {
                throw new ArgumentException("No Cart Items Found");
            }
            return cartItems;
        }
        public CartItem GetCartItemById(int cartId)
        {
            if (cartId <= 0)
            {
                throw new Exception("Invalid Cart ID");
            }
            var cartItem = _context.Cart.FirstOrDefault(c => c.cartId == cartId);
            if (cartItem == null)
            {
                throw new Exception("Cart item not found");
            }
            return cartItem;
        }
        public async Task<bool> AddItemToCart(int userId,CartItemModel cartModel)
        {
            var book = _bookRepoService.GetBookById(cartModel.bookId);
            if (book == null || book.quantity < 1)
            {
                throw new ArgumentException("Book is not available");
            }
            if(cartModel.bookQuantity > book.quantity)
            {
                throw new ArgumentException("Book quantity is not available");
            }
            var cartItem = new CartItem()
            {
                userId = userId,
                bookId = cartModel.bookId,
                bookQuantity = cartModel.bookQuantity,
                price = book.price-book.discountPrice,
                isPurchased = false
            };
            _context.Cart.Add(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateCartItem(int cartId, CartItemModel cart)
        {
            if (cartId < 1)
            {
                throw new Exception("Invalid CartID");
            }
            var cartItem = _context.Cart.FirstOrDefault(c => c.cartId == cartId);
            if (cartItem == null)
            {
                throw new Exception("Cart not found");
            }
            if (cart.bookQuantity <= 0)
            {
                await DeleteCartItem(cartItem.cartId);
            }
            var Book = _bookRepoService.GetBookById(cart.bookId);
            if (Book.quantity < cart.bookQuantity)
            {
                throw new Exception("Book is not available");
            }
            cartItem.bookQuantity = cart.bookQuantity;
            cartItem.price = Book.price-Book.discountPrice;       
            _context.Cart.Update(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteCartItem(int cartItemId)
        {
            if (cartItemId < 1)
            {
                throw new Exception("Invalid Cart Item");
            }
            var cartItem = _context.Cart.FirstOrDefault(x => x.cartId == cartItemId);
            if (cartItem == null)
            {
                throw new Exception($"No Cart item Found with Id = {cartItemId} to delete");
            }
            if (cartItem.bookQuantity > 1)
            {
                cartItem.bookQuantity--;
                _context.Cart.Update(cartItem);
                await _context.SaveChangesAsync();
                return true;
            }
            _context.Cart.Remove(cartItem);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<int> PurchaseCartItems(int userId)
        {
            if (userId <= 0) { 
            throw new Exception("Invalid User ID");
            }

            var cartItems = GetAllCartItems(userId);
            if (cartItems == null)
            {
                throw new Exception("Cart item not found");
            }
            var totalPrice = 0;
            foreach (var item in cartItems)
            {
                if (item.isPurchased == false)
                {
                    var book = _bookRepoService.GetBookById(item.bookId);
                    if (book == null || book.quantity < item.bookQuantity)
                    {
                        throw new ArgumentException("Book is not available");
                    }
                    item.isPurchased = true;
                    _context.Cart.Update(item);
                    await _context.SaveChangesAsync();
                    if (book != null)
                    {
                        book.quantity -= item.bookQuantity;
                        _context.Books.Update(book);
                        await _context.SaveChangesAsync();
                    }
                    totalPrice += item.price;
                }
            }
            
            return totalPrice;
        }
    }
}
