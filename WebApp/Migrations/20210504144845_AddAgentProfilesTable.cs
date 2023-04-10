using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class AddAgentProfilesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgentProfile_AirPort_DefaultAirPortId",
                table: "AgentProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_AgentProfile_AgentProfileId",
                table: "Contact");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AgentProfile",
                table: "AgentProfile");

            migrationBuilder.RenameTable(
                name: "AgentProfile",
                newName: "AgentProfiles");

            migrationBuilder.RenameIndex(
                name: "IX_AgentProfile_DefaultAirPortId",
                table: "AgentProfiles",
                newName: "IX_AgentProfiles_DefaultAirPortId");

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

            migrationBuilder.AddColumn<double>(
                name: "MonthlyFixedCost",
                table: "AgentProfiles",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AgentProfiles",
                table: "AgentProfiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AgentProfiles_AirPort_DefaultAirPortId",
                table: "AgentProfiles",
                column: "DefaultAirPortId",
                principalTable: "AirPort",
                principalColumn: "AirPortID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_AgentProfiles_AgentProfileId",
                table: "Contact",
                column: "AgentProfileId",
                principalTable: "AgentProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgentProfiles_AirPort_DefaultAirPortId",
                table: "AgentProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_AgentProfiles_AgentProfileId",
                table: "Contact");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AgentProfiles",
                table: "AgentProfiles");

            migrationBuilder.DropColumn(
                name: "MonthlyFixedCost",
                table: "AgentProfiles");

            migrationBuilder.RenameTable(
                name: "AgentProfiles",
                newName: "AgentProfile");

            migrationBuilder.RenameIndex(
                name: "IX_AgentProfiles_DefaultAirPortId",
                table: "AgentProfile",
                newName: "IX_AgentProfile_DefaultAirPortId");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_AgentProfile",
                table: "AgentProfile",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AgentProfile_AirPort_DefaultAirPortId",
                table: "AgentProfile",
                column: "DefaultAirPortId",
                principalTable: "AirPort",
                principalColumn: "AirPortID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_AgentProfile_AgentProfileId",
                table: "Contact",
                column: "AgentProfileId",
                principalTable: "AgentProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
