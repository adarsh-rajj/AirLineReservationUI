using AirLineReservationUI.AirlineMainPage;

namespace AirLineReservationUI
{
    public class Program
    {
        static async Task Main(string[] args)
        {
             await new PublicMenuPage().PublicMenu();
        }
    }
}

