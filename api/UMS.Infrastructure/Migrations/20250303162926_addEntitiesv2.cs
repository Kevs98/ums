using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addEntitiesv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Zones_role_id",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "ApplicationTypes",
                columns: table => new
                {
                    typeid = table.Column<int>(name: "type_id", type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationTypes", x => x.typeid);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    typeid = table.Column<int>(name: "type_id", type: "int", nullable: true),
                    zoneid = table.Column<int>(name: "zone_id", type: "int", nullable: true),
                    observations = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    approverid = table.Column<int>(name: "approver_id", type: "int", nullable: true),
                    applicantid = table.Column<int>(name: "applicant_id", type: "int", nullable: true),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime(6)", nullable: true),
                    approvedat = table.Column<DateTime>(name: "approved_at", type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.id);
                    table.ForeignKey(
                        name: "FK_Applications_ApplicationTypes_type_id",
                        column: x => x.typeid,
                        principalTable: "ApplicationTypes",
                        principalColumn: "type_id");
                    table.ForeignKey(
                        name: "FK_Applications_Users_applicant_id",
                        column: x => x.applicantid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Applications_Users_approver_id",
                        column: x => x.approverid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Applications_Zones_zone_id",
                        column: x => x.zoneid,
                        principalTable: "Zones",
                        principalColumn: "zone_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Users_zone_id",
                table: "Users",
                column: "zone_id");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_applicant_id",
                table: "Applications",
                column: "applicant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_approver_id",
                table: "Applications",
                column: "approver_id");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_type_id",
                table: "Applications",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_zone_id",
                table: "Applications",
                column: "zone_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Zones_zone_id",
                table: "Users",
                column: "zone_id",
                principalTable: "Zones",
                principalColumn: "zone_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Zones_zone_id",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "ApplicationTypes");

            migrationBuilder.DropIndex(
                name: "IX_Users_zone_id",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Zones_role_id",
                table: "Users",
                column: "role_id",
                principalTable: "Zones",
                principalColumn: "zone_id");
        }
    }
}
