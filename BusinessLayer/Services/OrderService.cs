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
   public class OrderService: IOrderService
    {
        private readonly IOrderRepoService _orderRepoService;
        public OrderService(IOrderRepoService orderRepoService)
        {
            _orderRepoService = orderRepoService;
        }

        public async Task<bool> AddOrder(int userId)
        {
            return await _orderRepoService.AddOrder(userId);
        }

        public List<Order> GetAllOrders(int userId)
        {
            return _orderRepoService.GetAllOrders(userId);
        }

        public Order GetOrderById(int userId, int orderId)
        {
            return _orderRepoService.GetOrderById(userId, orderId);
        }
    }
}
