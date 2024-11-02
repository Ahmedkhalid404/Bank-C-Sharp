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
            SystemManager.DisplayWelcomeScreen(current_user);

        }

        

        
        
    }
}