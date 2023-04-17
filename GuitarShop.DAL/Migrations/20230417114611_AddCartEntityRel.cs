using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuitarShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddCartEntityRel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CartEntities_UserEntityId",
                table: "CartEntities");

            migrationBuilder.CreateIndex(
                name: "IX_CartEntities_UserEntityId",
                table: "CartEntities",
                column: "UserEntityId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CartEntities_UserEntityId",
                table: "CartEntities");

            migrationBuilder.CreateIndex(
                name: "IX_CartEntities_UserEntityId",
                table: "CartEntities",
                column: "UserEntityId");
        }
    }
}
