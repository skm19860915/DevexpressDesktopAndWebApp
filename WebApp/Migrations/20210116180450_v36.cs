using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v36 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Contact_CreatorID",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Contact_IssuerID",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_CreatorID",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_IssuerID",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "AppointGUID",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "AppointmentEndTime",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "AppointmentStartTime",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CreatorID",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "EmailID",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "GUID",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "IssuerID",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "LastChanged",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "LastChangedID",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "MeetingID",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "MileStone",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "OutlookID",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "PrimaryOwnerID",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ReOccuringEnd",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ReOccuringStart",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "SaveMask",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "SeqOrder",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TempDatesCalculated",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "UserStoryID",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "isMeeting",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "usingProjectsPriority",
                table: "Tasks");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Tasks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedById",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedOn",
                table: "Tasks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
                name: "IX_Tasks_CreatedById",
                table: "Tasks",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_LastUpdatedById",
                table: "Tasks",
                column: "LastUpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Contact_CreatedById",
                table: "Tasks",
                column: "CreatedById",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Contact_LastUpdatedById",
                table: "Tasks",
                column: "LastUpdatedById",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Contact_CreatedById",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Contact_LastUpdatedById",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_CreatedById",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_LastUpdatedById",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "LastUpdatedById",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "LastUpdatedOn",
                table: "Tasks");

            migrationBuilder.AddColumn<string>(
                name: "AppointGUID",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppointmentEndTime",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppointmentStartTime",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Tasks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatorID",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmailID",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GUID",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IssuerID",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastChanged",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastChangedID",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Tasks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MeetingID",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MileStone",
                table: "Tasks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OutlookID",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrimaryOwnerID",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReOccuringEnd",
                table: "Tasks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReOccuringStart",
                table: "Tasks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SaveMask",
                table: "Tasks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "SeqOrder",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TempDatesCalculated",
                table: "Tasks",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserStoryID",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isMeeting",
                table: "Tasks",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "usingProjectsPriority",
                table: "Tasks",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
                name: "IX_Tasks_CreatorID",
                table: "Tasks",
                column: "CreatorID");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_IssuerID",
                table: "Tasks",
                column: "IssuerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Contact_CreatorID",
                table: "Tasks",
                column: "CreatorID",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Contact_IssuerID",
                table: "Tasks",
                column: "IssuerID",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
