using Clinic.BLL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.BLL.Services
{
    internal class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> Create(IdentityUser user, string password, string role)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return await _userManager.AddToRoleAsync(user, role);
            }

            return result;
        }

        public List<IdentityUser> GetAll()
        {
            return _userManager.Users.ToList();
        }

        public async Task<IdentityResult> Delete(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new ArgumentNullException();

            return await _userManager.DeleteAsync(user);
        }

        public async Task<IdentityResult> ChangePassword(string userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new ArgumentNullException();

            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public async Task<IdentityResult> AddRole(string roleName)
        {
            var role = await _roleManager.RoleExistsAsync(roleName);
            if (role) throw new ArgumentException("Role already exists");

            return await _roleManager.CreateAsync(new IdentityRole
            {
                Name = roleName
            });
        }

        public async Task<IdentityUser> GetById(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<IdentityResult> ChangeRoles(string userId, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new ArgumentNullException();

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            return await _userManager.AddToRolesAsync(user, roles);
        }

        public List<string> GetRolesNames()
        {
            return _roleManager.Roles.Select(x => x.Name).ToList();
        }

        public async Task<List<string>> GetRolesNamesForUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new ArgumentNullException();

            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }
    }
}