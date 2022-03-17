using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Haik.Migrations;
using Haik.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Haik.Pages
{
    public class SearchModel : PageModel
    {
        public readonly HaikDBContext context;
        public readonly List<TripDb> queriedTrips = new List<TripDb>();

        public SearchModel(HaikDBContext context)
        {
            this.context = context;
        }
        public async void OnGetAsync(string searchString = "")
        {
            var keywords = searchString.Split(" ");
            foreach(var word in keywords)
            {
                queriedTrips.AddRange(context.Trips.Where<TripDb>(t => t.Name.Contains(word) || t.Location.Contains(word)).ToList());
            }
        }

        public async Task OnPostAsync()
        {
            var keywords = Request.Form["search"].ToString().Split(" ");
            foreach (var word in keywords)
            {
                queriedTrips.AddRange(context.Trips.Where<TripDb>(t => t.Name.Contains(word) || t.Location.Contains(word)).ToList());
            }
        }
    }
}
