using HomeManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using HomeManager.Models.Identity;
using HomeManager.Models.Identity.Admin;
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace HomeManager.WebApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;

        public AdminController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Users()
        {
            var users = await _userManager.Users.ToListAsync();
            if (users == null)
            {
                return NotFound($"Unable to load users");
            }

            ViewBag.Users = users;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser()
        {
            return View();
        }

        public async Task<IActionResult> Roles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            if (roles == null)
            {
                return NotFound($"Unable to load roles");
            }

            ViewBag.Roles = roles;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(string name)
        {
            var roleExists = await _roleManager.RoleExistsAsync(name);
            if (!roleExists)
            {
                var role = new Role
                {
                    Name = name,
                    NormalizedName = name.ToUpper()
                };
                var setPhoneResult = await _roleManager.CreateAsync(role);
                if (!setPhoneResult.Succeeded)
                {
                    return View();
                }
            }
            return RedirectToAction("Roles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(Guid Id, string Name)
        {
            var role = await _roleManager.FindByIdAsync(Convert.ToString(Id));
            if (role == null)
            {
                return NotFound($"Unable to load role with ID '{Id}'.");
            }

            if (!ModelState.IsValid)
            {
                return View(role);
            }

            role.Name = Name;
            role.NormalizedName = Name.ToUpper();
            var setPhoneResult = await _roleManager.UpdateAsync(role);
            if (!setPhoneResult.Succeeded)
            {
                //StatusMessage = "Unexpected error when trying to set phone number.";
                return View();
            }

            return RedirectToAction("Roles");
        }

        public async Task<IActionResult> UserRoles()
        {
            var userRoles = new List<UserRolesModel>();

            var users = await _userManager.Users.ToListAsync();

            if (users == null)
            {
                return NotFound($"Unable to load users");
            }

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (roles == null)
                {
                    return NotFound($"Unable to load roles");
                }

                foreach (var roleName in roles)
                {
                    var role = await _roleManager.FindByNameAsync(roleName);

                    var userRole = new UserRolesModel
                    {
                        User = user.UserName,
                        UserId = user.Id,
                        Role = role.Name,
                        RoleId = role.Id
                    };

                    userRoles.Add(userRole);
                }
            }

            if (userRoles == null)
            {
                return NotFound($"Unable to load user roles");
            }

            ViewBag.UserRoles = userRoles;
            ViewData["UserId"] = new SelectList(await _userManager.Users.ToListAsync(), "Id", "UserName");
            ViewData["RoleId"] = new SelectList(await _roleManager.Roles.ToListAsync(), "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUserRole(UserRolesModel userRoles)
        {
            var user = await _userManager.FindByIdAsync(Convert.ToString(userRoles.UserId));
            var role = await _roleManager.FindByIdAsync(Convert.ToString(userRoles.RoleId));
            var result = await _userManager.AddToRoleAsync(user, role.Name);
            if (!result.Succeeded)
            {
                return View(userRoles);
            }

            return RedirectToAction("UserRoles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserRole(UserRolesModel userRoles)
        {
            return View();
        }

        public async Task<IActionResult> RemoveUserRole()
        {
            return View();
        }
    }
}
