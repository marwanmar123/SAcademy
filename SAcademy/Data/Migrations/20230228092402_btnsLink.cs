using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAcademy.Data.Migrations
{
    public partial class btnsLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ButtonLink",
                table: "Headers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ButtonThreeLink",
                table: "Headers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ButtonTwoLink",
                table: "Headers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ButtonLink",
                table: "Headers");

            migrationBuilder.DropColumn(
                name: "ButtonThreeLink",
                table: "Headers");

            migrationBuilder.DropColumn(
                name: "ButtonTwoLink",
                table: "Headers");
        }
    }
}
