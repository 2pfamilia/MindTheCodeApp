using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppCore.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUsernameFromUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "username",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "username",
                table: "Users",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "");
        }
    }
}
