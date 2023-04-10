using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class a34 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReferralId",
                table: "Opportunities",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Notes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactId",
                table: "Notes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OpportunityId",
                table: "Notes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Where",
                table: "Notes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Who",
                table: "Notes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Referrals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referrals", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_ReferralId",
                table: "Opportunities",
                column: "ReferralId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_CompanyId",
                table: "Notes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_ContactId",
                table: "Notes",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_OpportunityId",
                table: "Notes",
                column: "OpportunityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Companies_CompanyId",
                table: "Notes",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Contact_ContactId",
                table: "Notes",
                column: "ContactId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Opportunities_OpportunityId",
                table: "Notes",
                column: "OpportunityId",
                principalTable: "Opportunities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunities_Referrals_ReferralId",
                table: "Opportunities",
                column: "ReferralId",
                principalTable: "Referrals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Companies_CompanyId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Contact_ContactId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Opportunities_OpportunityId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Opportunities_Referrals_ReferralId",
                table: "Opportunities");

            migrationBuilder.DropTable(
                name: "Referrals");

            migrationBuilder.DropIndex(
                name: "IX_Opportunities_ReferralId",
                table: "Opportunities");

            migrationBuilder.DropIndex(
                name: "IX_Notes_CompanyId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_ContactId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_OpportunityId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "ReferralId",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "OpportunityId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "Where",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "Who",
                table: "Notes");
        }
    }
}
