using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.Data.Migrations
{
    public partial class emailUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Email_Employee_EmployeeId",
                table: "Email");

            migrationBuilder.DropIndex(
                name: "IX_Email_EmployeeId",
                table: "Email");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Email");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Employee",
                type: "TEXT",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Employee",
                type: "TEXT",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmailId",
                table: "Employee",
                type: "INTEGER",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Email_EmailId",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_EmailId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "EmailId",
                table: "Employee");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Employee",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Employee",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Email",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Email_EmployeeId",
                table: "Email",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Email_Employee_EmployeeId",
                table: "Email",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
