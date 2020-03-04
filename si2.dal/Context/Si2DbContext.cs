using EntityFrameworkCore.TemporalTables.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using si2.dal.Entities;
using si2.dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace si2.dal.Context
{
    public class Si2DbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DbSet<Dataflow> Dataflows { get; set; }
        public Si2DbContext(DbContextOptions<Si2DbContext> options) : base(options)
        {
            _httpContextAccessor = this.GetService<IHttpContextAccessor>();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.PreventTemporalTables();
            builder.Entity<Dataflow>(b => b.UseTemporalTable());
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            // seed the database with dummy data
        }

        public override async Task<int> SaveChangesAsync(CancellationToken ct)
        {
            var newEntities = this.ChangeTracker.Entries()
                .Where(
                    x => x.State == EntityState.Added &&
                    x.Entity != null &&
                    x.Entity as IHasFullAudit != null
                    )
                .Select(x => x.Entity as IHasFullAudit);

            var modifiedEntities = this.ChangeTracker.Entries()
                .Where(
                    x => x.State == EntityState.Modified &&
                    x.Entity != null &&
                    x.Entity as IHasFullAudit != null
                    )
                .Select(x => x.Entity as IHasFullAudit);

            foreach (var newEntity in newEntities)
            {
                newEntity.CreatedOn = DateTime.UtcNow;
                newEntity.CreatedBy = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                newEntity.LastModifiedOn = DateTime.UtcNow;
                newEntity.LastModifiedBy = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            foreach (var modifiedEntity in modifiedEntities)
            {
                modifiedEntity.LastModifiedOn = DateTime.UtcNow;
                modifiedEntity.LastModifiedBy = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            return await base.SaveChangesAsync(ct);
        }

        public override int SaveChanges()
        {
            var newEntities = this.ChangeTracker.Entries()
                .Where(
                    x => x.State == EntityState.Added &&
                    x.Entity != null &&
                    x.Entity as IHasFullAudit != null
                    )
                .Select(x => x.Entity as IHasFullAudit);

            var modifiedEntities = this.ChangeTracker.Entries()
                .Where(
                    x => x.State == EntityState.Modified &&
                    x.Entity != null &&
                    x.Entity as IHasFullAudit != null
                    )
                .Select(x => x.Entity as IHasFullAudit);

            foreach (var newEntity in newEntities)
            {
                newEntity.CreatedOn = DateTime.UtcNow;
                newEntity.CreatedBy = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                newEntity.LastModifiedOn = DateTime.UtcNow;
                newEntity.LastModifiedBy = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            foreach (var modifiedEntity in modifiedEntities)
            {
                modifiedEntity.LastModifiedOn = DateTime.UtcNow;
                modifiedEntity.LastModifiedBy = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            return base.SaveChanges();
        }
    }
}