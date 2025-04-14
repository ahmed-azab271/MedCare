using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class SchePatientRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PatientId",
                table: "DoctorSchedule",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedule_PatientId",
                table: "DoctorSchedule",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSchedule_Patients_PatientId",
                table: "DoctorSchedule",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSchedule_Patients_PatientId",
                table: "DoctorSchedule");

            migrationBuilder.DropIndex(
                name: "IX_DoctorSchedule_PatientId",
                table: "DoctorSchedule");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "DoctorSchedule");
        }
    }
}
