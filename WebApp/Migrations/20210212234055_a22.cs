using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class a22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_AmenityMaps_Hotel_AccommodationID",
                table: "AmenityMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Companies_TourOperatorID",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_FilteredAccommodations_Hotel_AccommodationID",
                table: "FilteredAccommodations");

            migrationBuilder.DropForeignKey(
                name: "FK_Paragraph_Hotel_HotelAccommodationID",
                table: "Paragraph");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Hotel_HotelID",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestResortFilter_Hotel_AccommodationID",
                table: "QuoteRequestResortFilter");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestResorts_Hotel_HotelAccommodationID",
                table: "QuoteRequestResorts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestResorts_Hotel_ResortId",
                table: "QuoteRequestResorts");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Hotel_AccommodationID",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_TripComponents_Hotel_AccommodationID",
                table: "TripComponents");

            migrationBuilder.DropTable(
                name: "Hotel");

            migrationBuilder.DropIndex(
                name: "IX_QuoteRequestResorts_HotelAccommodationID",
                table: "QuoteRequestResorts");

            migrationBuilder.DropIndex(
                name: "IX_Paragraph_HotelAccommodationID",
                table: "Paragraph");

            migrationBuilder.DropColumn(
                name: "HotelAccommodationID",
                table: "QuoteRequestResorts");

            migrationBuilder.DropColumn(
                name: "HotelAccommodationID",
                table: "Paragraph");

            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "QuoteRequestResorts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "Paragraph",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HyperLink",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbNail",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AAPreferredProvider",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address1",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address2",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AirPortID",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bedding",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckIn",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckOut",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FoodRating",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Header",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Inclusions",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuoteRequestID",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "RoomRating",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Stars",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Video",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "BeachRating",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PageId",
                table: "Companies",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TourOperatorID",
                table: "Booking",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CruiseLineId",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "Booking",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<int>(
                name: "CruiseLineId",
                table: "AmenityMaps",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequestResorts_HotelId",
                table: "QuoteRequestResorts",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Paragraph_HotelId",
                table: "Paragraph",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CountryId",
                table: "Companies",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_AirPortID",
                table: "Companies",
                column: "AirPortID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_QuoteRequestID",
                table: "Companies",
                column: "QuoteRequestID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CategoryId",
                table: "Companies",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_PageId",
                table: "Companies",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_CruiseLineId",
                table: "Booking",
                column: "CruiseLineId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_HotelId",
                table: "Booking",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_AmenityMaps_CruiseLineId",
                table: "AmenityMaps",
                column: "CruiseLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationRoomTypes_Companies_AccommodiationID",
                table: "AccommodationRoomTypes",
                column: "AccommodiationID",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AmenityMaps_Companies_AccommodationID",
                table: "AmenityMaps",
                column: "AccommodationID",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AmenityMaps_Companies_CruiseLineId",
                table: "AmenityMaps",
                column: "CruiseLineId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Companies_CruiseLineId",
                table: "Booking",
                column: "CruiseLineId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Companies_HotelId",
                table: "Booking",
                column: "HotelId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Companies_TourOperatorID",
                table: "Booking",
                column: "TourOperatorID",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Countries_CountryId",
                table: "Companies",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_AirPort_AirPortID",
                table: "Companies",
                column: "AirPortID",
                principalTable: "AirPort",
                principalColumn: "AirPortID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_QuoteRequests_QuoteRequestID",
                table: "Companies",
                column: "QuoteRequestID",
                principalTable: "QuoteRequests",
                principalColumn: "QuoteRequestID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Category_CategoryId",
                table: "Companies",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Pages_PageId",
                table: "Companies",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FilteredAccommodations_Companies_AccommodationID",
                table: "FilteredAccommodations",
                column: "AccommodationID",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Paragraph_Companies_HotelId",
                table: "Paragraph",
                column: "HotelId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Companies_HotelID",
                table: "Posts",
                column: "HotelID",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestResortFilter_Companies_AccommodationID",
                table: "QuoteRequestResortFilter",
                column: "AccommodationID",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestResorts_Companies_HotelId",
                table: "QuoteRequestResorts",
                column: "HotelId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestResorts_Companies_ResortId",
                table: "QuoteRequestResorts",
                column: "ResortId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Companies_AccommodationID",
                table: "Quotes",
                column: "AccommodationID",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripComponents_Companies_AccommodationID",
                table: "TripComponents",
                column: "AccommodationID",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationRoomTypes_Companies_AccommodiationID",
                table: "AccommodationRoomTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_AmenityMaps_Companies_AccommodationID",
                table: "AmenityMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_AmenityMaps_Companies_CruiseLineId",
                table: "AmenityMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Companies_CruiseLineId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Companies_HotelId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Companies_TourOperatorID",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Countries_CountryId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_AirPort_AirPortID",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_QuoteRequests_QuoteRequestID",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Category_CategoryId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Pages_PageId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_FilteredAccommodations_Companies_AccommodationID",
                table: "FilteredAccommodations");

            migrationBuilder.DropForeignKey(
                name: "FK_Paragraph_Companies_HotelId",
                table: "Paragraph");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Companies_HotelID",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestResortFilter_Companies_AccommodationID",
                table: "QuoteRequestResortFilter");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestResorts_Companies_HotelId",
                table: "QuoteRequestResorts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestResorts_Companies_ResortId",
                table: "QuoteRequestResorts");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Companies_AccommodationID",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_TripComponents_Companies_AccommodationID",
                table: "TripComponents");

            migrationBuilder.DropIndex(
                name: "IX_QuoteRequestResorts_HotelId",
                table: "QuoteRequestResorts");

            migrationBuilder.DropIndex(
                name: "IX_Paragraph_HotelId",
                table: "Paragraph");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CountryId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_AirPortID",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_QuoteRequestID",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CategoryId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_PageId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Booking_CruiseLineId",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_HotelId",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_AmenityMaps_CruiseLineId",
                table: "AmenityMaps");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "QuoteRequestResorts");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "Paragraph");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "HyperLink",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ThumbNail",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "AAPreferredProvider",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Address1",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Address2",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "AirPortID",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Bedding",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CheckIn",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CheckOut",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "FoodRating",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Header",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Inclusions",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "QuoteRequestID",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "RoomRating",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Stars",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Video",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "BeachRating",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "PageId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CruiseLineId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "CruiseLineId",
                table: "AmenityMaps");

            migrationBuilder.AddColumn<int>(
                name: "HotelAccommodationID",
                table: "QuoteRequestResorts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HotelAccommodationID",
                table: "Paragraph",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TourOperatorID",
                table: "Booking",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "Hotel",
                columns: table => new
                {
                    AccommodationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AAPreferredProvider = table.Column<bool>(type: "bit", nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirPortID = table.Column<int>(type: "int", nullable: true),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bedding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoodRating = table.Column<double>(type: "float", nullable: true),
                    Header = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HyperLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inclusions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuoteRequestID = table.Column<int>(type: "int", nullable: true),
                    RoomRating = table.Column<double>(type: "float", nullable: true),
                    Stars = table.Column<double>(type: "float", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThumbNail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Video = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeachRating = table.Column<double>(type: "float", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    PageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotel", x => x.AccommodationID);
                    table.ForeignKey(
                        name: "FK_Hotel_AirPort_AirPortID",
                        column: x => x.AirPortID,
                        principalTable: "AirPort",
                        principalColumn: "AirPortID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hotel_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hotel_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hotel_QuoteRequests_QuoteRequestID",
                        column: x => x.QuoteRequestID,
                        principalTable: "QuoteRequests",
                        principalColumn: "QuoteRequestID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hotel_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hotel_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequestResorts_HotelAccommodationID",
                table: "QuoteRequestResorts",
                column: "HotelAccommodationID");

            migrationBuilder.CreateIndex(
                name: "IX_Paragraph_HotelAccommodationID",
                table: "Paragraph",
                column: "HotelAccommodationID");

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_AirPortID",
                table: "Hotel",
                column: "AirPortID");

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_CityId",
                table: "Hotel",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_CountryId",
                table: "Hotel",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_QuoteRequestID",
                table: "Hotel",
                column: "QuoteRequestID");

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_CategoryId",
                table: "Hotel",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_PageId",
                table: "Hotel",
                column: "PageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationRoomTypes_Hotel_AccommodiationID",
                table: "AccommodationRoomTypes",
                column: "AccommodiationID",
                principalTable: "Hotel",
                principalColumn: "AccommodationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AmenityMaps_Hotel_AccommodationID",
                table: "AmenityMaps",
                column: "AccommodationID",
                principalTable: "Hotel",
                principalColumn: "AccommodationID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Companies_TourOperatorID",
                table: "Booking",
                column: "TourOperatorID",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FilteredAccommodations_Hotel_AccommodationID",
                table: "FilteredAccommodations",
                column: "AccommodationID",
                principalTable: "Hotel",
                principalColumn: "AccommodationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Paragraph_Hotel_HotelAccommodationID",
                table: "Paragraph",
                column: "HotelAccommodationID",
                principalTable: "Hotel",
                principalColumn: "AccommodationID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Hotel_HotelID",
                table: "Posts",
                column: "HotelID",
                principalTable: "Hotel",
                principalColumn: "AccommodationID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestResortFilter_Hotel_AccommodationID",
                table: "QuoteRequestResortFilter",
                column: "AccommodationID",
                principalTable: "Hotel",
                principalColumn: "AccommodationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestResorts_Hotel_HotelAccommodationID",
                table: "QuoteRequestResorts",
                column: "HotelAccommodationID",
                principalTable: "Hotel",
                principalColumn: "AccommodationID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestResorts_Hotel_ResortId",
                table: "QuoteRequestResorts",
                column: "ResortId",
                principalTable: "Hotel",
                principalColumn: "AccommodationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Hotel_AccommodationID",
                table: "Quotes",
                column: "AccommodationID",
                principalTable: "Hotel",
                principalColumn: "AccommodationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripComponents_Hotel_AccommodationID",
                table: "TripComponents",
                column: "AccommodationID",
                principalTable: "Hotel",
                principalColumn: "AccommodationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
