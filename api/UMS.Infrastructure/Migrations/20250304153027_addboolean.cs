using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addboolean : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "approved_at",
                table: "Applications",
                newName: "updated_at");

            migrationBuilder.AddColumn<bool>(
                name: "approved",
                table: "Applications",
                type: "tinyint(1)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "approved",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Applications",
                newName: "approved_at");
        }
    }
}
