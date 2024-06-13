using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserProfileMediaConfUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfileMedia_AspNetUsers_UserId",
                table: "UserProfileMedia");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfileMedia_AspNetUsers_UserId",
                table: "UserProfileMedia",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfileMedia_AspNetUsers_UserId",
                table: "UserProfileMedia");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfileMedia_AspNetUsers_UserId",
                table: "UserProfileMedia",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
