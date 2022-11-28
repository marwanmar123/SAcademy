using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAcademy.Data.Migrations
{
    public partial class BVS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BVLeftSize",
                table: "Headers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BVTopSize",
                table: "Headers",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BVLeftSize",
                table: "Headers");

            migrationBuilder.DropColumn(
                name: "BVTopSize",
                table: "Headers");
        }
    }
}
