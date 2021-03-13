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

namespace HomeManager.Api.Areas.Customize.Controllers
{
    [Area("Customize")]
    [ApiController]
    [Route("api/[area]/[controller]/[action]")]
    [Authorize]
    public class FinanceController : ControllerBase
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
    }
}
