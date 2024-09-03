using AirlineBAL.Services;

namespace AirLineReservationUI.AirlineMainPage
{
    public class UserLoginPage
    {
        private readonly UserService _userService;
        public UserLoginPage()
        {
            _userService = new();
        }

        public async Task UserLogin()
        {
            Console.Clear();
            Console.WriteLine("Welcome to User Login page.\n");

            string? userName;
            string? password;

            while (true)
            {
                Console.Write("Enter the UserName: ");
                userName = Console.ReadLine() ?? string.Empty;

                bool isExist = await _userService.CheckUserNameExistService(userName);

                if (isExist) { break; }
                else { Console.WriteLine("Incorrect UserName!\n"); }

            }

            while (true)
            {
                Console.Write("Enter the Password: ");
                password = Console.ReadLine() ?? string.Empty;

                bool isPasswordExist = await _userService.CheckPasswordExistService(userName, password);

                if (isPasswordExist) { break; }
                else { Console.WriteLine("Incorrect Password!\n"); }
               
            }

            Console.WriteLine("\nAuthenticated Successfully!");
            Console.WriteLine("Press any key for go to next page.");
            Console.ReadKey();
        }
    }
}
