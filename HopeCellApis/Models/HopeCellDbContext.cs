using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HopeCellApis.Models;

public partial class HopeCellDbContext : DbContext
{
    public HopeCellDbContext()
    {
    }

    public HopeCellDbContext(DbContextOptions<HopeCellDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<DashboardStat> DashboardStats { get; set; }

    public virtual DbSet<Donation> Donations { get; set; }

    public virtual DbSet<Donor> Donors { get; set; }

    public virtual DbSet<ManageDonation> ManageDonations { get; set; }

    public virtual DbSet<ManageUrgentCase> ManageUrgentCases { get; set; }

    public virtual DbSet<ManageVolunteer> ManageVolunteers { get; set; }

    public virtual DbSet<PaymentResponse> PaymentResponses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-2N3HRK1\\SQLEXPRESS01;Database=HopeCellDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__AuditLog__5E54864891EBF2C9");

            entity.Property(e => e.ActionType).HasMaxLength(50);
            entity.Property(e => e.ChangeDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ChangedBy).HasMaxLength(100);
            entity.Property(e => e.TableName).HasMaxLength(50);
        });

        modelBuilder.Entity<DashboardStat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Dashboar__3214EC07C1BFA869");

            entity.Property(e => e.DonationsReceived).HasColumnType("money");
        });

        modelBuilder.Entity<Donation>(entity =>
        {
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasDefaultValue("Pakistan");
            entity.Property(e => e.DonationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.SpecialAppeal).HasMaxLength(200);
            entity.Property(e => e.TransactionId).HasMaxLength(100);
        });

        modelBuilder.Entity<Donor>(entity =>
        {
            entity.HasKey(e => e.DonorId).HasName("PK__Donors__052E3F781C056F14");

            entity.HasIndex(e => e.BloodType, "IX_Donors_BloodType");

            entity.HasIndex(e => e.Country, "IX_Donors_Country");

            entity.HasIndex(e => e.Email, "IX_Donors_Email");

            entity.HasIndex(e => e.Email, "UQ__Donors__A9D10534F7EBFFD9").IsUnique();

            entity.Property(e => e.BloodType).HasMaxLength(10);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Ethnicity).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(20);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.StateProvince).HasMaxLength(50);
            entity.Property(e => e.StreetAddress).HasMaxLength(100);
            entity.Property(e => e.WillingnessToDonate).HasMaxLength(50);
            entity.Property(e => e.ZipPostalCode).HasMaxLength(20);
        });

        modelBuilder.Entity<ManageDonation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ManageDo__3214EC076B60027E");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Donor).HasMaxLength(100);
        });

        modelBuilder.Entity<ManageUrgentCase>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ManageUr__3214EC07A21ED171");

            entity.Property(e => e.Deadline).HasMaxLength(100);
            entity.Property(e => e.Location).HasMaxLength(200);
            entity.Property(e => e.Patient).HasMaxLength(200);
            entity.Property(e => e.Priority).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(100);
        });

        modelBuilder.Entity<ManageVolunteer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ManageVo__3214EC07A69AA92E");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<PaymentResponse>(entity =>
        {
            entity.HasKey(e => e.ResponseId).HasName("PK__PaymentR__1AAA646CA68DE050");

            entity.Property(e => e.PaymentUrl).HasMaxLength(500);
            entity.Property(e => e.ResponseDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TransactionId).HasMaxLength(100);

            entity.HasOne(d => d.Donation).WithMany(p => p.PaymentResponses)
                .HasForeignKey(d => d.DonationId)
                .HasConstraintName("FK__PaymentRe__Donat__2EDAF651");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07EAF5F0CE");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(200);
            entity.Property(e => e.Role).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
