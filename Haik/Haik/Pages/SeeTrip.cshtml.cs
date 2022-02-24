using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Haik.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Haik.Pages
{
    public class SeeTripModel : PageModel
    {

        public  string name = "Name";
        public string description = "Description";
        public string duration = "Duration";
        public string location = "Location";
        public string difficulty = "Difficulty";
        public string date = "Date";
        public string ownerID = "OwnerID";
        public string equipment = "Equipment";

        public void OnGet()
        {
        }
    }
}
