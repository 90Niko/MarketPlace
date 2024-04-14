using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPlace.Infrastructure.Migrations
{
    public partial class Orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f5563c5e-d780-4bce-812d-408f2c079ae2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2a6f2eb5-8bbd-455a-801f-e5f23128e192", "AQAAAAEAACcQAAAAEKFm4PIaeoKwv+Ja3tn/jnM72jYRfK1ZLaWYegZ/hRmxflXwvN/O8GIDMqQ+fp3mlg==", "fd04066a-51ed-4596-b7cd-13728e8dd610" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f5563c5e-d780-4bce-812d-408f2c079ae2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d68ee093-b905-4474-a9bd-679a9d632fb9", "AQAAAAEAACcQAAAAEOh/gZo7PqJmbDzn2WmLyv1iboDvcSOSCrnT+X/4Wd3TfqNqvcyvuL+HcmlEzb0YhA==", "07be6b33-e369-4e3d-b04a-4f7f86ff5d60" });
        }
    }
}
