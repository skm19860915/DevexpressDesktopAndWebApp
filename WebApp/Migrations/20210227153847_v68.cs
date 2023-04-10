using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v68 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Contact_ContactID",
                table: "Invoice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_ContactID",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "InvoiceID",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "ContactID",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "ClientStatus",
                table: "Contact");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Invoice",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_ClientId",
                table: "Invoice",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Contact_ClientId",
                table: "Invoice",
                column: "ClientId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Contact_ClientId",
                table: "Invoice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_ClientId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Invoice");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceID",
                table: "Invoice",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "ContactID",
                table: "Invoice",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientStatus",
                table: "Contact",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice",
                column: "InvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_ContactID",
                table: "Invoice",
                column: "ContactID");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Contact_ContactID",
                table: "Invoice",
                column: "ContactID",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
