using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAcademy.Data.Migrations
{
    public partial class statisticsnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Statistics");

            migrationBuilder.CreateTable(
                name: "StaticNums",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticNums", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaticNums");

            migrationBuilder.AddColumn<int>(
                name: "Description",
                table: "Statistics",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Statistics",
                type: "int",
                nullable: true);
        }
    }
}
