using System.Net.Mail;
using System.Net;
using AirLineShared.Enum;

namespace AirlineBAL.Utiliites
{
    public class SendEmailConfirmation
    {
        private static string _username = "adarsh@gmail.com";
        private static string _password = "qoia wfsy hvka gyrg";

        public static async Task SendConfirmationEmail(string passengerName, string email, string flightNumber, string sourceStation, string destinationStation, DateTime departureDateTime, string PNRNumber, AirlineEnums.PaymentMode paymentMode, AirlineEnums.Status status, AirlineEnums.Gender gender, decimal netAmount, decimal discount, decimal fair, long contactNo)
        {
            // Create HTML email body
            string htmlBody = $@"
            <html>
                <body>
                    <h1>AirLine Reservation Confirmation Mail</h1>
                    <p>Dear <strong>{passengerName}</strong>,</p>
    
                    <p>Thank you for choosing our Indigo AirLine service! Your ticket has been successfully booked. Here are the details of your journey:</p>
    
                    <h2>Journey Details</h2>
                    <p><strong>Flight Number:</strong> {flightNumber}</p>
                    <p><strong>Source Station:</strong> {sourceStation}</p>
                    <p><strong>Destination Station:</strong> {destinationStation}</p>
                    <p><strong>Departure Date & Time:</strong> {departureDateTime}</p>               
                    <p><strong>PNR Number:</strong> {PNRNumber}</p>
    
                    <h2>Passenger Details</h2>
                    <p><strong>Name:</strong> {passengerName}</p>                    
                    <p><strong>Email:</strong> {email}</p>       
                    <p><strong>Gender:</strong> {gender}</p> 
                    <p><strong>ContactNo.:</strong> {contactNo}</p>
    
                    <h2>Booking Summary</h2>
                    <p><strong>Total Amount:</strong> {fair}</p>                 
                    <p><strong>Discount :</strong> {discount}</p>
                    <p><strong>Net Amount:</strong> {netAmount}</p>

    
                    <p>We wish you a pleasant journey!</p>
                    <p>If you have any questions or need assistance, feel free to contact our support team.</p>
    
                    <footer>
                        <p>Thank you for traveling with us!</p>
                        <p><strong>Indigo Airline Services</strong></p>
                        <p>Email: support@indigoairway.com | Phone: +1-800-555-0199</p>
                    </footer>
                </body>
            </html>";

            // Create the email message
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_username);
            mailMessage.To.Add(new MailAddress(email));
            mailMessage.Subject = "Welcome to Indian Airways!";
            mailMessage.Body = htmlBody;
            mailMessage.IsBodyHtml = true;

            // Set up the SMTP client
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(_username, _password),
                EnableSsl = true,
                UseDefaultCredentials = false,
            };

            // send msg
            smtpClient.Send(mailMessage);
                
        }
    }
}
