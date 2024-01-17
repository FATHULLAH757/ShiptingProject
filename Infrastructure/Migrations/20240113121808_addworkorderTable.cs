using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class addworkorderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkOrderDestinations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DestinationBusinessName1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DestinationBusinessName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DestinationCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DestinationState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DestinationZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DestinationNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkOrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderDestinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrderDestinations_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderPickups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PickupBusinessName1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickupBusinessName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickupCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickupState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickupZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickupNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkOrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderPickups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrderPickups_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderDestinations_WorkOrderId",
                table: "WorkOrderDestinations",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderPickups_WorkOrderId",
                table: "WorkOrderPickups",
                column: "WorkOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkOrderDestinations");

            migrationBuilder.DropTable(
                name: "WorkOrderPickups");
        }
    }
}
