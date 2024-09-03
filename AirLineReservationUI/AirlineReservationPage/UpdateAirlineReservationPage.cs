using AirlineBAL.Services;
using AirLineReservationUI.AirlineMainPage;
using ConsoleTables;

namespace AirLineReservationUI.AirlineReservationPage
{
    public class UpdateAirlineReservationPage
    {
        private readonly AirlineReservationService _airlineReservationService;
        public static string? pnrNum { get; set; }
        public UpdateAirlineReservationPage()
        {
            _airlineReservationService = new();
        }

        public async Task UpdateAirlineReservation()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Update Airline Reservation Page.\n");

            string? pnrNumber;
            while (true)
            {
                Console.Write("Enter the PNR Number: ");
                pnrNumber = Console.ReadLine() ?? string.Empty;

                var searchTicket = await _airlineReservationService.SearchBookingTicketPNRWise(pnrNumber);
                pnrNum = pnrNumber;
                if (searchTicket.Any())
                {
                    var table = new ConsoleTable("TicketNo", "DateofJourney", "FlightNo", "Source", "Destination", "PassengerName", "Email", "Gender", "ContactNo", "Fair", "Discount", "NetAmount", "PaymentMode", "Status", "PNRNumber");
                    foreach (var item in searchTicket)
                    {
                        table.AddRow(item.TicketNo, item.DateofJourney, item.FlightNo, item.Source, item.Destination, item.PassengerName, item.Email, item.Gender, item.ContactNo, item.Fair, item.Discount, item.NetAmount, item.PaymentMode, item.Status, item.PNRNumber);
                    }
                    Console.WriteLine(table.ToString());

                    await new UpdateAirlineReservationPage().UpdateAirLineReservationMenu();
                }
                else
                {
                    Console.WriteLine($"No Data Found! on this {pnrNumber} PNR number.\n");
                }
                break;
            }
        }

        public async Task UpdateAirLineReservationMenu()
        {
            Console.Write("\nAre you sure to update the records (y/n): ");

            string? input = Console.ReadLine();

            if (input == "y" || input == "Y")
            {
                Console.Clear();
                Console.WriteLine("Welcome to Update Record Page. \n");

                Console.WriteLine("1. Flight No.");
                Console.WriteLine("2. Source");
                Console.WriteLine("3. Destination");
                Console.WriteLine("4. Email");
                Console.WriteLine("5. Contact no.");

                Console.Write("\nPress any key: ");
                _ = int.TryParse(Console.ReadLine(), out int options);

                switch (options)
                {
                    case 1:
                        await new UpdateAirlineMenuPage.UpdateAirlineMenuPage().UpdateAirlineFlightMenu();
                        break;
                    case 2:
                        await new UpdateAirlineMenuPage.UpdateAirlineMenuPage().UpdateAirlineSourceMenu();
                        break;
                    case 3:
                        await new UpdateAirlineMenuPage.UpdateAirlineMenuPage().UpdateAirlineDestinationMenu();
                        break;
                    case 4:
                        await new UpdateAirlineMenuPage.UpdateAirlineMenuPage().UpdateAirlineEmailMenu();
                        break;
                    case 5:
                        await new UpdateAirlineMenuPage.UpdateAirlineMenuPage().UpdateAirlineContactMenu(); 
                        break;
                    default:
                        Console.WriteLine("Invalid options.");
                        Console.WriteLine("Press any key for go back the railway page.");
                        Console.ReadKey();
                        await UpdateAirLineReservationMenu();
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nYou choose n i.e No you will redirect to the User Menu Page.");
                Console.WriteLine("Press any key for go back the User login page.");
                Console.ReadKey();
                await new UserMenuPage().UserMenu();
            }
            
        }

    }
}
