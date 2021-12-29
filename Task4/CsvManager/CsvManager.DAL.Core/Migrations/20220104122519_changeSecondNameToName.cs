using Microsoft.EntityFrameworkCore.Migrations;

namespace CsvManager.DAL.Core.Migrations
{
    public partial class changeSecondNameToName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecondName",
                table: "Managers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "SecondName",
                table: "Clients",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Managers",
                newName: "SecondName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Clients",
                newName: "SecondName");
        }
    }
}
