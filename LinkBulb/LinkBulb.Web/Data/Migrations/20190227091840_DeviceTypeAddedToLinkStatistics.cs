using Microsoft.EntityFrameworkCore.Migrations;

namespace LinkBulb.Web.Data.Migrations
{
    public partial class DeviceTypeAddedToLinkStatistics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeviceType",
                table: "LinkStatistics",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceType",
                table: "LinkStatistics");
        }
    }
}
