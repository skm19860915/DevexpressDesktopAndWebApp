using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Pages_PageId",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Countries_PageId",
                table: "Countries");

            migrationBuilder.AlterColumn<int>(
                name: "PageId",
                table: "Countries",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");


            migrationBuilder.CreateIndex(
                name: "IX_Countries_PageId",
                table: "Countries",
                column: "PageId",
                unique: true,
                filter: "[PageId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_Pages_PageId",
                table: "Countries",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Pages_PageId",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Countries_PageId",
                table: "Countries");

            migrationBuilder.AlterColumn<int>(
                name: "PageId",
                table: "Countries",
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

            migrationBuilder.CreateIndex(
                name: "IX_Countries_PageId",
                table: "Countries",
                column: "PageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_Pages_PageId",
                table: "Countries",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
