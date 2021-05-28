using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InstitutoEducativo.Migrations
{
    public partial class migracion2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calificaciones_Empleados_ProfesorId",
                table: "Calificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_MateriaCursadas_Empleados_ProfesorId",
                table: "MateriaCursadas");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Empleados");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Empleados",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Alumnos",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Profesores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    FechaAlta = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(maxLength: 50, nullable: false),
                    Dni = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Telefono = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(maxLength: 100, nullable: true),
                    Legajo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesores", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calificaciones_Profesores_ProfesorId",
                table: "Calificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_MateriaCursadas_Profesores_ProfesorId",
                table: "MateriaCursadas");

            migrationBuilder.DropTable(
                name: "Profesores");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Empleados",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Empleados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Alumnos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "FK_Calificaciones_Empleados_ProfesorId",
                table: "Calificaciones",
                column: "ProfesorId",
                principalTable: "Empleados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MateriaCursadas_Empleados_ProfesorId",
                table: "MateriaCursadas",
                column: "ProfesorId",
                principalTable: "Empleados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
