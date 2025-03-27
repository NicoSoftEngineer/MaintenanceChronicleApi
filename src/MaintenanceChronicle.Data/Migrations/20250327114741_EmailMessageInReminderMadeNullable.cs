using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaintenanceChronicle.Data.Migrations
{
    /// <inheritdoc />
    public partial class EmailMessageInReminderMadeNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceReminder_EmailMessage_EmailMessageId",
                table: "MaintenanceReminder");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmailMessageId",
                table: "MaintenanceReminder",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceReminder_EmailMessage_EmailMessageId",
                table: "MaintenanceReminder",
                column: "EmailMessageId",
                principalTable: "EmailMessage",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceReminder_EmailMessage_EmailMessageId",
                table: "MaintenanceReminder");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmailMessageId",
                table: "MaintenanceReminder",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceReminder_EmailMessage_EmailMessageId",
                table: "MaintenanceReminder",
                column: "EmailMessageId",
                principalTable: "EmailMessage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
