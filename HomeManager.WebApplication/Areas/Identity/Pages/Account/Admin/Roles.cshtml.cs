using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeManager.Models;
using HomeManager.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeManager.WebApplication.Areas.Identity.Pages.Account.Admin
{
    public class RolesModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager; 
        private readonly IRoleService _roleService; 

        public RolesModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager,
            IRoleService roleService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _roleService = roleService;
        }

        public ICollection<Role> roles { get; set; }

        private async Task LoadAsync(User user)
        {
            roles = await _roleService.GetAll();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }
    }
}
