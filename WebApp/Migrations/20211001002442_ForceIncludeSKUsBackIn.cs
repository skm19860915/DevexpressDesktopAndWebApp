using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class ForceIncludeSKUsBackIn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IncludedSKUs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilteredAccommodationId = table.Column<int>(nullable: false),
                    SKUId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncludedSKUs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncludedSKUs_FilteredAccommodations_FilteredAccommodationId",
                        column: x => x.FilteredAccommodationId,
                        principalTable: "FilteredAccommodations",
                        principalColumn: "FilterAcommodationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IncludedSKUs_SKUs_SKUId",
                        column: x => x.SKUId,
                        principalTable: "SKUs",
                        principalColumn: "SKUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IncludedSKUs_FilteredAccommodationId",
                table: "IncludedSKUs",
                column: "FilteredAccommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_IncludedSKUs_SKUId",
                table: "IncludedSKUs",
                column: "SKUId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
