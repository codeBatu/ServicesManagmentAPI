using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Model;

namespace Repository.DbContexts
{
    public partial class SmartPulseServiceManagerDbContext : DbContext
    {
        public SmartPulseServiceManagerDbContext()
        {
        }

        public SmartPulseServiceManagerDbContext(DbContextOptions<SmartPulseServiceManagerDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<GroupAccount> GroupAccounts { get; set; } = null!;
        public virtual DbSet<LogTable> LogTable { get; set; } = null!;
        public virtual DbSet<ServiceTable> ServiceTable { get; set; } = null!;
        public virtual DbSet<UserGroup> UserGroups { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=BATU;Initial Catalog=SmartPulseServiceManagerDb;Persist Security Info=True;User ID=sa;Password=1478");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(250);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.HasOne(d => d.UserGroup)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.UserGroupId)
                    .HasConstraintName("FK_Accounts_UserGroups");
            });

            modelBuilder.Entity<GroupAccount>(entity =>
            {
                entity.HasKey(e => e.AccountId);

                entity.Property(e => e.AccountId).ValueGeneratedNever();

            });

            modelBuilder.Entity<LogTable>(entity =>
            {
                entity.ToTable("LogTable");

                entity.Property(e => e.CreateDateTime).HasColumnType("date");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.LogTables)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK__LogTable__Servic__267ABA7A");
            });

            modelBuilder.Entity<ServiceTable>(entity =>
            {
                entity.ToTable("ServiceTable");

                entity.Property(e => e.ActiveLife).HasMaxLength(50);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.RestDateTime).HasColumnType("datetime");

                entity.Property(e => e.ServiceName).HasMaxLength(128);

                entity.Property(e => e.Version)
                    .HasMaxLength(16)
                    .IsFixedLength();
            });

            modelBuilder.Entity<UserGroup>(entity =>
            {
                entity.Property(e => e.GroupName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
