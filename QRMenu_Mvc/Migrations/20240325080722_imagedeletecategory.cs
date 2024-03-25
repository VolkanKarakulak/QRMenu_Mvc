using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QRMenu_Mvc.Migrations
{
    public partial class imagedeletecategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFileName",
                table: "Category");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageFileName",
                table: "Category",
                type: "nvarchar(180)",
                maxLength: 180,
                nullable: false,
                defaultValue: "");
        }
    }
}
