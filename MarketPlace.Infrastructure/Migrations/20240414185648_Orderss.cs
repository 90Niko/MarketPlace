using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPlace.Infrastructure.Migrations
{
    public partial class Orderss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f5563c5e-d780-4bce-812d-408f2c079ae2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2a6f2eb5-8bbd-455a-801f-e5f23128e192", "AQAAAAEAACcQAAAAEKFm4PIaeoKwv+Ja3tn/jnM72jYRfK1ZLaWYegZ/hRmxflXwvN/O8GIDMqQ+fp3mlg==", "fd04066a-51ed-4596-b7cd-13728e8dd610" });
        }
    }
}
