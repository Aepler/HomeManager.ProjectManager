using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.WebApplication.ViewModels.Account
{
    public class ForgotPasswordModel
    {
        [BindProperty]
        public ForgotPasswordInputModel Input { get; set; }
    }

    public class ForgotPasswordInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
