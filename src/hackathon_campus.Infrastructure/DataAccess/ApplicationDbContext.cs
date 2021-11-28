using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hackathon_campus.Core.Entities;
using hackathon_campus.Infrastructure.DataAccess.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace hackathon_campus.Infrastructure.DataAccess
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser, ApplicationRole, string, 
        IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, 
        IdentityUserToken<string>>
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<CategorySubscription> CategorySubscriptions { get; set; }

        public DbSet<EventSubscription> EventSubscriptions { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole
                {
                    Id = "1",
                    Name = "admin",
                    NormalizedName = "ADMIN"
                },
                new ApplicationRole
                {
                    Id = "2",
                    Name = "user",
                    NormalizedName = "USER"
                },
                new ApplicationRole
                {
                    Id = "3",
                    Name = "moderator",
                    NormalizedName = "MODERATOR"
                });
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = new Guid(0x1479a92a, 0x883f, 0x4844, 0xb8, 0x39, 
                    0xb2, 0x94, 0xb4, 0x91, 0x97, 0xe9),
                    Name = "IT",
                    Description = "IT events"
                });


            modelBuilder.ApplyConfiguration(new ImageMap());
            modelBuilder.ApplyConfiguration(new EventImageMap());
            modelBuilder.ApplyConfiguration(new CategoryImageMap());
            modelBuilder.ApplyConfiguration(new CategorySubscriptionMap());
            modelBuilder.ApplyConfiguration(new EventSubscriptionMap());
        }
    }
}
