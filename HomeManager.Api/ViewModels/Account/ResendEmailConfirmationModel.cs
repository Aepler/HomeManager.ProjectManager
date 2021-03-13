using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Api.ViewModels.Account
{
    public class ResendEmailConfirmationModel
    {
        [BindProperty]
        public ResendEmailConfirmationInputModel Input { get; set; }
    }

    public class ResendEmailConfirmationInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
