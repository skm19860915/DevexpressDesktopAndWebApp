using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class a30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_AccommodationRoomTypes_AccommodationRoomTypeID",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Companies_SupplierId",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Companies_TourOperatorID",
                table: "Quotes");

            migrationBuilder.AlterColumn<int>(
                name: "TourOperatorID",
                table: "Quotes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SupplierId",
                table: "Quotes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AccommodationRoomTypeID",
                table: "Quotes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_AccommodationRoomTypes_AccommodationRoomTypeID",
                table: "Quotes",
                column: "AccommodationRoomTypeID",
                principalTable: "AccommodationRoomTypes",
                principalColumn: "AccommodationRoomTypeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Companies_SupplierId",
                table: "Quotes",
                column: "SupplierId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Companies_TourOperatorID",
                table: "Quotes",
                column: "TourOperatorID",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_AccommodationRoomTypes_AccommodationRoomTypeID",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Companies_SupplierId",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Companies_TourOperatorID",
                table: "Quotes");

            migrationBuilder.AlterColumn<int>(
                name: "TourOperatorID",
                table: "Quotes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SupplierId",
                table: "Quotes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccommodationRoomTypeID",
                table: "Quotes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_AccommodationRoomTypes_AccommodationRoomTypeID",
                table: "Quotes",
                column: "AccommodationRoomTypeID",
                principalTable: "AccommodationRoomTypes",
                principalColumn: "AccommodationRoomTypeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Companies_SupplierId",
                table: "Quotes",
                column: "SupplierId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Companies_TourOperatorID",
                table: "Quotes",
                column: "TourOperatorID",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
