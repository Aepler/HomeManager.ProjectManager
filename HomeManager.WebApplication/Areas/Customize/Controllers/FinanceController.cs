using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HomeManager.Models.DataTableModels;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using HomeManager.Models.Interfaces.Finance;
using HomeManager.Models.Interfaces.Factories;
using Type = HomeManager.Models.Entities.Finance.Type;

namespace HomeManager.WebApplication.Areas.Customize.Controllers
{
    [Area("Customize")]
    [Authorize]
    public class FinanceController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ICategoryService _categoryService;
        private readonly ITypeService _typeService;
        private readonly IStatusService _statusService;
        private readonly ITemplateService _templateService;
        private readonly IDataTableFactory _dataTableFactory;

        public FinanceController(UserManager<User> userManager, ICategoryService categoryService, ITypeService typeService, IStatusService statusService, ITemplateService templateService, IDataTableFactory dataTableFactory)
        {
            _userManager = userManager;
            _categoryService = categoryService;
            _typeService = typeService;
            _statusService = statusService;
            _templateService = templateService;
            _dataTableFactory = dataTableFactory;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetCategory(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var category = await _categoryService.GetById(user, id);
            return Json(category);
        }

        [HttpGet]
        public async Task<JsonResult> GetTemplate(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var payment_Template = await _templateService.GetById(user, id);
            return Json(payment_Template);
        }

        [HttpGet]
        public async Task<JsonResult> GetType(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var type = await _typeService.GetById(user, id);
            return Json(type);
        }

        [HttpGet]
        public async Task<JsonResult> GetStatus(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var status = await _statusService.GetById(user, id);
            return Json(status);
        }

        public async Task<IActionResult> Categories()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetCategoryTableData(DataTableModel model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var categories = await _categoryService.GetByUser(user);
                var result = await _dataTableFactory.GetTableData(model, categories);

                return Json(new { draw = result.draw, recordsTotal = result.recordsTotal, recordsFiltered = result.recordsFiltered, data = result.data });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    await _categoryService.Add(user, category);
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
        public async Task<JsonResult> EditCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return Json(null);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    await _categoryService.Update(user, category);
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
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var category = await _categoryService.GetById(user, id);
                    if (category != null)
                    {
                        await _categoryService.Delete(user, category);
                    }
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Templates()
        {
            var user = await _userManager.GetUserAsync(User);

            ViewData["fk_TypeId"] = new SelectList(await _statusService.GetAll(user), "Id", "Name");
            ViewData["fk_CategoryId"] = new SelectList(await _statusService.GetAll(user), "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetTemplateTableData(DataTableModel model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var templates = await _templateService.GetAll(user);
                var result = await _dataTableFactory.GetTableData(model, templates);

                return Json(new { draw = result.draw, recordsTotal = result.recordsTotal, recordsFiltered = result.recordsFiltered, data = result.data });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CreateTemplate(Template template)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    await _templateService.Add(user, template);
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
        public async Task<JsonResult> EditTemplate(int id, Template template)
        {
            if (id != template.Id)
            {
                return Json(null);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    await _templateService.Update(user, template);
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
        public async Task<IActionResult> DeleteTemplate(int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var template = await _templateService.GetById(user, id);
                    if (template != null)
                    {
                        await _templateService.Delete(user, template);
                    }
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Types()
        {
            var user = await _userManager.GetUserAsync(User);

            ViewData["Status"] = new SelectList(await _statusService.GetByEndPoint(user, true), "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetTypeTableData(DataTableModel model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var types = await _typeService.GetByUser(user);
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
                var status = await _statusService.GetByUser(user);
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

        private async Task<bool> CategoryExists(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var categories = await _categoryService.GetAll(user);
            return categories.Any(e => e.Id == id);
        }
    }
}
