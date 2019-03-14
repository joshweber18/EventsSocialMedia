using Microsoft.EntityFrameworkCore.Migrations;

namespace EventsMedia.Migrations
{
    public partial class Appuserid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdventuresTable_AspNetUsers_UserId",
                table: "AdventuresTable");

            migrationBuilder.DropIndex(
                name: "IX_AdventuresTable_UserId",
                table: "AdventuresTable");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AdventuresTable");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "AdventuresPost",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdventuresPost_UserId",
                table: "AdventuresPost",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdventuresPost_AspNetUsers_UserId",
                table: "AdventuresPost",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdventuresPost_AspNetUsers_UserId",
                table: "AdventuresPost");

            migrationBuilder.DropIndex(
                name: "IX_AdventuresPost_UserId",
                table: "AdventuresPost");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AdventuresPost");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "AdventuresTable",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdventuresTable_UserId",
                table: "AdventuresTable",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdventuresTable_AspNetUsers_UserId",
                table: "AdventuresTable",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
