﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeManager.WebApplication.Areas.Settings.Controllers
{
    [Area("Settings")]
    [Authorize]
    public class FinanceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
