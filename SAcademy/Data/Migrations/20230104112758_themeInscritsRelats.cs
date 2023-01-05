using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAcademy.Data.Migrations
{
    public partial class themeInscritsRelats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThemeInscrits_Thematics_ThematicId",
                table: "ThemeInscrits");

            migrationBuilder.DropIndex(
                name: "IX_ThemeInscrits_ThematicId",
                table: "ThemeInscrits");

            migrationBuilder.DropColumn(
                name: "ThematicId",
                table: "ThemeInscrits");

            migrationBuilder.AddColumn<string>(
                name: "ThematicName",
                table: "ThemeInscrits",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThematicName",
                table: "ThemeInscrits");

            migrationBuilder.AddColumn<string>(
                name: "ThematicId",
                table: "ThemeInscrits",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThemeInscrits_ThematicId",
                table: "ThemeInscrits",
                column: "ThematicId");

            migrationBuilder.AddForeignKey(
                name: "FK_ThemeInscrits_Thematics_ThematicId",
                table: "ThemeInscrits",
                column: "ThematicId",
                principalTable: "Thematics",
                principalColumn: "Id");
        }
    }
}
