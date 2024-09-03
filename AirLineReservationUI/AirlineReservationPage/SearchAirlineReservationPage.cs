

namespace AirLineReservationUI.AirlineReservationPage
{
    public class SearchAirlineReservationPage
    {
        public async Task SearchAirlineReservation()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Search Airline Reservation Page. \n");

            Console.WriteLine("1. DateOfJourney and FlightNo wise   ");
            Console.WriteLine("2. Source and Destination");

            Console.Write("\nPress any key: ");
            _ = int.TryParse(Console.ReadLine(), out int options);

            switch (options)
            {
                case 1:
                    await new SearchAirlineMenuPage.SearchAirlineMenuPage().SearchAirlineDateFlightWise();
                    break;
                case 2:
                     await new SearchAirlineMenuPage.SearchAirlineMenuPage().SearchAirlineSourceDestinationWise();
                    break;
                default:
                    Console.WriteLine("Invalid options.");
                    Console.WriteLine("Press any key for go back the Search Airline Page.");
                    Console.ReadKey();
                    await new SearchAirlineReservationPage().SearchAirlineReservation();
                    break;
            }
        }
    }
}
