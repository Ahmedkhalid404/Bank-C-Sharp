using static System.Console;

namespace Bank_system
{
    internal class SystemManager
    {

        public static void ShowExitScreen(int seconds = 2)
        {
            string message = "Thank you for using our services.";
            int windowWidth = Console.WindowWidth;
            int messageLength = message.Length;
            int sleepTime = 25;

            Console.Clear();

            for (int i = 0; i < windowWidth - messageLength; i++)
            {
                Console.SetCursorPosition(i, Console.WindowHeight / 2);
                Console.WriteLine(message);
                Thread.Sleep(sleepTime);
                Console.Clear();
            }

            for (int i = windowWidth - messageLength; i >= 0; i--)
            {
                Console.SetCursorPosition(i, Console.WindowHeight / 2);
                Console.WriteLine(message);
                Thread.Sleep(sleepTime);
                Console.Clear();
            }

            Thread.Sleep(seconds * 1000);
            Console.Clear();
            Environment.Exit(0);
        }


        public static int DisplayWelcomeScreen(User user)
        {
            Console.Clear();

            int windowWidth = Console.WindowWidth;
            string welcomeMessage = $"Welcome {user.FirstName} {user.LastName}";
            int welcomeMessagePosition = (windowWidth - welcomeMessage.Length) / 2;
            Console.SetCursorPosition(welcomeMessagePosition, 5); 
            Console.WriteLine(welcomeMessage);

            Thread.Sleep(1000);

            List<string[]> options = new List<string[]>(){
                   new string[]{ "1. Add User", "2. Activation requests", "3. Exit" },
                   new string[]{ "1. My balance", "2. Activation request", "3. Send Money" , "4. Exit" }
            };
            int optionsStartPosition = (windowWidth - 20) / 2;

            int idx = (user.Role == Roles.Admin ? 0 : 1);

            for (int i = 0; i < options[idx].Length; i++)
            {
                Console.SetCursorPosition(optionsStartPosition, 8 + i * 2);
                Console.WriteLine(options[idx][i].PadRight(20));
            }

            Console.Write("\n\nPlease choose an option: ");
            int option = Convert.ToInt32(Console.ReadLine());
            return option;
        }

        public static bool login(ref User current_user,List<User> users)
        {
            String email, password;

            bool isValid = false;
          

            Write("\nEnter your email : ");
            email = ReadLine();

            Write("\nEnter your pass : ");
            password = ReadPassword();

            foreach (User user in users)
            {
                if (user.Email == email && user.Password == User.Hash(password))
                {
                    current_user = user;
                    isValid = true;
                    break;
                }
            }

            
            return isValid;
        }
        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(intercept: true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password[0..^1];
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);

            return password;
        }
        public static void ClearConsole()
        {
            Console.Clear();
        }

        public static void DisplayMessageForDuration(string message, int durationInSeconds)
        {
            Console.Clear();
            Console.WriteLine(message);
            Thread.Sleep(durationInSeconds * 1000);
            Console.Clear();
        }

        public static void ShowIncorretUser()
        {
            ClearConsole();
            ShowLoadingAnimation(2);
            Console.WriteLine("Incorrect User pls try again :)");
        }
        public static void ShowLoadingAnimation(int durationInSeconds)
        {
            ClearConsole();
            Console.Write("Loading... ");

            char[] spinner = { '|', '/', '-', '\\' };
            int spinnerIndex = 0;

            int durationInMilliseconds = durationInSeconds * 1000;
            int interval = 300;

            for (int i = 0; i < durationInMilliseconds / interval; i++)
            {
                Console.SetCursorPosition("Loading... ".Length, Console.CursorTop);
                Console.Write(spinner[spinnerIndex]); 
                spinnerIndex = (spinnerIndex + 1) % spinner.Length; 
                Thread.Sleep(interval); 
            }

            ClearConsole();
        }


    }
}
