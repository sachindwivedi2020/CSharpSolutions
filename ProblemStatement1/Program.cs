namespace ProblemStatement1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            var result = program.AttemptedLogin();
            if (result > 0)
            {
                var menus = program.GetMenuList();
                foreach (var menu in menus)
                {
                    Console.WriteLine($"{menu.Position}. {menu.Name}");
                }
                program.RepeatedInput();
            }
        }

        private (string name, string password) LoginScreen()
        {
            Console.WriteLine("==========Login Screen============");
            Console.WriteLine("enter a username");
            string username = Console.ReadLine();
            Console.WriteLine("enter a password");
            string password = Console.ReadLine();
            return (username, password);
        }
        private Login GetLoginCredential()
        {
            var login = new Login
            {
                UserName = "admin",
                Password = "1234",
                IsActive = true
            };
            return login;
        }
        private List<Menu> GetMenuList()
        {
            var menus = new List<Menu>
            {
                new Menu { Position = 1, Name = "Add New Product" },
                new Menu { Position = 2, Name = "Show Product" },
                new Menu { Position = 3, Name = "Renove Product" },
                new Menu { Position = 0, Name = "Exit" }

            };
            return menus;
        }
        private bool ValidateLogin(string username, string password)
        {
            var login = GetLoginCredential();
            if (login.UserName == username && login.Password == password && login.IsActive)
            {
                return true;
            }
            return false;
        }
        private int AttemptedLogin()
        {
            int attempts = 3;
            var userInput = LoginScreen();
            while (attempts > 1)
            {
                if (ValidateLogin(userInput.name, userInput.password))
                {
                    Console.WriteLine("Login Successful!");
                    return 1;
                }
                else
                {
                    attempts--;
                    Console.WriteLine($"Wrong username or password and You have {attempts} attempts left.");
                    if (attempts <= 3)
                        LoginScreen();
                    else
                        return 0;

                }
            }
            Console.WriteLine("Maximum login attempts exceeded.Exsiitng Application");
            return 0;
        }
        private void RepeatedInput()
        {
            while (true)
            {
                Console.WriteLine("Enter a number");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out int number))
                    continue; //Ask Again

                if (number == 0)
                    break; //Exit the loop

            }
        }
    }
}
