using Microsoft.EntityFrameworkCore.Migrations;

namespace Airport.Infrastructure.Migrations
{
    public partial class Encryption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Passengers",
                newName: "StreetNumber");

            migrationBuilder.AddColumn<string>(
                name: "StreetNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Passengers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Passengers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StreetNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Passengers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Passengers");

            migrationBuilder.RenameColumn(
                name: "StreetNumber",
                table: "Passengers",
                newName: "FullName");
        }
    }
}
