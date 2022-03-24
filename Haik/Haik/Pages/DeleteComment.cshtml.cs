using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Haik.Models;
using Haik.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Haik.Pages
{
    public class DeleteCommentModel : PageModel
    {
        public int id;
        public int index;
        HaikDBContext dbContext;
        public DeleteCommentModel(HaikDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void OnGet(int id, int index)
        {
            this.id = id;
            this.index = index;

        }
        public async Task<IActionResult> OnPostAsync(int id, int index)
        {
            var CommentToDelete = dbContext.Trips.Where<TripDb>(d => d.Id == id).FirstOrDefault();
            if (CommentToDelete.OwnerId == User.FindFirstValue(ClaimTypes.NameIdentifier) ||
                dbContext.Users.Where<ApplicationUser>(u => u.UserName == User.Identity.Name).First().Admin)
            {
                var jsonPrevdeser = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(CommentToDelete.CommentJSON);
                var x = index + 1;
                
                if (x != -1)
                {
                    jsonPrevdeser.RemoveAt(x);
                    jsonPrevdeser.RemoveAt(x - 1);
                    var newJson = Newtonsoft.Json.JsonConvert.SerializeObject(jsonPrevdeser);
                    dbContext.Trips.Where<TripDb>(w => w.Id == id).FirstOrDefault().CommentJSON = newJson;
                    await dbContext.SaveChangesAsync();
                }
                return RedirectToPage("/Index");
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }
    }
}
