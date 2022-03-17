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
    public class SeeTripModel : PageModel
    {
        public HaikDBContext context = null;
        private List<TripDb> datalookup;
        public int id;
        public TripDb trip;
        public readonly List<ApplicationUser> tripParticipants = new List<ApplicationUser>();

        public SeeTripModel(HaikDBContext context)
        {
            this.context = context;
            
        }

        public async Task OnGetAsync(int id)
        {
            ViewData["id"] = id;
            this.id = id;
            datalookup = await context.Trips.ToListAsync();

            

            foreach (var d in datalookup)
            {
                int temp = (int)d.Id;

                if (temp.ToString().Equals(id.ToString()))
                {
                    trip = d;
                }
            }
            if(trip.JsonParticipantUids != null)
            {

                var strList = JsonConvert.DeserializeObject<List<string>>(trip.JsonParticipantUids);
                foreach(var u in strList)
                {
                    tripParticipants.Add(context.Users.Where<ApplicationUser>(w => w.Id == u).First());
                }
            }
        }

        public IEnumerable<TripDb> trips { get; set; }
        public IEnumerable<ApplicationUser> users { get; set; }

        public async Task OnPostAsync(int id)
        {



            ViewData["id"] = id;
            this.id = id;
            datalookup = await context.Trips.ToListAsync();

            

            foreach (var d in datalookup)
            {
                int temp = (int)d.Id;

                if (temp.ToString().Equals(id.ToString()))
                {
                    trip = d;
                }
            }

            

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //trip updating

            trips = await context.Trips.ToListAsync();
            var JSONprevOccupantsString = trips.Where<TripDb>(w => w.Id == id).FirstOrDefault().JsonParticipantUids;


                if(JSONprevOccupantsString != null) {

                    var jsonPrevdeser = JsonConvert.DeserializeObject<List<string>>(JSONprevOccupantsString);


                    if (!jsonPrevdeser.Contains(userId))
                    {
                        jsonPrevdeser.Add(userId);
                    }
                    else
                    {
                        jsonPrevdeser.Remove(userId);
                    }

                    var newJson = JsonConvert.SerializeObject(jsonPrevdeser);

                    trips.Where<TripDb>(w => w.Id == id).FirstOrDefault().JsonParticipantUids = newJson;
                    context.SaveChanges();

                    //TODO set button to #"meld deg av"

                }
                else
                {
                    var jsonPrevdeser = new List<string>();


                    if (!jsonPrevdeser.Contains(userId))
                    {
                        jsonPrevdeser.Add(userId);
                    }
                    var newJson = JsonConvert.SerializeObject(jsonPrevdeser);

                    trips.Where<TripDb>(w => w.Id == id).FirstOrDefault().JsonParticipantUids = newJson;
                    context.SaveChanges();

                    //TODO set button to #"meld deg av"
                }



                //person updating


                users = await context.Users.ToListAsync();
                var uJSONprevOccupantsString = users.Where<ApplicationUser>(w => w.Id == userId).FirstOrDefault().JsonParticipatedTrips;





                if (uJSONprevOccupantsString != null)
                {

                    var ujsonPrevdeser = JsonConvert.DeserializeObject<List<string>>(uJSONprevOccupantsString);


                if (!ujsonPrevdeser.Contains(id.ToString()))
                {
                    ujsonPrevdeser.Add(id.ToString());
                }
                   

                    var unewJson = JsonConvert.SerializeObject(ujsonPrevdeser);

                    users.Where<ApplicationUser>(w => w.Id == userId).FirstOrDefault().JsonParticipatedTrips = unewJson;
                    context.SaveChanges();


                }
                else
                {
                    var ujsonPrevdeser = new List<string>();



                if (!ujsonPrevdeser.Contains(id.ToString()))
                {
                    ujsonPrevdeser.Add(id.ToString());
                }

                var unewJson = JsonConvert.SerializeObject(ujsonPrevdeser);

                    users.Where<ApplicationUser>(w => w.Id == userId).FirstOrDefault().JsonParticipatedTrips = unewJson;
                    context.SaveChanges();

                }
            var strList = JsonConvert.DeserializeObject<List<string>>(trip.JsonParticipantUids);
            foreach (var u in strList)
            {
                tripParticipants.Add(context.Users.Where<ApplicationUser>(w => w.Id == u).First());
            }
        }

    }
}
