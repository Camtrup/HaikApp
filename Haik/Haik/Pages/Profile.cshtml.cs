using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Haik.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Haik.Pages
{
    public class ProfileModel : PageModel
    {
        public HaikDBContext context;
        public ApplicationUser user { get; set; }
        public List<TripDb> trips = new List<TripDb>();
        public ProfileModel(HaikDBContext context)
        {
            this.context = context;
        }
        public void OnGet(string id)
        {
            user = context.Users.Where<ApplicationUser>(w => w.UserName == id).FirstOrDefault();
            foreach (var t in JsonConvert.DeserializeObject<List<int>>(user.JsonParticipatedTrips))
            {
                var trip = context.Trips.Where<TripDb>(u => u.Id == t).First();
                if(Convert.ToDateTime(trip.Date) < DateTime.Now)
                {
                    trips.Add(trip);
                }
            }
        }
    }
}
