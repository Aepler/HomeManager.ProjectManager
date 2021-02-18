using System;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using HomeManager.Models.DataTableModels;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using HomeManager.Models.Interfaces.Finance;
using HomeManager.Models.Interfaces.Factories;
using HomeManager.Models.Helpers;
using HomeManager.Models.Interfaces.Factories.Finance;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        private readonly ITemplateService _paymentTemplateService;
        private readonly IDataTableFactory _dataTableFactory;
        private readonly IFinanceFormFactory _financeFormFactory;

        public PaymentsController(UserManager<User> userManager, IPaymentService paymentService, ICategoryService categoryService, ITypeService typeService, IStatusService statusService, ITemplateService paymentTemplateService, IDataTableFactory dataTableFactory, IFinanceFormFactory financeFormFactory)
        {
            _userManager = userManager;
            _paymentService = paymentService;
            _categoryService = categoryService;
            _typeService = typeService;
            _statusService = statusService;
            _paymentTemplateService = paymentTemplateService;
            _dataTableFactory = dataTableFactory;
            _financeFormFactory = financeFormFactory;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewData["Types"] = new SelectList(await _typeService.GetAll(user), "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetTableData(DataTableModel model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var payments = await _paymentService.GetAll(user);
                var result = await _dataTableFactory.GetTableData(model, payments);

                return Json(new { draw = result.draw, recordsTotal = result.recordsTotal, recordsFiltered = result.recordsFiltered, data = result.data });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetPaymentCreate(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var data = await _financeFormFactory.GetCreateForm(user, id);
            return Json(data);
        }

        [HttpGet]
        public async Task<JsonResult> GetPaymentEdit(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var data = await _financeFormFactory.GetEditForm(user, id);
            return Json(data);
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
