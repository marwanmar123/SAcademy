using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAcademy.Data.Migrations
{
    public partial class fntfmlys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FontFamily",
                table: "Statistics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FontFamily",
                table: "Slides",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FontFamily",
                table: "SectionTheme",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FontFamily",
                table: "Sections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FontFamily",
                table: "Offres",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FontFamily",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FontFamily",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FontFamily",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "FontFamily",
                table: "Slides");

            migrationBuilder.DropColumn(
                name: "FontFamily",
                table: "SectionTheme");

            migrationBuilder.DropColumn(
                name: "FontFamily",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "FontFamily",
                table: "Offres");

            migrationBuilder.DropColumn(
                name: "FontFamily",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "FontFamily",
                table: "Abouts");
        }
    }
}
