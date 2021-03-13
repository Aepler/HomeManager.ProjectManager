using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using HomeManager.Models.DataTable;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using HomeManager.Models.Interfaces.Services.Finance;
using HomeManager.Models.Interfaces.Factories;
using Type = HomeManager.Models.Entities.Finance.Type;

namespace HomeManager.Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ApiController]
    [Route("api/[area]/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class FinanceController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ICategoryService _categoryService;
        private readonly ITypeService _typeService;
        private readonly IStatusService _statusService;
        private readonly IDataTableFactory _dataTableFactory;

        public FinanceController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager,
            ICategoryService categoryService,
            ITypeService typeService,
            IStatusService statusService,
            IDataTableFactory dataTableFactory)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _categoryService = categoryService;
            _typeService = typeService;
            _statusService = statusService;
            _dataTableFactory = dataTableFactory;
        }
    }
}
