using System;
using System.Collections.Generic;
using System.Text;
using EventsMedia.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventsMedia.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<Adventure> AdventuresTable { get; set; }
        public DbSet<AdventurePost> AdventuresPost { get; set; }

        public DbSet<CommentsTable> Comments { get; set; }
        public DbSet<FavoriteEvents> Favorites { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cuisine> Cuisines { get; set; }
        public DbSet<Establishment> Establishments { get; set; }
    }
}
