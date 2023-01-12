using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAcademy.Data.Migrations
{
    public partial class themchng : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Certification",
                table: "Thematics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Competences",
                table: "Thematics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Prerequis",
                table: "Thematics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Presentation",
                table: "Thematics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Programme",
                table: "Thematics",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Certification",
                table: "Thematics");

            migrationBuilder.DropColumn(
                name: "Competences",
                table: "Thematics");

            migrationBuilder.DropColumn(
                name: "Prerequis",
                table: "Thematics");

            migrationBuilder.DropColumn(
                name: "Presentation",
                table: "Thematics");

            migrationBuilder.DropColumn(
                name: "Programme",
                table: "Thematics");
        }
    }
}
