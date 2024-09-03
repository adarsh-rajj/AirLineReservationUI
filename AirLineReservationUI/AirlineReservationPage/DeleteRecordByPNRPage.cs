using AirlineBAL.Services;
using AirLineReservationUI.AirlineMainPage;

namespace AirLineReservationUI.AirlineReservationPage
{
    public class DeleteRecordByPNRPage
    {
        private readonly AirlineReservationService _airlineReservationService;
        public DeleteRecordByPNRPage()
        {
            _airlineReservationService = new();
        }
        public async Task DeleteRecordByPNR()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Delete Record by Pnr Page.\n");

            while (true)
            {
                try
                {
                    Console.Write("Enter the PNR Number: ");
                    string pnrNumber = Console.ReadLine() ?? string.Empty;

                    Console.Write("\nAre you sure to update the records (y/n): ");
                    string? input = Console.ReadLine();

                    if (input == "y" || input == "Y")
                    {
                        await _airlineReservationService.DeleteRecordByPNRService(pnrNumber);
                        Console.WriteLine("Records deleted successfully.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nYou choose n i.e No you will redirect to the User Menu Page.");
                        Console.WriteLine("Press any key for go back the user login page.");
                        Console.ReadKey();
                        await new UserMenuPage().UserMenu();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid PNR Number: " + ex.Message + "\n");
                }
            }

        }
    }
}

