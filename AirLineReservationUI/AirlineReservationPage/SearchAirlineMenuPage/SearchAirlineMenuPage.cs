using AirlineBAL.Services;
using AirlineBAL.Validation;
using ConsoleTables;
using System;

namespace AirLineReservationUI.AirlineReservationPage.SearchAirlineMenuPage
{
    public class SearchAirlineMenuPage
    {
        private readonly AirlineReservationService _airlineReservationService;
        public SearchAirlineMenuPage()
        {
            _airlineReservationService = new();
        }

        public async Task SearchAirlineDateFlightWise()
        {
            string? flightNo;
            DateTime dateofJourney;
            while (true)
            {
                Console.Write("Enter the Flight Number: ");
                flightNo = Console.ReadLine();

                if (flightNo != null && flightNo != "" )
                {
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

                    var searchRecord = await _airlineReservationService.SearchRecordFlightContactWise(flightNo, dateofJourney);
                    if (searchRecord.Any())
                    {
                        var table = new ConsoleTable("TicketNo", "DateofJourney", "FlightNo", "Source", "Destination", "PassengerName", "Email", "Gender", "ContactNo", "Fair", "Discount", "NetAmount", "PaymentMode", "Status", "PNRNumber");
                        foreach (var item in searchRecord)
                        {
                            table.AddRow(item.TicketNo, item.DateofJourney, item.FlightNo, item.Source, item.Destination, item.PassengerName, item.Email, item.Gender, item.ContactNo, item.Fair, item.Discount, item.NetAmount, item.PaymentMode, item.Status, item.PNRNumber);
                        }
                        Console.WriteLine(table.ToString());

                        break;
                    }
                    else
                    {
                        Console.WriteLine($"No Data Found! on this {flightNo} and {dateofJourney}.\n");
                    }
                    break;
                }
                else { Console.WriteLine("Flight no. should not be empty.\n"); }
            }
        }

        public async Task SearchAirlineSourceDestinationWise()
        {
            string? source;
            string? destination;
            while (true)
            {
                Console.Write("Enter the source: ");
                source = Console.ReadLine();
              
                if (source != null && source != "")
                {
                    while (true)
                    {
                        Console.Write("Enter the destination: ");
                        destination = Console.ReadLine() ?? string.Empty;
                        string isValidDestination = AirlineRervationValidation.ValidateDestination(source, destination);
                        if (isValidDestination.Length == 0) { break; }
                        else { Console.WriteLine(isValidDestination); }
                    }

                    var searchRecord = await _airlineReservationService.SearchRecordSourceDestinationWise(source, destination);
                    if (searchRecord.Any())
                    {
                        var table = new ConsoleTable("TicketNo", "DateofJourney", "FlightNo", "Source", "Destination", "PassengerName", "Email", "Gender", "ContactNo", "Fair", "Discount", "NetAmount", "PaymentMode", "Status", "PNRNumber");
                        foreach (var item in searchRecord)
                        {
                            table.AddRow(item.TicketNo, item.DateofJourney, item.FlightNo, item.Source, item.Destination, item.PassengerName, item.Email, item.Gender, item.ContactNo, item.Fair, item.Discount, item.NetAmount, item.PaymentMode, item.Status, item.PNRNumber);
                        }
                        Console.WriteLine(table.ToString());

                        break;
                    }
                    else
                    {
                        Console.WriteLine($"No Data Found! on this {source} and {destination}.\n");
                    }
                   
                }
                else { Console.WriteLine("Source Should not be empty.\n"); }
            }
        }
    }
}
