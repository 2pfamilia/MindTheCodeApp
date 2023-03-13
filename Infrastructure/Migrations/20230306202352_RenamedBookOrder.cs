using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppCore.Migrations
{
    /// <inheritdoc />
    public partial class RenamedBookOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books_Orders");

            migrationBuilder.CreateTable(
                name: "Order_Details",
                columns: table => new
                {
                    order_details_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<int>(type: "int", nullable: false),
                    book_id = table.Column<int>(type: "int", nullable: false),
                    unit_cost = table.Column<double>(type: "float", nullable: false),
                    total_cost = table.Column<double>(type: "float", nullable: false),
                    count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Details", x => x.order_details_id);
                    table.ForeignKey(
                        name: "FK_Order_Details_Books_book_id",
                        column: x => x.book_id,
                        principalTable: "Books",
                        principalColumn: "book_id");
                    table.ForeignKey(
                        name: "FK_Order_Details_Orders_order_id",
                        column: x => x.order_id,
                        principalTable: "Orders",
                        principalColumn: "order_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_Details_book_id",
                table: "Order_Details",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Details_order_id",
                table: "Order_Details",
                column: "order_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order_Details");

            migrationBuilder.CreateTable(
                name: "Books_Orders",
                columns: table => new
                {
                    book_order_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    book_id = table.Column<int>(type: "int", nullable: false),
                    order_id = table.Column<int>(type: "int", nullable: false),
                    count = table.Column<int>(type: "int", nullable: false),
                    total_cost = table.Column<double>(type: "float", nullable: false),
                    unit_cost = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books_Orders", x => x.book_order_id);
                    table.ForeignKey(
                        name: "FK_Books_Orders_Books_book_id",
                        column: x => x.book_id,
                        principalTable: "Books",
                        principalColumn: "book_id");
                    table.ForeignKey(
                        name: "FK_Books_Orders_Orders_order_id",
                        column: x => x.order_id,
                        principalTable: "Orders",
                        principalColumn: "order_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_Orders_book_id",
                table: "Books_Orders",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Orders_order_id",
                table: "Books_Orders",
                column: "order_id");
        }
    }
}
