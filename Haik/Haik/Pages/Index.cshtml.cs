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
using Newtonsoft.Json;

namespace Haik.Pages
{
    public class IndexModel : PageModel
    {
        public readonly HaikDBContext dbContext;
        public List<TripDb> data = null;
        public List<TripDb> userUpComingTrips = new List<TripDb>();
        public SignInManager<ApplicationUser> signInManager;
        public ApplicationUser user;


        public IndexModel(HaikDBContext dbContext, SignInManager<ApplicationUser> signInManager)
        {
            //this.user = user;
            this.dbContext = dbContext;
            this.signInManager = signInManager;
        }

        public async Task OnGetAsync()
        {
            data = await dbContext.Trips.ToListAsync(); 
            user = dbContext.Users.Where<ApplicationUser>(w => w.UserName == User.Identity.Name).FirstOrDefault();
            if(user!= null && user.JsonParticipatedTrips!= null)
            {
                foreach (var t in JsonConvert.DeserializeObject<List<int>>(user.JsonParticipatedTrips))
                {
                    var trip = dbContext.Trips.Where<TripDb>(u => u.Id == t).First();
                    if (Convert.ToDateTime(trip.Date) > DateTime.Now)
                    {
                        userUpComingTrips.Add(trip);
                    }
                }
            }

        }
        public async Task<RedirectToPageResult> OnPostAsync()
        {
            string keys= Request.Form["search"];
            return RedirectToPage("/Search", new { s = keys });
        }
    }
}
