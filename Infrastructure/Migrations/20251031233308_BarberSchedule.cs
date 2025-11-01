using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BarberSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarberSchedule_Barbers_BarberId",
                table: "BarberSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BarberSchedule",
                table: "BarberSchedule");

            migrationBuilder.RenameTable(
                name: "BarberSchedule",
                newName: "BarbersSchedules");

            migrationBuilder.RenameIndex(
                name: "IX_BarberSchedule_BarberId",
                table: "BarbersSchedules",
                newName: "IX_BarbersSchedules_BarberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BarbersSchedules",
                table: "BarbersSchedules",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BarbersSchedules_Barbers_BarberId",
                table: "BarbersSchedules",
                column: "BarberId",
                principalTable: "Barbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarbersSchedules_Barbers_BarberId",
                table: "BarbersSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BarbersSchedules",
                table: "BarbersSchedules");

            migrationBuilder.RenameTable(
                name: "BarbersSchedules",
                newName: "BarberSchedule");

            migrationBuilder.RenameIndex(
                name: "IX_BarbersSchedules_BarberId",
                table: "BarberSchedule",
                newName: "IX_BarberSchedule_BarberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BarberSchedule",
                table: "BarberSchedule",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BarberSchedule_Barbers_BarberId",
                table: "BarberSchedule",
                column: "BarberId",
                principalTable: "Barbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
