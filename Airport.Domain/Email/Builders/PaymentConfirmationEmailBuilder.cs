using MimeKit;

namespace Airport.Domain.Email.Builders
{
    public static class PaymentConfirmationEmailBuilder
    {
        public static EmailMessage BuildPaymentConfirmationMessage(string receiver, string username, string origin, string destination)
        {
            var messageBody = $"Hello {username}!<br/>";
            messageBody += $"Payment for your flight from {origin} to {destination} has been confirmed.<br/>";
            messageBody += "You can check the details on our website in Your reservation history.";
            
            return new EmailMessage
            {
                Receiver = new MailboxAddress(username, receiver),
                Subject = "Booking status update, OpoleAirport",
                Content = messageBody
            };
        }
    }
}