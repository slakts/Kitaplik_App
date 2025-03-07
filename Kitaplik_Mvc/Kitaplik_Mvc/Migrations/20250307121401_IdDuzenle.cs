using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitaplikMvc.Migrations
{
    /// <inheritdoc />
    public partial class IdDuzenle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YazarID",
                table: "Yazarlar",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UyeID",
                table: "Uyeler",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "OduncID",
                table: "Oduncler",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "KitapID",
                table: "Kitaplar",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "KategoriID",
                table: "Kategoriler",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Yazarlar",
                newName: "YazarID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Uyeler",
                newName: "UyeID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Oduncler",
                newName: "OduncID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Kitaplar",
                newName: "KitapID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Kategoriler",
                newName: "KategoriID");
        }
    }
}
