using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Food_Restaurant.DAL.Migrations
{
    public partial class updatelogic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ordername",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Ordername",
                table: "FinishedOrders");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "FinishedOrders");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Orders",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "FinishedOrders",
                newName: "TotalPrice");

            migrationBuilder.AlterColumn<string>(
                name: "AddressDetailes",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "OrderDetailes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrdersId = table.Column<int>(type: "int", nullable: false),
                    OrderedFinishedId = table.Column<int>(type: "int", nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetailes");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Orders",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "FinishedOrders",
                newName: "Price");

            migrationBuilder.AlterColumn<string>(
                name: "AddressDetailes",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Ordername",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Ordername",
                table: "FinishedOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "FinishedOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
