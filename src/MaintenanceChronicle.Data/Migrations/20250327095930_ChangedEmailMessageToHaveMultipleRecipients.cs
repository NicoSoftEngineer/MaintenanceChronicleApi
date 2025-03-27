using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaintenanceChronicle.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedEmailMessageToHaveMultipleRecipients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecipientEmail",
                table: "EmailMessage");

            migrationBuilder.DropColumn(
                name: "RecipientName",
                table: "EmailMessage");

            migrationBuilder.AddColumn<ValueTuple<string, string>[]>(
                name: "Recipients",
                table: "EmailMessage",
                type: "record[]",
                maxLength: 254,
                nullable: false,
                defaultValue: new ValueTuple<string, string>[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Recipients",
                table: "EmailMessage");

            migrationBuilder.AddColumn<string>(
                name: "RecipientEmail",
                table: "EmailMessage",
                type: "character varying(254)",
                maxLength: 254,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RecipientName",
                table: "EmailMessage",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}
