﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebClinic.Data;

#nullable disable

namespace WebClinic.Migrations
{
    [DbContext(typeof(ClinicContext))]
    partial class ClinicContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EmployeSpeciality", b =>
                {
                    b.Property<int>("EmployesId")
                        .HasColumnType("integer");

                    b.Property<int>("SpecialitiesId")
                        .HasColumnType("integer");

                    b.HasKey("EmployesId", "SpecialitiesId");

                    b.HasIndex("SpecialitiesId");

                    b.ToTable("EmployeSpeciality");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("WebClinic.Models.DomainModels.DiseaseHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateRecord")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_record");

                    b.Property<string>("Diagnosis")
                        .HasColumnType("text")
                        .HasColumnName("diagnosis");

                    b.Property<int?>("FkEmployee")
                        .HasColumnType("integer")
                        .HasColumnName("fk_employee");

                    b.Property<int?>("FkPatient")
                        .HasColumnType("integer")
                        .HasColumnName("fk_patient");

                    b.Property<string>("Therapy")
                        .HasColumnType("text")
                        .HasColumnName("therapy");

                    b.HasKey("Id")
                        .HasName("disease_history_pkey");

                    b.HasIndex("FkEmployee");

                    b.HasIndex("FkPatient");

                    b.ToTable("DiseaseHistory", (string)null);
                });

            modelBuilder.Entity("WebClinic.Models.DomainModels.Employe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("birthdate");

                    b.Property<int?>("DeleteFlag")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0)
                        .HasColumnName("delete_flag");

                    b.Property<int?>("FkSpeciality")
                        .HasColumnType("integer")
                        .HasColumnName("fk_speciality");

                    b.Property<int?>("FkUsers")
                        .HasColumnType("integer")
                        .HasColumnName("fk_users");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("gender");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Patronymic")
                        .HasColumnType("text")
                        .HasColumnName("patronymic");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("surname");

                    b.HasKey("Id")
                        .HasName("employes_pkey");

                    b.HasIndex("FkUsers");

                    b.ToTable("Employes", (string)null);
                });

            modelBuilder.Entity("WebClinic.Models.DomainModels.MedicalService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("DeleteFlag")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0)
                        .HasColumnName("delete_flag");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int?>("FkSpeciality")
                        .HasColumnType("integer")
                        .HasColumnName("fk_speciality");

                    b.Property<string>("NameService")
                        .HasColumnType("text")
                        .HasColumnName("name_service");

                    b.Property<int?>("Price")
                        .HasColumnType("integer")
                        .HasColumnName("price");

                    b.HasKey("Id")
                        .HasName("medical_services_pkey");

                    b.HasIndex("FkSpeciality");

                    b.ToTable("MedicalServices", (string)null);
                });

            modelBuilder.Entity("WebClinic.Models.DomainModels.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Birthdate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("birthdate");

                    b.Property<int?>("FkUsers")
                        .HasColumnType("integer")
                        .HasColumnName("fk_users");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("gender");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Patronymic")
                        .HasColumnType("text")
                        .HasColumnName("patronymic");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("surname");

                    b.HasKey("Id")
                        .HasName("patient_pkey");

                    b.HasIndex("FkUsers");

                    b.ToTable("Patient", (string)null);
                });

            modelBuilder.Entity("WebClinic.Models.DomainModels.Phonebook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("FkUsers")
                        .HasColumnType("integer")
                        .HasColumnName("fk_users");

                    b.Property<string>("Phone")
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.HasKey("Id")
                        .HasName("phonebook_pkey");

                    b.HasIndex("FkUsers");

                    b.ToTable("Phonebook", (string)null);
                });

            modelBuilder.Entity("WebClinic.Models.DomainModels.Record", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateRecords")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_records");

                    b.Property<int?>("FkEmployee")
                        .HasColumnType("integer")
                        .HasColumnName("fk_employee");

                    b.Property<int?>("FkPatient")
                        .HasColumnType("integer")
                        .HasColumnName("fk_patient");

                    b.Property<int?>("FkService")
                        .HasColumnType("integer")
                        .HasColumnName("fk_service");

                    b.HasKey("Id")
                        .HasName("records_pkey");

                    b.HasIndex("FkEmployee");

                    b.HasIndex("FkPatient");

                    b.HasIndex("FkService");

                    b.ToTable("Records", (string)null);
                });

            modelBuilder.Entity("WebClinic.Models.DomainModels.Rollback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Changes")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("changes");

                    b.Property<string>("Method_")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("method_");

                    b.Property<string>("TabName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("tab_name");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("time");

                    b.HasKey("Id")
                        .HasName("id");

                    b.ToTable("RollbackTable", (string)null);
                });

            modelBuilder.Entity("WebClinic.Models.DomainModels.Speciality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("DeleteFlag")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0)
                        .HasColumnName("delete_flag");

                    b.Property<string>("NameSpeciality")
                        .HasColumnType("text")
                        .HasColumnName("name_speciality");

                    b.HasKey("Id")
                        .HasName("specialities_pkey");

                    b.ToTable("Specialities", (string)null);
                });

            modelBuilder.Entity("WebClinic.Models.DomainModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("EmployeSpeciality", b =>
                {
                    b.HasOne("WebClinic.Models.DomainModels.Employe", null)
                        .WithMany()
                        .HasForeignKey("EmployesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebClinic.Models.DomainModels.Speciality", null)
                        .WithMany()
                        .HasForeignKey("SpecialitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("WebClinic.Models.DomainModels.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("WebClinic.Models.DomainModels.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebClinic.Models.DomainModels.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("WebClinic.Models.DomainModels.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebClinic.Models.DomainModels.DiseaseHistory", b =>
                {
                    b.HasOne("WebClinic.Models.DomainModels.Employe", "FkEmployeeNavigation")
                        .WithMany("DiseaseHistories")
                        .HasForeignKey("FkEmployee")
                        .HasConstraintName("disease_history_fk_employee_fkey");

                    b.HasOne("WebClinic.Models.DomainModels.Patient", "FkPatientNavigation")
                        .WithMany("DiseaseHistories")
                        .HasForeignKey("FkPatient")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("disease_history_fk_patient_fkey");

                    b.Navigation("FkEmployeeNavigation");

                    b.Navigation("FkPatientNavigation");
                });

            modelBuilder.Entity("WebClinic.Models.DomainModels.Employe", b =>
                {
                    b.HasOne("WebClinic.Models.DomainModels.User", "FkUsersNavigation")
                        .WithMany("Employes")
                        .HasForeignKey("FkUsers")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("employes_fk_users_fkey");

                    b.Navigation("FkUsersNavigation");
                });

            modelBuilder.Entity("WebClinic.Models.DomainModels.MedicalService", b =>
                {
                    b.HasOne("WebClinic.Models.DomainModels.Speciality", "FkSpecialityNavigation")
                        .WithMany("MedicalServices")
                        .HasForeignKey("FkSpeciality")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("medical_services_fk_speciality_fkey");

                    b.Navigation("FkSpecialityNavigation");
                });

            modelBuilder.Entity("WebClinic.Models.DomainModels.Patient", b =>
                {
                    b.HasOne("WebClinic.Models.DomainModels.User", "FkUsersNavigation")
                        .WithMany("Patients")
                        .HasForeignKey("FkUsers")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("patient_fk_users_fkey");

                    b.Navigation("FkUsersNavigation");
                });

            modelBuilder.Entity("WebClinic.Models.DomainModels.Phonebook", b =>
                {
                    b.HasOne("WebClinic.Models.DomainModels.User", "FkUsersNavigation")
                        .WithMany("Phonebooks")
                        .HasForeignKey("FkUsers")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("phonebook_fk_users_fkey");

                    b.Navigation("FkUsersNavigation");
                });

            modelBuilder.Entity("WebClinic.Models.DomainModels.Record", b =>
                {
                    b.HasOne("WebClinic.Models.DomainModels.Employe", "FkEmployeeNavigation")
                        .WithMany("Records")
                        .HasForeignKey("FkEmployee")
                        .HasConstraintName("records_fk_employee_fkey");

                    b.HasOne("WebClinic.Models.DomainModels.Patient", "FkPatientNavigation")
                        .WithMany("Records")
                        .HasForeignKey("FkPatient")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("records_fk_patient_fkey");

                    b.HasOne("WebClinic.Models.DomainModels.MedicalService", "FkServiceNavigation")
                        .WithMany("Records")
                        .HasForeignKey("FkService")
                        .HasConstraintName("records_fk_service_fkey");

                    b.Navigation("FkEmployeeNavigation");

                    b.Navigation("FkPatientNavigation");

                    b.Navigation("FkServiceNavigation");
                });

            modelBuilder.Entity("WebClinic.Models.DomainModels.Employe", b =>
                {
                    b.Navigation("DiseaseHistories");

                    b.Navigation("Records");
                });

            modelBuilder.Entity("WebClinic.Models.DomainModels.MedicalService", b =>
                {
                    b.Navigation("Records");
                });

            modelBuilder.Entity("WebClinic.Models.DomainModels.Patient", b =>
                {
                    b.Navigation("DiseaseHistories");

                    b.Navigation("Records");
                });

            modelBuilder.Entity("WebClinic.Models.DomainModels.Speciality", b =>
                {
                    b.Navigation("MedicalServices");
                });

            modelBuilder.Entity("WebClinic.Models.DomainModels.User", b =>
                {
                    b.Navigation("Employes");

                    b.Navigation("Patients");

                    b.Navigation("Phonebooks");
                });
#pragma warning restore 612, 618
        }
    }
}
