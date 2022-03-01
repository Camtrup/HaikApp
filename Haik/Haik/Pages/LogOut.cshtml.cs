using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Haik.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel;
using System.Diagnostics;

namespace Haik.Pages
{
    public class LogOutModel : PageModel
    {
        SignInManager<ApplicationUser> signInManager;
        public LogOutModel(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public async Task<IActionResult> OnGet()
        {
            var s = signInManager.SignOutAsync();
            return RedirectToPage("/index");

        }
    }
}
