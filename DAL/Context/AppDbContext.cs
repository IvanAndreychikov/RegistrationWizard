using Microsoft.EntityFrameworkCore;
using RegistrationWizard.Models;

namespace RegistrationWizard.DAL.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Province> Provinces { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Login)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasOne(u => u.Country)
                .WithMany()
                .HasForeignKey(u => u.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Province)
                .WithMany()
                .HasForeignKey(u => u.ProvinceId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Country>()
                .HasMany(c => c.Provinces)
                .WithOne(p => p.Country)
                .HasForeignKey(p => p.CountryId);

            // Seed data
            modelBuilder.Entity<Country>().HasData(
                new Country { Id = 1, Name = "Republic of South Africa" },
                new Country { Id = 2, Name = "Chile" }
            );
            modelBuilder.Entity<Province>().HasData(
                new Province { Id = 1, Name = "Gauteng", CountryId = 1 },
                new Province { Id = 2, Name = "Western Cape", CountryId = 1 },
                new Province { Id = 3, Name = "KwaZulu-Natal", CountryId = 1 },
                new Province { Id = 4, Name = "El Loa", CountryId = 2 },
                new Province { Id = 5, Name = "Huasco", CountryId = 2 },
                new Province { Id = 6, Name = "Melipilla", CountryId = 2 }
            );
        }
    }
}
