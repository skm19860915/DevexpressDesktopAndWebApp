using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class AddTOToFlight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TourOperatorId",
                table: "Transportations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TourOperatorID",
                table: "Staging_Flights",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_TourOperatorId",
                table: "Transportations",
                column: "TourOperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Staging_Flights_TourOperatorID",
                table: "Staging_Flights",
                column: "TourOperatorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Staging_Flights_Companies_TourOperatorID",
                table: "Staging_Flights",
                column: "TourOperatorID",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transportations_Companies_TourOperatorId",
                table: "Transportations",
                column: "TourOperatorId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staging_Flights_Companies_TourOperatorID",
                table: "Staging_Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Transportations_Companies_TourOperatorId",
                table: "Transportations");

            migrationBuilder.DropIndex(
                name: "IX_Transportations_TourOperatorId",
                table: "Transportations");

            migrationBuilder.DropIndex(
                name: "IX_Staging_Flights_TourOperatorID",
                table: "Staging_Flights");

            migrationBuilder.DropColumn(
                name: "TourOperatorId",
                table: "Transportations");

            migrationBuilder.AlterColumn<int>(
                name: "TourOperatorID",
                table: "Staging_Flights",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
