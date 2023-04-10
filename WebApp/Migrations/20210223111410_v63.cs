using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v63 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Filters_Quotes_QuoteID",
                table: "Filters");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteToResultsMappers_Quotes_QuoteID",
                table: "QuoteToResultsMappers");

            migrationBuilder.DropIndex(
                name: "IX_QuoteToResultsMappers_QuoteID",
                table: "QuoteToResultsMappers");

            migrationBuilder.DropIndex(
                name: "IX_Filters_QuoteID",
                table: "Filters");

            migrationBuilder.DropColumn(
                name: "QuoteID",
                table: "QuoteToResultsMappers");

            migrationBuilder.DropColumn(
                name: "QuoteID",
                table: "Filters");

            migrationBuilder.AddColumn<int>(
                name: "QuoteGroupID",
                table: "QuoteToResultsMappers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuoteGroupID",
                table: "Filters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_QuoteToResultsMappers_QuoteGroupID",
                table: "QuoteToResultsMappers",
                column: "QuoteGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Filters_QuoteGroupID",
                table: "Filters",
                column: "QuoteGroupID");

            migrationBuilder.AddForeignKey(
                name: "FK_Filters_QuoteGroups_QuoteGroupID",
                table: "Filters",
                column: "QuoteGroupID",
                principalTable: "QuoteGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteToResultsMappers_QuoteGroups_QuoteGroupID",
                table: "QuoteToResultsMappers",
                column: "QuoteGroupID",
                principalTable: "QuoteGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Filters_QuoteGroups_QuoteGroupID",
                table: "Filters");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteToResultsMappers_QuoteGroups_QuoteGroupID",
                table: "QuoteToResultsMappers");

            migrationBuilder.DropIndex(
                name: "IX_QuoteToResultsMappers_QuoteGroupID",
                table: "QuoteToResultsMappers");

            migrationBuilder.DropIndex(
                name: "IX_Filters_QuoteGroupID",
                table: "Filters");

            migrationBuilder.DropColumn(
                name: "QuoteGroupID",
                table: "QuoteToResultsMappers");

            migrationBuilder.DropColumn(
                name: "QuoteGroupID",
                table: "Filters");

            migrationBuilder.AddColumn<int>(
                name: "QuoteID",
                table: "QuoteToResultsMappers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuoteID",
                table: "Filters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_QuoteToResultsMappers_QuoteID",
                table: "QuoteToResultsMappers",
                column: "QuoteID");

            migrationBuilder.CreateIndex(
                name: "IX_Filters_QuoteID",
                table: "Filters",
                column: "QuoteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Filters_Quotes_QuoteID",
                table: "Filters",
                column: "QuoteID",
                principalTable: "Quotes",
                principalColumn: "QuoteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteToResultsMappers_Quotes_QuoteID",
                table: "QuoteToResultsMappers",
                column: "QuoteID",
                principalTable: "Quotes",
                principalColumn: "QuoteID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
