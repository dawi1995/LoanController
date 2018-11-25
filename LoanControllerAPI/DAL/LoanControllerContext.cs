using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LoanControllerAPI.DAL
{
    public partial class LoanControllerContext : DbContext
    {
        public LoanControllerContext()
        {
        }

        public LoanControllerContext(DbContextOptions<LoanControllerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Debt> Debt { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=den1.mssql1.gear.host;Database=loancontroller;Trusted_Connection=False;User ID=loancontroller;Password=Zc763U_D~JCe;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Debt>(entity =>
            {
                entity.Property(e => e.DebtAmount).HasColumnType("decimal(14, 2)");

                entity.Property(e => e.InstallmentAmount).HasColumnType("decimal(14, 2)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);
            });
        }
    }
}
