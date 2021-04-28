using HomeManager.Models.Core;
using HomeManager.Models.DataTables.Finance;
using HomeManager.Models.Entities;
using HomeManager.Models.Interfaces.Factories;
using HomeManager.Models.Interfaces.Services.Finance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HomeManager.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FinanceController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IPaymentService _paymentService;
        private readonly ITemplateService _templateService;
        private readonly IWalletService _walletService;
        private readonly IDataTableFactory _dataTableFactory;

        public FinanceController(UserManager<User> userManager,
            IPaymentService paymentService,
            ITemplateService templateService,
            IWalletService walletService,
            IDataTableFactory dataTableFactory)
        {
            _userManager = userManager;
            _paymentService = paymentService;
            _templateService = templateService;
            _walletService = walletService;
            _dataTableFactory = dataTableFactory;
        }

        [HttpPost]
        public async Task<DataTableResponse<PaymentDataTable>> GetDataTable(DataTableInput model)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _userManager.FindByIdAsync(userId);
                var payments = _paymentService.GetCompleted(user);
                var result = _dataTableFactory.GetTableData(model, payments);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
