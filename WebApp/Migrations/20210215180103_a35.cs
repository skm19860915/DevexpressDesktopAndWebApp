using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class a35 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgentProfileId",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Default",
                table: "AirPort",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AgentProfile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DefaultAirPortId = table.Column<int>(nullable: true),
                    TimeZoneDiff = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgentProfile_AirPort_DefaultAirPortId",
                        column: x => x.DefaultAirPortId,
                        principalTable: "AirPort",
                        principalColumn: "AirPortID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contact_AgentProfileId",
                table: "Contact",
                column: "AgentProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentProfile_DefaultAirPortId",
                table: "AgentProfile",
                column: "DefaultAirPortId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_AgentProfile_AgentProfileId",
                table: "Contact",
                column: "AgentProfileId",
                principalTable: "AgentProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_AgentProfile_AgentProfileId",
                table: "Contact");

            migrationBuilder.DropTable(
                name: "AgentProfile");

            migrationBuilder.DropIndex(
                name: "IX_Contact_AgentProfileId",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "AgentProfileId",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "Default",
                table: "AirPort");
        }
    }
}
