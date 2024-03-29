﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Airport.Domain.Models;

namespace Airport.Infrastructure.Persistence.DbSeed
{
    public class Seed
    {
        private readonly AirportDbContext _context;
        public Seed(AirportDbContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            SeedUsers();
            SeedAirports();
            SeedAirplanes();
            SeedFlights();
            SeedBookings();
            SeedPassengers();
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private void SeedUsers()
        {
            if (!_context.Users.Any())
            {
                var userData = File
                    .ReadAllText("../Airport.Infrastructure/Persistence/DbSeed/UserData.json");
                var users = JsonSerializer.Deserialize<List<User>>(userData);
                foreach (var user in users)
                {
                    CreatePasswordHash("password", out var passwordHash, out var passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.IsConfirmed = true;
                    _context.Users.Add(user);
                }
                _context.SaveChanges();
            }
        }

        private void SeedAirports()
        {
            if (!_context.Airports.Any())
            {
                var airportData = File
                    .ReadAllText("../Airport.Infrastructure/Persistence/DbSeed/AirportData.json");
                var airports = JsonSerializer.Deserialize<List<AirportEntity>>(airportData);
                foreach (var airport in airports)
                {
                    _context.Airports.Add(airport);
                }
                _context.SaveChanges();
            }
        }
      
        private void SeedAirplanes()
        {
            if (!_context.Airplanes.Any())
            {
                var airplaneData = File
                    .ReadAllText("../Airport.Infrastructure/Persistence/DbSeed/AirplaneData.json");
                var airplanes = JsonSerializer.Deserialize<List<Airplane>>(airplaneData);
                foreach (var airplane in airplanes)
                {
                    airplane.IsInRepair = false;
                    _context.Airplanes.Add(airplane);
                }
                _context.SaveChanges();
            }
        }

        private void SeedFlights()
        {
            if (!_context.Flights.Any())
            {
                var flightData = File
                    .ReadAllText("../Airport.Infrastructure/Persistence/DbSeed/FlightData.json");
                var flights = JsonSerializer.Deserialize<List<Flight>>(flightData);
                Random rnd = new Random();
                foreach (var flight in flights)
                {

                    DateTime date = DateTime.UtcNow;
                    flight.DateOfDeparture = date.AddHours(rnd.Next(1, 10));;
                    flight.DateOfArrival = flight.DateOfDeparture.AddHours(rnd.Next(1, 4));
                    flight.FlightNumber = rnd.Next(1000, 9999).ToString();
                    _context.Flights.Add(flight);
                }
                _context.SaveChanges();
            }
        }
        
        
        private void SeedBookings()
        {
            if (!_context.Bookings.Any())
            {
                var bookingData = File
                    .ReadAllText("../Airport.Infrastructure/Persistence/DbSeed/BookingData.json");
                var bookings = JsonSerializer.Deserialize<List<Booking>>(bookingData);
                foreach (var booking in bookings)
                {
                    _context.Bookings.Add(booking);
                }
                _context.SaveChanges();
            }
        }
        
        private void SeedPassengers()
        {
            for (int i = 1; i <= 4; i++)
            {
                _context.Passengers.Add(new Passenger
                {
                    IDNumber = (10 % i).ToString(),
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    City = "Opole",
                    Street = "Ozimska",
                    StreetNumber = "2",
                    Country = "Poland",
                    PostCode = "23-432"
                });
                _context.SaveChanges();
            
                _context.PassengerBookings.Add(new PassengerBooking
                {
                    BookingId = i,
                    PassengerId = i,
                });
            }
            _context.SaveChanges();
        }
    }
}