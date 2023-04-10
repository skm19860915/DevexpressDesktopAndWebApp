using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class ChangeAccomTypeToSKU : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestResorts_AccommodationRoomTypes_ResortRoomTypeID",
                table: "QuoteRequestResorts");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_AccommodationRoomTypes_AccommodationRoomTypeID",
                table: "Quotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccommodationRoomTypes",
                table: "AccommodationRoomTypes");

            migrationBuilder.DropColumn(
                name: "AccommodationRoomTypeID",
                table: "AccommodationRoomTypes");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "SKUID",
                table: "AccommodationRoomTypes",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccommodationRoomTypes",
                table: "AccommodationRoomTypes",
                column: "SKUID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestResorts_AccommodationRoomTypes_ResortRoomTypeID",
                table: "QuoteRequestResorts",
                column: "ResortRoomTypeID",
                principalTable: "AccommodationRoomTypes",
                principalColumn: "SKUID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_AccommodationRoomTypes_AccommodationRoomTypeID",
                table: "Quotes",
                column: "AccommodationRoomTypeID",
                principalTable: "AccommodationRoomTypes",
                principalColumn: "SKUID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestResorts_AccommodationRoomTypes_ResortRoomTypeID",
                table: "QuoteRequestResorts");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_AccommodationRoomTypes_AccommodationRoomTypeID",
                table: "Quotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccommodationRoomTypes",
                table: "AccommodationRoomTypes");

            migrationBuilder.DropColumn(
                name: "SKUID",
                table: "AccommodationRoomTypes");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AddColumn<int>(
                name: "AccommodationRoomTypeID",
                table: "AccommodationRoomTypes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccommodationRoomTypes",
                table: "AccommodationRoomTypes",
                column: "AccommodationRoomTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestResorts_AccommodationRoomTypes_ResortRoomTypeID",
                table: "QuoteRequestResorts",
                column: "ResortRoomTypeID",
                principalTable: "AccommodationRoomTypes",
                principalColumn: "AccommodationRoomTypeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_AccommodationRoomTypes_AccommodationRoomTypeID",
                table: "Quotes",
                column: "AccommodationRoomTypeID",
                principalTable: "AccommodationRoomTypes",
                principalColumn: "AccommodationRoomTypeID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
