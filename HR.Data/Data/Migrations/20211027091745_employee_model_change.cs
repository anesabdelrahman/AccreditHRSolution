using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.Data.Migrations
{
    public partial class employee_model_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Employee");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Employee",
                type: "TEXT",
                nullable: true);
        }
    }
}
