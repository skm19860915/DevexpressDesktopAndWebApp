using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Relationships_RelationshipID",
                table: "AppUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Relationships_RelationshipID",
                table: "AppUsers",
                column: "RelationshipID",
                principalTable: "Relationships",
                principalColumn: "RelationshipID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Relationships_RelationshipID",
                table: "AppUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Relationships_RelationshipID",
                table: "AppUsers",
                column: "RelationshipID",
                principalTable: "Relationships",
                principalColumn: "RelationshipID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
