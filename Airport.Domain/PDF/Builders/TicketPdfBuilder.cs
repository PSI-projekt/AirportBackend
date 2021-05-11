using System.Collections.Generic;
using System.IO;
using Airport.Domain.DTOs;
using Airport.Domain.Models;
using PdfSharpCore;
using VetCV.HtmlRendererCore.PdfSharpCore;

namespace Airport.Domain.PDF.Builders
{
    public static class TicketPdfBuilder
    {
        public static byte[] BuildTicketPdf(IEnumerable<PassengerForEditDto> passengers, Booking booking, FlightDetailsDto flight, User user)
        {
            var data = "<h2>Opole Airport reservation</h2>";
            data += $"Customer: {user.FirstName} {user.LastName}<br/>";
            data += $"Number of passengers: {booking.NumberOfPassengers}.<br/>";
            data += $"Flight from {flight.Origin.City}, " +
                    $"departure: {flight.DateOfDeparture:dddd, dd MMMM yyyy, HH:mm} UTC " +
                    $"to {flight.Destination.City}, " +
                    $"arrival: {flight.DateOfArrival:dddd, dd MMMM yyyy, HH:mm} UTC<br/>";
            data += $"Passengers for booking number {booking.Id}: <br/>";
            foreach (var passenger in passengers)
            {
                data += "<hr>";
                data += $"Name <b>{passenger.FirstName}</b>, Surname: <b>{passenger.LastName}</b>, " +
                        $"PESEL/Passport no.: <b>{passenger.IDNumber}</b> <br/>";
                data += $"Address: <b>{passenger.Street} {passenger.StreetNumber}</b>, City: <b>{passenger.City}</b>, " +
                        $"Post code: <b>{passenger.PostCode}</b>, Country: <b>{passenger.Country}</b><br/>";
            }

            using var ms = new MemoryStream();
            var pdf = PdfGenerator.GeneratePdf(data, PageSize.A4);
            pdf.Save(ms);
            return ms.ToArray();
        }
    }
}