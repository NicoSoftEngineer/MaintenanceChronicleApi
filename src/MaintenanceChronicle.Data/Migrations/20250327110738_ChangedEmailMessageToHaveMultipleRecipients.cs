using System.Collections.Generic;
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

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:recordType", "installation,maintenance,repair,un_installation")
                .Annotation("Npgsql:PostgresExtension:hstore", ",,")
                .OldAnnotation("Npgsql:Enum:recordType", "installation,maintenance,repair,un_installation");

            migrationBuilder.AddColumn<Dictionary<string, string>>(
                name: "Recipients",
                table: "EmailMessage",
                type: "hstore",
                maxLength: 254,
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Recipients",
                table: "EmailMessage");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:recordType", "installation,maintenance,repair,un_installation")
                .OldAnnotation("Npgsql:Enum:recordType", "installation,maintenance,repair,un_installation")
                .OldAnnotation("Npgsql:PostgresExtension:hstore", ",,");

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
