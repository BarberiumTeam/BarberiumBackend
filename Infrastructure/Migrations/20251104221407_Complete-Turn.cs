using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CompleteTurn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Turn_TurnId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Turn_Barbers_BarberId",
                table: "Turn");

            migrationBuilder.DropForeignKey(
                name: "FK_Turn_Clients_ClientId",
                table: "Turn");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Turn",
                table: "Turn");

            migrationBuilder.RenameTable(
                name: "Turn",
                newName: "Turns");

            migrationBuilder.RenameIndex(
                name: "IX_Turn_ClientId",
                table: "Turns",
                newName: "IX_Turns_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Turn_BarberId",
                table: "Turns",
                newName: "IX_Turns_BarberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Turns",
                table: "Turns",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Turns_TurnId",
                table: "Payment",
                column: "TurnId",
                principalTable: "Turns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turns_Barbers_BarberId",
                table: "Turns",
                column: "BarberId",
                principalTable: "Barbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turns_Clients_ClientId",
                table: "Turns",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Turns_TurnId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Turns_Barbers_BarberId",
                table: "Turns");

            migrationBuilder.DropForeignKey(
                name: "FK_Turns_Clients_ClientId",
                table: "Turns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Turns",
                table: "Turns");

            migrationBuilder.RenameTable(
                name: "Turns",
                newName: "Turn");

            migrationBuilder.RenameIndex(
                name: "IX_Turns_ClientId",
                table: "Turn",
                newName: "IX_Turn_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Turns_BarberId",
                table: "Turn",
                newName: "IX_Turn_BarberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Turn",
                table: "Turn",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Turn_TurnId",
                table: "Payment",
                column: "TurnId",
                principalTable: "Turn",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turn_Barbers_BarberId",
                table: "Turn",
                column: "BarberId",
                principalTable: "Barbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turn_Clients_ClientId",
                table: "Turn",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
