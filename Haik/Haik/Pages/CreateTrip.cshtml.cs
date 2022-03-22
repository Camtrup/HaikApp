using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Haik.Models;
using Haik.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment webHostEnvironment;
        [BindProperty]
        public CreateTripModel walkViewModel { get; set; }
        public CreateTrip(HaikDBContext dbContext, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostEnvironment)
        {
            this.dbContext = dbContext;
            this._userManager = userManager;
            this.webHostEnvironment = hostEnvironment;
        }
        public void OnGet()
        {
            var e = walkViewModel;
            var s = 2;
        }

        private string UploadedFile(CreateTripModel model)
        {
            string uniqueFileName = null;

            if (model.pictureToAdd != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Pictures");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.pictureToAdd.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.pictureToAdd.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
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
                Name = vm.name,
                Difficulty = vm.difficulty,
                Date = vm.date,
                Equipment = vm.equipment,
                OwnerId = userID,
                dbIsCommercial = u.IsCommercial
                //Predefined participants could be added her, but application owner did not request this.
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
            return RedirectToPage("/Index");
        }
    }
}
