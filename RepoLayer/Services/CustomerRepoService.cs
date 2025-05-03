using Microsoft.EntityFrameworkCore;
using ModelLayer;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoLayer.Services
{
    public class CustomerRepoService:ICustomerRepoService
    {
        private readonly AppDbContext _context;

        public CustomerRepoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCustomerDetailsAsync(int userId, CustomerModel customer)
        {
            if (userId <= 0)
                throw new ArgumentException("Invalid User ID");

            var isExist = await IsCustomerExistsAsync(userId);
            if (isExist)
            {
                throw new ArgumentException("Customer already exists");
            }

            var customerEntity = new CustomerDetail
            {
                fullName = customer.fullName,
                mobileNumber = customer.mobileNumber,
                address = customer.address,
                city = customer.city,
                state = customer.state,
                userId = userId
            };

            await _context.Customers.AddAsync(customerEntity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task DeleteCustomerDetailsAsync(int userId,int customerId)
        {
            if (customerId <= 0)
                throw new ArgumentException("Invalid customer ID");

            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.customerId == customerId && c.userId==userId);
            if (customer == null)
                throw new Exception("Customer not found");

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomerDetailsAsync(int userId,int customerId, CustomerModel customer)
        {
            if (userId <= 0)
                throw new ArgumentException("Invalid User ID");

            var existingCustomer = await _context.Customers.FirstOrDefaultAsync(c => c.userId == userId && c.customerId== customerId);
            if (existingCustomer == null)
                throw new Exception("Customer not found");

            existingCustomer.fullName = customer.fullName;
            existingCustomer.mobileNumber = customer.mobileNumber;
            existingCustomer.address = customer.address;
            existingCustomer.city = customer.city;
            existingCustomer.state = customer.state;

            await _context.SaveChangesAsync();
        }

        public async Task<List<CustomerDetail>> GetAllCustomersAsync()
        {
            var customers = await _context.Customers.ToListAsync();
            if (customers == null || customers.Count == 0)
                throw new Exception("No customers found");

            return customers;
        }

        public async Task<CustomerDetail> GetCustomerByIdAsync(int customerId)
        {
            if (customerId <= 0)
                throw new ArgumentException("Invalid Customer ID");

            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.customerId == customerId);
            if (customer == null)
                throw new Exception("Customer not found");

            return customer;
        }

        public async Task<bool> IsCustomerExistsAsync(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("Invalid User ID");

            return await _context.Customers.AnyAsync(c => c.userId == userId);
        }
    }
}
