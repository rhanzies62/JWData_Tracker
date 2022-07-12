﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using JWDataTracker.Infrastructure;

namespace JWDataTracker.Infrastructure.Repository
{
    public partial class DataTrackerContext : DbContext
    {
        public DataTrackerContext()
        {
        }

        public DataTrackerContext(DbContextOptions<DataTrackerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Congregation> Congregations { get; set; }
        public virtual DbSet<CongregationUser> CongregationUsers { get; set; }
        public virtual DbSet<MidWeekSchedule> MidWeekSchedules { get; set; }
        public virtual DbSet<MidWeekScheduleItem> MidWeekScheduleItems { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<ServiceReport> ServiceReports { get; set; }
        public virtual DbSet<WeekendMeetingSchedule> WeekendMeetingSchedules { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("DataSource=../DataTracker.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Congregation>(entity =>
            {
                entity.ToTable("Congregation");

                entity.Property(e => e.CreatedDate).IsRequired();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<CongregationUser>(entity =>
            {
                entity.ToTable("CongregationUser");

                entity.Property(e => e.CongregationId).HasDefaultValueSql("0");

                entity.Property(e => e.CreatedDate).IsRequired();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasDefaultValueSql("\"\"");

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.Salt).IsRequired();

                entity.Property(e => e.Username).IsRequired();
            });

            modelBuilder.Entity<MidWeekSchedule>(entity =>
            {
                entity.ToTable("MidWeekSchedule");

                entity.Property(e => e.ScheduledDate).IsRequired();
            });

            modelBuilder.Entity<MidWeekScheduleItem>(entity =>
            {
                entity.ToTable("MidWeekScheduleItem");

                entity.Property(e => e.HallNumber).IsRequired();

                entity.Property(e => e.Role).IsRequired();

                entity.HasOne(d => d.PartnerPublisher)
                    .WithMany(p => p.MidWeekScheduleItemPartnerPublishers)
                    .HasForeignKey(d => d.PartnerPublisherId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.MidWeekScheduleItemPublishers)
                    .HasForeignKey(d => d.PublisherId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.ToTable("Publisher");

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.IsMs).HasColumnName("IsMS");

                entity.Property(e => e.IsRp).HasColumnName("IsRP");

                entity.Property(e => e.LastName).IsRequired();

                entity.HasOne(d => d.Congregation)
                    .WithMany(p => p.Publishers)
                    .HasForeignKey(d => d.CongregationId);
            });

            modelBuilder.Entity<ServiceReport>(entity =>
            {
                entity.ToTable("ServiceReport");

                entity.Property(e => e.Date).IsRequired();
            });

            modelBuilder.Entity<WeekendMeetingSchedule>(entity =>
            {
                entity.ToTable("WeekendMeetingSchedule");

                entity.Property(e => e.Date).IsRequired();

                entity.Property(e => e.Speaker).IsRequired();

                entity.Property(e => e.SpeakerCongregation).IsRequired();

                entity.Property(e => e.Topic).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
