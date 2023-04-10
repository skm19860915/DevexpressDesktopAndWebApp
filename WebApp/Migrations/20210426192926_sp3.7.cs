using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class sp37 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phone_Companies_CompanyId",
                table: "Phone");

            migrationBuilder.DropForeignKey(
                name: "FK_Phone_PhoneType_PhoneTypeID",
                table: "Phone");

            migrationBuilder.DropForeignKey(
                name: "FK_Phone_Contact_UserId",
                table: "Phone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Phone",
                table: "Phone");

            migrationBuilder.RenameTable(
                name: "Phone",
                newName: "PhoneNumbers");

            migrationBuilder.RenameIndex(
                name: "IX_Phone_UserId",
                table: "PhoneNumbers",
                newName: "IX_PhoneNumbers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Phone_PhoneTypeID",
                table: "PhoneNumbers",
                newName: "IX_PhoneNumbers_PhoneTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_Phone_CompanyId",
                table: "PhoneNumbers",
                newName: "IX_PhoneNumbers_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhoneNumbers",
                table: "PhoneNumbers",
                column: "PhoneID");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_Companies_CompanyId",
                table: "PhoneNumbers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_PhoneType_PhoneTypeID",
                table: "PhoneNumbers",
                column: "PhoneTypeID",
                principalTable: "PhoneType",
                principalColumn: "PhoneTypeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_Contact_UserId",
                table: "PhoneNumbers",
                column: "UserId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_Companies_CompanyId",
                table: "PhoneNumbers");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_PhoneType_PhoneTypeID",
                table: "PhoneNumbers");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_Contact_UserId",
                table: "PhoneNumbers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhoneNumbers",
                table: "PhoneNumbers");

            migrationBuilder.RenameTable(
                name: "PhoneNumbers",
                newName: "Phone");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneNumbers_UserId",
                table: "Phone",
                newName: "IX_Phone_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneNumbers_PhoneTypeID",
                table: "Phone",
                newName: "IX_Phone_PhoneTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneNumbers_CompanyId",
                table: "Phone",
                newName: "IX_Phone_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Phone",
                table: "Phone",
                column: "PhoneID");

            migrationBuilder.AddForeignKey(
                name: "FK_Phone_Companies_CompanyId",
                table: "Phone",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Phone_PhoneType_PhoneTypeID",
                table: "Phone",
                column: "PhoneTypeID",
                principalTable: "PhoneType",
                principalColumn: "PhoneTypeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Phone_Contact_UserId",
                table: "Phone",
                column: "UserId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
