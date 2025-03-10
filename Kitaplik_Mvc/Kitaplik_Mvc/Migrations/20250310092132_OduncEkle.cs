using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitaplikMvc.Migrations
{
    /// <inheritdoc />
    public partial class OduncEkle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Oduncler_UyeID",
                table: "Oduncler",
                column: "UyeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Oduncler_Uyeler_UyeID",
                table: "Oduncler",
                column: "UyeID",
                principalTable: "Uyeler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Oduncler_Uyeler_UyeID",
                table: "Oduncler");

            migrationBuilder.DropIndex(
                name: "IX_Oduncler_UyeID",
                table: "Oduncler");
        }
    }
}
