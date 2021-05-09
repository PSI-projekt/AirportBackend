using Airport.Domain.DTOs;
using MimeKit;

namespace Airport.Domain.Email.Builders
{
    public static class PaymentInformationEmailBuilder
    {
        public static EmailMessage BuildPaymentInformationMessage(string receiver, string username,
            string referenceNumber, string iban, string bic, double amount, FlightDetailsDto flightDetails)
        {
            var messageBody = $"Hello {username}!<br/>";
            messageBody += "Thank you for choosing OpoleAirport! <br/>";
            messageBody += "Your flight is now booked. Before making payement, please check if your flight details are correct. You can find them below.<br/><br/>";

            messageBody += $"Origin: <b>{flightDetails.Origin.City}</b><br/>";
            messageBody += $"Destination: <b>{flightDetails.Destination.City}</b><br/>";
            messageBody += $"Date of arrival: <b>{flightDetails.DateOfArrival}</b><br/>";
            messageBody += $"Date of arrival: <b>{flightDetails.DateOfDeparture}</b><br/>";
            messageBody += $"Airplane: <b>{flightDetails.Airplane.Maker} {flightDetails.Airplane.Model}</b><br/>";
            messageBody += $"Flight number: <b>{flightDetails.FlightNumber}</b><br/><br/>";

            messageBody += $"If your flight details are correct, please make a payment to the account given below.<br/><br/>";
            messageBody += $"IBAN: <b>{iban}</b><br/>";
            messageBody += $"BIC: <b>{bic}</b><br/>";
            messageBody += $"Title of payment: <b>{referenceNumber}</b><br/>";
            messageBody += $"Amount: <b>{amount}</b><br/><br/>";

            messageBody += "We will send you another email with payment confirmation after your transfer.<br/>";
            messageBody += "You can check the details on our website in Your reservation history.";
            return new EmailMessage
            {
                Receiver = new MailboxAddress(username, receiver),
                Subject = "Payment details for flight, OpoleAirport",
                Content = messageBody
            };
        }
    }
}