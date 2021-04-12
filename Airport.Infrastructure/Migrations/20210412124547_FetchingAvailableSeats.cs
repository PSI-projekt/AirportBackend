using Microsoft.EntityFrameworkCore.Migrations;

namespace Airport.Infrastructure.Migrations
{
    public partial class FetchingAvailableSeats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Flights_FlightId1",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Passengers_UserBookings_UserBookingUserId_UserBookingBookingId",
                table: "Passengers");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Bookings_BookingId",
                table: "Payment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBookings",
                table: "UserBookings");

            migrationBuilder.DropIndex(
                name: "IX_Passengers_UserBookingUserId_UserBookingBookingId",
                table: "Passengers");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_FlightId1",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payment",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "UserBookingBookingId",
                table: "Passengers");

            migrationBuilder.DropColumn(
                name: "UserBookingUserId",
                table: "Passengers");

            migrationBuilder.DropColumn(
                name: "FlightId1",
                table: "Bookings");

            migrationBuilder.RenameTable(
                name: "Payment",
                newName: "Payments");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_BookingId",
                table: "Payments",
                newName: "IX_Payments_BookingId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserBookings",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPassengers",
                table: "UserBookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "UserBookingId",
                table: "Passengers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FlightId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfSeats",
                table: "Airplanes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBookings",
                table: "UserBookings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserBookings_UserId",
                table: "UserBookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_UserBookingId",
                table: "Passengers",
                column: "UserBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FlightId",
                table: "Bookings",
                column: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Flights_FlightId",
                table: "Bookings",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Passengers_UserBookings_UserBookingId",
                table: "Passengers",
                column: "UserBookingId",
                principalTable: "UserBookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Bookings_BookingId",
                table: "Payments",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Flights_FlightId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Passengers_UserBookings_UserBookingId",
                table: "Passengers");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Bookings_BookingId",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBookings",
                table: "UserBookings");

            migrationBuilder.DropIndex(
                name: "IX_UserBookings_UserId",
                table: "UserBookings");

            migrationBuilder.DropIndex(
                name: "IX_Passengers_UserBookingId",
                table: "Passengers");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_FlightId",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserBookings");

            migrationBuilder.DropColumn(
                name: "NumberOfPassengers",
                table: "UserBookings");

            migrationBuilder.DropColumn(
                name: "NumberOfSeats",
                table: "Airplanes");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "Payment");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_BookingId",
                table: "Payment",
                newName: "IX_Payment_BookingId");

            migrationBuilder.AlterColumn<string>(
                name: "UserBookingId",
                table: "Passengers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserBookingBookingId",
                table: "Passengers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserBookingUserId",
                table: "Passengers",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FlightId",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "FlightId1",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBookings",
                table: "UserBookings",
                columns: new[] { "UserId", "BookingId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payment",
                table: "Payment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_UserBookingUserId_UserBookingBookingId",
                table: "Passengers",
                columns: new[] { "UserBookingUserId", "UserBookingBookingId" });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FlightId1",
                table: "Bookings",
                column: "FlightId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Flights_FlightId1",
                table: "Bookings",
                column: "FlightId1",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Passengers_UserBookings_UserBookingUserId_UserBookingBookingId",
                table: "Passengers",
                columns: new[] { "UserBookingUserId", "UserBookingBookingId" },
                principalTable: "UserBookings",
                principalColumns: new[] { "UserId", "BookingId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Bookings_BookingId",
                table: "Payment",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
