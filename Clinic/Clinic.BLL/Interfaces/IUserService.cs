using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> Create(IdentityUser user, string password, string role);

        List<IdentityUser> GetAll();

        Task<IdentityUser> GetById(string userId);

        Task<IdentityResult> ChangeRoles(string userId, List<string> roles);

        Task<IdentityResult> Delete(string userId);

        Task<IdentityResult> ChangePassword(string userId, string currentPassword, string newPassword);

        Task<IdentityResult> AddRole(string roleName);

        List<string> GetRolesNames();

        Task<List<string>> GetRolesNamesForUser(string userId);
    }
}