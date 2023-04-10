using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Opportunities_Stages_StageID",
                table: "Opportunities");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestResorts_Hotel_AccommodationID",
                table: "QuoteRequestResorts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestResorts_AccommodationRoomTypes_AccommodationRoomTypeID",
                table: "QuoteRequestResorts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestResorts_AppUsers_BookedById",
                table: "QuoteRequestResorts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestResorts_TourOperators_TourOperatorID",
                table: "QuoteRequestResorts");

            migrationBuilder.DropIndex(
                name: "IX_QuoteRequestResorts_AccommodationID",
                table: "QuoteRequestResorts");

            migrationBuilder.DropIndex(
                name: "IX_QuoteRequestResorts_AccommodationRoomTypeID",
                table: "QuoteRequestResorts");

            migrationBuilder.DropIndex(
                name: "IX_QuoteRequestResorts_BookedById",
                table: "QuoteRequestResorts");

            migrationBuilder.DropIndex(
                name: "IX_QuoteRequestResorts_TourOperatorID",
                table: "QuoteRequestResorts");

            migrationBuilder.DropIndex(
                name: "IX_Opportunities_StageID",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "AccommodationID",
                table: "QuoteRequestResorts");

            migrationBuilder.DropColumn(
                name: "AccommodationRoomTypeID",
                table: "QuoteRequestResorts");

            migrationBuilder.DropColumn(
                name: "BookedById",
                table: "QuoteRequestResorts");

            migrationBuilder.DropColumn(
                name: "BookedOn",
                table: "QuoteRequestResorts");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "QuoteRequestResorts");

            migrationBuilder.DropColumn(
                name: "TourOperatorID",
                table: "QuoteRequestResorts");

            migrationBuilder.DropColumn(
                name: "StageID",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Opportunities");

            migrationBuilder.AddColumn<int>(
                name: "AccommodationID",
                table: "Quotes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccommodationRoomTypeID",
                table: "Quotes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Adjustment",
                table: "Quotes",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "BookedById",
                table: "Quotes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BookedOn",
                table: "Quotes",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Quotes",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "QuoteGroupID",
                table: "Quotes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Quotes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TourOperatorID",
                table: "Quotes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuoteGroupId",
                table: "QuoteRequestTickets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HotelAccommodationID",
                table: "QuoteRequestResorts",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Opportunities",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stage",
                table: "Opportunities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TripStatus",
                table: "Opportunities",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QuoteGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuoteRequestID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuoteGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuoteGroups_QuoteRequests_QuoteRequestID",
                        column: x => x.QuoteRequestID,
                        principalTable: "QuoteRequests",
                        principalColumn: "QuoteRequestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_AccommodationID",
                table: "Quotes",
                column: "AccommodationID");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_AccommodationRoomTypeID",
                table: "Quotes",
                column: "AccommodationRoomTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_BookedById",
                table: "Quotes",
                column: "BookedById");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_QuoteGroupID",
                table: "Quotes",
                column: "QuoteGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_TourOperatorID",
                table: "Quotes",
                column: "TourOperatorID");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequestTickets_QuoteGroupId",
                table: "QuoteRequestTickets",
                column: "QuoteGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequestResorts_HotelAccommodationID",
                table: "QuoteRequestResorts",
                column: "HotelAccommodationID");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteGroups_QuoteRequestID",
                table: "QuoteGroups",
                column: "QuoteRequestID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestResorts_Hotel_HotelAccommodationID",
                table: "QuoteRequestResorts",
                column: "HotelAccommodationID",
                principalTable: "Hotel",
                principalColumn: "AccommodationID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestTickets_QuoteGroups_QuoteGroupId",
                table: "QuoteRequestTickets",
                column: "QuoteGroupId",
                principalTable: "QuoteGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Hotel_AccommodationID",
                table: "Quotes",
                column: "AccommodationID",
                principalTable: "Hotel",
                principalColumn: "AccommodationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_AccommodationRoomTypes_AccommodationRoomTypeID",
                table: "Quotes",
                column: "AccommodationRoomTypeID",
                principalTable: "AccommodationRoomTypes",
                principalColumn: "AccommodationRoomTypeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_AppUsers_BookedById",
                table: "Quotes",
                column: "BookedById",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_QuoteGroups_QuoteGroupID",
                table: "Quotes",
                column: "QuoteGroupID",
                principalTable: "QuoteGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_TourOperators_TourOperatorID",
                table: "Quotes",
                column: "TourOperatorID",
                principalTable: "TourOperators",
                principalColumn: "TourOperatorID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestResorts_Hotel_HotelAccommodationID",
                table: "QuoteRequestResorts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestTickets_QuoteGroups_QuoteGroupId",
                table: "QuoteRequestTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Hotel_AccommodationID",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_AccommodationRoomTypes_AccommodationRoomTypeID",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_AppUsers_BookedById",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_QuoteGroups_QuoteGroupID",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_TourOperators_TourOperatorID",
                table: "Quotes");

            migrationBuilder.DropTable(
                name: "QuoteGroups");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_AccommodationID",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_AccommodationRoomTypeID",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_BookedById",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_QuoteGroupID",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_TourOperatorID",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_QuoteRequestTickets_QuoteGroupId",
                table: "QuoteRequestTickets");

            migrationBuilder.DropIndex(
                name: "IX_QuoteRequestResorts_HotelAccommodationID",
                table: "QuoteRequestResorts");

            migrationBuilder.DropColumn(
                name: "AccommodationID",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "AccommodationRoomTypeID",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "Adjustment",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "BookedById",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "BookedOn",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "QuoteGroupID",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "TourOperatorID",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "QuoteGroupId",
                table: "QuoteRequestTickets");

            migrationBuilder.DropColumn(
                name: "HotelAccommodationID",
                table: "QuoteRequestResorts");

            migrationBuilder.DropColumn(
                name: "Stage",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "TripStatus",
                table: "Opportunities");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Quotes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccommodationID",
                table: "QuoteRequestResorts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccommodationRoomTypeID",
                table: "QuoteRequestResorts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BookedById",
                table: "QuoteRequestResorts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BookedOn",
                table: "QuoteRequestResorts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "QuoteRequestResorts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "TourOperatorID",
                table: "QuoteRequestResorts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Opportunities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "StageID",
                table: "Opportunities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Opportunities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequestResorts_AccommodationID",
                table: "QuoteRequestResorts",
                column: "AccommodationID");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequestResorts_AccommodationRoomTypeID",
                table: "QuoteRequestResorts",
                column: "AccommodationRoomTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequestResorts_BookedById",
                table: "QuoteRequestResorts",
                column: "BookedById");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequestResorts_TourOperatorID",
                table: "QuoteRequestResorts",
                column: "TourOperatorID");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_StageID",
                table: "Opportunities",
                column: "StageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunities_Stages_StageID",
                table: "Opportunities",
                column: "StageID",
                principalTable: "Stages",
                principalColumn: "StageID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestResorts_Hotel_AccommodationID",
                table: "QuoteRequestResorts",
                column: "AccommodationID",
                principalTable: "Hotel",
                principalColumn: "AccommodationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestResorts_AccommodationRoomTypes_AccommodationRoomTypeID",
                table: "QuoteRequestResorts",
                column: "AccommodationRoomTypeID",
                principalTable: "AccommodationRoomTypes",
                principalColumn: "AccommodationRoomTypeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestResorts_AppUsers_BookedById",
                table: "QuoteRequestResorts",
                column: "BookedById",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestResorts_TourOperators_TourOperatorID",
                table: "QuoteRequestResorts",
                column: "TourOperatorID",
                principalTable: "TourOperators",
                principalColumn: "TourOperatorID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
