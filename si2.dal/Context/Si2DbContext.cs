using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using si2.dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace si2.dal.Context
{
    public class Si2DbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Dataflow> Dataflows { get; set; }
        public Si2DbContext(DbContextOptions<Si2DbContext> options) : base(options)
        {  
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            // seed the database with dummy data
        }
    }
}