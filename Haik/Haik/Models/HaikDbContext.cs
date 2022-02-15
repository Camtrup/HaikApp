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


        //public DbSet<TurAppen.Model.Walk> Walks { get; set; }
        //public DbSet<TurAppen.Model.Comment> Comments { get; set; }
    }
}
