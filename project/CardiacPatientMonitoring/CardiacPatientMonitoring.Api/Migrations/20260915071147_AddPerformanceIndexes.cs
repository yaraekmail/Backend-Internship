using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardiacPatientMonitoring.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddPerformanceIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VitalSigns_PatientId",
                table: "VitalSigns");

            migrationBuilder.DropIndex(
                name: "IX_Medications_PatientId",
                table: "Medications");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments");

            migrationBuilder.CreateIndex(
                name: "IX_VitalSigns_PatientId_RecordedAt",
                table: "VitalSigns",
                columns: new[] { "PatientId", "RecordedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Medications_PatientId_StartDate",
                table: "Medications",
                columns: new[] { "PatientId", "StartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId_AppointmentDate",
                table: "Appointments",
                columns: new[] { "PatientId", "AppointmentDate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VitalSigns_PatientId_RecordedAt",
                table: "VitalSigns");

            migrationBuilder.DropIndex(
                name: "IX_Medications_PatientId_StartDate",
                table: "Medications");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PatientId_AppointmentDate",
                table: "Appointments");

            migrationBuilder.CreateIndex(
                name: "IX_VitalSigns_PatientId",
                table: "VitalSigns",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_PatientId",
                table: "Medications",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");
        }
    }
}
