using static System.Console;

namespace Bank_system
{
    class Program
    {
        static void Main(string[] args)
        {

            List<User> users = User.getAllUsers();
            User Current_user = new User();

            String email , password;

            bool isValid = false;
            while (!isValid)
            {

                Write("\nEnter your email : ");
                email = ReadLine();

                Write("\nEnter your pass : ");
                password = ReadPassword();

                foreach (User user in users) 
                {
                    WriteLine($"{user.Email} {user.Password}");
                    if (user.Email == email && user.Password == User.Hash(password))
                    {
                        Current_user = user;
                        isValid = true;
                        break;
                    }
                }

            }


            WriteLine($"Welcome {Current_user.FirstName} {Current_user.LastName}");

        }

        static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(intercept: true); // Reads key without displaying it

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*"); // Displays * instead of the actual character
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password[0..^1];
                    Console.Write("\b \b"); // Removes last * from console
                }
            } while (key.Key != ConsoleKey.Enter);

            return password;
        }
    }
}