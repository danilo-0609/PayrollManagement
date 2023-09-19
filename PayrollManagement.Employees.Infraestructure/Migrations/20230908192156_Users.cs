using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollManagement.Infraestructure.Migrations
{
    public partial class Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(name: "Last Name", type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CitizenshipId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Salary = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    Vacation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPensionContribution = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MustContributeToPension = table.Column<bool>(type: "bit", nullable: false),
                    TotalHealthContribution = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MustContributeToHealth = table.Column<bool>(type: "bit", nullable: false),
                    SalaryBonus = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    City = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Neighborhood = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HouseAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmploymentContract_EmploymentContractType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JoiningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndedContractTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

         
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

       

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

   


            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropTable(
                name: "Employees");
            

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
