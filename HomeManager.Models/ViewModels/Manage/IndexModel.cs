using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.ViewModels.Manage
{
    public class IndexModel
    {
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public IndexInputModel Input { get; set; }
    }

    public class IndexInputModel
    {
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Last Name")]
        public string Lastname { get; set; }
    }
}
