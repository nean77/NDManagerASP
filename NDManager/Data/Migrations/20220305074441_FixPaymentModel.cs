using Microsoft.EntityFrameworkCore.Migrations;

namespace NDManager.Migrations
{
    public partial class FixPaymentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "WorkingDays",
                table: "Payment",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkingDays",
                table: "Payment");
        }
    }
}
