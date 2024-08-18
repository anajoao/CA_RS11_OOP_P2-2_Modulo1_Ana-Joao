using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSGymClientManagment.Migrations
{
    public partial class _02SeedInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "ClientName", "Email", "IBAN", "NIF", "Phone", "Username" },
                values: new object[,]
                {
                    { 1, "Rui Miguel Azevedo", "rma@mail.com", "PT50345678902345678901234", "111111111", "913456789", "rma1" },
                    { 2, "Ana Dinis Silva", "ads@mail.com", "", "222222222", "917654321", "ads2" }
                });

            migrationBuilder.InsertData(
                table: "GymClasses",
                columns: new[] { "GymClassId", "ClassName", "ClassPrice" },
                values: new object[,]
                {
                    { 1, "Yoga", 10m },
                    { 2, "Pilates", 15m },
                    { 3, "Body Strength", 20m }
                });

            migrationBuilder.InsertData(
                table: "Loyalties",
                columns: new[] { "LoyaltyId", "Discount", "LoyaltyProgram" },
                values: new object[,]
                {
                    { 1, 10m, true },
                    { 2, 0m, false }
                });

            migrationBuilder.InsertData(
                table: "Contracts",
                columns: new[] { "ContractId", "ClientId", "Contract", "EndDate", "LoyaltyId", "MonthlyFee", "StartDate" },
                values: new object[] { 1, 1, 0, null, 1, 50m, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Contracts",
                columns: new[] { "ContractId", "ClientId", "Contract", "EndDate", "LoyaltyId", "MonthlyFee", "StartDate" },
                values: new object[] { 2, 2, 1, new DateTime(2024, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 0m, new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "ContractsGymClasses",
                columns: new[] { "Id", "ContractId", "GymClassId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 1, 3 },
                    { 4, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "ContractId", "PaymentDate", "PaymentType", "PaymentValue" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 45m },
                    { 2, 2, new DateTime(2024, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 15m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ContractsGymClasses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ContractsGymClasses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ContractsGymClasses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ContractsGymClasses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Contracts",
                keyColumn: "ContractId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Contracts",
                keyColumn: "ContractId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GymClasses",
                keyColumn: "GymClassId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GymClasses",
                keyColumn: "GymClassId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GymClasses",
                keyColumn: "GymClassId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Loyalties",
                keyColumn: "LoyaltyId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Loyalties",
                keyColumn: "LoyaltyId",
                keyValue: 2);
        }
    }
}
