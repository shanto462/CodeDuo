using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeDuo.Migrations
{
    public partial class InitialCodeTableAndPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CodeDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastAccessed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CodeSegment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CodeDatas_AspNetUsers_OwnerUserId",
                        column: x => x.OwnerUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CodeDataShares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Permission = table.Column<int>(type: "int", nullable: false),
                    CodeDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeDataShares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CodeDataShares_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CodeDataShares_CodeDatas_CodeDataId",
                        column: x => x.CodeDataId,
                        principalTable: "CodeDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CodeDatas_OwnerUserId",
                table: "CodeDatas",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CodeDataShares_CodeDataId",
                table: "CodeDataShares",
                column: "CodeDataId");

            migrationBuilder.CreateIndex(
                name: "IX_CodeDataShares_UserId",
                table: "CodeDataShares",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodeDataShares");

            migrationBuilder.DropTable(
                name: "CodeDatas");
        }
    }
}
