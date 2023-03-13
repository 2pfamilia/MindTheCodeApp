using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppCore.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOnDeleteToCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Books_Authors_author_id",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Books_Category_category_id",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Books_Photos_photo_id",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Details_Books_book_id",
                table: "Order_Details");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Details_Orders_order_id",
                table: "Order_Details");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_user_id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Address_Information_address_information_id",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_User_Roles_role_id",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Books_Authors_author_id",
                table: "Books",
                column: "author_id",
                principalTable: "Books_Authors",
                principalColumn: "author_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Books_Category_category_id",
                table: "Books",
                column: "category_id",
                principalTable: "Books_Category",
                principalColumn: "category_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Books_Photos_photo_id",
                table: "Books",
                column: "photo_id",
                principalTable: "Books_Photos",
                principalColumn: "photo_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Details_Books_book_id",
                table: "Order_Details",
                column: "book_id",
                principalTable: "Books",
                principalColumn: "book_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Details_Orders_order_id",
                table: "Order_Details",
                column: "order_id",
                principalTable: "Orders",
                principalColumn: "order_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_user_id",
                table: "Orders",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Address_Information_address_information_id",
                table: "Users",
                column: "address_information_id",
                principalTable: "Address_Information",
                principalColumn: "address_information_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_User_Roles_role_id",
                table: "Users",
                column: "role_id",
                principalTable: "User_Roles",
                principalColumn: "role_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Books_Authors_author_id",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Books_Category_category_id",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Books_Photos_photo_id",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Details_Books_book_id",
                table: "Order_Details");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Details_Orders_order_id",
                table: "Order_Details");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_user_id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Address_Information_address_information_id",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_User_Roles_role_id",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Books_Authors_author_id",
                table: "Books",
                column: "author_id",
                principalTable: "Books_Authors",
                principalColumn: "author_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Books_Category_category_id",
                table: "Books",
                column: "category_id",
                principalTable: "Books_Category",
                principalColumn: "category_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Books_Photos_photo_id",
                table: "Books",
                column: "photo_id",
                principalTable: "Books_Photos",
                principalColumn: "photo_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Details_Books_book_id",
                table: "Order_Details",
                column: "book_id",
                principalTable: "Books",
                principalColumn: "book_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Details_Orders_order_id",
                table: "Order_Details",
                column: "order_id",
                principalTable: "Orders",
                principalColumn: "order_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_user_id",
                table: "Orders",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Address_Information_address_information_id",
                table: "Users",
                column: "address_information_id",
                principalTable: "Address_Information",
                principalColumn: "address_information_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_User_Roles_role_id",
                table: "Users",
                column: "role_id",
                principalTable: "User_Roles",
                principalColumn: "role_id");
        }
    }
}
