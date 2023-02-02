using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAcademy.Data.Migrations
{
    public partial class hedslide : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Background",
                table: "Headers");

            migrationBuilder.AddColumn<string>(
                name: "HeaderId",
                table: "Images",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_HeaderId",
                table: "Images",
                column: "HeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Headers_HeaderId",
                table: "Images",
                column: "HeaderId",
                principalTable: "Headers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Headers_HeaderId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_HeaderId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "HeaderId",
                table: "Images");

            migrationBuilder.AddColumn<byte[]>(
                name: "Background",
                table: "Headers",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
