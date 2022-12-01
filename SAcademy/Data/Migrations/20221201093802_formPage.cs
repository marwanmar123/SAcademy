using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAcademy.Data.Migrations
{
    public partial class formPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormationPages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PoneContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PoneContentBgColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PoneContentHeight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PoneDescriptionFilter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PtwoContent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormationPages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormationPages");
        }
    }
}
