using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebClinic.Models;

public partial class ClinicContext : DbContext
{
    public ClinicContext()
    {
    }

    public ClinicContext(DbContextOptions<ClinicContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DiseaseHistory> DiseaseHistories { get; set; }

    public virtual DbSet<Employe> Employes { get; set; }

    public virtual DbSet<MedicalService> MedicalServices { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Phonebook> Phonebooks { get; set; }

    public virtual DbSet<Record> Records { get; set; }

    public virtual DbSet<Speciality> Specialities { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DiseaseHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("disease_history_pkey");

            entity.ToTable("disease_history");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateRecord)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_record");
            entity.Property(e => e.Diagnosis).HasColumnName("diagnosis");
            entity.Property(e => e.FkEmployee).HasColumnName("fk_employee");
            entity.Property(e => e.FkPatient).HasColumnName("fk_patient");
            entity.Property(e => e.Therapy).HasColumnName("therapy");

            entity.HasOne(d => d.FkEmployeeNavigation).WithMany(p => p.DiseaseHistories)
                .HasForeignKey(d => d.FkEmployee)
                .HasConstraintName("disease_history_fk_employee_fkey");

            entity.HasOne(d => d.FkPatientNavigation).WithMany(p => p.DiseaseHistories)
                .HasForeignKey(d => d.FkPatient)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("disease_history_fk_patient_fkey");
        });

        modelBuilder.Entity<Employe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("employes_pkey");

            entity.ToTable("employes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.DeleteFlag).HasColumnName("delete_flag");
            entity.Property(e => e.FkSpeciality).HasColumnName("fk_speciality");
            entity.Property(e => e.FkUsers).HasColumnName("fk_users");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Patronymic).HasColumnName("patronymic");
            entity.Property(e => e.Surname).HasColumnName("surname");

            entity.HasOne(d => d.FkSpecialityNavigation).WithMany(p => p.Employes)
                .HasForeignKey(d => d.FkSpeciality)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("employes_fk_speciality_fkey");

            entity.HasOne(d => d.FkUsersNavigation).WithMany(p => p.Employes)
                .HasForeignKey(d => d.FkUsers)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("employes_fk_users_fkey");
        });

        modelBuilder.Entity<MedicalService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("medical_services_pkey");

            entity.ToTable("medical_services");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DeleteFlag).HasColumnName("delete_flag");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.NameService).HasColumnName("name_service");
            entity.Property(e => e.Price).HasColumnName("price");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("patient_pkey");

            entity.ToTable("patient");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.FkUsers).HasColumnName("fk_users");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Patronymic).HasColumnName("patronymic");
            entity.Property(e => e.Surname).HasColumnName("surname");

            entity.HasOne(d => d.FkUsersNavigation).WithMany(p => p.Patients)
                .HasForeignKey(d => d.FkUsers)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("patient_fk_users_fkey");
        });

        modelBuilder.Entity<Phonebook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("phonebook_pkey");

            entity.ToTable("phonebook");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FkUsers).HasColumnName("fk_users");
            entity.Property(e => e.Phone).HasColumnName("phone");

            entity.HasOne(d => d.FkUsersNavigation).WithMany(p => p.Phonebooks)
                .HasForeignKey(d => d.FkUsers)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("phonebook_fk_users_fkey");
        });

        modelBuilder.Entity<Record>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("records_pkey");

            entity.ToTable("records");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateRecords)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_records");
            entity.Property(e => e.FkEmployee).HasColumnName("fk_employee");
            entity.Property(e => e.FkPatient).HasColumnName("fk_patient");
            entity.Property(e => e.FkService).HasColumnName("fk_service");

            entity.HasOne(d => d.FkEmployeeNavigation).WithMany(p => p.Records)
                .HasForeignKey(d => d.FkEmployee)
                .HasConstraintName("records_fk_employee_fkey");

            entity.HasOne(d => d.FkPatientNavigation).WithMany(p => p.Records)
                .HasForeignKey(d => d.FkPatient)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("records_fk_patient_fkey");

            entity.HasOne(d => d.FkServiceNavigation).WithMany(p => p.Records)
                .HasForeignKey(d => d.FkService)
                .HasConstraintName("records_fk_service_fkey");
        });

        modelBuilder.Entity<Speciality>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("specialities_pkey");

            entity.ToTable("specialities");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NameSpeciality).HasColumnName("name_speciality");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Login)
                .HasMaxLength(64)
                .HasColumnName("login");
            entity.Property(e => e.Pass)
                .HasMaxLength(64)
                .HasColumnName("pass");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
