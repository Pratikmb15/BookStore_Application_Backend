using BusinessLayer.Interfaces;
using ModelLayer;
using RepoLayer.Entity;
using RepoLayer.Interfaces;
using RepoLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class AdminService: IAdminService
    {
        private readonly IAdminRepoService _adminRepoServices;
        private readonly IAuthService _authService;
        public AdminService(IAdminRepoService adminRepoServices, IAuthService authService)
        {
            _adminRepoServices = adminRepoServices;
            _authService = authService;
        }
        public Task<Admin> AdminLogin(UserLoginModel model)
        {
            var admin = _adminRepoServices.AdminLogin(model);
            return admin;
        }
        public Task<bool> AddAdmin(UserRegisterModel model)
        {
            var success = _adminRepoServices.AddAdmin(model);
            return success;
        }
        public async Task<bool> RegisterAdmin(UserRegisterModel model)
        {
            return await _adminRepoServices.AddAdmin(model);
        }

        public void DeleteAdmin(int id)
        {
            _adminRepoServices.DeleteAdmin(id);
        }
        public Admin GetAdminByEmail(string email)
        {
            var admin = _adminRepoServices.GetAdminByEmail(email);
            if (admin == null)
            {
                throw new ArgumentException("Admin not found");
            }
            return admin;
        }

        public async Task<bool> UpdateAdmin(int userId, UserRegisterModel model)
        {
            return await _adminRepoServices.UpdateAdmin(userId, model);
        }

        public string AdminForgetPassword(string email)
        {
            var admin = _adminRepoServices.GetAdminByEmail(email);
            Console.WriteLine(admin);
            if (admin == null)
            {
                throw new Exception("User does not exist");
            }

            int userId = admin.userId;
            string newToken = _authService.GenerateToken(admin);
            return _adminRepoServices.AdminForgetPassword(newToken, email);
        }

        public bool AdminResetPassword(string email, ResetPasswordModel model)
        {
            return _adminRepoServices.AdminResetPassword(email, model);
        }

    }
}
