using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Haik.Models;
using Haik.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Haik.Pages
{
    public class DeleteTripModel : PageModel
    {
        public int id;
        HaikDBContext dbContext;
        public DeleteTripModel(HaikDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void OnGet(int id)
        {
            this.id = id;
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var tripToDelete = dbContext.Trips.Where<TripDb>(d => d.Id == id).FirstOrDefault();
            if (tripToDelete.OwnerId == User.FindFirstValue(ClaimTypes.NameIdentifier) || 
                dbContext.Users.Where<ApplicationUser>(u => u.UserName == User.Identity.Name).First().Admin)
            {
                dbContext.Trips.Remove(tripToDelete);
                await dbContext.SaveChangesAsync();
                return RedirectToPage("/Index");
            }
            else
            {
                return RedirectToPage("/Index");
            }


        }


    }
}
