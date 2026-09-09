using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardiacPatientMonitoring.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddHasData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Address", "City", "DateOfBirth", "Email", "FirstName", "Gender", "LastName", "Phone", "State" },
                values: new object[] { new Guid("66666666-6666-6666-6666-666666666666"), "100 Test Street", "New York", new DateTime(1995, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "test.patient@example.com", "Test", "Male", "Patient", "555-0106", "NY" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));
        }
    }
}
