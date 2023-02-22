using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAcademy.Data.Migrations
{
    public partial class sendmailAdress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Message",
                table: "Emails",
                newName: "To");

            migrationBuilder.RenameColumn(
                name: "Mail",
                table: "Emails",
                newName: "Password");

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Emails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailAdress",
                table: "Emails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "Emails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Body",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "EmailAdress",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "From",
                table: "Emails");

            migrationBuilder.RenameColumn(
                name: "To",
                table: "Emails",
                newName: "Message");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Emails",
                newName: "Mail");
        }
    }
}
