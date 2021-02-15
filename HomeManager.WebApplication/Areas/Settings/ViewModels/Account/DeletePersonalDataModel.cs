using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.WebApplication.Areas.Settings.ViewModels.Account
{
    public class DeletePersonalDataModel
    {
        [BindProperty]
        public DeletePersonalDataInputModel Input { get; set; }

        public bool RequirePassword { get; set; }
    }

    public class DeletePersonalDataInputModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
