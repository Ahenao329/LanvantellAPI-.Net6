using Microsoft.EntityFrameworkCore.Migrations;

namespace LavantellAPIS.Migrations
{
    public partial class tercer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FeachaExpedicion",
                table: "Tarjeta",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeachaExpedicion",
                table: "Tarjeta");
        }
    }
}
