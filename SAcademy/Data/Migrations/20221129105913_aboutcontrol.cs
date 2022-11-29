using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAcademy.Data.Migrations
{
    public partial class aboutcontrol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TitleColor",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TitleSize",
                table: "Abouts",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TitleColor",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "TitleSize",
                table: "Abouts");
        }
    }
}
