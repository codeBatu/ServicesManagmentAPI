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

        public virtual DbSet<LogTable> LogTable { get; set; } = null!;
        public virtual DbSet<ServiceTable> ServiceTable { get; set; } = null!;
        public virtual DbSet<MailTable> MailTables { get; set; } = null!;



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source=BURAKPC;Initial Catalog=SmartPulseServiceManagerDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogTable>(entity =>
            {
                entity.ToTable("LogTable");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

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

            modelBuilder.Entity<MailTable>(entity =>
            {
                entity.ToTable("MailTable");

                entity.Property(e => e.Cc)
                    .HasMaxLength(128)
                    .HasColumnName("cc");

                entity.Property(e => e.Gmail).HasMaxLength(128);

                entity.Property(e => e.Sender).HasMaxLength(128);

                entity.Property(e => e.Topic).HasMaxLength(128);

                entity.HasOne(d => d.Log)
                    .WithMany(p => p.MailTables)
                    .HasForeignKey(d => d.LogId)
                    .HasConstraintName("FK__MailTable__LogId__6754599E");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
