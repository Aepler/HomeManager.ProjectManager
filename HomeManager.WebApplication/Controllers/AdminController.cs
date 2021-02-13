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
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using HomeManager.Models.Interfaces;
using HomeManager.Models.Interfaces.Factories;
using System.Linq.Dynamic.Core;
using Type = HomeManager.Models.Type;
using HomeManager.Models.DataTableModels;

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
        private readonly IDataTableFactory _dataTableFactory;

        public AdminController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager,
            ITypeService typeService,
            IStatusService statusService,
            IDataTableFactory dataTableFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _typeService = typeService;
            _statusService = statusService;
            _dataTableFactory = dataTableFactory;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Users()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetUserTableData(DataTableModel model)
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
        public async Task<JsonResult> CreateUser(User user, string userPassword)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _userManager.CreateAsync(user, userPassword);
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
                    await _userManager.UpdateAsync(user);
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
        public async Task<JsonResult> GetRoleTableData(DataTableModel model)
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

            if (!ModelState.IsValid)
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
        public async Task<JsonResult> GetUserRoleTableData(DataTableModel model)
        {
            try
            {
                var userRoles = new List<UserRoleDataTableModel>();

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

                        var userRole = new UserRoleDataTableModel
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
        public async Task<JsonResult> CreateUserRole(UserRoleDataTableModel userRoles)
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

        public async Task<IActionResult> Types()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetTypeTableData(DataTableModel model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var types = await _typeService.GetAll(user);
                var result = await _dataTableFactory.GetTableData(model, types);

                return Json(new { draw = result.draw, recordsTotal = result.recordsTotal, recordsFiltered = result.recordsFiltered, data = result.data });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CreateType(Type type)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    await _typeService.Add(user, type);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

                return Json(null);
            }
            return Json(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EditType(int id, Type type)
        {
            if (id != type.Id)
            {
                return Json(null);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    await _typeService.Update(user, type);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return Json(null);
            }
            return Json(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteType(int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var type = await _typeService.GetById(user, id);
                    if (type != null)
                    {
                        await _typeService.Delete(user, type);
                    }
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Status()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetStatusTableData(DataTableModel model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var status = await _statusService.GetAll(user);
                var result = await _dataTableFactory.GetTableData(model, status);

                return Json(new { draw = result.draw, recordsTotal = result.recordsTotal, recordsFiltered = result.recordsFiltered, data = result.data });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CreateStatus(Status status)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    await _statusService.Add(user, status);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

                return Json(null);
            }
            return Json(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EditStatus(int id, Status status)
        {
            if (id != status.Id)
            {
                return Json(null);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    await _statusService.Update(user, status);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return Json(null);
            }
            return Json(null);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var status = await _statusService.GetById(user, id);
                    if (status != null)
                    {
                        await _statusService.Delete(user, status);
                    }
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
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
