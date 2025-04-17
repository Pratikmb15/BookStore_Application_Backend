using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ModelLayer;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Services
{
    public class UserRepoServices : IUserRepoServices
    {
        private readonly AppDbContext _context;
        public UserRepoServices(AppDbContext context) { _context = context; }
        public async Task<bool> Login(UserLoginModel model)
        {
            try
            {
                var user = GetUserByEmail(model.email);
                if (user == null)
                {
                    throw new ArgumentException("User not found");
                }
                var isValid = VerifyUser(model.email, model.password);
                if (!isValid)
                {
                    throw new ArgumentException("Invalid password");
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public async Task<bool> AddUser(UserRegisterModel model)
        {
            model.password = BCrypt.Net.BCrypt.HashPassword(model.password);
            User user = new User
            {
                fullName = model.fullName,
                email = model.email,
                password = model.password,
                mobileNum = model.mobileNum,
                role = model.role
            };
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<User> GetUserById(int userId)
        {
            if (userId > 0) {    
            return await _context.Users.FirstOrDefaultAsync(u => u.userId == userId);
            }
            else
            {
                throw new ArgumentException("Enter Valid User Id");
            }
        }
        public  User GetUserByEmail(string email)
        {
            try
            {
                return  _context.Users.FirstOrDefault(u => u.email == email);
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching user by email", ex);
            }
        }
        public bool VerifyUser(string email, string password)
        {
            var user = GetUserByEmail(email);
            return user != null && BCrypt.Net.BCrypt.Verify(password, user.password);
        }

       
    }
}
