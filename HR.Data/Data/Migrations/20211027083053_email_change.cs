using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.Data.Migrations
{
    public partial class email_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Email_EmailId",
                table: "Employee");

            migrationBuilder.DropTable(
                name: "Email");

            migrationBuilder.DropIndex(
                name: "IX_Employee_EmailId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "EmailId",
                table: "Employee");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Employee",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Employee");

            migrationBuilder.AddColumn<int>(
                name: "EmailId",
                table: "Employee",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Email",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmailAddress = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Email", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_EmailId",
                table: "Employee",
                column: "EmailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Email_EmailId",
                table: "Employee",
                column: "EmailId",
                principalTable: "Email",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
