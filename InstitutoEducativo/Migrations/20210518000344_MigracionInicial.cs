using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InstitutoEducativo.Migrations
{
    public partial class MigracionInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carreras",
                columns: table => new
                {
                    CarreraId = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carreras", x => x.CarreraId);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    FechaAlta = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(maxLength: 50, nullable: false),
                    Dni = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Telefono = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(maxLength: 100, nullable: true),
                    Legajo = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Alumnos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    FechaAlta = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(maxLength: 50, nullable: false),
                    Dni = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Telefono = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(maxLength: 100, nullable: true),
                    Legajo = table.Column<string>(nullable: true),
                    Activo = table.Column<bool>(nullable: false),
                    NumeroMatricula = table.Column<int>(nullable: false),
                    CarreraId = table.Column<Guid>(nullable: false)
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
                name: "Materias",
                columns: table => new
                {
                    MateriaId = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 100, nullable: false),
                    CodigoMateria = table.Column<string>(maxLength: 6, nullable: false),
                    Descripcion = table.Column<string>(maxLength: 150, nullable: false),
                    CupoMaximo = table.Column<int>(nullable: false),
                    CarreraId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materias", x => x.MateriaId);
                    table.ForeignKey(
                        name: "FK_Materias_Carreras_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "Carreras",
                        principalColumn: "CarreraId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MateriaCursadas",
                columns: table => new
                {
                    MateriaCursadaId = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 100, nullable: false),
                    Anio = table.Column<int>(nullable: false),
                    Cuatrimestre = table.Column<int>(nullable: false),
                    Activo = table.Column<bool>(nullable: false),
                    MateriaId = table.Column<Guid>(nullable: false),
                    ProfesorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriaCursadas", x => x.MateriaCursadaId);
                    table.ForeignKey(
                        name: "FK_MateriaCursadas_Materias_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materias",
                        principalColumn: "MateriaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MateriaCursadas_Empleados_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Calificaciones",
                columns: table => new
                {
                    CalificacionId = table.Column<Guid>(nullable: false),
                    NotaFinal = table.Column<int>(nullable: false),
                    MateriaId = table.Column<Guid>(nullable: false),
                    ProfesorId = table.Column<Guid>(nullable: false),
                    MateriaCursadaId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificaciones", x => x.CalificacionId);
                    table.ForeignKey(
                        name: "FK_Calificaciones_MateriaCursadas_MateriaCursadaId",
                        column: x => x.MateriaCursadaId,
                        principalTable: "MateriaCursadas",
                        principalColumn: "MateriaCursadaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Calificaciones_Materias_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materias",
                        principalColumn: "MateriaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Calificaciones_Empleados_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlumnoMateriaCursadas",
                columns: table => new
                {
                    AlumnoId = table.Column<Guid>(nullable: false),
                    MateriaCursadaId = table.Column<Guid>(nullable: false),
                    CalificacionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlumnoMateriaCursadas", x => new { x.AlumnoId, x.MateriaCursadaId });
                    table.ForeignKey(
                        name: "FK_AlumnoMateriaCursadas_Alumnos_AlumnoId",
                        column: x => x.AlumnoId,
                        principalTable: "Alumnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlumnoMateriaCursadas_Calificaciones_CalificacionId",
                        column: x => x.CalificacionId,
                        principalTable: "Calificaciones",
                        principalColumn: "CalificacionId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AlumnoMateriaCursadas_MateriaCursadas_MateriaCursadaId",
                        column: x => x.MateriaCursadaId,
                        principalTable: "MateriaCursadas",
                        principalColumn: "MateriaCursadaId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlumnoMateriaCursadas_CalificacionId",
                table: "AlumnoMateriaCursadas",
                column: "CalificacionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AlumnoMateriaCursadas_MateriaCursadaId",
                table: "AlumnoMateriaCursadas",
                column: "MateriaCursadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Alumnos_CarreraId",
                table: "Alumnos",
                column: "CarreraId");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_MateriaCursadaId",
                table: "Calificaciones",
                column: "MateriaCursadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_MateriaId",
                table: "Calificaciones",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_ProfesorId",
                table: "Calificaciones",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_MateriaCursadas_MateriaId",
                table: "MateriaCursadas",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_MateriaCursadas_ProfesorId",
                table: "MateriaCursadas",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Materias_CarreraId",
                table: "Materias",
                column: "CarreraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlumnoMateriaCursadas");

            migrationBuilder.DropTable(
                name: "Alumnos");

            migrationBuilder.DropTable(
                name: "Calificaciones");

            migrationBuilder.DropTable(
                name: "MateriaCursadas");

            migrationBuilder.DropTable(
                name: "Materias");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Carreras");
        }
    }
}
