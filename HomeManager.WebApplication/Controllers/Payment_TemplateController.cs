using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HomeManager.Data;
using HomeManager.Models;

namespace HomeManager.WebApplication.Controllers
{
    public class Payment_TemplateController : Controller
    {
        private readonly HomeManagerContext _context;

        public Payment_TemplateController(HomeManagerContext context)
        {
            _context = context;
        }

        // GET: Payment_Template
        public async Task<IActionResult> Index()
        {
            var homeManagerContext = _context.Payment_Templates.Include(p => p.Category).Include(p => p.Type).Include(p => p.User);
            return View(await homeManagerContext.ToListAsync());
        }

        // GET: Payment_Template/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment_Template = await _context.Payment_Templates
                .Include(p => p.Category)
                .Include(p => p.Type)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment_Template == null)
            {
                return NotFound();
            }

            return View(payment_Template);
        }

        // GET: Payment_Template/Create
        public IActionResult Create()
        {
            ViewData["fk_CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["fk_TypeId"] = new SelectList(_context.Types, "Id", "EndTaxType");
            ViewData["fk_UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Payment_Template/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,fk_UserId,Date,Description,Amount,Invoice,Deleted,DeletedOn,fk_TypeId,fk_CategoryId")] Payment_Template payment_Template)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment_Template);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["fk_CategoryId"] = new SelectList(_context.Categories, "Id", "Name", payment_Template.fk_CategoryId);
            ViewData["fk_TypeId"] = new SelectList(_context.Types, "Id", "EndTaxType", payment_Template.fk_TypeId);
            ViewData["fk_UserId"] = new SelectList(_context.Users, "Id", "Id", payment_Template.fk_UserId);
            return View(payment_Template);
        }

        // GET: Payment_Template/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment_Template = await _context.Payment_Templates.FindAsync(id);
            if (payment_Template == null)
            {
                return NotFound();
            }
            ViewData["fk_CategoryId"] = new SelectList(_context.Categories, "Id", "Name", payment_Template.fk_CategoryId);
            ViewData["fk_TypeId"] = new SelectList(_context.Types, "Id", "EndTaxType", payment_Template.fk_TypeId);
            ViewData["fk_UserId"] = new SelectList(_context.Users, "Id", "Id", payment_Template.fk_UserId);
            return View(payment_Template);
        }

        // POST: Payment_Template/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,fk_UserId,Date,Description,Amount,Invoice,Deleted,DeletedOn,fk_TypeId,fk_CategoryId")] Payment_Template payment_Template)
        {
            if (id != payment_Template.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment_Template);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Payment_TemplateExists(payment_Template.Id))
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
            ViewData["fk_CategoryId"] = new SelectList(_context.Categories, "Id", "Name", payment_Template.fk_CategoryId);
            ViewData["fk_TypeId"] = new SelectList(_context.Types, "Id", "EndTaxType", payment_Template.fk_TypeId);
            ViewData["fk_UserId"] = new SelectList(_context.Users, "Id", "Id", payment_Template.fk_UserId);
            return View(payment_Template);
        }

        // GET: Payment_Template/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment_Template = await _context.Payment_Templates
                .Include(p => p.Category)
                .Include(p => p.Type)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment_Template == null)
            {
                return NotFound();
            }

            return View(payment_Template);
        }

        // POST: Payment_Template/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment_Template = await _context.Payment_Templates.FindAsync(id);
            _context.Payment_Templates.Remove(payment_Template);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Payment_TemplateExists(int id)
        {
            return _context.Payment_Templates.Any(e => e.Id == id);
        }
    }
}
