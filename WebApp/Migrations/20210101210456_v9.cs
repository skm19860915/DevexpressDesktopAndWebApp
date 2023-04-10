using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartureAirPortID2",
                table: "QuoteRequests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartureAirPortID3",
                table: "QuoteRequests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfAdults",
                table: "QuoteRequests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfRooms",
                table: "QuoteRequests",
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

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HouseHoldId",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaritalStatus",
                table: "AppUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "HouseHolds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseHolds", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequests_DepartureAirPortID2",
                table: "QuoteRequests",
                column: "DepartureAirPortID2");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequests_DepartureAirPortID3",
                table: "QuoteRequests",
                column: "DepartureAirPortID3");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_HouseHoldId",
                table: "AppUsers",
                column: "HouseHoldId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_HouseHolds_HouseHoldId",
                table: "AppUsers",
                column: "HouseHoldId",
                principalTable: "HouseHolds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequests_AirPort_DepartureAirPortID2",
                table: "QuoteRequests",
                column: "DepartureAirPortID2",
                principalTable: "AirPort",
                principalColumn: "AirPortID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequests_AirPort_DepartureAirPortID3",
                table: "QuoteRequests",
                column: "DepartureAirPortID3",
                principalTable: "AirPort",
                principalColumn: "AirPortID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_HouseHolds_HouseHoldId",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequests_AirPort_DepartureAirPortID2",
                table: "QuoteRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequests_AirPort_DepartureAirPortID3",
                table: "QuoteRequests");

            migrationBuilder.DropTable(
                name: "HouseHolds");

            migrationBuilder.DropIndex(
                name: "IX_QuoteRequests_DepartureAirPortID2",
                table: "QuoteRequests");

            migrationBuilder.DropIndex(
                name: "IX_QuoteRequests_DepartureAirPortID3",
                table: "QuoteRequests");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_HouseHoldId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "DepartureAirPortID2",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "DepartureAirPortID3",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "NumberOfAdults",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "NumberOfRooms",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "HouseHoldId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "MaritalStatus",
                table: "AppUsers");

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
        }
    }
}
