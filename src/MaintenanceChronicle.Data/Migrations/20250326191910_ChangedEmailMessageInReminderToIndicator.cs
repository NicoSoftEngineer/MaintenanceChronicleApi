using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaintenanceChronicle.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedEmailMessageInReminderToIndicator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceReminder_EmailMessage_EmailMessageId",
                table: "MaintenanceReminder");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceReminder_EmailMessageId",
                table: "MaintenanceReminder");

            migrationBuilder.DropColumn(
                name: "EmailMessageId",
                table: "MaintenanceReminder");

            migrationBuilder.AddColumn<bool>(
                name: "WasEmailCreated",
                table: "MaintenanceReminder",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WasEmailCreated",
                table: "MaintenanceReminder");

            migrationBuilder.AddColumn<Guid>(
                name: "EmailMessageId",
                table: "MaintenanceReminder",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceReminder_EmailMessageId",
                table: "MaintenanceReminder",
                column: "EmailMessageId");

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
