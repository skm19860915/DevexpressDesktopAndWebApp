using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_AddressMap_AddressMapID",
                table: "AppUsers");

            migrationBuilder.DropTable(
                name: "AddressMap");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "AddressType");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_AddressMapID",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "AddressMapID",
                table: "AppUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressMapID",
                table: "AppUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AddressType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AddressMap",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressID = table.Column<int>(type: "int", nullable: false),
                    AddressTypeID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressMap", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AddressMap_Address_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Address",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressMap_AddressType_AddressTypeID",
                        column: x => x.AddressTypeID,
                        principalTable: "AddressType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressMap_AppUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_AddressMapID",
                table: "AppUsers",
                column: "AddressMapID");

            migrationBuilder.CreateIndex(
                name: "IX_AddressMap_AddressID",
                table: "AddressMap",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_AddressMap_AddressTypeID",
                table: "AddressMap",
                column: "AddressTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_AddressMap_UserID",
                table: "AddressMap",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_AddressMap_AddressMapID",
                table: "AppUsers",
                column: "AddressMapID",
                principalTable: "AddressMap",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
