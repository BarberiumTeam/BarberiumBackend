using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addmigrationException : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleException_Barbers_BarberId",
                table: "ScheduleException");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleException",
                table: "ScheduleException");

            migrationBuilder.RenameTable(
                name: "ScheduleException",
                newName: "ScheduleExceptions");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleException_BarberId",
                table: "ScheduleExceptions",
                newName: "IX_ScheduleExceptions_BarberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleExceptions",
                table: "ScheduleExceptions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleExceptions_Barbers_BarberId",
                table: "ScheduleExceptions",
                column: "BarberId",
                principalTable: "Barbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleExceptions_Barbers_BarberId",
                table: "ScheduleExceptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleExceptions",
                table: "ScheduleExceptions");

            migrationBuilder.RenameTable(
                name: "ScheduleExceptions",
                newName: "ScheduleException");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleExceptions_BarberId",
                table: "ScheduleException",
                newName: "IX_ScheduleException_BarberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleException",
                table: "ScheduleException",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleException_Barbers_BarberId",
                table: "ScheduleException",
                column: "BarberId",
                principalTable: "Barbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
