using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using HomeManager.Models.Interfaces.Factories;
using HomeManager.Models.Interfaces.Services.Finance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeManager.WebApplication.Areas.Finance.Controllers
{
    [Area("Finance")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IWalletService _walletService;
        private readonly IDataTableFactory _dataTableFactory;

        public HomeController(UserManager<User> userManager,
            IWalletService walletService,
            IDataTableFactory dataTableFactory)
        {
            _userManager = userManager;
            _walletService = walletService;
            _dataTableFactory = dataTableFactory;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewData["Wallets"] = await _walletService.GetAll(user);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SelectWallet(Guid id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    user.CurrentWallet = id;
                    await _userManager.UpdateAsync(user);
                }
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWallet(Wallet wallet) 
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    wallet.CurrentBalance = wallet.StartBalance;
                    await _walletService.Add(user, wallet);

                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return RedirectToAction("Index");
        }
    }
}
