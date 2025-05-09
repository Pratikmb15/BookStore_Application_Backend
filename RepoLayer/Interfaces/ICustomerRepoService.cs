﻿
using ModelLayer;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Interfaces
{
    public interface ICustomerRepoService
    {
        public  Task<bool> AddCustomerDetailsAsync(int userId, CustomerModel customer);
        public  Task DeleteCustomerDetailsAsync(int userId,int customerId);
        public  Task UpdateCustomerDetailsAsync(int userId,int customerId, CustomerModel customer);
        public  Task<List<CustomerDetail>> GetAllCustomersAsync();
        public  Task<CustomerDetail> GetCustomerByIdAsync(int customerId);
        public  Task<bool> IsCustomerExistsAsync(int userId);
        public  Task<CustomerDetail> GetCustomerId(int userId);


    }
}
