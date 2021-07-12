using Microsoft.EntityFrameworkCore.Migrations;

namespace Gateways.EntityFrameworkCore.Migrations
{
    public partial class Add_Index_Gateway : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Gateways_SerialNumber",
                table: "Gateways",
                column: "SerialNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Gateways_SerialNumber",
                table: "Gateways");
        }
    }
}
