using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.ViewModels.Account
{
    public class LoginWithRecoveryCodeModel
    {
        [BindProperty]
        public LoginWithRecoveryCodeInputModel Input { get; set; }

        public string ReturnUrl { get; set; }
    }

    public class LoginWithRecoveryCodeInputModel
    {
        [BindProperty]
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Recovery Code")]
        public string RecoveryCode { get; set; }
    }

}
