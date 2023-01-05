using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAcademy.Data.Migrations
{
    public partial class themeInscritsRelat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
