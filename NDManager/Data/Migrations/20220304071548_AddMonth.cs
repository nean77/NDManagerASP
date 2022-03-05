using Microsoft.EntityFrameworkCore.Migrations;

namespace NDManager.Migrations
{
    public partial class AddMonth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KidId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthNo = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Kids_KidId",
                        column: x => x.KidId,
                        principalTable: "Kids",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_KidId",
                table: "Payment",
                column: "KidId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment");
        }
    }
}
