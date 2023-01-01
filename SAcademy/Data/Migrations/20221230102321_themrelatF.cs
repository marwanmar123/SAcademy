using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAcademy.Data.Migrations
{
    public partial class themrelatF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThematicId",
                table: "Formations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Formations_ThematicId",
                table: "Formations",
                column: "ThematicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Formations_Thematics_ThematicId",
                table: "Formations",
                column: "ThematicId",
                principalTable: "Thematics",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Formations_Thematics_ThematicId",
                table: "Formations");

            migrationBuilder.DropIndex(
                name: "IX_Formations_ThematicId",
                table: "Formations");

            migrationBuilder.DropColumn(
                name: "ThematicId",
                table: "Formations");
        }
    }
}
