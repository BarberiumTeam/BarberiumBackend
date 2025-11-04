using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewException : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExceptionDate",
                table: "ScheduleExceptions",
                newName: "ExceptionStartDate");

            migrationBuilder.AddColumn<DateOnly>(
                name: "ExceptionEndDate",
                table: "ScheduleExceptions",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExceptionEndDate",
                table: "ScheduleExceptions");

            migrationBuilder.RenameColumn(
                name: "ExceptionStartDate",
                table: "ScheduleExceptions",
                newName: "ExceptionDate");
        }
    }
}
