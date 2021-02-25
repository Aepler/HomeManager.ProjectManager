using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeManager.Data.Migrations
{
    public partial class HomeManagerDefaultTaxRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefaultTaxRate",
                table: "FinanceTypes",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultTaxRate",
                table: "FinanceTypes");
        }
    }
}
