using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class AddedRoomType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SKUs_Companies_AccommodiationID",
                table: "SKUs");

            migrationBuilder.DropIndex(
                name: "IX_SKUs_AccommodiationID",
                table: "SKUs");

            migrationBuilder.AlterColumn<int>(
                name: "AccommodiationID",
                table: "SKUs",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AccommodationId",
                table: "SKUs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "SKUs",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "QuoteSKUs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuoteID = table.Column<int>(nullable: false),
                    SKUID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuoteSKUs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuoteSKUs_Quotes_QuoteID",
                        column: x => x.QuoteID,
                        principalTable: "Quotes",
                        principalColumn: "QuoteID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuoteSKUs_SKUs_SKUID",
                        column: x => x.SKUID,
                        principalTable: "SKUs",
                        principalColumn: "SKUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SKUs_AccommodationId",
                table: "SKUs",
                column: "AccommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteSKUs_QuoteID",
                table: "QuoteSKUs",
                column: "QuoteID");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteSKUs_SKUID",
                table: "QuoteSKUs",
                column: "SKUID");

            migrationBuilder.AddForeignKey(
                name: "FK_SKUs_Companies_AccommodationId",
                table: "SKUs",
                column: "AccommodationId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SKUs_Companies_AccommodationId",
                table: "SKUs");

            migrationBuilder.DropTable(
                name: "QuoteSKUs");

            migrationBuilder.DropIndex(
                name: "IX_SKUs_AccommodationId",
                table: "SKUs");

            migrationBuilder.DropColumn(
                name: "AccommodationId",
                table: "SKUs");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "SKUs");

            migrationBuilder.AlterColumn<int>(
                name: "AccommodiationID",
                table: "SKUs",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SKUs_AccommodiationID",
                table: "SKUs",
                column: "AccommodiationID");

            migrationBuilder.AddForeignKey(
                name: "FK_SKUs_Companies_AccommodiationID",
                table: "SKUs",
                column: "AccommodiationID",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
