using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPlace.Infrastructure.Migrations
{
    public partial class UPDATE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Check deleted");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f5563c5e-d780-4bce-812d-408f2c079ae2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aef55e92-abc6-44ad-be60-9084b67b44e2", "AQAAAAEAACcQAAAAEG9shEAnLa9hRLJT8QsO7uq6IEsqe+dFBz3s7IhUcqc/tjzOSqhxjsDP/5Y60KkuSg==", "8921bdd8-5d8a-4c1c-ab3a-d764b3b5f759" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f5563c5e-d780-4bce-812d-408f2c079ae2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "926ee6ba-4628-4e63-a176-907b9e24b152", "AQAAAAEAACcQAAAAENbSKnnMDB66mNkmonp+GWgl1QnJUEAlLJJvgQnvtLFlr0zEhhEWVs8cNj5h0nxFuA==", "2cd7752d-148d-497c-a442-72db458ca6f9" });
        }
    }
}
