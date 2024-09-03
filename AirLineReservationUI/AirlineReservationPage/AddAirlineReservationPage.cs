using AirlineBAL.DTOs;
using AirlineBAL.Services;
using AirlineBAL.Validation;
using static AirLineShared.Enum.AirlineEnums;

namespace AirLineReservationUI.AirlineReservationPage
{
    public class AddAirlineReservationPage
    {
        private readonly AirlineReservationService _airlineReservationService;
        public AddAirlineReservationPage()
        {
            _airlineReservationService = new();
        }

        public async Task AddAirlineReservation()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Add Airline Reservaiton Page.\n");


            DateTime dateofJourney;
            while (true)
            {
                Console.Write("Enter the DateOfJourney - (DD/MM/YYYY) Formate: ");
                string input = Console.ReadLine() ?? string.Empty;

                if (AirlineRervationValidation.IsValidDate(input, out dateofJourney))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a valid date in (DD/MM/YYYY) Formate.\n");
                }
            }

            string? flightNumber;
            while (true)
            {
                Console.Write("Enter the flight Number: ");
                flightNumber = Console.ReadLine() ?? string.Empty;
                string isValidFlightNo = AirlineRervationValidation.ValidateFlightNumber(flightNumber);
                if (isValidFlightNo.Length == 0) { break; }
                else { Console.WriteLine(isValidFlightNo); }
            }

            string? source;
            while (true)
            {
                Console.Write("Enter the Source: ");
                source = Console.ReadLine() ?? string.Empty;
                string isValidSource = AirlineRervationValidation.ValidateSource(source);
                if (isValidSource.Length == 0) { break; }
                else { Console.WriteLine(isValidSource); }
            }

            string? destination;
            while (true)
            {
                Console.Write("Enter the destination: ");
                destination = Console.ReadLine() ?? string.Empty;
                string isValidDestination = AirlineRervationValidation.ValidateDestination(source, destination);
                if (isValidDestination.Length == 0) { break; }
                else { Console.WriteLine(isValidDestination); }
            }

            string? passengerName;
            while (true)
            {
                Console.Write("Enter the passangerName: ");
                passengerName = Console.ReadLine() ?? string.Empty;
                string isValid = AirlineRervationValidation.ValidatePassengerName(passengerName);
                if (isValid.Length == 0) { break; }
                else { Console.WriteLine(isValid); }
            }

            string? email;
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

            Gender genderOutput;
            Dictionary<string, Gender> genderKeyValuePairs = new Dictionary<string, Gender>()
            {
                {"M", Gender.Male},
                {"F", Gender.Female },
            };
            while (true)
            {
                Console.WriteLine("Enter Gender [Male: M, Female: F]");
                string? gender = Console.ReadLine() ?? string.Empty;
                bool isValid = AirlineRervationValidation.ValidateGender(gender);
                if (isValid)
                {
                    Gender key;
                    if (genderKeyValuePairs.TryGetValue(gender.ToUpper(), out key))
                    {
                        genderOutput = key;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Enter correct Option.\n");
                    }
                }
                else { Console.WriteLine("Invalid Option!\n"); }
            }

            long contactNo;
            while (true)
            {
                Console.Write("Enter the phoneNumber: ");
                long.TryParse(Console.ReadLine(), out contactNo);
                bool isValid = AirlineRervationValidation.IsValidContactNumber(contactNo);
                if (isValid) { break; }
                else { Console.WriteLine("Phone Number must have 10 digits numeric only.\n"); }
            }

            decimal fair;
            while (true)
            {
                Console.Write("Enter the Fair: ");
                decimal.TryParse(Console.ReadLine(), out fair); 
                bool isValidFair = AirlineRervationValidation.ValidateFair(fair);
                if (isValidFair) { break; }
                else { Console.WriteLine("Fair must be between 1500 to 5000.\n"); }
            }

            PaymentMode paymentModeOutput;
            Dictionary<string, PaymentMode> paymentKeyValuePairs = new Dictionary<string, PaymentMode>()
            {
                {"CC", PaymentMode.CreditCard },
                {"C", PaymentMode.Cash },
                {"G", PaymentMode.GooglePay },
            };
            while (true)
            {
                Console.WriteLine("Enter Payment Mode [CreditCard: CC, Cash: C, GooglePay: G]");
                string? paymentMode = Console.ReadLine() ?? string.Empty;
                bool isValid = AirlineRervationValidation.ValidatePaymentMode(paymentMode);
                if (isValid)
                {
                    PaymentMode key;
                    if (paymentKeyValuePairs.TryGetValue(paymentMode.ToUpper(), out key))
                    {
                        paymentModeOutput = key;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Enter correct Option.\n");
                    }
                }
                else { Console.WriteLine("Enter Correct Payment Mode Option.\n"); }
            }

            Status statusOutput;
            Dictionary<string, Status> statusKeyValuePairs = new Dictionary<string, Status>()
            {
                { "C", Status.Canceled },
                { "B", Status.Booked },
            };
            while (true)
            {
                Console.WriteLine("Enter Status [Booked: B, Canceled: C]");
                string? status = Console.ReadLine() ?? string.Empty;
                bool isValid = AirlineRervationValidation.ValidateStatus(status);
                if (isValid)
                {
                    Status key;
                    if (statusKeyValuePairs.TryGetValue(status.ToUpper(), out key))
                    {
                        statusOutput = key;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Enter correct option.\n");
                    }
                }
                else { Console.WriteLine("Enter the correct Status option.\n"); }
            }


            var airlineResult = new AirlineReservationDtos()
            {
                DateofJourney = dateofJourney,
                FlightNo = flightNumber,
                Source = source,
                Destination = destination,
                PassengerName = passengerName,
                Email = email,
                Gender = genderOutput,
                ContactNo = contactNo,
                Fair = fair,
                PaymentMode = paymentModeOutput,
                Status = statusOutput,
            };

            await _airlineReservationService.AddAirlineReservationServices(airlineResult);

            Console.WriteLine("Ticket is booked successfully.");
            Console.WriteLine("\nEmail sent succesfully to the customer.");
            Console.WriteLine("Press any key for continue");
            Console.ReadKey();

        }
    }
}
