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
    public class OrderRepoService : IOrderRepoService
    {
        private readonly AppDbContext _context;
        private readonly ICartRepoService _cartRepoService;
        public OrderRepoService(AppDbContext context,ICartRepoService cartRepoService)
        {
            _context = context;
            _cartRepoService = cartRepoService;
        }
        public async Task<bool> AddOrder(int userId)
        {
            try
            {
                var cart = _cartRepoService.GetAllCartItems(userId);
                if (cart == null || cart.cartItems == null || !cart.cartItems.Any())
                {
                    throw new Exception("Cart not found or is empty.");
                }

                var orders = new List<Order>();

                foreach (var item in cart.cartItems)
                {
                    var book = _context.Books.FirstOrDefault(x => x.bookId == item.bookId);
                    if (book == null)
                    {
                        throw new ArgumentException($"Book with ID {item.bookId} not found.");
                    }

                    if (book.quantity < item.bookQuantity)
                    {
                        throw new ArgumentException($"Not enough stock for book ID {item.bookId}.");
                    }

                    book.quantity -= item.bookQuantity;
                    _context.Books.Update(book);

                    item.isPurchased = true;
                    _context.Carts.Update(item);

                    orders.Add(new Order
                    {
                        userId = userId,
                        bookId = item.bookId,
                        quantity = item.bookQuantity,
                        totalPrice = item.price * item.bookQuantity,
                        orderDate = DateTime.Now
                    });
                }

                await _context.Orders.AddRangeAsync(orders);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding order: " + ex.Message);
            }
        }

        public List<Order> GetAllOrders(int userId)
        {
            var user = _context.Users.FirstOrDefault(c => c.userId == userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var orders = _context.Orders.Include(i => i.Book).Where(x => x.userId == userId).ToList();
            if (orders == null || orders.Count == 0)
            {
                throw new Exception("No orders found");
            }
            return orders;
        }
        public Order GetOrderById(int userId, int orderId)
        {
            var user = _context.Users.FirstOrDefault(c => c.userId == userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var order = _context.Orders.Include(i => i.Book).FirstOrDefault(x => x.orderId == orderId && x.userId == userId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            return order;
        }
    }
}
