using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VTS.Web.Migrations
{
    public partial class AddedSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "HeadId",
                table: "HolidaysAcception",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Role" },
                values: new object[] { 1, "bordun@gmail.com", "Mykhailo", "Bordun", "Clerk" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Role" },
                values: new object[] { 2, "bordun@gmail.com", "Joe", "Doe", "Employee" });

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
                    { new Guid("2ac054d6-8508-4daf-e348-08d91b0fb7e6"), "PaidDayOffs", "Mind refresh", new DateTime(2021, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 24L, new DateTime(2021, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 22, 14, 43, 12, 622, DateTimeKind.Local).AddTicks(7924), 1 },
                    { new Guid("c6e7762b-ec65-42c3-e349-08d91b0fb7e6"), "UnPaidSickness", "Flu", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 72L, new DateTime(2021, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 22, 14, 43, 12, 622, DateTimeKind.Local).AddTicks(8067), 1 },
                    { new Guid("389da9dc-7bdd-43ad-80ab-08d91c73789f"), "PaidSickness", "Flu", new DateTime(2021, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 48L, new DateTime(2021, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 22, 14, 43, 12, 622, DateTimeKind.Local).AddTicks(8081), 1 },
                    { new Guid("1aafbbf1-0261-4522-8267-d6ae96e9fbcb"), "UnPaidDayOffs", "Mind refresh", new DateTime(2021, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 24L, new DateTime(2021, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 22, 14, 43, 12, 622, DateTimeKind.Local).AddTicks(8091), 2 },
                    { new Guid("7e99ea45-e7ef-464a-a779-75c636d90bfb"), "PaidDayOffs", "Mind refresh", new DateTime(2021, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 72L, new DateTime(2021, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 22, 14, 43, 12, 622, DateTimeKind.Local).AddTicks(8099), 3 }
                });

            migrationBuilder.InsertData(
                table: "UsersVacationInfo",
                columns: new[] { "Id", "BonusPaidDayOffs", "PaidDayOffs", "PaidSickness", "StartedWorking", "UnPaidDayOffs", "UnPaidSickness", "UserId" },
                values: new object[,]
                {
                    { new Guid("709596dd-bd7c-4c7c-8865-7a4f1cf30c20"), 0L, 15L, 15L, new DateTime(2020, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 15L, 15L, 1 },
                    { new Guid("4b619e29-8648-4b3b-938a-261f11d0fa98"), 0L, 15L, 15L, new DateTime(2020, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 15L, 15L, 2 },
                    { new Guid("b7054959-eb96-44f5-b618-a23ee5a05e32"), 0L, 15L, 15L, new DateTime(2021, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 15L, 15L, 3 }
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
                    { new Guid("b8a26335-2787-4b15-a58d-a37615df7db9"), "Have a good time :)", 1, new Guid("2ac054d6-8508-4daf-e348-08d91b0fb7e6"), "Canceled" },
                    { new Guid("ab62af14-f0b9-4f94-b3e4-c5be891c34c0"), "Wish you good health :)", 1, new Guid("c6e7762b-ec65-42c3-e349-08d91b0fb7e6"), "Approved" },
                    { new Guid("f4f57284-e130-4de4-8c8d-1a8cfe9ba952"), "Wish you good health :)", 1, new Guid("389da9dc-7bdd-43ad-80ab-08d91c73789f"), "Approved" },
                    { new Guid("004db92b-d619-4b41-a0c1-8aa858f11511"), "Have a good time :)", 1, new Guid("1aafbbf1-0261-4522-8267-d6ae96e9fbcb"), "Approved" },
                    { new Guid("4da2f382-87c2-4623-b7ce-91a117606b7f"), "Have a good time :)", 1, new Guid("7e99ea45-e7ef-464a-a779-75c636d90bfb"), "Approved" }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "Id", "HeadId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "ManagerId", "UserId" },
                values: new object[] { 1, 1, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clerks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "HolidaysAcception",
                keyColumn: "Id",
                keyValue: new Guid("004db92b-d619-4b41-a0c1-8aa858f11511"));

            migrationBuilder.DeleteData(
                table: "HolidaysAcception",
                keyColumn: "Id",
                keyValue: new Guid("4da2f382-87c2-4623-b7ce-91a117606b7f"));

            migrationBuilder.DeleteData(
                table: "HolidaysAcception",
                keyColumn: "Id",
                keyValue: new Guid("ab62af14-f0b9-4f94-b3e4-c5be891c34c0"));

            migrationBuilder.DeleteData(
                table: "HolidaysAcception",
                keyColumn: "Id",
                keyValue: new Guid("b8a26335-2787-4b15-a58d-a37615df7db9"));

            migrationBuilder.DeleteData(
                table: "HolidaysAcception",
                keyColumn: "Id",
                keyValue: new Guid("f4f57284-e130-4de4-8c8d-1a8cfe9ba952"));

            migrationBuilder.DeleteData(
                table: "UsersVacationInfo",
                keyColumn: "Id",
                keyValue: new Guid("4b619e29-8648-4b3b-938a-261f11d0fa98"));

            migrationBuilder.DeleteData(
                table: "UsersVacationInfo",
                keyColumn: "Id",
                keyValue: new Guid("709596dd-bd7c-4c7c-8865-7a4f1cf30c20"));

            migrationBuilder.DeleteData(
                table: "UsersVacationInfo",
                keyColumn: "Id",
                keyValue: new Guid("b7054959-eb96-44f5-b618-a23ee5a05e32"));

            migrationBuilder.DeleteData(
                table: "Heads",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: new Guid("1aafbbf1-0261-4522-8267-d6ae96e9fbcb"));

            migrationBuilder.DeleteData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: new Guid("2ac054d6-8508-4daf-e348-08d91b0fb7e6"));

            migrationBuilder.DeleteData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: new Guid("389da9dc-7bdd-43ad-80ab-08d91c73789f"));

            migrationBuilder.DeleteData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: new Guid("7e99ea45-e7ef-464a-a779-75c636d90bfb"));

            migrationBuilder.DeleteData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: new Guid("c6e7762b-ec65-42c3-e349-08d91b0fb7e6"));

            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Heads",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<int>(
                name: "HeadId",
                table: "HolidaysAcception",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
