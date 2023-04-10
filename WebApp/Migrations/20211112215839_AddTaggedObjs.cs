using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class AddTaggedObjs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Files_FileID",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.RenameColumn(
                name: "TagName",
                table: "Tags",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_Tag_FileID",
                table: "Tags",
                newName: "IX_Tags_FileID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TaggedObjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagId = table.Column<int>(type: "int", nullable: false),
                    ContactId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OpportunityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaggedObjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaggedObjects_Contact_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaggedObjects_Opportunities_OpportunityId",
                        column: x => x.OpportunityId,
                        principalTable: "Opportunities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaggedObjects_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaggedObjects_ContactId",
                table: "TaggedObjects",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_TaggedObjects_OpportunityId",
                table: "TaggedObjects",
                column: "OpportunityId");

            migrationBuilder.CreateIndex(
                name: "IX_TaggedObjects_TagId",
                table: "TaggedObjects",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Files_FileID",
                table: "Tags",
                column: "FileID",
                principalTable: "Files",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Files_FileID",
                table: "Tags");

            migrationBuilder.DropTable(
                name: "TaggedObjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tag",
                newName: "TagName");

            migrationBuilder.RenameIndex(
                name: "IX_Tags_FileID",
                table: "Tag",
                newName: "IX_Tag_FileID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Files_FileID",
                table: "Tag",
                column: "FileID",
                principalTable: "Files",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
