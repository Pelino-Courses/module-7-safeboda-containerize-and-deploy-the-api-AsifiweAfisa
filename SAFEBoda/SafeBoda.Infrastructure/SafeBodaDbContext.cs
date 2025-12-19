using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SafeBoda.Core.Models;

namespace SafeBoda.Infrastructure
{
    public class SafeBodaDbContext : IdentityDbContext<ApplicationUser>
    {
        public SafeBodaDbContext(DbContextOptions<SafeBodaDbContext> options) : base(options)
        {
        }

        public DbSet<Rider> Riders { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Trip> Trips { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Trip>()
                .Property(t => t.Fare)
                .HasPrecision(18, 2);
            
            modelBuilder.Entity<Trip>().OwnsOne(t => t.Start, sa =>
            {
                sa.Property(p => p.Latitude).HasColumnName("StartLatitude");
                sa.Property(p => p.Longitude).HasColumnName("StartLongitude");
            });

            modelBuilder.Entity<Trip>().OwnsOne(t => t.End, ea =>
            {
                ea.Property(p => p.Latitude).HasColumnName("EndLatitude");
                ea.Property(p => p.Longitude).HasColumnName("EndLongitude");
            });
        }

    }
}
