using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airtrafic.Migrations
{
    /// <inheritdoc />
    public partial class AddedAuthentication13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AirtraficUserId",
                table: "AspNetRoles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_AirtraficUserId",
                table: "AspNetRoles",
                column: "AirtraficUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_AirtraficUserId",
                table: "AspNetRoles",
                column: "AirtraficUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_AirtraficUserId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_AirtraficUserId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "AirtraficUserId",
                table: "AspNetRoles");
        }
    }
}
