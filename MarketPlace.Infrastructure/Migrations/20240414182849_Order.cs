using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPlace.Infrastructure.Migrations
{
    public partial class Order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f5563c5e-d780-4bce-812d-408f2c079ae2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d68ee093-b905-4474-a9bd-679a9d632fb9", "AQAAAAEAACcQAAAAEOh/gZo7PqJmbDzn2WmLyv1iboDvcSOSCrnT+X/4Wd3TfqNqvcyvuL+HcmlEzb0YhA==", "07be6b33-e369-4e3d-b04a-4f7f86ff5d60" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f5563c5e-d780-4bce-812d-408f2c079ae2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "21d61803-da01-42b9-9fe4-0934a9ffd200", "AQAAAAEAACcQAAAAEL6IgrkqiNFYI/Z4UFEmOL958HQ3ylNb7pTiO+Tl7Ml24gjMsNPwk9Wmg2U5dUDrQQ==", "e79c4bd4-13db-4973-b478-a75ebc6b758d" });
        }
    }
}
