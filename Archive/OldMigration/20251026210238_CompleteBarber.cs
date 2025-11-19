using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CompleteBarber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarberSchedule_Barber_BarberId",
                table: "BarberSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleException_Barber_BarberId",
                table: "ScheduleException");

            migrationBuilder.DropForeignKey(
                name: "FK_Turn_Barber_BarberId",
                table: "Turn");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Barber",
                table: "Barber");

            migrationBuilder.RenameTable(
                name: "Barber",
                newName: "Barbers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Barbers",
                table: "Barbers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BarberSchedule_Barbers_BarberId",
                table: "BarberSchedule",
                column: "BarberId",
                principalTable: "Barbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleException_Barbers_BarberId",
                table: "ScheduleException",
                column: "BarberId",
                principalTable: "Barbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turn_Barbers_BarberId",
                table: "Turn",
                column: "BarberId",
                principalTable: "Barbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarberSchedule_Barbers_BarberId",
                table: "BarberSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleException_Barbers_BarberId",
                table: "ScheduleException");

            migrationBuilder.DropForeignKey(
                name: "FK_Turn_Barbers_BarberId",
                table: "Turn");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Barbers",
                table: "Barbers");

            migrationBuilder.RenameTable(
                name: "Barbers",
                newName: "Barber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Barber",
                table: "Barber",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BarberSchedule_Barber_BarberId",
                table: "BarberSchedule",
                column: "BarberId",
                principalTable: "Barber",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleException_Barber_BarberId",
                table: "ScheduleException",
                column: "BarberId",
                principalTable: "Barber",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turn_Barber_BarberId",
                table: "Turn",
                column: "BarberId",
                principalTable: "Barber",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
