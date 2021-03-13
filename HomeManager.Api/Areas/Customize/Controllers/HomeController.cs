using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeManager.Api.Areas.Customize.Controllers
{
    [Area("Customize")]
    [ApiController]
    [Route("api/[area]/[controller]/[action]")]
    [Authorize]
    public class HomeController : ControllerBase
    {

    }
}
