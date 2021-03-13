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

namespace HomeManager.Api.Areas.Finance.Controllers
{
    [Area("Finance")]
    [ApiController]
    [Route("api/[area]/[controller]/[action]")]
    [Authorize]
    public class HomeController : ControllerBase
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
    }
}
