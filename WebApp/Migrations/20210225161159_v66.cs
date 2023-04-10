using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v66 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilteredAccommodations_Filters_FilterID",
                table: "FilteredAccommodations");

            migrationBuilder.AlterColumn<int>(
                name: "FilterID",
                table: "FilteredAccommodations",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PreferenceId",
                table: "FilteredAccommodations",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_FilteredAccommodations_PreferenceId",
                table: "FilteredAccommodations",
                column: "PreferenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_FilteredAccommodations_Filters_FilterID",
                table: "FilteredAccommodations",
                column: "FilterID",
                principalTable: "Filters",
                principalColumn: "FilterID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FilteredAccommodations_AgentAirPortPreferences_PreferenceId",
                table: "FilteredAccommodations",
                column: "PreferenceId",
                principalTable: "AgentAirPortPreferences",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilteredAccommodations_Filters_FilterID",
                table: "FilteredAccommodations");

            migrationBuilder.DropForeignKey(
                name: "FK_FilteredAccommodations_AgentAirPortPreferences_PreferenceId",
                table: "FilteredAccommodations");

            migrationBuilder.DropIndex(
                name: "IX_FilteredAccommodations_PreferenceId",
                table: "FilteredAccommodations");

            migrationBuilder.DropColumn(
                name: "PreferenceId",
                table: "FilteredAccommodations");

            migrationBuilder.AlterColumn<int>(
                name: "FilterID",
                table: "FilteredAccommodations",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_FilteredAccommodations_Filters_FilterID",
                table: "FilteredAccommodations",
                column: "FilterID",
                principalTable: "Filters",
                principalColumn: "FilterID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
