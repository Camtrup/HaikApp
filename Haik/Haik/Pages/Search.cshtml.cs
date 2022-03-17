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
        public List<TripDb> queriedTrips = new List<TripDb>();

        public SearchModel(HaikDBContext context)
        {
            this.context = context;
        }
        public async void OnGetAsync(string s)
        {
            if(s == null)
            {
                queriedTrips = context.Trips.ToList<TripDb>();
            }
            else
            {
                var keywords = s.Split(" ");
                foreach(var word in keywords)
                {
                    queriedTrips.AddRange(context.Trips.Where<TripDb>(t => !queriedTrips.Contains(t) && (t.Name.Contains(word) || t.Location.Contains(word))).ToList());
                }
            }
        }

        public async Task OnPostAsync()
        {
            var keywords = Request.Form["search"].ToString().Split(" ");
            var sort = Request.Form["select"];
            foreach (var word in keywords)
            {
                queriedTrips.AddRange(context.Trips.Where<TripDb>(t => !queriedTrips.Contains(t) && (t.Name.Contains(word) || t.Location.Contains(word))).ToList());
            }
            if(sort == "asc")
            {
                queriedTrips = queriedTrips.OrderBy(o => o.Date).ToList<TripDb>();
            }
            else if(sort == "desc")
            {
                queriedTrips = queriedTrips.OrderByDescending(o => o.Date).ToList<TripDb>();
            }
            else if(sort == "finished")
            {
                foreach(var t in queriedTrips.ToList())
                {
                    if ((Convert.ToDateTime(t.Date) > DateTime.Now))
                    {
                        queriedTrips.Remove(t);
                    }
                }
            }
            else if(sort == "upcoming")
            {
                foreach (var t in queriedTrips.ToList())
                {
                    if ((Convert.ToDateTime(t.Date) < DateTime.Now))
                    {
                        queriedTrips.Remove(t);
                    }
                }
            }
        }
    }
}
