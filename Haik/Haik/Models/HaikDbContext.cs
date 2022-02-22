using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Haik.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool Admin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }

    }

    public class HaikDBContext : IdentityDbContext<ApplicationUser>
    {

        public HaikDBContext(DbContextOptions<HaikDBContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }


        public DbSet<Haik.Models.TripDb> Trips { get; set; } 
    }
}
