using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HomeManager.Models.DataTable;
using HomeManager.Models.DataTable.Finance;
using HomeManager.Models.Entities;
using HomeManager.Models.Interfaces.Factories;
using System.Linq;

namespace HomeManager.Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ApiController]
    [Route("api/[area]/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class ManageController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IDataTableFactory _dataTableFactory;

        public ManageController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IDataTableFactory dataTableFactory)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dataTableFactory = dataTableFactory;
        }
    }
}
