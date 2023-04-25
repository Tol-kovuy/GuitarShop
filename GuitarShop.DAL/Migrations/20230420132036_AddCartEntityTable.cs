using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuitarShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddCartEntityTable : Migration
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

            migrationBuilder.CreateIndex(
                name: "IX_CartEntities_UserEntityId",
                table: "CartEntities",
                column: "UserEntityId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartEntities");
        }
    }
}
