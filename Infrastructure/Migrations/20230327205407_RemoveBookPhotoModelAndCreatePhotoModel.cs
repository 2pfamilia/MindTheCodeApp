using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppCore.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBookPhotoModelAndCreatePhotoModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Books_Photos_photo_id",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Books_Photos");

            migrationBuilder.AddColumn<int>(
                name: "photo_id",
                table: "Books_Authors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    photo_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    file = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date_created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.photo_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_Authors_photo_id",
                table: "Books_Authors",
                column: "photo_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Photos_photo_id",
                table: "Books",
                column: "photo_id",
                principalTable: "Photos",
                principalColumn: "photo_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_Photos_photo_id",
                table: "Books_Authors",
                column: "photo_id",
                principalTable: "Photos",
                principalColumn: "photo_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Photos_photo_id",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_Photos_photo_id",
                table: "Books_Authors");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Books_Authors_photo_id",
                table: "Books_Authors");

            migrationBuilder.DropColumn(
                name: "photo_id",
                table: "Books_Authors");

            migrationBuilder.CreateTable(
                name: "Books_Photos",
                columns: table => new
                {
                    photo_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date_created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    file = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books_Photos", x => x.photo_id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Books_Photos_photo_id",
                table: "Books",
                column: "photo_id",
                principalTable: "Books_Photos",
                principalColumn: "photo_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
