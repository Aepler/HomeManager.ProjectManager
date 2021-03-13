using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeManager.Api.Areas.Settings.Controllers
{
    [Area("Settings")]
    [ApiController]
    [Route("api/[area]/[controller]/[action]")]
    [Authorize]
    public class FinanceController : ControllerBase
    {

    }
}
