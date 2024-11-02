using static System.Console;

namespace Bank_system
{
    class Program
    {
        static void Main(string[] args)
        {

            List<User> users = User.getAllUsers();
            User current_user = new User();

            bool isValid = SystemManager.login(ref current_user, users);

            while( !isValid )
            {
                SystemManager.ShowIncorretUser();
                isValid = SystemManager.login(ref current_user, users);
            }
            SystemManager.ShowLoadingAnimation(3);

            //WriteLine($"Welcome {current_user.FirstName} {current_user.LastName}");
            WriteLine($"{current_user.FirstName} {current_user.LastName}");
            int option = SystemManager.DisplayWelcomeScreen(current_user);

            WriteLine( current_user.Role + " " + option );

            if( current_user.Role == Roles.Admin)
            {
                while( true)
                {
                    if (option == 1) // add user
                    {
                        SystemManager.ClearConsole();
                        users = User.AddUser(users);
                    }
                    else if (option == 2)
                    {

                    }
                    else if (option == 3)
                    {
                        SystemManager.ShowExitScreen();
                    }


                    option = SystemManager.DisplayWelcomeScreen(current_user);
                }

            }
            else
            {
                while( true)
                {
                    if (option == 1)
                    {

                    }
                    else if (option == 2)
                    {

                    }
                    else if (option == 3)
                    {
                    }
                    else if (option == 4)
                    {
                        SystemManager.ShowExitScreen();
                    }
                    option = SystemManager.DisplayWelcomeScreen(current_user);
                }
            }

        }

        

        
        
    }
}