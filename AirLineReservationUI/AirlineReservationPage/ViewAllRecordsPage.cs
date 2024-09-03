using AirlineBAL.Services;
using ConsoleTables;

namespace AirLineReservationUI.AirlineReservationPage
{
    public class ViewAllRecordsPage
    {
        private readonly AirlineReservationService _airlineReservationService;
        public ViewAllRecordsPage()
        {
            _airlineReservationService = new();
        }

        public async Task ViewAllRecords()
        {
            var allRecords = await _airlineReservationService.ViewAllRecordService();
            var table = new ConsoleTable("TicketNo", "DateofJourney", "FlightNo", "Source", "Destination", "PassengerName", "Email", "Gender", "ContactNo", "Fair", "Discount", "NetAmount", "PaymentMode", "Status", "PNRNumber");
            foreach (var item in allRecords)
            {
                table.AddRow(item.TicketNo, item.DateofJourney, item.FlightNo, item.Source, item.Destination, item.PassengerName, item.Email, item.Gender, item.ContactNo, item.Fair, item.Discount, item.NetAmount, item.PaymentMode, item.Status, item.PNRNumber);
            }
            Console.WriteLine(table.ToString());

            Console.WriteLine("Press any key for go back to Railway Page.");
            Console.ReadKey();
        }
    }
}
