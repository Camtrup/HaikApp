using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Haik.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Haik.Pages
{
    public class SeeTripModel : PageModel
    {
        public HaikDBContext context;
        public List<TripDb> datalookup;
        public int id;
        public TripDb trip;

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

        }
    }
}
