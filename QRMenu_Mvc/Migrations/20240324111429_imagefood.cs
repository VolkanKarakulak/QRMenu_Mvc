using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QRMenu_Mvc.Migrations
{
    public partial class imagefood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_State_StateId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_State_StateId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Food_State_StateId",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurant_State_StateId",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "ImageFileName",
                table: "Food");

            migrationBuilder.CreateTable(
                name: "RestaurantUser",
                columns: table => new
                {
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantUser", x => new { x.UserId, x.RestaurantId });
                    table.ForeignKey(
                        name: "FK_RestaurantUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestaurantUser_Restaurant_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantUser_RestaurantId",
                table: "RestaurantUser",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_State_StateId",
                table: "AspNetUsers",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_State_StateId",
                table: "Category",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_State_StateId",
                table: "Food",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurant_State_StateId",
                table: "Restaurant",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_State_StateId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_State_StateId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Food_State_StateId",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurant_State_StateId",
                table: "Restaurant");

            migrationBuilder.DropTable(
                name: "RestaurantUser");

            migrationBuilder.AddColumn<string>(
                name: "ImageFileName",
                table: "Food",
                type: "nvarchar(180)",
                maxLength: 180,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_State_StateId",
                table: "AspNetUsers",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_State_StateId",
                table: "Category",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Food_State_StateId",
                table: "Food",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurant_State_StateId",
                table: "Restaurant",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
