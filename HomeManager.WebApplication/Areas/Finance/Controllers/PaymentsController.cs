using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using HomeManager.Data;
using HomeManager.Models;
using HomeManager.Models.ViewModels;
using HomeManager.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using HomeManager.Models.Helpers;
using System.IO;
using System.Linq.Dynamic.Core;
using HomeManager.Models.Factories;

namespace HomeManager.WebApplication.Areas.Finance.Controllers
{
    [Area("Finance")]
    [Authorize]
    public class PaymentsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IPaymentService _paymentService;
        private readonly ICategoryService _categoryService;
        private readonly ITypeService _typeService;
        private readonly IStatusService _statusService;
        private readonly IPayment_TemplateService _payment_templateService;

        public PaymentsController(UserManager<User> userManager, IPaymentService paymentService, ICategoryService categoryService, ITypeService typeService, IStatusService statusService, IPayment_TemplateService payment_templateService)
        {
            _userManager = userManager;
            _paymentService = paymentService;
            _categoryService = categoryService;
            _typeService = typeService;
            _statusService = statusService;
            _payment_templateService = payment_templateService;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var payments = await _paymentService.GetAll(user);

            ViewBag.Payments = payments;

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetTableData(DataTableModel model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var payments = await _paymentService.GetCompleted(user);
                int totalRecords = payments.Count;


                var modifiedData = payments.Select(d => new PaymentViewModel
                {
                    Id = d.Id.ToString(),
                    Date = d.Date.ToString("dd.MM.yyyy"),
                    Description = d.Description,
                    Description_Extra = d.Description_Extra,
                    Description_Tax = d.Description_Tax,
                    Tax = d.Tax.ToString(),
                    Amount = d.Amount.ToString(),
                    Amount_Tax = d.Amount_Tax.ToString(),
                    Amount_Gross = d.Amount_Gross.ToString(),
                    Amount_Net = d.Amount_Net.ToString(),
                    Amount_Extra = d.Amount_Extra.ToString(),
                    Amount_TaxList = d.Amount_TaxList,
                    fk_TypeId = d.fk_TypeId.ToString(),
                    Type = d.Type.Name,
                    fk_CategoryId = d.fk_CategoryId.ToString(),
                    Category = d.fk_CategoryId != null ? d.Category.Name : d.Type.Name,
                    fk_StatusId = d.fk_StatusId.ToString(),
                    Status = d.Status.Name
                }
                    );

                if (!string.IsNullOrEmpty(model.search.value) &&
                    !string.IsNullOrWhiteSpace(model.search.value))
                {
                    modifiedData = modifiedData.Where(p => p.Date.Contains(model.search.value) ||
                        p.Description.ToLower().Contains(model.search.value) ||
                        p.Amount.Contains(model.search.value) ||
                        p.Type.ToLower().Contains(model.search.value) ||
                        p.Category.ToLower().Contains(model.search.value)
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

        [HttpGet]
        public async Task<JsonResult> GetPayment(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var payment = await _paymentService.GetById(user, id);
            return Json(payment);
        }

        [HttpGet]
        public async Task<JsonResult> GetTypeList()
        {
            var user = await _userManager.GetUserAsync(User);
            var types = await _typeService.GetAll(user);
            return Json(types);
        }

        [HttpGet]
        public async Task<JsonResult> GetTemplate()
        {
            var user = await _userManager.GetUserAsync(User);
            var payment_template = await _payment_templateService.GetAll(user);
            return Json(payment_template);
        }

        [HttpGet]
        public async Task<JsonResult> GetCategoryList()
        {
            var user = await _userManager.GetUserAsync(User);
            var categories = await _categoryService.GetAll(user);
            return Json(categories);
        }

        [HttpGet]
        public async Task<JsonResult> GetStatusListByType(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var status = await _statusService.GetPossibleStatus(user, id);
            return Json(status);
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create(Payment payment, IFormFile files)
        {
            if (ModelState.IsValid)
            {
                if (files != null && files.IsImage())
                {
                    using (var target = new MemoryStream())
                    {
                        files.CopyTo(target);
                        payment.Invoice = target.ToArray();
                        payment.DataType = files.ContentType;
                    }
                }
                else if (files != null && files.IsPDF())
                {
                    using (var target = new MemoryStream())
                    {
                        files.CopyTo(target);
                        payment.Invoice = target.ToArray();
                        payment.DataType = files.ContentType;
                    }
                }

                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    await _paymentService.Add(user, payment);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                
                return Json(null);
            }
            return Json(null);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Edit(int id, Payment payment)
        {
            if (id != payment.Id)
            {
                return Json(null);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    await _paymentService.Update(user, payment);
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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var payment = await _paymentService.GetById(user, id);
                    if (payment != null)
                    {
                        await _paymentService.Delete(user, payment);
                    }
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PaymentExistsAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var payments = await _paymentService.GetAll(user);
            return payments.Any(e => e.Id == id);
        }
    }
}
