using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductsInventory.Api.Migrations
{
    /// <inheritdoc />
    public partial class PatchMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EndProducts_ProductionProcesses_ProductionProcessId",
                table: "EndProducts");

            migrationBuilder.DropTable(
                name: "ProductionOrders");

            migrationBuilder.DropTable(
                name: "ProductionProcessRawMaterials");

            migrationBuilder.DropIndex(
                name: "IX_EndProducts_ProductionProcessId",
                table: "EndProducts");

            migrationBuilder.DropColumn(
                name: "ProductionProcessId",
                table: "EndProducts");

            migrationBuilder.AddColumn<int>(
                name: "EndProductId",
                table: "ProductionProcesses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ProductionProcesses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "EndProducts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "RawMaterialForProductions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RawMaterialId = table.Column<int>(type: "int", nullable: false),
                    QuantityAvailable = table.Column<int>(type: "int", nullable: false),
                    EndProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawMaterialForProductions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RawMaterialForProductions_EndProducts_EndProductId",
                        column: x => x.EndProductId,
                        principalTable: "EndProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RawMaterialForProductions_RawMaterials_RawMaterialId",
                        column: x => x.RawMaterialId,
                        principalTable: "RawMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShippingItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShipmentId = table.Column<int>(type: "int", nullable: false),
                    EndProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingItems_EndProducts_EndProductId",
                        column: x => x.EndProductId,
                        principalTable: "EndProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShippingItems_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductionProcesses_EndProductId",
                table: "ProductionProcesses",
                column: "EndProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterialForProductions_EndProductId",
                table: "RawMaterialForProductions",
                column: "EndProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterialForProductions_RawMaterialId",
                table: "RawMaterialForProductions",
                column: "RawMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingItems_EndProductId",
                table: "ShippingItems",
                column: "EndProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingItems_ShipmentId",
                table: "ShippingItems",
                column: "ShipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionProcesses_EndProducts_EndProductId",
                table: "ProductionProcesses",
                column: "EndProductId",
                principalTable: "EndProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductionProcesses_EndProducts_EndProductId",
                table: "ProductionProcesses");

            migrationBuilder.DropTable(
                name: "RawMaterialForProductions");

            migrationBuilder.DropTable(
                name: "ShippingItems");

            migrationBuilder.DropIndex(
                name: "IX_ProductionProcesses_EndProductId",
                table: "ProductionProcesses");

            migrationBuilder.DropColumn(
                name: "EndProductId",
                table: "ProductionProcesses");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ProductionProcesses");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "EndProducts");

            migrationBuilder.AddColumn<int>(
                name: "ProductionProcessId",
                table: "EndProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductionOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductionProcessId = table.Column<int>(type: "int", nullable: false),
                    ShipmentId = table.Column<int>(type: "int", nullable: false),
                    EndProduction = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    StartProduction = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionOrders_ProductionProcesses_ProductionProcessId",
                        column: x => x.ProductionProcessId,
                        principalTable: "ProductionProcesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionOrders_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductionProcessRawMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductionProcessId = table.Column<int>(type: "int", nullable: false),
                    RawMaterialId = table.Column<int>(type: "int", nullable: false),
                    QuantityUsed = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionProcessRawMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionProcessRawMaterials_ProductionProcesses_ProductionProcessId",
                        column: x => x.ProductionProcessId,
                        principalTable: "ProductionProcesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionProcessRawMaterials_RawMaterials_RawMaterialId",
                        column: x => x.RawMaterialId,
                        principalTable: "RawMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EndProducts_ProductionProcessId",
                table: "EndProducts",
                column: "ProductionProcessId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrders_ProductionProcessId",
                table: "ProductionOrders",
                column: "ProductionProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrders_ShipmentId",
                table: "ProductionOrders",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionProcessRawMaterials_ProductionProcessId",
                table: "ProductionProcessRawMaterials",
                column: "ProductionProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionProcessRawMaterials_RawMaterialId",
                table: "ProductionProcessRawMaterials",
                column: "RawMaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_EndProducts_ProductionProcesses_ProductionProcessId",
                table: "EndProducts",
                column: "ProductionProcessId",
                principalTable: "ProductionProcesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
