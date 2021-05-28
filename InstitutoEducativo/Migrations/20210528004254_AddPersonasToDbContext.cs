using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InstitutoEducativo.Migrations
{
    public partial class AddPersonasToDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlumnoMateriaCursadas_Alumnos_AlumnoId",
                table: "AlumnoMateriaCursadas");

            migrationBuilder.DropForeignKey(
                name: "FK_Calificaciones_Profesores_ProfesorId",
                table: "Calificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_MateriaCursadas_Profesores_ProfesorId",
                table: "MateriaCursadas");

            migrationBuilder.DropTable(
                name: "Alumnos");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profesores",
                table: "Profesores");

            migrationBuilder.RenameTable(
                name: "Profesores",
                newName: "Personas");

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Personas",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CarreraId",
                table: "Personas",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumeroMatricula",
                table: "Personas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Personas",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Personas",
                table: "Personas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_CarreraId",
                table: "Personas",
                column: "CarreraId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlumnoMateriaCursadas_Personas_AlumnoId",
                table: "AlumnoMateriaCursadas",
                column: "AlumnoId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Calificaciones_Personas_ProfesorId",
                table: "Calificaciones",
                column: "ProfesorId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MateriaCursadas_Personas_ProfesorId",
                table: "MateriaCursadas",
                column: "ProfesorId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Carreras_CarreraId",
                table: "Personas",
                column: "CarreraId",
                principalTable: "Carreras",
                principalColumn: "CarreraId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlumnoMateriaCursadas_Personas_AlumnoId",
                table: "AlumnoMateriaCursadas");

            migrationBuilder.DropForeignKey(
                name: "FK_Calificaciones_Personas_ProfesorId",
                table: "Calificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_MateriaCursadas_Personas_ProfesorId",
                table: "MateriaCursadas");

            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Carreras_CarreraId",
                table: "Personas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Personas",
                table: "Personas");

            migrationBuilder.DropIndex(
                name: "IX_Personas_CarreraId",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "CarreraId",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "NumeroMatricula",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Personas");

            migrationBuilder.RenameTable(
                name: "Personas",
                newName: "Profesores");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profesores",
                table: "Profesores",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Alumnos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CarreraId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Dni = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Legajo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumeroMatricula = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alumnos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alumnos_Carreras_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "Carreras",
                        principalColumn: "CarreraId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Dni = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Legajo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alumnos_CarreraId",
                table: "Alumnos",
                column: "CarreraId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlumnoMateriaCursadas_Alumnos_AlumnoId",
                table: "AlumnoMateriaCursadas",
                column: "AlumnoId",
                principalTable: "Alumnos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Calificaciones_Profesores_ProfesorId",
                table: "Calificaciones",
                column: "ProfesorId",
                principalTable: "Profesores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MateriaCursadas_Profesores_ProfesorId",
                table: "MateriaCursadas",
                column: "ProfesorId",
                principalTable: "Profesores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
