using ModelLayer;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Interfaces
{
    public interface IOrderRepoService
    {
        public Task<bool> AddOrder(int userId);
        public List<Order> GetAllOrders(int userId);
        public Order GetOrderById(int userId, int orderId);

    }
}
