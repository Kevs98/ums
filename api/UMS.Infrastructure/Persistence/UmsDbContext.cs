using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UMS.Core.Entities;

namespace UMS.Infrastructure.Persistence
{
    public class UmsDbContext : DbContext
    {
        public UmsDbContext(DbContextOptions<UmsDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Applications> Applications { get; set; }
        public DbSet<ApplicationType> ApplicationTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Applications>()
                .HasOne(a => a.approver)
                .WithMany(u => u.ApplicationsApproved)
                .HasForeignKey(a => a.approver_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Applications>()
                .HasOne(a => a.applicant)
                .WithMany(u => u.ApplicationsApplicant)
                .HasForeignKey(a => a.applicant_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Applications>()
                .Property(a => a.created_at)
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
