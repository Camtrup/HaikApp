using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Haik.Models;
using Haik.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Haik.Pages
{
    public class LoginModel : PageModel
    {

        private readonly SignInManager<ApplicationUser> signInManager;

        [BindProperty]
        public LoginViewModel loginViewModel { get; set; }

        public LoginModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            
            return Page();
        }
    }
}