using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAcademy.Data.Migrations
{
    public partial class slideTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SlideTwoId",
                table: "Images",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SlideTwos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleSize = table.Column<int>(type: "int", nullable: true),
                    FontFamily = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Visible = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlideTwos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_SlideTwoId",
                table: "Images",
                column: "SlideTwoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_SlideTwos_SlideTwoId",
                table: "Images",
                column: "SlideTwoId",
                principalTable: "SlideTwos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_SlideTwos_SlideTwoId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "SlideTwos");

            migrationBuilder.DropIndex(
                name: "IX_Images_SlideTwoId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "SlideTwoId",
                table: "Images");
        }
    }
}
