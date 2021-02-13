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
using System.Linq.Dynamic.Core;
using Type = HomeManager.Models.Type;
using HomeManager.Models.Factories;

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
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetUserTableData(DataTableModel model)
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();
                int totalRecords = users.Count;


                var modifiedData = users.Select(d => new UserViewModel
                {
                    Id = d.Id.ToString(),
                    UserName = d.UserName,
                    Email = d.Email,
                    Name = d.Name,
                    Lastname = d.Lastname,
                    PhoneNumber = d.PhoneNumber.ToString(),
                    TwoFactorEnabled = d.TwoFactorEnabled.ToString(),
                    Buttons = "<button class='buttonEditUserAdmin btn btn-outline-secondary' value='" + d.Id + "' data-bs-toggle='modal' data-bs-target='#modalEditUserAdmin'>Edit</button>" +
                    " | " +
                              "<button class='buttonDeleteUserAdmin btn btn-outline-danger' value='" + d.Id + "' data-bs-toggle='modal' data-bs-target='#modalDeleteUserAdmin'>Delete</button>"
                }
                    );

                if (!string.IsNullOrEmpty(model.search.value) &&
                    !string.IsNullOrWhiteSpace(model.search.value))
                {
                    modifiedData = modifiedData.Where(p => p.UserName.ToLower().Contains(model.search.value) ||
                        p.Email.ToLower().Contains(model.search.value) ||
                        p.Name.ToLower().Contains(model.search.value) ||
                        p.Lastname.ToLower().Contains(model.search.value) ||
                        p.PhoneNumber.ToLower().Contains(model.search.value) ||
                        p.TwoFactorEnabled.ToLower().Contains(model.search.value)
                     ).ToList();
                }

                string sortBy = "";
                string sortDir = "";

                if (model.order != null)
                {
                    // in this example we just default sort on the 1st column
                    sortBy = model.columns[model.order[0].column].data;
                    sortDir = model.order[0].dir.ToLower();
                }

                //int recFilter = test.Count();
                modifiedData = modifiedData.AsQueryable().OrderBy(sortBy + " " + sortDir).Skip(model.start).Take(model.length).ToList();
                //var test2 = modifiedData.AsQueryable().OrderBy(sortBy + " " + sortDir);

                return Json(new { draw = model.draw, recordsTotal = totalRecords, recordsFiltered = 2, data = modifiedData });
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return Json(null);
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
                int totalRecords = roles.Count;


                var modifiedData = roles.Select(d => new RoleViewModel
                {
                    Id = d.Id.ToString(),
                    Name = d.Name,
                    NormalizedName = d.NormalizedName,
                    ConcurrencyStamp = d.ConcurrencyStamp.ToString(),
                    Buttons = "<button class='buttonEditRoleAdmin btn btn-outline-secondary' value='" + d.Id + "' data-bs-toggle='modal' data-bs-target='#modalEditRoleAdmin'>Edit</button>" +
                    " | " +
                              "<button class='buttonDeleteRoleAdmin btn btn-outline-danger' value='" + d.Id + "' data-bs-toggle='modal' data-bs-target='#modalDeleteRoleAdmin'>Delete</button>"
                }
                    );

                if (!string.IsNullOrEmpty(model.search.value) &&
                    !string.IsNullOrWhiteSpace(model.search.value))
                {
                    modifiedData = modifiedData.Where(p => p.Name.ToLower().Contains(model.search.value)
                     ).ToList();
                }

                string sortBy = "";
                string sortDir = "";

                if (model.order != null)
                {
                    // in this example we just default sort on the 1st column
                    sortBy = model.columns[model.order[0].column].data;
                    sortDir = model.order[0].dir.ToLower();
                }

                int recFilter = modifiedData.Count();
                modifiedData = modifiedData.AsQueryable().OrderBy(sortBy + " " + sortDir).Skip(model.start).Take(model.length).ToList();

                return Json(new { draw = model.draw, recordsTotal = totalRecords, recordsFiltered = recFilter, data = modifiedData });
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return Json(null);
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
                var userRoles = new List<UserRoleViewModel>();

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

                        var userRole = new UserRoleViewModel
                        {
                            User = user.UserName,
                            UserId = user.Id.ToString(),
                            Role = role.Name,
                            RoleId = role.Id.ToString()
                        };

                        userRoles.Add(userRole);
                    }
                }

                int totalRecords = userRoles.Count;


                var modifiedData = userRoles.Select(d => new UserRoleViewModel
                {
                    User = d.User,
                    UserId = d.UserId,
                    Role = d.Role,
                    RoleId = d.RoleId,
                    Buttons = "<button class='buttonDeleteUserRoleAdmin btn btn-outline-danger' value='" + d.UserId + "' role='" + d.Role + "' data-bs-toggle='modal' data-bs-target='#modalDeleteUserRoleAdmin'>Delete</button>"
                }
                    );

                if (!string.IsNullOrEmpty(model.search.value) &&
                    !string.IsNullOrWhiteSpace(model.search.value))
                {
                    modifiedData = modifiedData.Where(p => p.User.ToLower().Contains(model.search.value) ||
                        p.Role.ToLower().Contains(model.search.value)
                     ).ToList();
                }

                string sortBy = "";
                string sortDir = "";

                if (model.order != null)
                {
                    // in this example we just default sort on the 1st column
                    sortBy = model.columns[model.order[0].column].data;
                    sortDir = model.order[0].dir.ToLower();
                }

                int recFilter = modifiedData.Count();
                modifiedData = modifiedData.AsQueryable().OrderBy(sortBy + " " + sortDir).Skip(model.start).Take(model.length).ToList();

                return Json(new { draw = model.draw, recordsTotal = totalRecords, recordsFiltered = recFilter, data = modifiedData });
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return Json(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CreateUserRole(UserRoleViewModel userRoles)
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
                int totalRecords = types.Count;


                var modifiedData = types.Select(d => new TypeViewModel
                {
                    Id = d.Id.ToString(),
                    Name = d.Name,
                    EndTaxType = d.EndTaxType,
                    Debit = d.Debit.ToString(),
                    ExtraInput = d.ExtraInput,
                    fk_StatusId = d.fk_StatusId.ToString(),
                    Status = d.Status.Name,
                    Buttons = "<button class='buttonEditTypeAdmin btn btn-outline-secondary' value='" + d.Id + "' data-bs-toggle='modal' data-bs-target='#modalEditTypeAdmin'>Edit</button>" +
                    " | " +
                              "<button class='buttonDeleteTypeAdmin btn btn-outline-danger' value='" + d.Id + "' data-bs-toggle='modal' data-bs-target='#modalLabelDeleteTypeAdmin'>Delete</button>"
                }
                    );

                if (!string.IsNullOrEmpty(model.search.value) &&
                    !string.IsNullOrWhiteSpace(model.search.value))
                {
                    modifiedData = modifiedData.Where(p => p.Name.ToLower().Contains(model.search.value) ||
                        p.EndTaxType.ToLower().Contains(model.search.value) ||
                        p.Debit.ToLower().Contains(model.search.value) ||
                        p.ExtraInput.Contains(model.search.value) ||
                        p.Status.ToLower().Contains(model.search.value)
                     ).ToList();
                }

                string sortBy = "";
                string sortDir = "";

                if (model.order != null)
                {
                    // in this example we just default sort on the 1st column
                    sortBy = model.columns[model.order[0].column].data;
                    sortDir = model.order[0].dir.ToLower();
                }

                int recFilter = modifiedData.Count();
                modifiedData = modifiedData.AsQueryable().OrderBy(sortBy + " " + sortDir).Skip(model.start).Take(model.length).ToList();

                return Json(new { draw = model.draw, recordsTotal = totalRecords, recordsFiltered = recFilter, data = modifiedData });
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return Json(null);
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
                int totalRecords = status.Count;


                var modifiedData = status.Select(d => new StatusViewModel
                {
                    Id = d.Id.ToString(),
                    Name = d.Name,
                    EndPoint = d.EndPoint.ToString(),
                    Buttons = "<button class='buttonEditStatusAdmin btn btn-outline-secondary' value='" + d.Id + "' data-bs-toggle='modal' data-bs-target='#modalEditStatusAdmin'>Edit</button>" +
                              " | " +
                              "<button class='buttonDeleteStatusAdmin btn btn-outline-danger' value='" + d.Id + "' data-bs-toggle='modal' data-bs-target='#modalDeleteStatusAdmin'>Delete</button>"
                }
                    );

                if (!string.IsNullOrEmpty(model.search.value) &&
                    !string.IsNullOrWhiteSpace(model.search.value))
                {
                    modifiedData = modifiedData.Where(p => p.Name.ToLower().Contains(model.search.value) ||
                        p.EndPoint.ToLower().Contains(model.search.value)
                     ).ToList();
                }

                string sortBy = "";
                string sortDir = "";

                if (model.order != null)
                {
                    // in this example we just default sort on the 1st column
                    sortBy = model.columns[model.order[0].column].data;
                    sortDir = model.order[0].dir.ToLower();
                }

                int recFilter = modifiedData.Count();
                modifiedData = modifiedData.AsQueryable().OrderBy(sortBy + " " + sortDir).Skip(model.start).Take(model.length).ToList();

                return Json(new { draw = model.draw, recordsTotal = totalRecords, recordsFiltered = recFilter, data = modifiedData });
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return Json(null);
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
