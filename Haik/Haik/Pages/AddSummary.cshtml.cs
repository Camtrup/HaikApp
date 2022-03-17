using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Haik.Models;
using Haik.ViewModels;
namespace Haik.Pages
{
    public class AddSummaryModel : PageModel
    {
        public HaikDBContext context;
        public TripDb trip;

        [BindProperty]
        public EditWalkViewModel walkViewModel { get; set; }
        public int id;
        public AddSummaryModel(HaikDBContext context)
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
            if(walkViewModel != null)
            {
                if(walkViewModel.Summary != null)
                {
                    trip.Summary = walkViewModel.Summary;
                }
            }
            context.SaveChanges();
        }
    }
}