using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v25 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestResorts_TourOperators_TourOperatorID",
                table: "QuoteRequestResorts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestTourOperatorFilter_TourOperators_TourOperatorID",
                table: "QuoteRequestTourOperatorFilter");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_TourOperators_TourOperatorID",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_WebSrvLogins_TourOperators_TourOperatorID",
                table: "WebSrvLogins");

            migrationBuilder.DropTable(
                name: "TourOperators");

            migrationBuilder.DropColumn(
                name: "Supplier",
                table: "Booking");

            migrationBuilder.AddColumn<int>(
                name: "TourOperatorID",
                table: "Booking",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_TourOperatorID",
                table: "Booking",
                column: "TourOperatorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Companies_TourOperatorID",
                table: "Booking",
                column: "TourOperatorID",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestResorts_Companies_TourOperatorID",
                table: "QuoteRequestResorts",
                column: "TourOperatorID",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestTourOperatorFilter_Companies_TourOperatorID",
                table: "QuoteRequestTourOperatorFilter",
                column: "TourOperatorID",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Companies_TourOperatorID",
                table: "Quotes",
                column: "TourOperatorID",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WebSrvLogins_Companies_TourOperatorID",
                table: "WebSrvLogins",
                column: "TourOperatorID",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Companies_TourOperatorID",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestResorts_Companies_TourOperatorID",
                table: "QuoteRequestResorts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestTourOperatorFilter_Companies_TourOperatorID",
                table: "QuoteRequestTourOperatorFilter");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Companies_TourOperatorID",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_WebSrvLogins_Companies_TourOperatorID",
                table: "WebSrvLogins");

            migrationBuilder.DropIndex(
                name: "IX_Booking_TourOperatorID",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "TourOperatorID",
                table: "Booking");

            migrationBuilder.AddColumn<string>(
                name: "Supplier",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TourOperators",
                columns: table => new
                {
                    TourOperatorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourOperators", x => x.TourOperatorID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestResorts_TourOperators_TourOperatorID",
                table: "QuoteRequestResorts",
                column: "TourOperatorID",
                principalTable: "TourOperators",
                principalColumn: "TourOperatorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestTourOperatorFilter_TourOperators_TourOperatorID",
                table: "QuoteRequestTourOperatorFilter",
                column: "TourOperatorID",
                principalTable: "TourOperators",
                principalColumn: "TourOperatorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_TourOperators_TourOperatorID",
                table: "Quotes",
                column: "TourOperatorID",
                principalTable: "TourOperators",
                principalColumn: "TourOperatorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WebSrvLogins_TourOperators_TourOperatorID",
                table: "WebSrvLogins",
                column: "TourOperatorID",
                principalTable: "TourOperators",
                principalColumn: "TourOperatorID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
