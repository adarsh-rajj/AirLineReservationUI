using AirlineBAL.Services;
using AirLineReservationUI.AirlineReservationPage;

namespace AirLineReservationUI.AirlineMainPage
{
    public class UserMenuPage
    {
        private readonly AirlineReservationService _airlineReservationService;
        public UserMenuPage()
        {
            _airlineReservationService = new();
        }
        public async Task UserMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to User Menu Page\n");

            Console.WriteLine("1. Add.");
            Console.WriteLine("2. Update.");
            Console.WriteLine("3. View All Records");
            Console.WriteLine("4. Search Based on Selection.");
            Console.WriteLine("5. Delete.");
            Console.WriteLine("6. Back to User Menu.");

            Console.Write("\nChoose the options: ");
            _ = int.TryParse(Console.ReadLine(), out int options);

            switch (options)
            {
                case 1:
                    await new AddAirlineReservationPage().AddAirlineReservation();
                    await UserMenu();
                    break;
                case 2:
                    await new UpdateAirlineReservationPage().UpdateAirlineReservation();
                    Console.WriteLine("Press any key for reback the User Menu Page.");
                    Console.ReadKey();
                    await UserMenu();
                    break;
                case 3:
                    await new ViewAllRecordsPage().ViewAllRecords();    
                    await UserMenu();
                    break;
                case 4:
                    await new SearchAirlineReservationPage().SearchAirlineReservation();
                    Console.WriteLine("Press any key for reback the  User Menu Page.");
                    Console.ReadKey();
                    await UserMenu();
                    break;
                case 5:
                    await new DeleteRecordByPNRPage().DeleteRecordByPNR();
                    Console.WriteLine("Press any key for reback the  User Menu Page.");
                    Console.ReadKey();
                    await UserMenu();
                    break;
                case 6:
                    await new PublicMenuPage().PublicMenu();
                    break;
                default:
                    Console.WriteLine("\nInvalid option! Enter valid options.");
                    Console.WriteLine("Press any key for reback the  User Menu Page.");
                    Console.ReadKey();
                    await UserMenu();
                    break;
            }

        }
    }
}
