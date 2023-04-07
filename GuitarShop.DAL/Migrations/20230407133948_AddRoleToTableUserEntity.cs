using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuitarShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleToTableUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmPassword",
                table: "UserEntities");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "UserEntities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "UserEntities");

            migrationBuilder.AddColumn<string>(
                name: "ConfirmPassword",
                table: "UserEntities",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
