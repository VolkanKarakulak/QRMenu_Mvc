using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QRMenu_Mvc.Migrations
{
    public partial class imagefood2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageFileName",
                table: "Food",
                type: "nvarchar(180)",
                maxLength: 180,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFileName",
                table: "Food");
        }
    }
}
