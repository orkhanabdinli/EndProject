using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserAboutTableUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "UsersAbout",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "UsersAbout");
        }
    }
}
