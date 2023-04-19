using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuitarShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddTableCartEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "CartEntityProductEntity",
                columns: table => new
                {
                    CartEntitiesId = table.Column<long>(type: "bigint", nullable: false),
                    ProductEntitiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartEntityProductEntity", x => new { x.CartEntitiesId, x.ProductEntitiesId });
                    table.ForeignKey(
                        name: "FK_CartEntityProductEntity_CartEntities_CartEntitiesId",
                        column: x => x.CartEntitiesId,
                        principalTable: "CartEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartEntityProductEntity_ProductEntities_ProductEntitiesId",
                        column: x => x.ProductEntitiesId,
                        principalTable: "ProductEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartEntities_UserEntityId",
                table: "CartEntities",
                column: "UserEntityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartEntityProductEntity_ProductEntitiesId",
                table: "CartEntityProductEntity",
                column: "ProductEntitiesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartEntityProductEntity");

            migrationBuilder.DropTable(
                name: "CartEntities");
        }
    }
}
