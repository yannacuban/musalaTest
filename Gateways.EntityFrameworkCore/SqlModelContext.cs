using GateWays.EntityFrameworkCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Gateways.EntityFrameworkCore
{
    public class SqlModelContext: DbContext
    {
        public SqlModelContext(DbContextOptions<SqlModelContext> options) : base(options)
        {
        }

        public DbSet<Gateway> Gateways { get; set; }

        public DbSet<Peripheral> Peripherals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gateway>(entity =>
            {
                entity.HasKey(m => m.Id);

                entity.Property(e => e.Id).UseIdentityColumn();

                entity.Property(e => e.SerialNumber)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasIndex(e => e.SerialNumber)
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IpAddress)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.ToTable("Gateways");

            });

            modelBuilder.Entity<Peripheral>(entity =>
            {
                entity.HasKey(m => m.Id);

                entity.Property(e => e.Id).UseIdentityColumn();

                entity.Property(e => e.UID)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Vendor)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(p => p.Gateway)
                    .WithMany(g => g.PeripheralList)
                    .HasForeignKey(p => p.GatewayId);

                entity.ToTable("Peripherals");

            });
        }
    }
}
