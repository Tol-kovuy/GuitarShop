using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuitarShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddCartEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CartEntityId",
                table: "ProductEntities",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CartEntities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserEntityId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartEntities_UserEntities_UserEntityId",
                        column: x => x.UserEntityId,
                        principalTable: "UserEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductEntities_CartEntityId",
                table: "ProductEntities",
                column: "CartEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CartEntities_UserEntityId",
                table: "CartEntities",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductEntities_CartEntities_CartEntityId",
                table: "ProductEntities",
                column: "CartEntityId",
                principalTable: "CartEntities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductEntities_CartEntities_CartEntityId",
                table: "ProductEntities");

            migrationBuilder.DropTable(
                name: "CartEntities");

            migrationBuilder.DropIndex(
                name: "IX_ProductEntities_CartEntityId",
                table: "ProductEntities");

            migrationBuilder.DropColumn(
                name: "CartEntityId",
                table: "ProductEntities");
        }
    }
}
