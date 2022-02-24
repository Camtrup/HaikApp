
﻿using Haik.Models;
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
        private readonly HaikDBContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
    
        [BindProperty]
        public RegisterViewModel registerViewModel { get; set; }


        public RegisterModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, HaikDBContext dbContext)
        {
        this.userManager = userManager;
        this.signInManager = signInManager;
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
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName,
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.Email,
                    Description = registerViewModel.Description,
                    Gender = registerViewModel.Gender,
                    DateOfBirth = registerViewModel.DateOfBirth
                };

                var result = await userManager.CreateAsync(user, registerViewModel.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToPage("/Index");
                }
            }
            return Page();

        }
    }
}

