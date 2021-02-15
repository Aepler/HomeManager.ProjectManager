using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeManager.Data.Migrations
{
    public partial class HomeManagerUpdateFavorites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Favorites_FavoritesId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_FavoritesId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "FavoritesId",
                table: "Recipes");

            migrationBuilder.AddColumn<int>(
                name: "fk_RecipeId",
                table: "Favorites",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_fk_RecipeId",
                table: "Favorites",
                column: "fk_RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Recipes_fk_RecipeId",
                table: "Favorites",
                column: "fk_RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Recipes_fk_RecipeId",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_fk_RecipeId",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "fk_RecipeId",
                table: "Favorites");

            migrationBuilder.AddColumn<int>(
                name: "FavoritesId",
                table: "Recipes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_FavoritesId",
                table: "Recipes",
                column: "FavoritesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Favorites_FavoritesId",
                table: "Recipes",
                column: "FavoritesId",
                principalTable: "Favorites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
