using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAcademy.Data.Migrations
{
    public partial class themeInscritsRelatss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThemeInscritId",
                table: "Thematics",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Thematics_ThemeInscritId",
                table: "Thematics",
                column: "ThemeInscritId");

            migrationBuilder.AddForeignKey(
                name: "FK_Thematics_ThemeInscrits_ThemeInscritId",
                table: "Thematics",
                column: "ThemeInscritId",
                principalTable: "ThemeInscrits",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Thematics_ThemeInscrits_ThemeInscritId",
                table: "Thematics");

            migrationBuilder.DropIndex(
                name: "IX_Thematics_ThemeInscritId",
                table: "Thematics");

            migrationBuilder.DropColumn(
                name: "ThemeInscritId",
                table: "Thematics");
        }
    }
}
