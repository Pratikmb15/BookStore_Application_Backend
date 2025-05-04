using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ModelLayer;
using RepoLayer.Entity;

namespace RepoLayer.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<CartItem> Carts { get; set; }
        public DbSet<WishListItem> WhishList { get; set; }
        public DbSet<CustomerDetail> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
