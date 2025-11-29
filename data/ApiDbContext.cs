using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Entidades.data
{
    public class ApiDbContext : DbContext
    {
        public virtual DbSet<models.Team> Teams { get; set; }
        public virtual DbSet<models.Driver> Drivers { get; set; }
        public virtual DbSet<models.DriverMedia> DriverMedias { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1:N Relationship between Team and Driver
            modelBuilder.Entity<models.Driver>(entity =>
            {
                entity.HasOne(t => t.Team)
                        .WithMany(d => d.Drivers)
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("FK_Driver_Team");
            });

            // 1:1 Relationship between Driver and DriverMedia
            modelBuilder.Entity<models.DriverMedia>(entity =>
            {
                entity.HasOne(d => d.Driver)
                        .WithOne(m => m.DriverMedia)
                        .HasForeignKey<models.DriverMedia>(d => d.DriverId);
            });
        }
    }
}