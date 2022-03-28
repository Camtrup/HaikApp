using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Haik.Models;
using Haik.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Drawing;
using System.Web;

namespace Haik.Pages
{
    public class SeeTripModel : PageModel
    {
        public HaikDBContext context = null;
        private List<TripDb> datalookup;
        public int id;
        public TripDb trip;
        public readonly List<ApplicationUser> tripParticipants = new List<ApplicationUser>();

        public string Image1Source { get; set; }
        public string Image2Source { get; set; }
        public string Image3Source { get; set; }

        public void setImages(string base64String)
        {
            //Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            //Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            
            //return System.Drawing.Image.FromStream(ms);
        }

        public List<string> comments = new List<string>();
        [BindProperty]
        public EditWalkViewModel walkViewModel { get; set; }

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


            if (trip.JsonParticipantUids != null)
            {

                var strList = JsonConvert.DeserializeObject<List<string>>(trip.JsonParticipantUids);
                foreach (var u in strList)
                {
                    tripParticipants.Add(context.Users.Where<ApplicationUser>(w => w.Id == u).First());
                }
            }

            Image1Source = "data:image/png;base64," + trip.ImageBlobOne;
            Image2Source = "data:image/png;base64," + trip.ImageBlobTwo;
            Image3Source = "data:image/png;base64," + trip.ImageBlobThree;
            if (trip.CommentJSON != null)
            {
                comments = JsonConvert.DeserializeObject<List<string>>(trip.CommentJSON);
                
                //for (int i = 0; i < strList.Count; i += 2)
                //{
                //    var u = context.Users.Where<ApplicationUser>(w => w.Id == strList[i + 1]).First();
                //    comments.Add(strList[i], u);
                //}

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

            //image
            if (trip.JsonParticipantUids != null)
            {

                var strList = JsonConvert.DeserializeObject<List<string>>(trip.JsonParticipantUids);
                foreach (var u in strList)
                {
                    tripParticipants.Add(context.Users.Where<ApplicationUser>(w => w.Id == u).First());
                }
            }

            Image1Source = "data:image/png;base64," + trip.ImageBlobOne;
            Image2Source = "data:image/png;base64," + trip.ImageBlobTwo;
            Image3Source = "data:image/png;base64," + trip.ImageBlobThree;
            //image

            if (!string.IsNullOrEmpty(walkViewModel.CommentJSON)) // adding comments
            {
                if (trip.CommentJSON is null) {
                    List<String> jsonPrevdeser = new List<string> { };
                    jsonPrevdeser.Add(walkViewModel.CommentJSON);
                    jsonPrevdeser.Add(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    var newJson = JsonConvert.SerializeObject(jsonPrevdeser);
                    trip.CommentJSON = newJson;
                    context.SaveChanges();
                }
                else {
                    var jsonPrevdeser = JsonConvert.DeserializeObject<List<string>>(trip.CommentJSON);
                    jsonPrevdeser.Add(walkViewModel.CommentJSON);
                    jsonPrevdeser.Add(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    var newJson = JsonConvert.SerializeObject(jsonPrevdeser);
                    trip.CommentJSON = newJson;
                    context.SaveChanges();
                }
                
                
                var jsonComments  = JsonConvert.DeserializeObject<List<string>>(trip.CommentJSON);
                for (int i = 0; i < jsonComments.Count; i += 2)
                {
                    var u = context.Users.Where<ApplicationUser>(w => w.Id == jsonComments[i + 1]).First();

                    comments.Add(jsonComments[i]);
                    comments.Add(u.Id);
                }
                this.walkViewModel.CommentJSON = string.Empty;

            }

            else
            {


                //trip updating

                trips = await context.Trips.ToListAsync();
                var JSONprevOccupantsString = trips.Where<TripDb>(w => w.Id == id).FirstOrDefault().JsonParticipantUids;


                if (JSONprevOccupantsString != null)
                {

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
                    else
                    {
                        ujsonPrevdeser.Remove(id.ToString());
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
}
