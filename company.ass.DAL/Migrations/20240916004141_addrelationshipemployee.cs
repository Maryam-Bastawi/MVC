using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace company.ass.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addrelationshipemployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "workforid",
                table: "employee",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "department",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_employee_workforid",
                table: "employee",
                column: "workforid");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_department_workforid",
                table: "employee",
                column: "workforid",
                principalTable: "department",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_department_workforid",
                table: "employee");

            migrationBuilder.DropIndex(
                name: "IX_employee_workforid",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "workforid",
                table: "employee");

            migrationBuilder.AlterColumn<int>(
                name: "code",
                table: "department",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
