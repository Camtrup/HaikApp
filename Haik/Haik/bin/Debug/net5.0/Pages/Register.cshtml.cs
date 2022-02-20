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
    public  RegisterViewModel registerViewModel { get; set; }
    private IEnumerable<ApplicationUser> DBUsers { get; set; }
    private SignInManager<ApplicationUser> signInManager;


        public RegisterModel(UserManager<ApplicationUser> userManager, HaikDBContext dbContext)
    {
        this.userManager = userManager;
        this.dbContext = dbContext;
        
    }

    public void OnGet()
    {
      
    }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = registerViewModel.Email,
                    Email = registerViewModel.Email
                };

                var result = await userManager.CreateAsync(user, registerViewModel.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToPage("Index");
                }
            }
            return Page();

        }
    }
}
