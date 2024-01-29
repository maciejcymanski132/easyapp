using Microsoft.EntityFrameworkCore;
using Airtrafic.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Airtrafic.Data
{
    public class AirtraficContext : IdentityDbContext<AirtraficUser,AirtraficUserRole,string>
    {
        public AirtraficContext (DbContextOptions<AirtraficContext> options)
            : base(options)
        {
        }
        public DbSet<Airtrafic.Models.Aircraft> Aircraft { get; set; } = default!;
        public DbSet<Airtrafic.Models.Airport> Airport { get; set; } = default!;
        public DbSet<Airtrafic.Models.AircraftManufacturer> AircraftManufacturer { get; set; } = default!;
        public DbSet<Airtrafic.Models.AircraftModel> AircraftModel { get; set; } = default!;
        public DbSet<Airtrafic.Models.Flight> Flight { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AirtraficUserRole>().HasData(
                new AirtraficUserRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new AirtraficUserRole { Id = "2", Name = "Pilot", NormalizedName = "PILOT" }
                );
            modelBuilder.Entity<AirtraficUser>()
                   .HasKey(u => u.Id);

            modelBuilder.Entity<AirtraficUserRole>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<AirtraficUser>()
                .HasOne(u => u.UserRole)
                .WithMany(ur => ur.Users)
                .HasForeignKey(u => u.UserRoleId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Flight>()
                .HasOne(f => f.Aircraft)
                .WithMany()
                .HasForeignKey(f => f.AircraftId);

            modelBuilder.Entity<Flight>()
                .HasOne(f => f.DepartureAirport)
                .WithMany()
                .HasForeignKey(f => f.DepartureAirportId);

            modelBuilder.Entity<Flight>()
                .HasOne(f => f.ArrivalAirport)
                .WithMany()
                .HasForeignKey(f => f.ArrivalAirportId);

            modelBuilder.Entity<Flight>()
                .HasOne(f => f.Aircraft)
                .WithMany()
                .HasForeignKey(f => f.AircraftId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Flight>()
                .HasOne(f => f.DepartureAirport)
                .WithMany()
                .HasForeignKey(f => f.DepartureAirportId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Flight>()
                .HasOne(f => f.ArrivalAirport)
                .WithMany()
                .HasForeignKey(f => f.ArrivalAirportId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Flight)
                .WithMany(f => f.Tickets)
                .HasForeignKey(t => t.FlightId)
                .OnDelete(DeleteBehavior.Cascade);

        }

           
    }
}
