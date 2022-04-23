using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeDuo.Migrations
{
    public partial class DockerInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CodeDatas_AspNetUsers_OwnerUserId",
                table: "CodeDatas");

            migrationBuilder.DropIndex(
                name: "IX_CodeDatas_OwnerUserId",
                table: "CodeDatas");

            migrationBuilder.DropColumn(
                name: "OwnerUserId",
                table: "CodeDatas");

            migrationBuilder.AddColumn<int>(
                name: "Column",
                table: "CodeDataShares",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Row",
                table: "CodeDataShares",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Column",
                table: "CodeDataShares");

            migrationBuilder.DropColumn(
                name: "Row",
                table: "CodeDataShares");

            migrationBuilder.AddColumn<string>(
                name: "OwnerUserId",
                table: "CodeDatas",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CodeDatas_OwnerUserId",
                table: "CodeDatas",
                column: "OwnerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CodeDatas_AspNetUsers_OwnerUserId",
                table: "CodeDatas",
                column: "OwnerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
