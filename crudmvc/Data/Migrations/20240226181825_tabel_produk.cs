using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace crudmvc.Data.Migrations
{
    /// <inheritdoc />
    public partial class tabel_produk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produk",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Harga = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produk", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produk");
        }
    }
}
