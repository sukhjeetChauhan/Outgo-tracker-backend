using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Outgo_tracker_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleToProjectUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "ProjectUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "ProjectUsers");
        }
    }
}
