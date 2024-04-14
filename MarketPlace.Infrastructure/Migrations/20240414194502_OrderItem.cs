using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPlace.Infrastructure.Migrations
{
    public partial class OrderItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_OrderId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrderId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShipingAddress",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f5563c5e-d780-4bce-812d-408f2c079ae2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b32ba0ba-5ade-41bd-8877-8047ebd558d7", "AQAAAAEAACcQAAAAEKO11WBb2qXFB1t1vWmz55aQMVkBHv5QYNhX2onUYXAi1RijvAWuBIiAoDZUb7Drhg==", "4ee9223c-6341-4de5-afb1-e65e558c5fd6" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShipingAddress",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f5563c5e-d780-4bce-812d-408f2c079ae2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "24576fbd-f338-4c32-a115-d9de919f63df", "AQAAAAEAACcQAAAAECxPXNUsXTPsXqA3U3n7R2ILK27s4ljUphP3D9OlKsJi/UQy4rM4OxwINlaVQTLCZg==", "d7073da8-7cb3-48c6-8061-ddd7981addbf" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderId",
                table: "Products",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_OrderId",
                table: "Products",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
