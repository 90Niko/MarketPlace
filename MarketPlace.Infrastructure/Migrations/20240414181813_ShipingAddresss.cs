using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPlace.Infrastructure.Migrations
{
    public partial class ShipingAddresss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BuyAt",
                table: "ProductBuyers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f5563c5e-d780-4bce-812d-408f2c079ae2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "21d61803-da01-42b9-9fe4-0934a9ffd200", "AQAAAAEAACcQAAAAEL6IgrkqiNFYI/Z4UFEmOL958HQ3ylNb7pTiO+Tl7Ml24gjMsNPwk9Wmg2U5dUDrQQ==", "e79c4bd4-13db-4973-b478-a75ebc6b758d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyAt",
                table: "ProductBuyers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f5563c5e-d780-4bce-812d-408f2c079ae2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e70ea338-0ad2-4307-9ed0-b032bd71a83b", "AQAAAAEAACcQAAAAECjwLHL4i8pZDUHijqMvT9WYuvaTtC3lsPmvCxYV/wNGCvZZoZeJeYMFfGVj2ZP9ng==", "faa90f1b-1bb9-4d92-b31c-faadc527c200" });
        }
    }
}
