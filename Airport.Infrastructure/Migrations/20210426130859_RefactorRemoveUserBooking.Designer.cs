﻿// <auto-generated />
using System;
using Airport.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Airport.Infrastructure.Migrations
{
    [DbContext(typeof(AirportDbContext))]
    [Migration("20210426130859_RefactorRemoveUserBooking")]
    partial class RefactorRemoveUserBooking
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Airport.Domain.Models.Airplane", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Airline")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Identifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsInRepair")
                        .HasColumnType("bit");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Maker")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfSeats")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Airplanes");
                });

            modelBuilder.Entity("Airport.Domain.Models.AirportEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodeIATA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("Airport.Domain.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBooking")
                        .HasColumnType("datetime2");

                    b.Property<int>("FlightId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("bit");

                    b.Property<int>("NumberOfPassengers")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FlightId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("Airport.Domain.Models.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AirplaneId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfArrival")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfDeparture")
                        .HasColumnType("datetime2");

                    b.Property<int>("DestinationId")
                        .HasColumnType("int");

                    b.Property<string>("FlightNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OriginId")
                        .HasColumnType("int");

                    b.Property<double>("PricePerPassenger")
                        .HasColumnType("float");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AirplaneId");

                    b.HasIndex("DestinationId");

                    b.HasIndex("OriginId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("Airport.Domain.Models.Passenger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IDNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Passengers");
                });

            modelBuilder.Entity("Airport.Domain.Models.PassengerBooking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<int>("PassengerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookingId");

                    b.HasIndex("PassengerId");

                    b.ToTable("PassengerBookings");
                });

            modelBuilder.Entity("Airport.Domain.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<string>("ReferenceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BookingId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Airport.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IDNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PostCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Privileges")
                        .HasColumnType("int");

                    b.Property<string>("RegistrationToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistrationTokenGeneratedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Airport.Domain.Models.Airplane", b =>
                {
                    b.HasOne("Airport.Domain.Models.AirportEntity", "Location")
                        .WithMany("Airplanes")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("Airport.Domain.Models.Booking", b =>
                {
                    b.HasOne("Airport.Domain.Models.Flight", "Flight")
                        .WithMany("Bookings")
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Airport.Domain.Models.User", null)
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("Airport.Domain.Models.Flight", b =>
                {
                    b.HasOne("Airport.Domain.Models.Airplane", "Airplane")
                        .WithMany("Flights")
                        .HasForeignKey("AirplaneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Airport.Domain.Models.AirportEntity", "Destination")
                        .WithMany("Destinations")
                        .HasForeignKey("DestinationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Airport.Domain.Models.AirportEntity", "Origin")
                        .WithMany("Origins")
                        .HasForeignKey("OriginId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Airplane");

                    b.Navigation("Destination");

                    b.Navigation("Origin");
                });

            modelBuilder.Entity("Airport.Domain.Models.PassengerBooking", b =>
                {
                    b.HasOne("Airport.Domain.Models.Booking", "Booking")
                        .WithMany("PassengerBookings")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Airport.Domain.Models.Passenger", "Passenger")
                        .WithMany("PassengerBookings")
                        .HasForeignKey("PassengerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");

                    b.Navigation("Passenger");
                });

            modelBuilder.Entity("Airport.Domain.Models.Payment", b =>
                {
                    b.HasOne("Airport.Domain.Models.Booking", "Booking")
                        .WithMany("Payments")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");
                });

            modelBuilder.Entity("Airport.Domain.Models.Airplane", b =>
                {
                    b.Navigation("Flights");
                });

            modelBuilder.Entity("Airport.Domain.Models.AirportEntity", b =>
                {
                    b.Navigation("Airplanes");

                    b.Navigation("Destinations");

                    b.Navigation("Origins");
                });

            modelBuilder.Entity("Airport.Domain.Models.Booking", b =>
                {
                    b.Navigation("PassengerBookings");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Airport.Domain.Models.Flight", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("Airport.Domain.Models.Passenger", b =>
                {
                    b.Navigation("PassengerBookings");
                });

            modelBuilder.Entity("Airport.Domain.Models.User", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
