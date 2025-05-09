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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepoService _customerRepoService;
        public CustomerService(ICustomerRepoService customerRepoService)
        {
            _customerRepoService = customerRepoService;
        }
        public Task<bool> AddCustomerDetailsAsync(int userId, CustomerModel customer)
        {
            return _customerRepoService.AddCustomerDetailsAsync(userId, customer);
        }

        public Task DeleteCustomerDetailsAsync(int userId,int customerId)
        {
            return _customerRepoService.DeleteCustomerDetailsAsync(userId,customerId);
        }

        public Task<List<CustomerDetail>> GetAllCustomersAsync()
        {
            return _customerRepoService.GetAllCustomersAsync();
        }

        public Task<CustomerDetail> GetCustomerByIdAsync(int customerId)
        {
            return _customerRepoService.GetCustomerByIdAsync(customerId);
        }

        public Task<bool> IsCustomerExistsAsync(int userId)
        {
            return _customerRepoService.IsCustomerExistsAsync(userId);
        }

        public Task UpdateCustomerDetailsAsync(int userId,int customerId, CustomerModel customer)
        {
            return _customerRepoService.UpdateCustomerDetailsAsync(userId, customerId, customer);
        }
        public async Task<CustomerDetail> GetCustomerId(int userId)
        {
            return await _customerRepoService.GetCustomerId(userId);
        }
    }
}
