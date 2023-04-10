using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class AddNameReplacements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NameReplacements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourOperatorId = table.Column<int>(nullable: false),
                    ReplaceType = table.Column<int>(nullable: false),
                    Original = table.Column<string>(nullable: true),
                    NewName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NameReplacements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NameReplacements_Companies_TourOperatorId",
                        column: x => x.TourOperatorId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NameReplacements_TourOperatorId",
                table: "NameReplacements",
                column: "TourOperatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NameReplacements");
        }
    }
}
