using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAcademy.Data.Migrations
{
    public partial class Offrstyle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BgColor",
                table: "FTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "FTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OffreFBgColor",
                table: "Formations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OffreFBgColorButton",
                table: "Formations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OffreFColor",
                table: "Formations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OffreFSize",
                table: "Formations",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BgColor",
                table: "FTypes");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "FTypes");

            migrationBuilder.DropColumn(
                name: "OffreFBgColor",
                table: "Formations");

            migrationBuilder.DropColumn(
                name: "OffreFBgColorButton",
                table: "Formations");

            migrationBuilder.DropColumn(
                name: "OffreFColor",
                table: "Formations");

            migrationBuilder.DropColumn(
                name: "OffreFSize",
                table: "Formations");
        }
    }
}
