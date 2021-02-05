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
using HomeManager.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HomeManager.WebApplication.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IPaymentService _paymentService;
        private readonly ICategoryService _categoryService;
        private readonly ITypeService _typeService;
        private readonly IStatusService _statusService;

        public PaymentsController(UserManager<User> userManager, IPaymentService paymentService , ICategoryService categoryService, ITypeService typeService, IStatusService statusService)
        {
            _userManager = userManager;
            _paymentService = paymentService;
            _categoryService = categoryService;
            _typeService = typeService;
            _statusService = statusService;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var payments = await _paymentService.GetAll(user);
            return View(payments);
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var payment = await _paymentService.GetById(user, id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public async Task<IActionResult> Create()
        {
            ViewData["fk_CategoryId"] = new SelectList(await _categoryService.GetAll(), "Id", "Name");
            ViewData["fk_StatusId"] = new SelectList(await _statusService.GetAll(), "Id", "Name");
            ViewData["fk_TypeId"] = new SelectList(await _typeService.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,fk_UserId,Date,Description,Description_Extra,Description_Tax,Tax,Amount,Amount_Tax,Amount_Gross,Amount_Net,Amount_Extra,Invoice,fk_TypeId,fk_CategoryId,fk_StatusId,Deleted")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                payment.fk_UserId = user.Id;
                await _paymentService.Add(user, payment);
                return RedirectToAction(nameof(Index));
            }
            ViewData["fk_CategoryId"] = new SelectList(await _categoryService.GetAll(), "Id", "Name", payment.fk_CategoryId);
            ViewData["fk_StatusId"] = new SelectList(await _statusService.GetAll(), "Id", "Name", payment.fk_StatusId);
            ViewData["fk_TypeId"] = new SelectList(await _typeService.GetAll(), "Id", "Name", payment.fk_TypeId);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var payment = await _paymentService.GetById(user, id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["fk_CategoryId"] = new SelectList(await _categoryService.GetAll(), "Id", "Name", payment.fk_CategoryId);
            ViewData["fk_StatusId"] = new SelectList(await _statusService.GetAll(), "Id", "Name", payment.fk_StatusId);
            ViewData["fk_TypeId"] = new SelectList(await _typeService.GetAll(), "Id", "Name", payment.fk_TypeId);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,fk_UserId,Date,Description,Description_Extra,Description_Tax,Tax,Amount,Amount_Tax,Amount_Gross,Amount_Net,Amount_Extra,Invoice,fk_TypeId,fk_CategoryId,fk_StatusId,Deleted")] Payment payment)
        {
            if (id != payment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    payment.fk_UserId = user.Id;
                    await _paymentService.Update(user, payment);
                }
                catch (DbUpdateConcurrencyException)
                {
                    bool exist = await PaymentExistsAsync(payment.Id);
                    if (!exist)
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
            ViewData["fk_CategoryId"] = new SelectList(await _categoryService.GetAll(), "Id", "Name", payment.fk_CategoryId);
            ViewData["fk_StatusId"] = new SelectList(await _statusService.GetAll(), "Id", "Name", payment.fk_StatusId);
            ViewData["fk_TypeId"] = new SelectList(await _typeService.GetAll(), "Id", "Name", payment.fk_TypeId);
            return View(payment);
        }

        private async Task<bool> PaymentExistsAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var payments = await _paymentService.GetAll(user);
            return payments.Any(e => e.Id == id);
        }
    }
}
