using ModelLayer;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Interfaces
{
    public interface IAdminRepoService
    {
        public Task<Admin> AdminLogin(UserLoginModel model);
        public Task<bool> AddAdmin(UserRegisterModel model);
        public void DeleteAdmin(int id);
        public Admin GetAdminByEmail(string email);
        public Task<bool> UpdateAdmin(int userId, UserRegisterModel model);
        public string AdminForgetPassword(string newToken, string email);
        public bool AdminResetPassword(string Email, ResetPasswordModel model);
        public bool UpdateAdminToken(Admin admin);
    }
}
