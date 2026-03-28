using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FinanceService.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace FinanceService.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Currency> Currency => Set<Currency>();
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

            // Table UserFavorite
            modelBuilder.Entity<UserFavorite>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.CurrencyName });

                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.Property(e => e.CurrencyName).HasColumnName("CurrencyName");
            });
        }
    }
}
