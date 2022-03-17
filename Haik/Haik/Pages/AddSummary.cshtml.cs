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
        public HaikDBContext haikDb;
        public TripDb trip;
        public EditWalkViewModel walkViewModel { get; set; }
        public int id;
        public AddSummaryModel(HaikDBContext haikDB)
        {
            this.haikDb = haikDb;
        }
        public void OnGet(int id)
        {
            this.id = id;
            trip = haikDb.Trips.Where<TripDb>(t => t.Id == id).First();

        }

        public async Task OnPostAsync(int id)
        {
            trip = haikDb.Trips.Where<TripDb>(t => t.Id == id).First();

            trip.Summary = walkViewModel.Summary == null ? trip.Summary : walkViewModel.Summary;

            haikDb.SaveChanges();


        }

    }
}
