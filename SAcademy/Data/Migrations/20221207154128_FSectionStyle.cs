using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAcademy.Data.Migrations
{
    public partial class FSectionStyle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentSection",
                table: "Formations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleSection",
                table: "Formations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleSectionColor",
                table: "Formations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleSectionSize",
                table: "Formations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentSection",
                table: "Formations");

            migrationBuilder.DropColumn(
                name: "TitleSection",
                table: "Formations");

            migrationBuilder.DropColumn(
                name: "TitleSectionColor",
                table: "Formations");

            migrationBuilder.DropColumn(
                name: "TitleSectionSize",
                table: "Formations");
        }
    }
}
