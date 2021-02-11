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

namespace HomeManager.WebApplication.Controllers
{
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
        public async Task<JsonResult> GetTableData()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                var startRec = Request.Form["start"].FirstOrDefault();
                var pageSize = Request.Form["length"].FirstOrDefault();
                var order = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var orderDir = Request.Form["order[0][dir]"].FirstOrDefault();
                var search = Request.Form["search[value]"].FirstOrDefault().ToLower().Trim();
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

                if (!string.IsNullOrEmpty(search) &&
                    !string.IsNullOrWhiteSpace(search))
                {
                    modifiedData = modifiedData.Where(p => p.Date.Contains(search) ||
                        p.Description.ToLower().Contains(search) ||
                        p.Amount.Contains(search) ||
                        p.Type.ToLower().Contains(search) ||
                        p.Category.ToLower().Contains(search)
                     ).ToList();
                }

                modifiedData = SortTableData(order, orderDir, modifiedData);
                int recFilter = modifiedData.Count();
                modifiedData = modifiedData.Skip(Convert.ToInt32(startRec)).Take(Convert.ToInt32(pageSize)).ToList();

                return Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRecords, recordsFiltered = recFilter, data = modifiedData });
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return Json(null);
        }

        private IEnumerable<PaymentViewModel> SortTableData(string order, string orderDir, IEnumerable<PaymentViewModel> data)
        {
            IEnumerable<PaymentViewModel> lst = new List<PaymentViewModel>();
            try
            {
                switch (order)
                {
                    case "1":
                        lst = orderDir.Equals("ASC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Date).ToList()
                                                                                                 : data.OrderBy(p => p.Date).ToList();
                        break;
                    case "2":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Description).ToList()
                                                                                                 : data.OrderBy(p => p.Description).ToList();
                        break;
                    case "3":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Type).ToList()
                                                                                                   : data.OrderBy(p => p.Type).ToList();
                        break;
                    case "4":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Amount).ToList()
                                                                                                   : data.OrderBy(p => p.Amount).ToList();
                        break;
                    default:
                        lst = orderDir.Equals("ASC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Date).ToList()
                                                                                                 : data.OrderBy(p => p.Date).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return lst;
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
        public async Task<JsonResult> Create(Payments payment, IFormFile files)
        {
            var user = await _userManager.GetUserAsync(User);
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
        public async Task<JsonResult> Edit(int id, Payments payment)
        {
            var user = await _userManager.GetUserAsync(User);
            if (id != payment.Id)
            {
                return Json(null);
            }

            if (ModelState.IsValid)
            {
                try
                {
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
