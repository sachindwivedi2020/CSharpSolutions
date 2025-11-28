namespace ProblemStatement1
{
    /// <summary>
    /// Provides the main entry point and core logic for the console-based application, including user authentication
    /// and menu navigation.
    /// </summary>
    /// <remarks>This class is intended for internal use as the application's startup type. It manages user
    /// login attempts, displays available menu options, and handles user input through the console. The class is not
    /// intended to be instantiated or used directly by external code.</remarks>
    internal class Program
    {
        private readonly ProductService _productService;
        public Program()
        {
            // Use the product service which performs validations and delegates to the repository
            _productService = new ProductService();
        }
        /// <summary>
        /// Serves as the entry point for the application.
        /// </summary>
        /// <param name="args">An array of command-line arguments supplied to the application.</param>
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
        /// <summary>
        /// Prompts the user to enter a username and password via the console and returns the entered values.
        /// </summary>
        /// <remarks>Both the username and password are read as plain text from the console. No validation
        /// or masking is performed on the input.</remarks>
        /// <returns>A tuple containing the username and password entered by the user. The first element is the username; the
        /// second element is the password.</returns>
        private (string name, string password) LoginScreen()
        {
            Console.WriteLine("==========Login Screen============");
            Console.WriteLine("enter a username");
            string username = Console.ReadLine();
            Console.WriteLine("enter a password");
            string password = Console.ReadLine();
            return (username, password);
        }
        /// <summary>
        /// Retrieves a default set of login credentials for administrative access.
        /// </summary>
        /// <returns>A <see cref="Login"/> object containing the default administrative username, password, and active status.</returns>
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
        /// <summary>
        /// Retrieves a list of available menu options for the application.
        /// </summary>
        /// <returns>A list of <see cref="Menu"/> objects representing the available menu options. The list includes all standard
        /// options in their display order.</returns>
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
        /// <summary>
        /// Validates the specified username and password against the stored login credentials and checks if the account
        /// is active.
        /// </summary>
        /// <param name="username">The username to validate. Cannot be null.</param>
        /// <param name="password">The password to validate. Cannot be null.</param>
        /// <returns>true if the username and password match the stored credentials and the account is active; otherwise, false.</returns>
        private bool ValidateLogin(string username, string password)
        {
            var login = GetLoginCredential();
            if (login.UserName == username && login.Password == password && login.IsActive)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Attempts to authenticate a user by prompting for credentials, allowing up to three attempts.
        /// </summary>
        /// <remarks>This method prompts the user for their username and password and validates the
        /// credentials. If the user fails to authenticate after three attempts, the method returns 0 to indicate
        /// failure.</remarks>
        /// <returns>1 if the user is successfully authenticated; otherwise, 0 if all login attempts fail.</returns>
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
        /// <summary>
        /// Prompts the user to enter numbers repeatedly until a valid integer value of 0 is entered.
        /// </summary>
        /// <remarks>Only valid integer inputs are accepted; invalid inputs are ignored and the prompt is
        /// repeated. The method continues prompting until the user enters 0, at which point it exits.</remarks>
        private void RepeatedInput()
        {
            while (true)
            {
                Console.WriteLine("Enter a number");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out int number))
                    continue; //Ask Again

                if (number == 1 || number == 2 || number == 3 || number == 4)
                {
                    switch (number)
                    {
                        case 1:
                            AddProduct();
                            break;
                        case 2:
                            ShowProducts();
                            break;
                        case 3:
                            RemoveProduct();
                            break;
                        case 4:
                            UpdateProduct();
                            break;
                        default:
                            break;
                    }
                }
                if (number == 0)
                    break; //Exit the loop

            }
        }
        private void ShowProducts()
        {
            var products = _productService.GetAllProducts();
            Console.WriteLine("Available Products:");
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}, Available Quantity: {product.AvailableQuantity}");
            }
        }
        private void ProductFormInput(out int id, out string name, out decimal price, out int quantity)
        {
            Console.WriteLine("Enter Product Details:");
            Console.Write("ID: ");
            id = int.Parse(Console.ReadLine());
            Console.Write("Name: ");
            name = Console.ReadLine();
            Console.Write("Price: ");
            price = decimal.Parse(Console.ReadLine());
            Console.Write("Available Quantity: ");
            quantity = int.Parse(Console.ReadLine());
        }
        private void AddProduct()
        {
            ProductFormInput(out int id, out string name, out decimal price, out int quantity);
            var product = new Product
            {
                Id = id,
                Name = name,
                Price = price,
                AvailableQuantity = quantity
            };
            _productService.AddProduct(product);
            Console.WriteLine("Product added successfully.");
        }
        private void RemoveProduct()
        {
            Console.Write("Enter Product ID to remove: ");
            int id = int.Parse(Console.ReadLine());
            var productResult = _productService.DeleteProduct(id);
            if (!productResult)
            {
                Console.WriteLine("Product not found.");
            }
            Console.WriteLine("Product removed successfully.");
        }
        private void UpdateProduct()
        {
            ProductFormInput(out int id, out string name, out decimal price, out int quantity);
            var product = new Product
            {
                Id = id,
                Name = name,
                Price = price,
                AvailableQuantity = quantity
            };
            bool isUpdated = _productService.UpdateProduct(product);
            if (isUpdated)
            {
                Console.WriteLine("Product updated successfully.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }
    }
}
