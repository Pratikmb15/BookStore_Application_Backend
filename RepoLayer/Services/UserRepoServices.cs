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
        public async Task<User> Login(UserLoginModel model)
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
                return user;  
        }

        public async Task<bool> AddUser(UserRegisterModel model)
        {
            var checkAlreadyExists = CheckUserExists(model.email);
            if (checkAlreadyExists)
            {
                throw new ArgumentException("User already exists");
            }
            if (string.IsNullOrEmpty(model.password))
                throw new ArgumentNullException(nameof(model.password), "Password cannot be null or empty");
            model.password = BCrypt.Net.BCrypt.HashPassword(model.password);
            User user = new User
            {
                fullName = model.fullName,
                email = model.email,
                password = model.password,
                mobileNum = model.mobileNum,
                role = "User"
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateUser(int userId, UserRegisterModel model)
        {
            var user = _context.Users.FirstOrDefault(u => u.userId == userId);
            if (user == null)
            {
                throw new Exception(" User not Found ");
            }
            user.fullName = model.fullName;
            user.email = model.email;
            user.password = model.password;
            user.mobileNum = model.mobileNum;
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
        public void DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
        public bool CheckUserExists(string email)
        {
            var alreadyExists = _context.Users.Any(u => u.email == email);
            return alreadyExists;
        }
        public  User GetUserByEmail(string email)
        {
            try
            {
                return  _context.Users.FirstOrDefault(u => u.email == email);
            }
            catch (Exception ex)
            {
               Console.WriteLine("Error fetching user by email", ex);
                return null;
            }
        }
        public bool VerifyUser(string email, string password)
        {
            var user = GetUserByEmail(email);
            return user != null && BCrypt.Net.BCrypt.Verify(password, user.password);
        }
        public string ForgetPassword(string newToken, string email)
        {

            User user = _context.Users.FirstOrDefault(u => u.email == email);
            if (user != null)
            {
                // Construct Reset Password Link
                string resetLink = $"http://localhost:4200/Resetpassword?token={newToken}";
                return resetLink;
            }
            else
            {
                throw new Exception("User does not exist");
            }
        }
        public bool ResetPassword(string Email, ResetPasswordModel model)
        {
            var user = _context.Users.FirstOrDefault(u => u.email == Email);
            if (user != null)
            {
                user.password = BCrypt.Net.BCrypt.HashPassword(model.NewPassWord);
                _context.SaveChanges();
                return true;
            }
            else
            {
                throw new Exception("User does not exist");
            }
        }
        public bool UpdateUserToken(User user)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.userId == user.userId);
            if (existingUser != null)
            {
                existingUser.refreshToken = user.refreshToken;
                existingUser.refreshTokenExpiryTime = user.refreshTokenExpiryTime;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
