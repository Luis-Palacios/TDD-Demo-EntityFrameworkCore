using Microsoft.EntityFrameworkCore.Migrations;

namespace TddDemo.Web.Migrations
{
    public partial class AddFKForecastDetailBudgetDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BudgetDetailId",
                table: "ForecastDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ForecastDetails_BudgetDetailId",
                table: "ForecastDetails",
                column: "BudgetDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_ForecastDetails_BudgetDetails_BudgetDetailId",
                table: "ForecastDetails",
                column: "BudgetDetailId",
                principalTable: "BudgetDetails",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForecastDetails_BudgetDetails_BudgetDetailId",
                table: "ForecastDetails");

            migrationBuilder.DropIndex(
                name: "IX_ForecastDetails_BudgetDetailId",
                table: "ForecastDetails");

            migrationBuilder.DropColumn(
                name: "BudgetDetailId",
                table: "ForecastDetails");
        }
    }
}
