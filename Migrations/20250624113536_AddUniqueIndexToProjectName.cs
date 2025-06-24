using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Outgo_tracker_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexToProjectName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Projects_Name",
                table: "Projects",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Projects_Name",
                table: "Projects");
        }
    }
}
