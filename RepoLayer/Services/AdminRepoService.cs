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
    public class AdminRepoService: IAdminRepoService
    {
        private readonly AppDbContext _context;
        public AdminRepoService(AppDbContext context) { _context = context; }
        public async Task<Admin> AdminLogin(UserLoginModel model)
        {
            var admin = _context.Admins.FirstOrDefault(u => u.email == model.email);
            if (admin != null && BCrypt.Net.BCrypt.Verify(model.password, admin.password))
            {
                return admin;
            }
            else
            {
                throw new ArgumentException("Invalid credentials");

            }
        }
        public async Task<bool> AddAdmin(UserRegisterModel model)
        {
            var checkAlreadyExists = CheckAdminExists(model.email);
            if (checkAlreadyExists)
            {
                throw new ArgumentException("Admin already exists");
            }
            if (string.IsNullOrEmpty(model.password))
                throw new ArgumentNullException(nameof(model.password), "Password cannot be null or empty");
            model.password = BCrypt.Net.BCrypt.HashPassword(model.password);
            Admin admin = new Admin
            {
                fullName = model.fullName,
                email = model.email,
                password = model.password,
                mobileNum = model.mobileNum,
                role = "Admin"
            };
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();
            return true;
        }
        public void DeleteAdmin(int id)
        {
            var admin = _context.Admins.Find(id);
            if (admin != null)
            {
                _context.Admins.Remove(admin);
                _context.SaveChanges();
            }
        }
        public async Task<bool> UpdateAdmin(int userId, UserRegisterModel model)
        {
            var admin = _context.Admins.FirstOrDefault(u => u.userId == userId);
            if (admin == null)
            {
                throw new Exception(" Admin not Found ");
            }
            admin.fullName = model.fullName;
            admin.email = model.email;
            admin.password = model.password;
            admin.mobileNum = model.mobileNum;
            await _context.SaveChangesAsync();
            return true;
        }
        public bool CheckAdminExists(string email)
        {
            var alreadyExists = _context.Admins.Any(u => u.email == email);
            return alreadyExists;
        }

        public string AdminForgetPassword(string newToken, string email)
        {

            var user = _context.Admins.FirstOrDefault(u => u.email == email);
            if (user != null)
            {
                // Construct Reset Password Link
                string resetLink = $"http://localhost:4200/Resetpassword?token={newToken}";
                return resetLink;
            }
            else
            {
                throw new Exception("Admin does not exist");
            }
        }
        public bool AdminResetPassword(string Email, ResetPasswordModel model)
        {
            var admin = _context.Admins.FirstOrDefault(u => u.email == Email);
            if (admin != null)
            {
                admin.password = BCrypt.Net.BCrypt.HashPassword(model.NewPassWord);
                _context.SaveChanges();
                return true;
            }
            else
            {
                throw new Exception("Admin does not exist");
            }
        }
        public Admin GetAdminByEmail(string email)
        {
            try
            {
                return _context.Admins.FirstOrDefault(u => u.email == email);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching user by email", ex);
                return null;
            }
        }
        public bool UpdateAdminToken(Admin admin)
        {
            var existingAdmin = _context.Admins.FirstOrDefault(u => u.userId == admin.userId);
            if (existingAdmin != null)
            {
                existingAdmin.refreshToken = admin.refreshToken;
                existingAdmin.refreshTokenExpiryTime = admin.refreshTokenExpiryTime;
                _context.SaveChanges();
                return true;
            }
            else
            {
                throw new Exception("Admin not found");
            }
        }


    }
}
