using Microsoft.EntityFrameworkCore.Migrations;

namespace Airport.Infrastructure.Migrations
{
    public partial class BookingCancelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAccepted",
                table: "Bookings",
                newName: "IsCancelled");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCancelled",
                table: "Bookings",
                newName: "IsAccepted");
        }
    }
}
