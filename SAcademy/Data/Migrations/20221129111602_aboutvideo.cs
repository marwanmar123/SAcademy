using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAcademy.Data.Migrations
{
    public partial class aboutvideo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VideoHeight",
                table: "Abouts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VideoWidth",
                table: "Abouts",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoHeight",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "VideoWidth",
                table: "Abouts");
        }
    }
}
