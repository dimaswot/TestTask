using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CurrencyUpdater.Entities;
using CurrencyUpdater.Models;

using Microsoft.EntityFrameworkCore;

namespace CurrencyUpdater.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options) { }

        public DbSet<Currency> Currency => Set<Currency>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Table Currency
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Rate)
                    .IsRequired()
                    .HasColumnType("numeric(18,6)");

                entity.HasIndex(e => e.Name).IsUnique();
            });
        }
    }
}
