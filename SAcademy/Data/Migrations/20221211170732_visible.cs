using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAcademy.Data.Migrations
{
    public partial class visible : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Offres",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Contacts",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Abouts",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Offres");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Abouts");
        }
    }
}
