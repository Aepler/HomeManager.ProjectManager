using HomeManager.Models.DataTable;
using HomeManager.Models.DataTable.Finance;
using HomeManager.Models.Entities;
using HomeManager.Models.Interfaces.Factories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IDataTableFactory _dataTableFactory;

        public AdminController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IDataTableFactory dataTableFactory)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dataTableFactory = dataTableFactory;
        }

        [HttpPost]
        public DataTableResponse<UserDataTable> GetUserTableData([FromBody]DataTableInput model)
        {
            var users = _userManager.Users.ToList();
            return _dataTableFactory.GetTableData(model, users);
        }
    }
}
