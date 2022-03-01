using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using Haik.Models;
using Microsoft.EntityFrameworkCore;

namespace Haik.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HaikDBContext dbContext;
        private ApplicationUser user;
        public List<TripDb> data = null;
        public List<TripDb> userUpComingTrips = null;
        public SignInManager<ApplicationUser> signInManager;


        public IndexModel(HaikDBContext dbContext, SignInManager<ApplicationUser> signInManager)
        {
            //this.user = user;
            this.dbContext = dbContext;
            this.signInManager = signInManager;
        }

        public async Task OnGetAsync()
        {
            data = await dbContext.Trips.ToListAsync();
            // user = await user.Trips.ToListAsync();

        }
    }
}
