using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QRMenu_Mvc.Migrations
{
    public partial class edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brand_Brand_BrandId",
                table: "Brand");

            migrationBuilder.DropIndex(
                name: "IX_Brand_BrandId",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Brand");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Brand",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brand_BrandId",
                table: "Brand",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brand_Brand_BrandId",
                table: "Brand",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id");
        }
    }
}
