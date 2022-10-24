using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Food_Restaurant.DAL.Migrations
{
    public partial class updatelogic2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetailes");

            migrationBuilder.AddColumn<string>(
                name: "OrderDetails",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrderDetails",
                table: "FinishedOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderDetails",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderDetails",
                table: "FinishedOrders");

            migrationBuilder.CreateTable(
                name: "OrderDetailes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderedFinishedId = table.Column<int>(type: "int", nullable: false),
                    OrdersId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailes", x => x.Id);
                    table.ForeignKey(
                        name: "OrderDetailsandFinishedOrders",
                        column: x => x.OrderedFinishedId,
                        principalTable: "FinishedOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "OrderDetailsandOrders",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailes_OrderedFinishedId",
                table: "OrderDetailes",
                column: "OrderedFinishedId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailes_OrdersId",
                table: "OrderDetailes",
                column: "OrdersId");
        }
    }
}
