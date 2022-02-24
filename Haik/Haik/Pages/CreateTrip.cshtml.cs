using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Haik.Models;
using Haik.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Haik.Pages
{
    public class CreateTrip : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HaikDBContext dbContext;
        [BindProperty]
        public CreateTripModel walkViewModel { get; set; }
        public CreateTrip(HaikDBContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this._userManager = userManager;
        }
        public void OnGet()
        {
            var e = walkViewModel;
            var s = 2;
        }
        public IEnumerable<TripDb> trips { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            var vm = walkViewModel;
            var users = dbContext.Users.Where(w => w.UserName == User.Identity.Name);
            ApplicationUser u = null;
            if (users.Count() > 0)
            {
                u = users.First();
            }
            string userID = null;
            if (u != null)
            {
                
                userID = await _userManager.GetUserIdAsync(u);
            }
            var newTrip = new TripDb()
            {
                Description = vm.description,
                Location = vm.location,
                Duration = vm.duration,
                Name =  vm.name,
                Difficulty = vm.difficulty,
                Date = vm.date, 
                Equipment = vm.equipment,
                OwnerId = userID
                //Her må man legge inn deltagere på turen etter hvert når de trykker "bli med på tur"?
            };
            dbContext.Trips.Add(newTrip);
            dbContext.SaveChanges();
            /*trips = await dbContext.Trips.ToListAsync();
            foreach(var i in trips)
            {
                if (i.OwnerId != null)
                {
                    return RedirectToPage("Error");
                }
            }*/
            return Page();
        }
    }
}
