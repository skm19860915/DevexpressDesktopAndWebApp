using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class QRToQG : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestResorts_QuoteRequests_QuoteRequestID",
                table: "QuoteRequestResorts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestTickets_QuoteGroups_QuoteGroupID",
                table: "QuoteRequestTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Transportations_QuoteRequests_QuoteRequestID",
                table: "Transportations");

            migrationBuilder.DropIndex(
                name: "IX_QuoteRequestResorts_QuoteRequestID",
                table: "QuoteRequestResorts");

            migrationBuilder.DropColumn(
                name: "LandOnly",
                table: "Staging_Hotels");

            migrationBuilder.DropColumn(
                name: "RequestID",
                table: "Staging_Flights");

            migrationBuilder.DropColumn(
                name: "QuoteRequestID",
                table: "QuoteRequestResorts");

            migrationBuilder.RenameColumn(
                name: "QuoteGroupID",
                table: "QuoteRequestTickets",
                newName: "QuoteGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_QuoteRequestTickets_QuoteGroupID",
                table: "QuoteRequestTickets",
                newName: "IX_QuoteRequestTickets_QuoteGroupId");

            migrationBuilder.AlterColumn<int>(
                name: "QuoteRequestID",
                table: "Transportations",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "QuoteGroupId",
                table: "Transportations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuoteGroupId",
                table: "Staging_Hotels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuoteGroupId",
                table: "Staging_Flights",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuoteGroupId",
                table: "QuoteRequestResorts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuoteGroupId",
                table: "Leg",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_QuoteGroupId",
                table: "Transportations",
                column: "QuoteGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Staging_Hotels_QuoteGroupId",
                table: "Staging_Hotels",
                column: "QuoteGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Staging_Flights_QuoteGroupId",
                table: "Staging_Flights",
                column: "QuoteGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequestResorts_QuoteGroupId",
                table: "QuoteRequestResorts",
                column: "QuoteGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Leg_QuoteGroupId",
                table: "Leg",
                column: "QuoteGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leg_QuoteGroups_QuoteGroupId",
                table: "Leg",
                column: "QuoteGroupId",
                principalTable: "QuoteGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestResorts_QuoteGroups_QuoteGroupId",
                table: "QuoteRequestResorts",
                column: "QuoteGroupId",
                principalTable: "QuoteGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestTickets_QuoteGroups_QuoteGroupId",
                table: "QuoteRequestTickets",
                column: "QuoteGroupId",
                principalTable: "QuoteGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Staging_Flights_QuoteGroups_QuoteGroupId",
                table: "Staging_Flights",
                column: "QuoteGroupId",
                principalTable: "QuoteGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Staging_Hotels_QuoteGroups_QuoteGroupId",
                table: "Staging_Hotels",
                column: "QuoteGroupId",
                principalTable: "QuoteGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transportations_QuoteGroups_QuoteGroupId",
                table: "Transportations",
                column: "QuoteGroupId",
                principalTable: "QuoteGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transportations_QuoteRequests_QuoteRequestID",
                table: "Transportations",
                column: "QuoteRequestID",
                principalTable: "QuoteRequests",
                principalColumn: "QuoteRequestID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leg_QuoteGroups_QuoteGroupId",
                table: "Leg");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestResorts_QuoteGroups_QuoteGroupId",
                table: "QuoteRequestResorts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestTickets_QuoteGroups_QuoteGroupId",
                table: "QuoteRequestTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Staging_Flights_QuoteGroups_QuoteGroupId",
                table: "Staging_Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Staging_Hotels_QuoteGroups_QuoteGroupId",
                table: "Staging_Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_Transportations_QuoteGroups_QuoteGroupId",
                table: "Transportations");

            migrationBuilder.DropForeignKey(
                name: "FK_Transportations_QuoteRequests_QuoteRequestID",
                table: "Transportations");

            migrationBuilder.DropIndex(
                name: "IX_Transportations_QuoteGroupId",
                table: "Transportations");

            migrationBuilder.DropIndex(
                name: "IX_Staging_Hotels_QuoteGroupId",
                table: "Staging_Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Staging_Flights_QuoteGroupId",
                table: "Staging_Flights");

            migrationBuilder.DropIndex(
                name: "IX_QuoteRequestResorts_QuoteGroupId",
                table: "QuoteRequestResorts");

            migrationBuilder.DropIndex(
                name: "IX_Leg_QuoteGroupId",
                table: "Leg");

            migrationBuilder.DropColumn(
                name: "QuoteGroupId",
                table: "Transportations");

            migrationBuilder.DropColumn(
                name: "QuoteGroupId",
                table: "Staging_Hotels");

            migrationBuilder.DropColumn(
                name: "QuoteGroupId",
                table: "Staging_Flights");

            migrationBuilder.DropColumn(
                name: "QuoteGroupId",
                table: "QuoteRequestResorts");

            migrationBuilder.DropColumn(
                name: "QuoteGroupId",
                table: "Leg");

            migrationBuilder.RenameColumn(
                name: "QuoteGroupId",
                table: "QuoteRequestTickets",
                newName: "QuoteGroupID");

            migrationBuilder.RenameIndex(
                name: "IX_QuoteRequestTickets_QuoteGroupId",
                table: "QuoteRequestTickets",
                newName: "IX_QuoteRequestTickets_QuoteGroupID");

            migrationBuilder.AlterColumn<int>(
                name: "QuoteRequestID",
                table: "Transportations",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "LandOnly",
                table: "Staging_Hotels",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RequestID",
                table: "Staging_Flights",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuoteRequestID",
                table: "QuoteRequestResorts",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequestResorts_QuoteRequestID",
                table: "QuoteRequestResorts",
                column: "QuoteRequestID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestResorts_QuoteRequests_QuoteRequestID",
                table: "QuoteRequestResorts",
                column: "QuoteRequestID",
                principalTable: "QuoteRequests",
                principalColumn: "QuoteRequestID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestTickets_QuoteGroups_QuoteGroupID",
                table: "QuoteRequestTickets",
                column: "QuoteGroupID",
                principalTable: "QuoteGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transportations_QuoteRequests_QuoteRequestID",
                table: "Transportations",
                column: "QuoteRequestID",
                principalTable: "QuoteRequests",
                principalColumn: "QuoteRequestID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
