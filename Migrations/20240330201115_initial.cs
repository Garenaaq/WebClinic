using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebClinic.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rollback_table",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    changes = table.Column<string>(type: "text", nullable: false),
                    method_ = table.Column<string>(type: "text", nullable: false),
                    tab_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "specialities",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_speciality = table.Column<string>(type: "text", nullable: true),
                    delete_flag = table.Column<int>(type: "integer", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("specialities_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    login = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    pass = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "medical_services",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_service = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    price = table.Column<int>(type: "integer", nullable: true),
                    delete_flag = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    fk_speciality = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("medical_services_pkey", x => x.id);
                    table.ForeignKey(
                        name: "medical_services_fk_speciality_fkey",
                        column: x => x.fk_speciality,
                        principalTable: "specialities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fk_users = table.Column<int>(type: "integer", nullable: true),
                    fk_speciality = table.Column<int>(type: "integer", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    surname = table.Column<string>(type: "text", nullable: false),
                    patronymic = table.Column<string>(type: "text", nullable: false),
                    birthdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    gender = table.Column<string>(type: "text", nullable: false),
                    delete_flag = table.Column<int>(type: "integer", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("employes_pkey", x => x.id);
                    table.ForeignKey(
                        name: "employes_fk_speciality_fkey",
                        column: x => x.fk_speciality,
                        principalTable: "specialities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "employes_fk_users_fkey",
                        column: x => x.fk_users,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "patient",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fk_users = table.Column<int>(type: "integer", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    surname = table.Column<string>(type: "text", nullable: false),
                    patronymic = table.Column<string>(type: "text", nullable: false),
                    birthdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    gender = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("patient_pkey", x => x.id);
                    table.ForeignKey(
                        name: "patient_fk_users_fkey",
                        column: x => x.fk_users,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "phonebook",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fk_users = table.Column<int>(type: "integer", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("phonebook_pkey", x => x.id);
                    table.ForeignKey(
                        name: "phonebook_fk_users_fkey",
                        column: x => x.fk_users,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "disease_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date_record = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    fk_patient = table.Column<int>(type: "integer", nullable: true),
                    fk_employee = table.Column<int>(type: "integer", nullable: true),
                    diagnosis = table.Column<string>(type: "text", nullable: true),
                    therapy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("disease_history_pkey", x => x.id);
                    table.ForeignKey(
                        name: "disease_history_fk_employee_fkey",
                        column: x => x.fk_employee,
                        principalTable: "employes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "disease_history_fk_patient_fkey",
                        column: x => x.fk_patient,
                        principalTable: "patient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "records",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date_records = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    fk_patient = table.Column<int>(type: "integer", nullable: true),
                    fk_employee = table.Column<int>(type: "integer", nullable: true),
                    fk_service = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("records_pkey", x => x.id);
                    table.ForeignKey(
                        name: "records_fk_employee_fkey",
                        column: x => x.fk_employee,
                        principalTable: "employes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "records_fk_patient_fkey",
                        column: x => x.fk_patient,
                        principalTable: "patient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "records_fk_service_fkey",
                        column: x => x.fk_service,
                        principalTable: "medical_services",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_disease_history_fk_employee",
                table: "disease_history",
                column: "fk_employee");

            migrationBuilder.CreateIndex(
                name: "IX_disease_history_fk_patient",
                table: "disease_history",
                column: "fk_patient");

            migrationBuilder.CreateIndex(
                name: "IX_employes_fk_speciality",
                table: "employes",
                column: "fk_speciality");

            migrationBuilder.CreateIndex(
                name: "IX_employes_fk_users",
                table: "employes",
                column: "fk_users");

            migrationBuilder.CreateIndex(
                name: "IX_medical_services_fk_speciality",
                table: "medical_services",
                column: "fk_speciality");

            migrationBuilder.CreateIndex(
                name: "IX_patient_fk_users",
                table: "patient",
                column: "fk_users");

            migrationBuilder.CreateIndex(
                name: "IX_phonebook_fk_users",
                table: "phonebook",
                column: "fk_users");

            migrationBuilder.CreateIndex(
                name: "IX_records_fk_employee",
                table: "records",
                column: "fk_employee");

            migrationBuilder.CreateIndex(
                name: "IX_records_fk_patient",
                table: "records",
                column: "fk_patient");

            migrationBuilder.CreateIndex(
                name: "IX_records_fk_service",
                table: "records",
                column: "fk_service");

            migrationBuilder.CreateIndex(
                name: "login_unique",
                table: "users",
                column: "login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "disease_history");

            migrationBuilder.DropTable(
                name: "phonebook");

            migrationBuilder.DropTable(
                name: "records");

            migrationBuilder.DropTable(
                name: "rollback_table");

            migrationBuilder.DropTable(
                name: "employes");

            migrationBuilder.DropTable(
                name: "patient");

            migrationBuilder.DropTable(
                name: "medical_services");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "specialities");
        }
    }
}
