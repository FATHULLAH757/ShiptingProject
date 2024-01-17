using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class addDrayageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drayage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrayageAccountManager = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrayageSeal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrayageLFD = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DrayageVesselETA = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DrayageContainer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrayageSizedrayageSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrayageOW = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrayageHazmat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrayageBKG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrayageChassis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrayageOutGateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DrayageOutGateDriver = table.Column<int>(type: "int", nullable: false),
                    DrayageInGateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DrayageInGateDriver = table.Column<int>(type: "int", nullable: false),
                    DrayageStateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DrayageLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkOrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drayage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drayage_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drayage_WorkOrderId",
                table: "Drayage",
                column: "WorkOrderId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drayage");
        }
    }
}
