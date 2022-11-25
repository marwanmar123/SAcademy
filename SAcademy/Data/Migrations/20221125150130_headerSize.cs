using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAcademy.Data.Migrations
{
    public partial class headerSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeftSize",
                table: "Headers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TopSize",
                table: "Headers",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeftSize",
                table: "Headers");

            migrationBuilder.DropColumn(
                name: "TopSize",
                table: "Headers");
        }
    }
}
