﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace Haik.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        
        public async Task<IActionResult> OnGet()
        {
            Debug.WriteLine(User.Identity.Name);
            if (User.Identity.Name != null) //Sjekke om en har sjekket av "husk meg"
            {
                return RedirectToPage("/IndexLoggedIn");
            }
            return Page();
        }
    }
}
