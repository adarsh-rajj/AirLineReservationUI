namespace AirLineReservationUI.AirlineMainPage
{
    public class PublicMenuPage
    {
        public async Task PublicMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Indian AirLine Reservation Portal.\n");

            Console.WriteLine("Press 1 for Login.");
            Console.WriteLine("Press 2 for Quit.");

            Console.Write("\nPress the option: ");
            _ = int.TryParse(Console.ReadLine(), out int options);

            switch (options)
            {
                case 1:
                    await new UserLoginPage().UserLogin();
                    await new UserMenuPage().UserMenu();
                    break;
                case 2:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid Options! Enter Valid option");
                    Console.WriteLine("Press any key for go to public menu page.");
                    Console.ReadKey();
                    await PublicMenu();
                    break;
            }
        }
    }
}
