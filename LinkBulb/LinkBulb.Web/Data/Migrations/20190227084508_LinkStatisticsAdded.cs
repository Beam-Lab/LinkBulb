using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LinkBulb.Web.Data.Migrations
{
    public partial class LinkStatisticsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LinkStatistics",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    LinkID = table.Column<Guid>(nullable: false),
                    ClickDate = table.Column<DateTime>(nullable: false),
                    Browser = table.Column<string>(nullable: true),
                    OS = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkStatistics", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkStatistics");
        }
    }
}
