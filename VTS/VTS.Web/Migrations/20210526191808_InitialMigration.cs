using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VTS.Web.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Role = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Heads",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Heads_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Category = table.Column<string>(nullable: false),
                    Hours = table.Column<long>(nullable: false),
                    Start = table.Column<DateTime>(nullable: false),
                    Expires = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    SubmissionTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Holidays_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersVacationInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    PaidDayOffs = table.Column<long>(nullable: false),
                    BonusPaidDayOffs = table.Column<long>(nullable: false),
                    UnPaidDayOffs = table.Column<long>(nullable: false),
                    PaidSickness = table.Column<long>(nullable: false),
                    UnPaidSickness = table.Column<long>(nullable: false),
                    StartedWorking = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersVacationInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersVacationInfo_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clerks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeadId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clerks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clerks_Heads_HeadId",
                        column: x => x.HeadId,
                        principalTable: "Heads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeadId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Managers_Heads_HeadId",
                        column: x => x.HeadId,
                        principalTable: "Heads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HolidaysAcception",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    HeadId = table.Column<int>(nullable: true),
                    HolidayId = table.Column<Guid>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    Description = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidaysAcception", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HolidaysAcception_Heads_HeadId",
                        column: x => x.HeadId,
                        principalTable: "Heads",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HolidaysAcception_Holidays_HolidayId",
                        column: x => x.HolidayId,
                        principalTable: "Holidays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    ManagerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Managers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Managers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Role" },
                values: new object[] { 1, "bordun@gmail.com", "Mykhailo", "Bordun", "Clerk" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Role" },
                values: new object[] { 2, "doe@gmail.com", "Joe", "Doe", "Employee" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Role" },
                values: new object[] { 3, "payne@gmail.com", "Max", "Payne", "Manager" });

            migrationBuilder.InsertData(
                table: "Heads",
                columns: new[] { "Id", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "Holidays",
                columns: new[] { "Id", "Category", "Description", "Expires", "Hours", "Start", "SubmissionTime", "UserId" },
                values: new object[,]
                {
                    { new Guid("2ac054d6-8508-4daf-e348-08d91b0fb7e6"), "PaidDayOffs", "Mind refresh", new DateTime(2021, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 24L, new DateTime(2021, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 26, 22, 18, 7, 864, DateTimeKind.Local).AddTicks(4398), 1 },
                    { new Guid("c6e7762b-ec65-42c3-e349-08d91b0fb7e6"), "UnPaidSickness", "Flu", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 72L, new DateTime(2021, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 26, 22, 18, 7, 864, DateTimeKind.Local).AddTicks(4521), 1 },
                    { new Guid("389da9dc-7bdd-43ad-80ab-08d91c73789f"), "PaidSickness", "Flu", new DateTime(2021, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 48L, new DateTime(2021, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 26, 22, 18, 7, 864, DateTimeKind.Local).AddTicks(4531), 1 },
                    { new Guid("1aafbbf1-0261-4522-8267-d6ae96e9fbcb"), "UnPaidDayOffs", "Mind refresh", new DateTime(2021, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 24L, new DateTime(2021, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 26, 22, 18, 7, 864, DateTimeKind.Local).AddTicks(4539), 2 },
                    { new Guid("7e99ea45-e7ef-464a-a779-75c636d90bfb"), "PaidDayOffs", "Mind refresh", new DateTime(2021, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 72L, new DateTime(2021, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 26, 22, 18, 7, 864, DateTimeKind.Local).AddTicks(4546), 3 }
                });

            migrationBuilder.InsertData(
                table: "UsersVacationInfo",
                columns: new[] { "Id", "BonusPaidDayOffs", "PaidDayOffs", "PaidSickness", "StartedWorking", "UnPaidDayOffs", "UnPaidSickness", "UserId" },
                values: new object[,]
                {
                    { new Guid("8a7b3f3c-ad20-4aea-a4e3-a3f5b2e86f2b"), 0L, 15L, 15L, new DateTime(2020, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 15L, 15L, 1 },
                    { new Guid("c328a406-b7d9-4303-b48b-6765ceda26b1"), 0L, 15L, 15L, new DateTime(2020, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 15L, 15L, 2 },
                    { new Guid("07424251-d705-49ea-8495-76bed32c9bbd"), 0L, 15L, 15L, new DateTime(2021, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 15L, 15L, 3 }
                });

            migrationBuilder.InsertData(
                table: "Clerks",
                columns: new[] { "Id", "HeadId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "HolidaysAcception",
                columns: new[] { "Id", "Description", "HeadId", "HolidayId", "Status" },
                values: new object[,]
                {
                    { new Guid("eeaf1dac-95c7-4e45-88a0-7faa9b038494"), "Have a good time :)", 1, new Guid("2ac054d6-8508-4daf-e348-08d91b0fb7e6"), "Canceled" },
                    { new Guid("4388f760-c14a-48d0-9a7c-1f26dec94e29"), "Wish you good health :)", 1, new Guid("c6e7762b-ec65-42c3-e349-08d91b0fb7e6"), "Approved" },
                    { new Guid("b48bb03f-a299-4bf1-ae23-f8d4e24e025f"), "Wish you good health :)", 1, new Guid("389da9dc-7bdd-43ad-80ab-08d91c73789f"), "Approved" },
                    { new Guid("a808bc99-aa80-4ecd-818d-752972eb72f8"), "Have a good time :)", 1, new Guid("1aafbbf1-0261-4522-8267-d6ae96e9fbcb"), "Approved" },
                    { new Guid("437e4799-db1f-4961-b55e-cd11f82af017"), "Have a good time :)", 1, new Guid("7e99ea45-e7ef-464a-a779-75c636d90bfb"), "Approved" }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "Id", "HeadId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "ManagerId", "UserId" },
                values: new object[] { 1, 1, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Clerks_HeadId",
                table: "Clerks",
                column: "HeadId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ManagerId",
                table: "Employees",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Heads_UserId",
                table: "Heads",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_UserId",
                table: "Holidays",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HolidaysAcception_HeadId",
                table: "HolidaysAcception",
                column: "HeadId");

            migrationBuilder.CreateIndex(
                name: "IX_HolidaysAcception_HolidayId",
                table: "HolidaysAcception",
                column: "HolidayId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Managers_HeadId",
                table: "Managers",
                column: "HeadId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersVacationInfo_UserId",
                table: "UsersVacationInfo",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clerks");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "HolidaysAcception");

            migrationBuilder.DropTable(
                name: "UsersVacationInfo");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DropTable(
                name: "Heads");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
