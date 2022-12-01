using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAcademy.Data.Migrations
{
    public partial class inscriptionPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PoneContent",
                table: "FormationPages");

            migrationBuilder.RenameColumn(
                name: "PtwoContent",
                table: "FormationPages",
                newName: "DescriptionFilter");

            migrationBuilder.RenameColumn(
                name: "PoneDescriptionFilter",
                table: "FormationPages",
                newName: "ContentBgColor");

            migrationBuilder.RenameColumn(
                name: "PoneContentHeight",
                table: "FormationPages",
                newName: "ContentHeight");

            migrationBuilder.RenameColumn(
                name: "PoneContentBgColor",
                table: "FormationPages",
                newName: "Content");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DescriptionFilter",
                table: "FormationPages",
                newName: "PtwoContent");

            migrationBuilder.RenameColumn(
                name: "ContentHeight",
                table: "FormationPages",
                newName: "PoneContentHeight");

            migrationBuilder.RenameColumn(
                name: "ContentBgColor",
                table: "FormationPages",
                newName: "PoneDescriptionFilter");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "FormationPages",
                newName: "PoneContentBgColor");

            migrationBuilder.AddColumn<string>(
                name: "PoneContent",
                table: "FormationPages",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
