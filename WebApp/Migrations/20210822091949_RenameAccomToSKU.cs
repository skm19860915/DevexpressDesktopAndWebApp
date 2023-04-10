using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class RenameAccomToSKU : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationRoomTypes_Companies_AccommodiationID",
                table: "AccommodationRoomTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestResorts_AccommodationRoomTypes_ResortRoomTypeID",
                table: "QuoteRequestResorts");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_AccommodationRoomTypes_AccommodationRoomTypeID",
                table: "Quotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccommodationRoomTypes",
                table: "AccommodationRoomTypes");

            migrationBuilder.RenameTable(
                name: "AccommodationRoomTypes",
                newName: "SKUs");

            migrationBuilder.RenameIndex(
                name: "IX_AccommodationRoomTypes_AccommodiationID",
                table: "SKUs",
                newName: "IX_SKUs_AccommodiationID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SKUs",
                table: "SKUs",
                column: "SKUID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestResorts_SKUs_ResortRoomTypeID",
                table: "QuoteRequestResorts",
                column: "ResortRoomTypeID",
                principalTable: "SKUs",
                principalColumn: "SKUID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_SKUs_AccommodationRoomTypeID",
                table: "Quotes",
                column: "AccommodationRoomTypeID",
                principalTable: "SKUs",
                principalColumn: "SKUID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SKUs_Companies_AccommodiationID",
                table: "SKUs",
                column: "AccommodiationID",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestResorts_SKUs_ResortRoomTypeID",
                table: "QuoteRequestResorts");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_SKUs_AccommodationRoomTypeID",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_SKUs_Companies_AccommodiationID",
                table: "SKUs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SKUs",
                table: "SKUs");

            migrationBuilder.RenameTable(
                name: "SKUs",
                newName: "AccommodationRoomTypes");

            migrationBuilder.RenameIndex(
                name: "IX_SKUs_AccommodiationID",
                table: "AccommodationRoomTypes",
                newName: "IX_AccommodationRoomTypes_AccommodiationID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccommodationRoomTypes",
                table: "AccommodationRoomTypes",
                column: "SKUID");

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationRoomTypes_Companies_AccommodiationID",
                table: "AccommodationRoomTypes",
                column: "AccommodiationID",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestResorts_AccommodationRoomTypes_ResortRoomTypeID",
                table: "QuoteRequestResorts",
                column: "ResortRoomTypeID",
                principalTable: "AccommodationRoomTypes",
                principalColumn: "SKUID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_AccommodationRoomTypes_AccommodationRoomTypeID",
                table: "Quotes",
                column: "AccommodationRoomTypeID",
                principalTable: "AccommodationRoomTypes",
                principalColumn: "SKUID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
