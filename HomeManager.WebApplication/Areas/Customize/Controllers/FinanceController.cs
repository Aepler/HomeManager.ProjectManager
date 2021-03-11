using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HomeManager.Models.DataTable;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using HomeManager.Models.Interfaces.Services.Finance;
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
        private readonly IWalletService _walletService;
        private readonly IDataTableFactory _dataTableFactory;

        public FinanceController(UserManager<User> userManager, ICategoryService categoryService, ITypeService typeService, IStatusService statusService, ITemplateService templateService, IWalletService walletService, IDataTableFactory dataTableFactory)
        {
            _userManager = userManager;
            _categoryService = categoryService;
            _typeService = typeService;
            _statusService = statusService;
            _templateService = templateService;
            _walletService = walletService;
            _dataTableFactory = dataTableFactory;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetWallet(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            var wallet = await _walletService.GetById(user, id);
            return Json(wallet);
        }

        [HttpGet]
        public async Task<JsonResult> GetCategory(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            var category = await _categoryService.GetById(user, id);
            return Json(category);
        }

        [HttpGet]
        public async Task<JsonResult> GetTemplate(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            var template = await _templateService.GetById(user, id);
            return Json(template);
        }

        [HttpGet]
        public async Task<JsonResult> GetType(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            var type = await _typeService.GetById(user, id);
            return Json(type);
        }

        [HttpGet]
        public async Task<JsonResult> GetStatus(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            var status = await _statusService.GetById(user, id);
            return Json(status);
        }

        public async Task<IActionResult> Wallets()
        {
            var user = await _userManager.GetUserAsync(User);

            var wallets = await _walletService.GetAll(user);
            ViewData["WalletCount"] = wallets.Count;

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetWalletTableData(DataTableInput model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var wallets = await _walletService.GetAll(user);
                var result = await _dataTableFactory.GetTableData(model, wallets);

                return Json(new { draw = result.draw, recordsTotal = result.recordsTotal, recordsFiltered = result.recordsFiltered, data = result.data });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CreateWallet(Wallet wallet)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    wallet.CurrentBalance = wallet.StartBalance;
                    await _walletService.Add(user, wallet);
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
        public async Task<JsonResult> EditWallet(Guid id, Wallet wallet)
        {
            if (id != wallet.Id)
            {
                return Json(null);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    await _walletService.Update(user, wallet);
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
        public async Task<IActionResult> DeleteWallet(Guid id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var wallet = await _walletService.GetById(user, id);
                    if (wallet != null)
                    {
                        await _walletService.Delete(user, wallet);
                        user.CurrentWallet = null;
                        await _userManager.UpdateAsync(user);
                    }
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Categories()
        {
            var user = await _userManager.GetUserAsync(User);

            var categories = await _categoryService.GetByUser(user);
            ViewData["CategoryCount"] = categories.Count;

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetCategoryTableData(DataTableInput model)
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
        public async Task<JsonResult> EditCategory(Guid id, Category category)
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
        public async Task<IActionResult> DeleteCategory(Guid id)
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

            var templates = await _templateService.GetAll(user);
            ViewData["TemplateCount"] = templates.Count;

            ViewData["fk_TypeId"] = new SelectList(await _statusService.GetAll(user), "Id", "Name");
            ViewData["fk_CategoryId"] = new SelectList(await _statusService.GetAll(user), "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetTemplateTableData(DataTableInput model)
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
        public async Task<JsonResult> EditTemplate(Guid id, Template template)
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
        public async Task<IActionResult> DeleteTemplate(Guid id)
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

            var types = await _typeService.GetByUser(user);
            ViewData["TypeCount"] = types.Count;

            ViewData["Status"] = new SelectList(await _statusService.GetByEndPoint(user, true), "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetTypeTableData(DataTableInput model)
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
        public async Task<JsonResult> EditType(Guid id, Type type)
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
        public async Task<IActionResult> DeleteType(Guid id)
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
            var user = await _userManager.GetUserAsync(User);

            var statuses = await _statusService.GetByUser(user);
            ViewData["StatusCount"] = statuses.Count;

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetStatusTableData(DataTableInput model)
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
        public async Task<JsonResult> EditStatus(Guid id, Status status)
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
        public async Task<IActionResult> DeleteStatus(Guid id)
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
    }
}
