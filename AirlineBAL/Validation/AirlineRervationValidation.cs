using Azure;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace AirlineBAL.Validation
{
    public class AirlineRervationValidation
    {

        public static bool IsValidDate(string input, out DateTime dateOfJourney)
        {
            if (DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfJourney))
            {
                DateTime today = DateTime.Now.Date; 
                if (dateOfJourney >= today)
                {
                    return true; 
                }
            }

            dateOfJourney = DateTime.MinValue;
            return false;
        }

        public static string ValidateFlightNumber(string flightNumber)
        {
            if (string.IsNullOrEmpty(flightNumber))
            {
                return "Flight Number should not be empty.\n";
            }
            else
            {
                return "";
            }
        }

        public static string ValidateSource(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return "Source should not be empty.\n";
            }
            else
            {
                return "";
            }
        }
        public static string ValidateDestination(string source, string destination)
        {
            if (string.IsNullOrEmpty(destination))
            {
                return "destination should not be empty.\n";
            }
            else if(destination.ToUpper() == source.ToUpper())
            {
                return "Source and destination not be same.\n";
            }
            else
            {
                return "";
            }
        }

        public static string ValidatePassengerName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "Name should not be empty.\n";
            }
            else
            {
                return "";
            }
        }

        public static string ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return "Email should not be empty.\n";
            }
            else if (!Regex.IsMatch(email, "^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$"))
            {
                return "Email should not be inValid.\n";
            }
            else
            {
                return "";
            }
        }

        public static bool ValidateGender(string gender)
        {
            if(gender == "f" || gender == "F" || gender == "m" || gender == "M")
            {
                return true; 
            }
            else { return false; }
        }

        public static bool IsValidContactNumber(long contact)
        {
            string contactNumber = contact.ToString();
            if (contactNumber.IsNullOrEmpty())
            {
                return false;
            }
            else if (Regex.IsMatch(contactNumber, "^[0-9]{10}$"))
            {
                return true;
            }
            return false;
        }

        public static bool ValidateFair(decimal fair)
        {
            if (fair < 1500 || fair > 5000 || fair == 0)
            {
                return false;
            }
            else { return true; }
        }

        public static bool ValidatePaymentMode(string paymentMode)
        {
            if (paymentMode == "CC" || paymentMode == "cc" || paymentMode == "C" || paymentMode == "c" || paymentMode == "G" || paymentMode == "g")
            {
                return true;
            }
            else { return false; }
        }

        public static bool ValidateStatus(string status)
        {
            if (status == "B" || status == "b" || status == "C" || status == "c")
            {
                return true;
            }
            else { return false; }
        }
    }
}
