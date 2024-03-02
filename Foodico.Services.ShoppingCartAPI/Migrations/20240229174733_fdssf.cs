using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foodico.Services.ShoppingCartAPI.Migrations
{
    /// <inheritdoc />
    public partial class fdssf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_CartHeaderDto_CartHeaderId",
                table: "CartDetails");

            migrationBuilder.DropTable(
                name: "CartHeaderDto");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_CartHeaders_CartHeaderId",
                table: "CartDetails",
                column: "CartHeaderId",
                principalTable: "CartHeaders",
                principalColumn: "CartHeaderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_CartHeaders_CartHeaderId",
                table: "CartDetails");

            migrationBuilder.CreateTable(
                name: "CartHeaderDto",
                columns: table => new
                {
                    CartHeaderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartTotal = table.Column<double>(type: "float", nullable: false),
                    CouponCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartHeaderDto", x => x.CartHeaderId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_CartHeaderDto_CartHeaderId",
                table: "CartDetails",
                column: "CartHeaderId",
                principalTable: "CartHeaderDto",
                principalColumn: "CartHeaderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
