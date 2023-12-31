using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class changeworkorderproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DriverPay",
                table: "WorkOrders",
                newName: "FirstDriverPay");

            migrationBuilder.RenameColumn(
                name: "DriverId",
                table: "WorkOrders",
                newName: "FirstDriverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstDriverPay",
                table: "WorkOrders",
                newName: "DriverPay");

            migrationBuilder.RenameColumn(
                name: "FirstDriverId",
                table: "WorkOrders",
                newName: "DriverId");
        }
    }
}
