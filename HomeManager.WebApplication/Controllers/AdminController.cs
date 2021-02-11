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
using HomeManager.Models.ViewModels;
using HomeManager.Models.ViewModels.Admin;
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using HomeManager.Models.Interfaces;
using Type = HomeManager.Models.Type;

namespace HomeManager.WebApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ITypeService _typeService;
        private readonly IStatusService _statusService;

        public AdminController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager,
            ITypeService typeService,
            IStatusService statusService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _typeService = typeService;
            _statusService = statusService;
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

        public async Task<IActionResult> Types()
        {
            var user = await _userManager.GetUserAsync(User);
            var types = await _typeService.GetAll(user);
            if (types == null)
            {
                return NotFound($"Unable to load roles");
            }

            ViewBag.Types = types;
            ViewData["Status"] = new SelectList(await _statusService.GetByEndPoint(user, true), "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateType(Type type)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                await _typeService.Add(user, type);
                return RedirectToAction(nameof(Types));
            }
            ViewData["Status"] = new SelectList(await _statusService.GetByEndPoint(user, true), "Id", "Name", type.fk_StatusId);
            return View(type);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTypes(int id, Type type)
        {
            var user = await _userManager.GetUserAsync(User);
            if (id != type.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _typeService.Update(user, type);
                }
                catch (DbUpdateConcurrencyException)
                {
                    bool exist = await TypeExists(type.Id);
                    if (exist)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Status"] = new SelectList(await _statusService.GetByEndPoint(user, true), "Id", "Name", type.fk_StatusId);
            return View(type);
        }

        public async Task<IActionResult> Status()
        {
            var user = await _userManager.GetUserAsync(User);
            var status = await _statusService.GetAll(user);
            if (status == null)
            {
                return NotFound($"Unable to load roles");
            }

            ViewBag.Status = status;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStatus(Status status)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                await _statusService.Add(user, status);
                return RedirectToAction(nameof(Status));
            }
            return View(status);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStatus(int id, Status status)
        {
            var user = await _userManager.GetUserAsync(User);
            if (id != status.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _statusService.Update(user, status);
                }
                catch (DbUpdateConcurrencyException)
                {
                    bool exist = await StatusExists(status.Id);
                    if (exist)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(status);
        }

        private async Task<bool> TypeExists(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var types = await _typeService.GetAll(user);
            return types.Any(e => e.Id == id);
        }

        private async Task<bool> StatusExists(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var status = await _statusService.GetAll(user);
            return status.Any(e => e.Id == id);
        }
    }
}
