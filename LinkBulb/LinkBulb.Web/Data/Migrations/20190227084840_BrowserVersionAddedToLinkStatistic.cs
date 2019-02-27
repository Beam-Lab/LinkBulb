using Microsoft.EntityFrameworkCore.Migrations;

namespace LinkBulb.Web.Data.Migrations
{
    public partial class BrowserVersionAddedToLinkStatistic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BrowserVersion",
                table: "LinkStatistics",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrowserVersion",
                table: "LinkStatistics");
        }
    }
}
