using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAcademy.Data.Migrations
{
    public partial class filterdescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionFilter",
                table: "FormationPages");

            migrationBuilder.AddColumn<string>(
                name: "FilterDescription",
                table: "Formations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilterDescription",
                table: "Formations");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionFilter",
                table: "FormationPages",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
