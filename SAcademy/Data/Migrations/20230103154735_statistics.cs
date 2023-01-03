using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAcademy.Data.Migrations
{
    public partial class statistics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Menus");

            migrationBuilder.CreateTable(
                name: "Statistics",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleSize = table.Column<int>(type: "int", nullable: true),
                    Number = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<int>(type: "int", nullable: true),
                    Visible = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Statistics");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Menus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
