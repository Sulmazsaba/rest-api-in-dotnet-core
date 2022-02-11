using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample.Migrations
{
    public partial class d1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("8702b207-d45e-4328-ae36-0216b95e19f5"),
                column: "DateTime",
                value: new DateTimeOffset(new DateTime(2021, 12, 16, 16, 50, 27, 397, DateTimeKind.Unspecified).AddTicks(521), new TimeSpan(0, 3, 30, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("c8e7f681-fa1d-4e96-a897-d19439441e35"),
                column: "DateTime",
                value: new DateTimeOffset(new DateTime(2021, 12, 16, 16, 50, 27, 397, DateTimeKind.Unspecified).AddTicks(574), new TimeSpan(0, 3, 30, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("e4978846-3eec-432d-b54d-db08b75a2a83"),
                column: "DateTime",
                value: new DateTimeOffset(new DateTime(2021, 12, 16, 16, 50, 27, 394, DateTimeKind.Unspecified).AddTicks(2176), new TimeSpan(0, 3, 30, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("8702b207-d45e-4328-ae36-0216b95e19f5"),
                column: "DateTime",
                value: new DateTimeOffset(new DateTime(2020, 10, 23, 20, 19, 12, 14, DateTimeKind.Unspecified).AddTicks(4629), new TimeSpan(0, 3, 30, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("c8e7f681-fa1d-4e96-a897-d19439441e35"),
                column: "DateTime",
                value: new DateTimeOffset(new DateTime(2020, 10, 23, 20, 19, 12, 14, DateTimeKind.Unspecified).AddTicks(5105), new TimeSpan(0, 3, 30, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("e4978846-3eec-432d-b54d-db08b75a2a83"),
                column: "DateTime",
                value: new DateTimeOffset(new DateTime(2020, 10, 23, 20, 19, 12, 0, DateTimeKind.Unspecified).AddTicks(7772), new TimeSpan(0, 3, 30, 0, 0)));
        }
    }
}
