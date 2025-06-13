using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductsInventory.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateValueInRawMaterialForProduction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuantityAvailable",
                table: "RawMaterialForProductions",
                newName: "QuantityNeeded");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuantityNeeded",
                table: "RawMaterialForProductions",
                newName: "QuantityAvailable");
        }
    }
}
