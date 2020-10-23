using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Activity = table.Column<string>(maxLength: 100, nullable: true),
                    NumberOfStaff = table.Column<int>(nullable: false),
                    DateTime = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobPositions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    Degree = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobPositions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Activity", "DateTime", "Name", "NumberOfStaff" },
                values: new object[] { new Guid("e4978846-3eec-432d-b54d-db08b75a2a83"), "web developing", new DateTimeOffset(new DateTime(2020, 10, 23, 20, 19, 12, 0, DateTimeKind.Unspecified).AddTicks(7772), new TimeSpan(0, 3, 30, 0, 0)), "Best Company For Ever", 10 });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Activity", "DateTime", "Name", "NumberOfStaff" },
                values: new object[] { new Guid("8702b207-d45e-4328-ae36-0216b95e19f5"), "developing software", new DateTimeOffset(new DateTime(2020, 10, 23, 20, 19, 12, 14, DateTimeKind.Unspecified).AddTicks(4629), new TimeSpan(0, 3, 30, 0, 0)), "Microsoft", 400 });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Activity", "DateTime", "Name", "NumberOfStaff" },
                values: new object[] { new Guid("c8e7f681-fa1d-4e96-a897-d19439441e35"), "Hardware Manufacturing", new DateTimeOffset(new DateTime(2020, 10, 23, 20, 19, 12, 14, DateTimeKind.Unspecified).AddTicks(5105), new TimeSpan(0, 3, 30, 0, 0)), "Huawei", 100 });

            migrationBuilder.InsertData(
                table: "JobPositions",
                columns: new[] { "Id", "CompanyId", "Degree", "Description", "Title" },
                values: new object[] { new Guid("b01bf89a-8125-4518-93e9-6c826b3f4d73"), new Guid("e4978846-3eec-432d-b54d-db08b75a2a83"), 1, ".experienced with entity frame work,.good knowledge of sql server", ".Net Developer" });

            migrationBuilder.InsertData(
                table: "JobPositions",
                columns: new[] { "Id", "CompanyId", "Degree", "Description", "Title" },
                values: new object[] { new Guid("c8edaad1-c78f-4034-a447-130d12a2d59e"), new Guid("8702b207-d45e-4328-ae36-0216b95e19f5"), 2, ".having good knowledge of IIS", "Dev op" });

            migrationBuilder.InsertData(
                table: "JobPositions",
                columns: new[] { "Id", "CompanyId", "Degree", "Description", "Title" },
                values: new object[] { new Guid("ce159fbb-84aa-4d8a-aaf5-ec1fb727a664"), new Guid("8702b207-d45e-4328-ae36-0216b95e19f5"), 0, ".Experienced with CSS3 and Html5", "Front End Developer" });

            migrationBuilder.CreateIndex(
                name: "IX_JobPositions_CompanyId",
                table: "JobPositions",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobPositions");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
