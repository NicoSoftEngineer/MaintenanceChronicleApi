using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace MaintenanceChronicle.Data.Migrations
{
    /// <inheritdoc />
    public partial class MaintenanceRemindersAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaintenanceReminder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Date = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    MachineId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmailMessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false),
                    DeletedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceReminder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintenanceReminder_EmailMessage_EmailMessageId",
                        column: x => x.EmailMessageId,
                        principalTable: "EmailMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaintenanceReminder_Machine_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaintenanceReminder_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceReminder_EmailMessageId",
                table: "MaintenanceReminder",
                column: "EmailMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceReminder_MachineId",
                table: "MaintenanceReminder",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceReminder_TenantId",
                table: "MaintenanceReminder",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaintenanceReminder");
        }
    }
}
