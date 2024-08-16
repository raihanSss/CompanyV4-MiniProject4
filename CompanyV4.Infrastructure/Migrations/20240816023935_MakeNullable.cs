using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyV4.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departements_Employees_MgrEmpNo",
                table: "Departements");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departements_DeptNo",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Departements_DeptNo",
                table: "Projects");

            migrationBuilder.AlterColumn<int>(
                name: "HoursWorked",
                table: "Worksons",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Worksons",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "DeptNo",
                table: "Projects",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "Employees",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "Employees",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "DeptNo",
                table: "Employees",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DOB",
                table: "Employees",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Employees",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "MgrEmpNo",
                table: "Departements",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Departements_Employees_MgrEmpNo",
                table: "Departements",
                column: "MgrEmpNo",
                principalTable: "Employees",
                principalColumn: "EmpNo");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departements_DeptNo",
                table: "Employees",
                column: "DeptNo",
                principalTable: "Departements",
                principalColumn: "DeptNo");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Departements_DeptNo",
                table: "Projects",
                column: "DeptNo",
                principalTable: "Departements",
                principalColumn: "DeptNo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departements_Employees_MgrEmpNo",
                table: "Departements");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departements_DeptNo",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Departements_DeptNo",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Worksons");

            migrationBuilder.AlterColumn<int>(
                name: "HoursWorked",
                table: "Worksons",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeptNo",
                table: "Projects",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "Employees",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "Employees",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeptNo",
                table: "Employees",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DOB",
                table: "Employees",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Employees",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MgrEmpNo",
                table: "Departements",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Departements_Employees_MgrEmpNo",
                table: "Departements",
                column: "MgrEmpNo",
                principalTable: "Employees",
                principalColumn: "EmpNo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departements_DeptNo",
                table: "Employees",
                column: "DeptNo",
                principalTable: "Departements",
                principalColumn: "DeptNo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Departements_DeptNo",
                table: "Projects",
                column: "DeptNo",
                principalTable: "Departements",
                principalColumn: "DeptNo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
