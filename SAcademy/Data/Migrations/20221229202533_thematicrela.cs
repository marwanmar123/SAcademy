using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAcademy.Data.Migrations
{
    public partial class thematicrela : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TypeId",
                table: "Thematics",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Thematics_TypeId",
                table: "Thematics",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Thematics_FTypes_TypeId",
                table: "Thematics",
                column: "TypeId",
                principalTable: "FTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Thematics_FTypes_TypeId",
                table: "Thematics");

            migrationBuilder.DropIndex(
                name: "IX_Thematics_TypeId",
                table: "Thematics");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Thematics");
        }
    }
}
