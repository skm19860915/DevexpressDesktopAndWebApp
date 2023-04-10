using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class AddTOToLeg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TourOperatorId",
                table: "Leg",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Leg_TourOperatorId",
                table: "Leg",
                column: "TourOperatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leg_Companies_TourOperatorId",
                table: "Leg",
                column: "TourOperatorId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leg_Companies_TourOperatorId",
                table: "Leg");

            migrationBuilder.DropIndex(
                name: "IX_Leg_TourOperatorId",
                table: "Leg");

            migrationBuilder.DropColumn(
                name: "TourOperatorId",
                table: "Leg");
        }
    }
}
