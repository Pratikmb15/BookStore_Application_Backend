using Microsoft.EntityFrameworkCore;
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
        public GetAllCartItemModel<CartItem> GetAllCartItems(int userId)
        {
            var cartItems = _context.Carts.Include(c => c.Book).Where(n => n.userId == userId && n.isPurchased==false).ToList();
            if (cartItems == null || cartItems.Count == 0)
            {
                throw new ArgumentException(" Cart is Empty ");
            }
            int totalPrice = 0;
            foreach (var item in cartItems)
            {
                if (item.isPurchased == false)
                {
                    totalPrice += (item.price*item.bookQuantity);
                }
            }
            var cartItemModel = new GetAllCartItemModel<CartItem>()
            {
                cartItems = cartItems,
                totalPrice = totalPrice
            };
            return cartItemModel;
        }
        public GetAllCartItemModel<CartItem> GetCartItemById(int cartId)
        {
            if (cartId <= 0)
            {
                throw new Exception("Invalid Cart ID");
            }
            var cartItem = _context.Carts.Include(c => c.Book).FirstOrDefault(c => c.cartId == cartId);
            if (cartItem == null)
            {
                throw new Exception("Cart is empty ");
            }
            var cartItemModel = new GetAllCartItemModel<CartItem>()
            {
                cartItems = new List<CartItem> { cartItem },
                totalPrice = cartItem.price * cartItem.bookQuantity
            };
            return cartItemModel;
        }
        public async Task<bool> AddItemToCart(int userId,CartItemModel cartModel)
        {
            var book = _bookRepoService.GetBookById(cartModel.bookId);
            if (book == null || book.quantity < 1)
            {
                throw new ArgumentException("Book is not available");
            }
            var checkCartItemExists = CheckCartItemExists(userId, cartModel.bookId);
            if (checkCartItemExists)
            { 
            var cartId = getCartId(userId, cartModel.bookId);
                var cartItemBook = _context.Carts.FirstOrDefault(c => c.cartId == cartId);
                if (cartItemBook != null)
                {
                    cartItemBook.bookQuantity ++;
                    _context.Carts.Update(cartItemBook);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            var cartItem = new CartItem()
            {
                userId = userId,
                bookId = cartModel.bookId,
                bookQuantity = 1,
                price = book.price-book.discountPrice,
                isPurchased = false,
              
            };
           
            _context.Carts.Add(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateCartItem(int cartId, UCartItemModel cart)
        {
            if (cartId < 1)
            {
                throw new Exception("Invalid CartID");
            }
            var cartItem = _context.Carts.FirstOrDefault(c => c.cartId == cartId);
            if (cartItem == null)
            {
                throw new Exception("Cart not found");
            }
            if (cart.bookQuantity <= 0)
            {
                await DeleteCartItem(cartItem.cartId);
            }
            var Book = _bookRepoService.GetBookById(cart.bookId);
            if (Book == null)
            {
                throw new Exception("Book not found");
            }
            if (Book.quantity < cart.bookQuantity)
            {
                throw new Exception("Book is not available");
            }
            cartItem.bookId = cart.bookId;
            cartItem.bookQuantity = cart.bookQuantity;
            cartItem.price = Book.price-Book.discountPrice;    
            _context.Carts.Update(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteCartItem(int cartItemId)
        {
            if (cartItemId < 1)
            {
                throw new Exception("Invalid Cart Item");
            }
            var cartItem = _context.Carts.FirstOrDefault(x => x.cartId == cartItemId);
            if (cartItem == null)
            {
                throw new Exception($"No Cart item Found with Id = {cartItemId} to delete");
            }
            if (cartItem.bookQuantity > 1)
            {
                cartItem.bookQuantity--;
                _context.Carts.Update(cartItem);
                await _context.SaveChangesAsync();
                return true;
            }
            _context.Carts.Remove(cartItem);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<int> PurchaseCartItems(int userId)
        {
            if (userId <= 0)
            {
                throw new Exception("Invalid User ID");
            }

            var cartItems = _context.Carts.Where(n => n.userId == userId && n.isPurchased==false).ToList();
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
                    _context.Carts.Update(item);
                    await _context.SaveChangesAsync();
                    if (book != null)
                    {
                        book.quantity -= item.bookQuantity;
                        _context.Books.Update(book);
                        await _context.SaveChangesAsync();
                    }
                    totalPrice += (item.price*item.bookQuantity);
                }
            }

            return totalPrice;
        }
        public bool CheckCartItemExists(int userId, int bookId)
        {
            var cartItem = _context.Carts.FirstOrDefault(c => c.userId == userId && c.bookId == bookId);
            return cartItem != null;
        }
        public int getCartId(int userId, int bookId)
        {
            var cartItem = _context.Carts.FirstOrDefault(c => c.userId == userId && c.bookId == bookId);
            if (cartItem != null)
            {
                return cartItem.cartId;
            }
            return 0;
        }
    }
}
