using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.ViewModels
{
    public class StatusMessageModel
    {
        [TempData]
        public string StatusMessage { get; set; }
    }
}
