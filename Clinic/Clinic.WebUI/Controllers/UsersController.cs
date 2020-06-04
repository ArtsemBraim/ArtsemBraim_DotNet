using AutoMapper;
using Clinic.BLL.Interfaces;
using Clinic.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.WebUI.Controllers
{
    public class UsersController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            SignInManager<IdentityUser> signInManager,
            IUserService userService,
            IMapper mapper,
            ILogger<UsersController> logger)
        {
            _signInManager = signInManager;
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    _logger.LogInformation($"User with {model.Email} email failed sign in process");
                    ModelState.AddModelError("", "Email or password was incorrect");
                }
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { Email = model.Email, UserName = model.Email };
                var result = await _userService.Create(user, model.Password, Roles.User);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Index()
        {
            var users = _mapper.Map<List<UserViewModel>>(_userService.GetAll());
            foreach (var user in users)
            {
                user.RolesNames = await _userService.GetRolesNamesForUser(user.Id);
            }

            return View(users);
        }

        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> ChangeRoles(string id)
        {
            var user = await _userService.GetById(id);
            var userRoles = await _userService.GetRolesNamesForUser(id);
            var roles = _userService.GetRolesNames();
            var userView = _mapper.Map<UserViewModel>(user);
            var selectedRoles = roles.Select(x => new SelectListItem
            {
                Text = x,
                Value = x,
                Selected = userRoles.Contains(x)
            }).ToList();

            userView.Roles = selectedRoles;

            return View(userView);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> ChangeRoles(UserViewModel user)
        {
            try
            {
                await _userService.ChangeRoles(user.Id, user.RolesNames);
            }
            catch (ArgumentNullException)
            {
                _logger.LogWarning($"User with {user.Id} id not found.");
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> Delete(string id)
        {
            var user = await _userService.GetById(id);
            var userRoles = await _userService.GetRolesNamesForUser(id);

            var userView = _mapper.Map<UserViewModel>(user);
            userView.RolesNames = userRoles;

            return View(userView);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> DeleteUser(string id)
        {
            try
            {
                await _userService.Delete(id);
            }
            catch (ArgumentNullException)
            {
                _logger.LogError($"User with {id} not found");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
