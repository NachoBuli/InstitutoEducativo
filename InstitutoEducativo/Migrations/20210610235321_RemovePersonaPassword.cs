using Microsoft.EntityFrameworkCore.Migrations;

namespace InstitutoEducativo.Migrations
{
    public partial class RemovePersonaPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Personas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Personas",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
