using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeManager.Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ApiController]
    [Route("api/[area]/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class HomeController : ControllerBase
    {

    }
}
