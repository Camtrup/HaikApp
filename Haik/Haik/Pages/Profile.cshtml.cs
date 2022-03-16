using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Haik.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Haik.Pages
{
    public class ProfileModel : PageModel
    {
        public HaikDBContext context;
        public ApplicationUser user { get; set; }
        public IEnumerable<TripDb> trips { get; set; }
        
        public ProfileModel(HaikDBContext context)
        {
            this.context = context;
        }
        public void OnGet(string id)
        {
            user = context.Users.Where<ApplicationUser>(w => w.UserName == id).FirstOrDefault();
        }
    }
}
