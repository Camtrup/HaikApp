using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Haik.Models;
using Haik.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Haik.Pages
{
    public class EditTripModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HaikDBContext dbContext;
        [BindProperty]
        public EditWalkViewModel walkViewModel { get; set; }
        public EditTripModel(HaikDBContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this._userManager = userManager;
        }
        public TripDb trip;
        public int id;
        private List<TripDb> datalookup;


        public async Task<IActionResult> RemoveImage(int id)
        {

            var trip = dbContext.Trips.Where<TripDb>(w => w.Id == id).FirstOrDefault();

            if (id == 1)
            {
                trip.ImageBlobOne = null;
            }
            if (id == 2)
            {
                trip.ImageBlobTwo = null;
            }
            if (id == 3)
            {
                trip.ImageBlobThree = null;
            }

            await dbContext.SaveChangesAsync();
            return Page();

        }

        public async void OnGet(int id)
        {
            ViewData["id"] = id;
            this.id = id;
            datalookup = await dbContext.Trips.ToListAsync();



            foreach (var d in datalookup)
            {
                int temp = (int)d.Id;

                if (temp.ToString().Equals(id.ToString()))
                {
                    trip = d;
                }
            }
        }


        public async Task<IActionResult> AddImage(EditWalkViewModel model, int id)
        {

            var trip = dbContext.Trips.Where<TripDb>(w => w.Id == id).FirstOrDefault();

            using (var ms = new MemoryStream())
            {
                model.PictureToAdd.CopyTo(ms);


                var fileBytes = ms.ToArray();

                string s = Convert.ToBase64String(fileBytes);


                if (id == 1)
                {
                    trip.ImageBlobOne = s;
                }
                if (id == 2)
                {
                    trip.ImageBlobTwo = s;
                }
                if (id == 3)
                {
                    trip.ImageBlobThree = s;
                }


                await dbContext.SaveChangesAsync();
            }
            return Page();

        }

        

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var vm = walkViewModel;
            vm.Id = id;
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


            if (userID != null)
            {
                TripDb foundTrip = null;
                if (u.Admin)
                {
                    foundTrip = dbContext.Trips.Where<TripDb>(w => w.Id == id).First();
                }
                else
                {
                    foundTrip = dbContext.Trips.Where<TripDb>(w => w.OwnerId == userID && w.Id == vm.Id).First();
                }

                foundTrip.Description = vm.Description == null ? foundTrip.Description : vm.Description;
                foundTrip.Difficulty = vm.Difficulty == null ? foundTrip.Difficulty : vm.Difficulty;
                foundTrip.Date = vm.Date == null ? foundTrip.Date : vm.Date;
                foundTrip.Duration = vm.Duration == null ? foundTrip.Duration : vm.Duration;
                foundTrip.Location = vm.Location == null ? foundTrip.Location : vm.Location;
                foundTrip.Name = vm.Name == null ? foundTrip.Name : vm.Name;
                foundTrip.Equipment = vm.Equipment == null ? foundTrip.Equipment : vm.Equipment;
                dbContext.SaveChanges();
                return RedirectToPage("/Index");
            }
            else
            {
                return Page();
            }

        }
    }
}
