using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HomeManager.Models.DataTable;
using HomeManager.Models.Entities;
using HomeManager.Models.Interfaces.Factories;
using HomeManager.WebApplication.Areas.Admin.ViewModels.Manage;

namespace HomeManager.WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManageController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IDataTableFactory _dataTableFactory;

        public ManageController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IDataTableFactory dataTableFactory)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dataTableFactory = dataTableFactory;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return Json(user);
        }

        [HttpGet]
        public async Task<JsonResult> GetRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            return Json(role);
        }

        public async Task<IActionResult> Users()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetUserTableData(DataTableInput model)
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();
                var result = await _dataTableFactory.GetTableData(model, users);

                return Json(new { draw = result.draw, recordsTotal = result.recordsTotal, recordsFiltered = result.recordsFiltered, data = result.data });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CreateUser(UserModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new User { UserName = model.UserName, Email = model.Email, FirstName = model.Name, LastName = model.Lastname, PhoneNumber = model.PhoneNumber };
                    await _userManager.CreateAsync(user, model.Password);
                }
                catch (Exception)
                {
                    throw;
                }

                return Json(null);
            }
            return Json(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EditUser(string id, User user)
        {
            if (Guid.Parse(id) != user.Id)
            {
                return Json(null);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var tempUser = await _userManager.FindByIdAsync(id);
                    tempUser.UserName = user.UserName;
                    tempUser.NormalizedUserName = user.UserName.ToUpper();
                    tempUser.Email = user.Email;
                    tempUser.NormalizedEmail = user.Email.ToUpper(); ;
                    tempUser.FirstName = user.FirstName;
                    tempUser.LastName = user.LastName;
                    tempUser.PhoneNumber = user.PhoneNumber;
                    await _userManager.UpdateAsync(tempUser);
                }
                catch (Exception)
                {
                    throw;
                }
                return Json(null);
            }
            return Json(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteUser(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    await _userManager.DeleteAsync(user); ;
                }
                return Json(null);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> Roles()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetRoleTableData(DataTableInput model)
        {
            try
            {
                var roles = await _roleManager.Roles.ToListAsync();
                var result = await _dataTableFactory.GetTableData(model, roles);

                return Json(new { draw = result.draw, recordsTotal = result.recordsTotal, recordsFiltered = result.recordsFiltered, data = result.data });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CreateRole(string name)
        {
            if (name != null)
            {
                var roleExists = await _roleManager.RoleExistsAsync(name);
                if (!roleExists)
                {
                    var role = new Role
                    {
                        Name = name,
                        NormalizedName = name.ToUpper()
                    };
                    try
                    {
                        await _roleManager.CreateAsync(role);
                        return Json(null);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            return Json(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EditRole(string id, string Name)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return Json(null);
            }

            if (ModelState.IsValid)
            {
                role.Name = Name;
                role.NormalizedName = Name.ToUpper();

                try
                {
                    await _roleManager.UpdateAsync(role);
                    return Json(null);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return Json(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteRole(string id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role != null)
                {
                    await _roleManager.DeleteAsync(role); ;
                }
                return Json(null);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> UserRoles()
        {
            ViewData["UserId"] = new SelectList(await _userManager.Users.ToListAsync(), "Id", "UserName");
            ViewData["RoleId"] = new SelectList(await _roleManager.Roles.ToListAsync(), "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetUserRoleTableData(DataTableInput model)
        {
            try
            {
                var userRoles = new List<UserRoleDataTable>();

                var users = await _userManager.Users.ToListAsync();

                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles == null)
                    {
                        return Json(null);
                    }

                    foreach (var roleName in roles)
                    {
                        var role = await _roleManager.FindByNameAsync(roleName);

                        var userRole = new UserRoleDataTable
                        {
                            User = user.UserName,
                            UserId = user.Id.ToString(),
                            Role = role.Name,
                            RoleId = role.Id.ToString()
                        };

                        userRoles.Add(userRole);
                    }
                }

                var result = await _dataTableFactory.GetTableData(model, userRoles);

                return Json(new { draw = result.draw, recordsTotal = result.recordsTotal, recordsFiltered = result.recordsFiltered, data = result.data });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CreateUserRole(UserRoleDataTable userRoles)
        {
            var user = await _userManager.FindByIdAsync(userRoles.UserId);
            var role = await _roleManager.FindByIdAsync(userRoles.RoleId);
            try
            {
                await _userManager.AddToRoleAsync(user, role.Name);
                return Json(null);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteUserRole(string userId, string role)
        {
            if (userId != null && role != null)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        await _userManager.RemoveFromRoleAsync(user, role);
                        return Json(null);
                    }
                    return Json(null);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return Json(null);
        }
    }
}
