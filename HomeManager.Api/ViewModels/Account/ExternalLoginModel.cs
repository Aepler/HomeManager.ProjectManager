using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Api.ViewModels.Account
{
    public class ExternalLoginModel
    {
        [BindProperty]
        public ExternalLoginInputModel Input { get; set; }

        public string ProviderDisplayName { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
    }

    public class ExternalLoginInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
