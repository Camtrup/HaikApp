using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Haik.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Haik.Pages
{
    public class ProfileCommercial : PageModel
    {
        public HaikDBContext context = null;

        [BindProperty]
        public ApplicationUser user { get; set; }
        public ProfileCommercial(HaikDBContext context)
        {
            this.context = context;
        }

        public IEnumerable<ApplicationUser> users { get; set; }
        public IEnumerable<TripDb> trips { get; set; }
        public void OnGet()
        {
            users = context.Users.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            user = users.Where<ApplicationUser>(w => w.Id == userId).FirstOrDefault();
            
        }
    }
}
