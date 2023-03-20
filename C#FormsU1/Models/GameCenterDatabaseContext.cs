using System;
using System.Collections.Generic;
using GameCenter.DAL.ConfigurationHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using GameCenter;

namespace GameCenter.Models;

public partial class GameCenterDatabaseContext : DbContext
{
    public GameCenterDatabaseContext()
    {
    }

    public GameCenterDatabaseContext(DbContextOptions<GameCenterDatabaseContext> options)
        : base(options)
    {
    }

    

    public virtual DbSet<Computer> Computers { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeSchedule> EmployeeSchedules { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = MyConfigurationHelper.GetConfiguration();
            var connectionString = configuration.GetConnectionString("GameCenterDatabaseConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Computer>(entity =>
        {
            entity.HasKey(e => e.ComputerId).HasName("PK__Computer__0DBB971FAF8B99E1");

            entity.ToTable("Computer");

            entity.Property(e => e.ComputerId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("computerId");
            entity.Property(e => e.Cpu)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("cpu");
            entity.Property(e => e.DataStorage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("dataStorage");
            entity.Property(e => e.Gpu)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("gpu");
            entity.Property(e => e.Ram).HasColumnName("ram");
            entity.Property(e => e.Reserved).HasColumnName("reserved");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__B611CB7DBA7F3938");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.Email, "UQ__Customer__AB6E616446111CC3").IsUnique();

            entity.Property(e => e.CustomerId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("customerId");
            entity.Property(e => e.Adress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("adress");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.LoyaltyLevel).HasColumnName("loyaltyLevel");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("phonenumber");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__C134C9C1BC594031");

            entity.ToTable("Employee");

            entity.HasIndex(e => e.Email, "UQ__Employee__AB6E616468C38B68").IsUnique();

            entity.Property(e => e.EmployeeId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("employeeId");
            entity.Property(e => e.Adress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("adress");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.HireDate)
                .HasColumnType("date")
                .HasColumnName("hireDate");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("phonenumber");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("jobTitle");
        });

        modelBuilder.Entity<EmployeeSchedule>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.ShiftDate }).HasName("PK__Employee__A26E26435F997AF0");

            entity.ToTable("EmployeeSchedule");

            entity.Property(e => e.EmployeeId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("employeeId");
            entity.Property(e => e.ShiftDate)
                .HasColumnType("date")
                .HasColumnName("shiftDate");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.ShiftResponsibilities)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("shiftResponsibilities");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeSchedules)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__EmployeeS__emplo__412EB0B6");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.GameId).HasName("PK__Game__DA90B452C233515E");

            entity.ToTable("Game");

            entity.HasIndex(e => new { e.ComputerId, e.Title }, "UQ__Game__33E936A5B509FABA").IsUnique();

            entity.Property(e => e.GameId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("gameId");
            entity.Property(e => e.ComputerId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("computerId");
            entity.Property(e => e.Genre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("genre");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.HasOne(d => d.Computer).WithMany(p => p.Games)
                .HasForeignKey(d => d.ComputerId)
                .HasConstraintName("FK__Game__computerId__4BAC3F29");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.LoginId).HasName("PK__Login__1F5EF4CFD0AC5BAC");

            entity.ToTable("Login");

            entity.Property(e => e.LoginId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("loginId");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("customerId");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("employeeId");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.AccessLevel)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("accessLevel");

            entity.HasOne(d => d.Customer).WithMany(p => p.Logins)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Login__customerI__5AEE82B9");

            entity.HasOne(d => d.Employee).WithMany(p => p.Logins)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Login__employeeI__5BE2A6F2");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => new { e.ComputerId, e.TimeDate }).HasName("PK__Reservat__4BC549F57434BDC9");

            entity.ToTable("Reservation");

            entity.Property(e => e.ComputerId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("computerId");
            entity.Property(e => e.TimeDate)
                .HasColumnType("date")
                .HasColumnName("timeDate");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("customerId");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("employeeId");

            entity.HasOne(d => d.Computer).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.ComputerId)
                .HasConstraintName("FK__Reservati__compu__45F365D3");

            entity.HasOne(d => d.Customer).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Reservati__custo__46E78A0C");

            entity.HasOne(d => d.Employee).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Reservati__emplo__47DBAE45");
        });

        OnModelCreatingPartial(modelBuilder);

    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
