using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedbackgroundcolortoworkspace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "backgroundColor",
                table: "Workspaces",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "#FFFFFF");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "backgroundColor",
                table: "Workspaces");
        }
    }
}
