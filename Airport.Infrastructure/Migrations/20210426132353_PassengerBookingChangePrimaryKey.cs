using Microsoft.EntityFrameworkCore.Migrations;

namespace Airport.Infrastructure.Migrations
{
    public partial class PassengerBookingChangePrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PassengerBookings",
                table: "PassengerBookings");

            migrationBuilder.DropIndex(
                name: "IX_PassengerBookings_BookingId",
                table: "PassengerBookings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PassengerBookings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PassengerBookings",
                table: "PassengerBookings",
                columns: new[] { "BookingId", "PassengerId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PassengerBookings",
                table: "PassengerBookings");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PassengerBookings",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PassengerBookings",
                table: "PassengerBookings",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PassengerBookings_BookingId",
                table: "PassengerBookings",
                column: "BookingId");
        }
    }
}
