using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Haik.Models;
using Haik.ViewModels;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Haik.Pages
{
    public class AddCommentModel : PageModel
    {
        public HaikDBContext context;
        public TripDb trip;
        [BindProperty]
        public EditWalkViewModel walkViewModel { get; set; }
        public int id;
        public AddCommentModel(HaikDBContext context)
        {
            this.context = context;
        }
        public async void OnGetAsync(int id)
        {
            this.id = id;
            trip = context.Trips.Where<TripDb>(t => t.Id == id).First();
        }
        public async Task OnPostAsync(int id)
        {
            trip = context.Trips.Where<TripDb>(t => t.Id == id).First();
            var jsonPrevdeser = JsonConvert.DeserializeObject<List<string>>(trip.CommentJSON);
            jsonPrevdeser.Add(walkViewModel.CommentJSON);
            jsonPrevdeser.Add(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var newJson = JsonConvert.SerializeObject(jsonPrevdeser);
            context.Trips.Where<TripDb>(w => w.Id == id).FirstOrDefault().CommentJSON = newJson;
            context.SaveChanges();
        }
    }
}
