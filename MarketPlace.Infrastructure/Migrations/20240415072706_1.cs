using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPlace.Infrastructure.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f5563c5e-d780-4bce-812d-408f2c079ae2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cd23d26f-5abd-4839-839b-74c8527e3b19", "AQAAAAEAACcQAAAAEKMAJdDEqzO/UshxU+0BrUHo2aeUiosQkVMyPyXqRotrbGmeK11oml54rbVKopMzeg==", "628465df-c5cc-4158-954d-41b11aff47a2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f5563c5e-d780-4bce-812d-408f2c079ae2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b32ba0ba-5ade-41bd-8877-8047ebd558d7", "AQAAAAEAACcQAAAAEKO11WBb2qXFB1t1vWmz55aQMVkBHv5QYNhX2onUYXAi1RijvAWuBIiAoDZUb7Drhg==", "4ee9223c-6341-4de5-afb1-e65e558c5fd6" });
        }
    }
}
