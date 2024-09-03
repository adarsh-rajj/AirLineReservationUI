using AirlineBAL.Services;
using AirlineBAL.Validation;
using AirLineReservationUI.AirlineReservationPage;

namespace AirLineReservationUI.AirlineReservationPage.UpdateAirlineMenuPage
{
    public class UpdateAirlineMenuPage
    {
        private readonly AirlineReservationService _airlineReservationService;
        public UpdateAirlineMenuPage()
        {
            _airlineReservationService = new AirlineReservationService();
        }

        public async Task UpdateAirlineFlightMenu()
        {
            string? flightNumber;
            string? pnrNumber = UpdateAirlineReservationPage.pnrNum ?? string.Empty;
            while (true)
            {
                Console.Write("Enter the flight Number: ");
                flightNumber = Console.ReadLine() ?? string.Empty;
                string isValidFlightNo = AirlineRervationValidation.ValidateFlightNumber(flightNumber);
                if (isValidFlightNo.Length == 0) { break; }
                else { Console.WriteLine(isValidFlightNo); }
            }

            await _airlineReservationService.EditFlightNumberServices(pnrNumber, flightNumber);
            Console.WriteLine($"\nCustomer Flight Number {flightNumber} have been Updated successfully.");
             
        }
        
        string? source;
        public async Task UpdateAirlineSourceMenu()
        {
            string? pnrNumber = UpdateAirlineReservationPage.pnrNum ?? string.Empty;
            while (true)
            {
                Console.Write("Enter the Source: ");
                source = Console.ReadLine() ?? string.Empty;
                string isValidSource = AirlineRervationValidation.ValidateSource(source);
                if (isValidSource.Length == 0) { break; }
                else { Console.WriteLine(isValidSource); }
            }

            await _airlineReservationService.EditSourceServices(pnrNumber, source);
            Console.WriteLine($"\nCustomer source {source} have been Updated successfully.");
             
        }

        public async Task UpdateAirlineDestinationMenu()
        {
            string? destination;
            string? pnrNumber = UpdateAirlineReservationPage.pnrNum ?? string.Empty;
            while (true)
            {
                Console.Write("Enter the destination: ");
                destination = Console.ReadLine() ?? string.Empty;
                string isValidDestination = AirlineRervationValidation.ValidateDestination(source, destination);
                if (isValidDestination.Length == 0) { break; }
                else { Console.WriteLine(isValidDestination); }
            }

            await _airlineReservationService.EditDestinationServices(pnrNumber, destination);
            Console.WriteLine($"\nCustomer destination {destination} have been Updated successfully.");
             
        }
        public async Task UpdateAirlineEmailMenu()
        {
            string? email;
            string? pnrNumber = UpdateAirlineReservationPage.pnrNum ?? string.Empty;
            while (true)
            {
                Console.Write("Enter the Email address: ");
                email = Console.ReadLine() ?? string.Empty;
                string isValid = AirlineRervationValidation.ValidateEmail(email);
                if (isValid.Length == 0)
                {
                    bool isEmailExitToday = await _airlineReservationService.CheckPasswordExistService(email);
                    if (!isEmailExitToday)
                    {
                        break;
                    }
                    else { Console.WriteLine("Only one ticket can be booked per email address per day.\n"); }
                }
                else { Console.WriteLine(isValid); }
            }
            await _airlineReservationService.EditEmailServices(pnrNumber, email);
            Console.WriteLine($"\nCustomer email {email} have been Updated successfully.");
             
        }
        public async Task UpdateAirlineContactMenu()
        {
            string? pnrNumber = UpdateAirlineReservationPage.pnrNum ?? string.Empty;
            long contactNo;
            while (true)
            {
                Console.Write("Enter the phoneNumber: ");
                long.TryParse(Console.ReadLine(), out contactNo);
                bool isValid = AirlineRervationValidation.IsValidContactNumber(contactNo);
                if (isValid) { break; }
                else { Console.WriteLine("Phone Number must have 10 digits numeric only.\n"); }

                await _airlineReservationService.EditContactServices(pnrNumber, contactNo);
                Console.WriteLine($"\nCustomer contact {contactNo} have been Updated successfully.");
                 
            }
        }
    }
}
