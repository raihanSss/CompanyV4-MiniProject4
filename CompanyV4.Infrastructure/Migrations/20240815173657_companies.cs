using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CompanyV4.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class companies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departements",
                columns: table => new
                {
                    DeptNo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeptName = table.Column<string>(type: "text", nullable: false),
                    MgrEmpNo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departements", x => x.DeptNo);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmpNo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FName = table.Column<string>(type: "text", nullable: false),
                    LName = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    DOB = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Sex = table.Column<string>(type: "text", nullable: false),
                    Position = table.Column<string>(type: "text", nullable: false),
                    DeptNo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmpNo);
                    table.ForeignKey(
                        name: "FK_Employees_Departements_DeptNo",
                        column: x => x.DeptNo,
                        principalTable: "Departements",
                        principalColumn: "DeptNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjNo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjName = table.Column<string>(type: "text", nullable: false),
                    DeptNo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjNo);
                    table.ForeignKey(
                        name: "FK_Projects_Departements_DeptNo",
                        column: x => x.DeptNo,
                        principalTable: "Departements",
                        principalColumn: "DeptNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Worksons",
                columns: table => new
                {
                    EmpNo = table.Column<int>(type: "integer", nullable: false),
                    ProjNo = table.Column<int>(type: "integer", nullable: false),
                    DateWorked = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HoursWorked = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worksons", x => new { x.EmpNo, x.ProjNo, x.DateWorked });
                    table.ForeignKey(
                        name: "FK_Worksons_Employees_EmpNo",
                        column: x => x.EmpNo,
                        principalTable: "Employees",
                        principalColumn: "EmpNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Worksons_Projects_ProjNo",
                        column: x => x.ProjNo,
                        principalTable: "Projects",
                        principalColumn: "ProjNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departements_MgrEmpNo",
                table: "Departements",
                column: "MgrEmpNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DeptNo",
                table: "Employees",
                column: "DeptNo");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DeptNo",
                table: "Projects",
                column: "DeptNo");

            migrationBuilder.CreateIndex(
                name: "IX_Worksons_ProjNo",
                table: "Worksons",
                column: "ProjNo");

            migrationBuilder.AddForeignKey(
                name: "FK_Departements_Employees_MgrEmpNo",
                table: "Departements",
                column: "MgrEmpNo",
                principalTable: "Employees",
                principalColumn: "EmpNo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departements_Employees_MgrEmpNo",
                table: "Departements");

            migrationBuilder.DropTable(
                name: "Worksons");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departements");
        }
    }
}
