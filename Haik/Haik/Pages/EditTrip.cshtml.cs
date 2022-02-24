﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Haik.Models;
using Haik.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        public void OnGet()
        {

        }

        

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


            if(userID != null)
            {
                TripDb foundTrip = dbContext.Trips.Where<TripDb>(w => w.OwnerId == userID && w.Id == vm.Id).First();

                foundTrip.Description = vm.Description == null ? foundTrip.Description : vm.Description;
                foundTrip.Difficulty = vm.Difficulty == null ? foundTrip.Difficulty : vm.Difficulty;
                foundTrip.Date = vm.Date == null ? foundTrip.Date : vm.Date;
                foundTrip.Duration = vm.Duration == null ? foundTrip.Duration : vm.Duration;
                foundTrip.Location = vm.Location == null ? foundTrip.Location : vm.Location;
                foundTrip.Name = vm.Name == null ? foundTrip.Name : vm.Name;
                foundTrip.Equipment = vm.Equipment == null ? foundTrip.Equipment : vm.Equipment;
                dbContext.SaveChanges();
                return Page();
            }
            else
            {
                return Page();
            }

        }
    }
}
