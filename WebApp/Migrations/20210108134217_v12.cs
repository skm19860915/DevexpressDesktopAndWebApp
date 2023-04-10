using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_AppUsers_AgentID",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_AgentID",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "AgentID",
                table: "AppUsers");

            migrationBuilder.AddColumn<double>(
                name: "FlightPrice",
                table: "Quotes",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ResortPrice",
                table: "Quotes",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SubTotal",
                table: "Quotes",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "Quotes",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "QuoteRequestResorts",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ResortId",
                table: "QuoteRequestResorts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResortRoomTypeID",
                table: "QuoteRequestResorts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TourOperatorID",
                table: "QuoteRequestResorts",
                nullable: false,
                defaultValue: 0);


            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequestResorts_ResortId",
                table: "QuoteRequestResorts",
                column: "ResortId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequestResorts_ResortRoomTypeID",
                table: "QuoteRequestResorts",
                column: "ResortRoomTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequestResorts_TourOperatorID",
                table: "QuoteRequestResorts",
                column: "TourOperatorID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestResorts_Hotel_ResortId",
                table: "QuoteRequestResorts",
                column: "ResortId",
                principalTable: "Hotel",
                principalColumn: "AccommodationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestResorts_AccommodationRoomTypes_ResortRoomTypeID",
                table: "QuoteRequestResorts",
                column: "ResortRoomTypeID",
                principalTable: "AccommodationRoomTypes",
                principalColumn: "AccommodationRoomTypeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestResorts_TourOperators_TourOperatorID",
                table: "QuoteRequestResorts",
                column: "TourOperatorID",
                principalTable: "TourOperators",
                principalColumn: "TourOperatorID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestResorts_Hotel_ResortId",
                table: "QuoteRequestResorts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestResorts_AccommodationRoomTypes_ResortRoomTypeID",
                table: "QuoteRequestResorts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestResorts_TourOperators_TourOperatorID",
                table: "QuoteRequestResorts");

            migrationBuilder.DropIndex(
                name: "IX_QuoteRequestResorts_ResortId",
                table: "QuoteRequestResorts");

            migrationBuilder.DropIndex(
                name: "IX_QuoteRequestResorts_ResortRoomTypeID",
                table: "QuoteRequestResorts");

            migrationBuilder.DropIndex(
                name: "IX_QuoteRequestResorts_TourOperatorID",
                table: "QuoteRequestResorts");

            migrationBuilder.DropColumn(
                name: "FlightPrice",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "ResortPrice",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "QuoteRequestResorts");

            migrationBuilder.DropColumn(
                name: "ResortId",
                table: "QuoteRequestResorts");

            migrationBuilder.DropColumn(
                name: "ResortRoomTypeID",
                table: "QuoteRequestResorts");

            migrationBuilder.DropColumn(
                name: "TourOperatorID",
                table: "QuoteRequestResorts");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Quotes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

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

            migrationBuilder.AddColumn<string>(
                name: "AgentID",
                table: "AppUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_AgentID",
                table: "AppUsers",
                column: "AgentID");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_AppUsers_AgentID",
                table: "AppUsers",
                column: "AgentID",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
