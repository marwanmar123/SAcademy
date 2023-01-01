using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAcademy.Data.Migrations
{
    public partial class sh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Slides",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "InscriptionPages",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Slides");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "InscriptionPages");
        }
    }
}
