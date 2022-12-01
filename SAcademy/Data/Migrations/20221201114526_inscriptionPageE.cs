using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAcademy.Data.Migrations
{
    public partial class inscriptionPageE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InscriptionPages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContentOne = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentBgColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentHeight = table.Column<int>(type: "int", nullable: true),
                    ContentTwo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InscriptionPages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InscriptionPages");
        }
    }
}
