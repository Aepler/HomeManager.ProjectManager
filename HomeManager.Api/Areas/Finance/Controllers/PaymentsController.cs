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
using HomeManager.Models.DataTable;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using HomeManager.Models.Interfaces.Services.Finance;
using HomeManager.Models.Interfaces.Factories;
using HomeManager.Models.Helpers;
using HomeManager.Models.Interfaces.Factories.Finance;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeManager.Api.Areas.Finance.Controllers
{
    [Area("Finance")]
    [ApiController]
    [Route("api/[area]/[controller]/[action]")]
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IPaymentService _paymentService;
        private readonly ICategoryService _categoryService;
        private readonly ITypeService _typeService;
        private readonly IStatusService _statusService;
        private readonly ITemplateService _templateService;
        private readonly IWalletService _walletService;
        private readonly IDataTableFactory _dataTableFactory;
        private readonly IFinanceFormFactory _financeFormFactory;

        public PaymentsController(UserManager<User> userManager,
            IPaymentService paymentService,
            ICategoryService categoryService,
            ITypeService typeService,
            IStatusService statusService,
            ITemplateService templateService,
            IWalletService walletService,
            IDataTableFactory dataTableFactory,
            IFinanceFormFactory financeFormFactory)
        {
            _userManager = userManager;
            _paymentService = paymentService;
            _categoryService = categoryService;
            _typeService = typeService;
            _statusService = statusService;
            _templateService = templateService;
            _walletService = walletService;
            _dataTableFactory = dataTableFactory;
            _financeFormFactory = financeFormFactory;
        }
    }
}
