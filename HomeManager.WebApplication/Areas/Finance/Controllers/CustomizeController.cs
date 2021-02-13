using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HomeManager.Data;
using HomeManager.Models;
using HomeManager.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Type = HomeManager.Models.Type;
using Microsoft.AspNetCore.Authorization;

namespace HomeManager.WebApplication.Areas.Finance.Controllers
{
    [Area("Finance")]
    [Authorize]
    public class CustomizeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ICategoryService _categoryService;
        private readonly ITypeService _typeService;
        private readonly IStatusService _statusService;
        private readonly IPayment_TemplateService _payment_templateService;

        public CustomizeController(UserManager<User> userManager, ICategoryService categoryService, ITypeService typeService, IStatusService statusService, IPayment_TemplateService payment_templateService)
        {
            _userManager = userManager;
            _categoryService = categoryService;
            _typeService = typeService;
            _statusService = statusService;
            _payment_templateService = payment_templateService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Categories()
        {
            var user = await _userManager.GetUserAsync(User);
            var categories = await _categoryService.GetByUser(user);

            ViewBag.Categories = categories;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory([Bind("Id,fk_UserId,Name,Deleted,DeletedOn")] Category category)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                await _categoryService.Add(user, category);
                return RedirectToAction(nameof(Types));
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(int id, [Bind("Id,fk_UserId,Name,Deleted,DeletedOn")] Category category)
        {
            var user = await _userManager.GetUserAsync(User);
            if (id != category.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.Update(user, category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    bool exist = await CategoryExists(category.Id);
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

            return View(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var category = await _categoryService.GetById(user, id);
            await _categoryService.Delete(user, category);
            return RedirectToAction(nameof(Categories));
        }

        public async Task<IActionResult> PaymentTemplates()
        {
            var user = await _userManager.GetUserAsync(User);
            var payment_Templates = await _payment_templateService.GetAll(user);

            ViewBag.Payment_Templates = payment_Templates;

            ViewData["fk_TypeId"] = new SelectList(await _statusService.GetAll(user), "Id", "Name");
            ViewData["fk_CategoryId"] = new SelectList(await _statusService.GetAll(user), "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePaymentTemplate(Payment_Template payment_Template)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                await _payment_templateService.Add(user, payment_Template);
                return RedirectToAction(nameof(Types));
            }
            ViewData["fk_TypeId"] = new SelectList(await _statusService.GetAll(user), "Id", "Name", payment_Template.fk_TypeId);
            ViewData["fk_CategoryId"] = new SelectList(await _statusService.GetAll(user), "Id", "Name", payment_Template.fk_CategoryId);
            return View(payment_Template);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPaymentTemplate(int id, Payment_Template payment_Template)
        {
            var user = await _userManager.GetUserAsync(User);
            if (id != payment_Template.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _payment_templateService.Update(user, payment_Template);
                }
                catch (DbUpdateConcurrencyException)
                {
                    bool exist = await TypeExists(payment_Template.Id);
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
            ViewData["fk_TypeId"] = new SelectList(await _statusService.GetAll(user), "Id", "Name", payment_Template.fk_TypeId);
            ViewData["fk_CategoryId"] = new SelectList(await _statusService.GetAll(user), "Id", "Name", payment_Template.fk_CategoryId);
            ViewData["fk_CategoryId"] = new SelectList(await _statusService.GetAll(user), "Id", "Name", payment_Template.fk_CategoryId);
            return View(payment_Template);
        }

        public async Task<IActionResult> Types()
        {
            var user = await _userManager.GetUserAsync(User);
            var types = await _typeService.GetByUser(user);

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
            var status = await _statusService.GetByUser(user);

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

        private async Task<bool> CategoryExists(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var categories = await _categoryService.GetAll(user);
            return categories.Any(e => e.Id == id);
        }
    }
}
