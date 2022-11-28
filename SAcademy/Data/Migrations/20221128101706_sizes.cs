using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAcademy.Data.Migrations
{
    public partial class sizes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeftSize",
                table: "Headers");

            migrationBuilder.DropColumn(
                name: "TopSize",
                table: "Headers");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Headers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Headers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
