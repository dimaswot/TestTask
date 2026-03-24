using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using MigrationService.Entities;

namespace MigrationService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options) { }

        public DbSet<Currency> Currency => Set<Currency>();
        public DbSet<User> User => Set<User>();
        public DbSet<UserFavorite> UserFavorite => Set<UserFavorite>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Table Currency
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Rate)
                    .IsRequired()
                    .HasColumnType("numeric(18,6)");

                entity.HasIndex(e => e.Name).IsUnique();
            });

            // Table User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.ToTable("user");
            });

            // Table UserFavorite
            modelBuilder.Entity<UserFavorite>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.CurrencyName });

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Favorites)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Currency)
                    .WithMany()
                    .HasForeignKey(e => e.CurrencyName)
                    .HasPrincipalKey(c => c.Name)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
