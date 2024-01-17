using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class addAdditionalChargesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkOrderAdditionalCharges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FSCDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FSCDetailAmount = table.Column<float>(type: "real", nullable: false),
                    ChasisRentDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChasisRentDetailAmount = table.Column<float>(type: "real", nullable: false),
                    PREFullDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PREFullDetailAmount = table.Column<float>(type: "real", nullable: false),
                    StorageDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StorageDetailAmount = table.Column<float>(type: "real", nullable: false),
                    DetentionDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetentionDetailAmount = table.Column<float>(type: "real", nullable: false),
                    LayOverDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LayOverDetailAmount = table.Column<float>(type: "real", nullable: false),
                    PortCngestionDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PortCngestionDetailAmount = table.Column<float>(type: "real", nullable: false),
                    OverWeightFeeDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OverWeightFeeDetailAmount = table.Column<float>(type: "real", nullable: false),
                    ReeferFeeDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReeferFeeDetailAmount = table.Column<float>(type: "real", nullable: false),
                    TruckOrderNotUsedDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TruckOrderNotUsedDetailAmount = table.Column<float>(type: "real", nullable: false),
                    WorkOrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderAdditionalCharges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrderAdditionalCharges_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderAdditionalCharges_WorkOrderId",
                table: "WorkOrderAdditionalCharges",
                column: "WorkOrderId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkOrderAdditionalCharges");
        }
    }
}
