using Haik.Models;
using Haik.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Haik.Pages
{
  public class RegisterModel : PageModel
  {
    private readonly ILogger<RegisterModel> _logger;
    private readonly HaikDBContext dbContext;
    private readonly UserManager<ApplicationUser> userManager;
    [BindProperty]
    public RegisterViewModel registerViewModel { get; set; }
    private IEnumerable<ApplicationUser> DBUsers { get; set; }

        private readonly SignInManager<ApplicationUser> signInManager;


        public RegisterModel(UserManager<ApplicationUser> userManager, HaikDBContext dbContext, SignInManager<ApplicationUser> signInManager)
    {
        this.userManager = userManager;
        this.dbContext = dbContext;
            this.signInManager = signInManager;


        }

        public void OnGet()
    {
      
    }

    public async Task<IActionResult> OnPostAsync()
    {
 
        if(Environment.GetEnvironmentVariable("ApiKey") == registerViewModel.APIkey)
            {
                //admin confirmed
                if (ModelState.IsValid)
                {
                    var u = new ApplicationUser()
                    {
                        UserName = registerViewModel.Email,
                        Email = registerViewModel.Email,
                        Admin = true
                    };

                    var result = await userManager.CreateAsync(u, registerViewModel.Password);
                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(u, false);
                        return RedirectToPage("Index");
                    }
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var u = new ApplicationUser()
                    {
                        UserName = registerViewModel.Email,
                        Email = registerViewModel.Email,
                        Admin = false
                    };

                    var result = await userManager.CreateAsync(u, registerViewModel.Password);
                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(u, false);
                        return RedirectToPage("Index");
                    }
                }
            }
        return Page();
    }
  }
}
