﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Model;
namespace Repository.DbContexts
{

    public partial class SmartPulseServiceManagerContext : DbContext
    {
        public SmartPulseServiceManagerContext()
        {
        }

        public SmartPulseServiceManagerContext(DbContextOptions<SmartPulseServiceManagerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LogTable> LogTables { get; set; } = null!;
        public virtual DbSet<ServiceTable> ServiceTables { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source=BATU;Initial Catalog=SmartPulseServiceManager;Persist Security Info=True;User ID=sa;Password=1478");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
