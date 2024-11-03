using static System.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.ComponentModel;

namespace Bank_system
{
    internal class User
    {
        private int id;
        private string first_name;
        private string last_name;
        private string email;
        private string password;
        private Roles role;
        private UserStatus status;

        public static List<User> AddUser(List<User> users,User Current_user)
        {
            WriteLine("Add User : ");
            User NewUser = new User();
            NewUser.Id = users.Max(x => x.Id) + 1;
            Write("First Name : ");
            NewUser.FirstName = ReadLine();
            Write("Last Name : ");
            NewUser.LastName = ReadLine();
            Write("Email : ");
            NewUser.Email = ReadLine();
            Write("Password : ");
            NewUser.Password = SystemManager.ReadPassword();

            string role = "user";

            if( Current_user.role == Roles.Owner)
            {
                Write("\nRole - if you want admin print 'a' and 'u' for user and 'o' for owner: ");
                string op = ReadLine().ToLower();
                if(op[0] == 'o')
                {
                    role = "owner";
                    NewUser.Role = Roles.Owner;
                }
                else if (op[0] == 'a')
                {
                    role = "admin";
                    NewUser.Role = Roles.Admin;
                }
                else
                {
                    role = "user";
                    NewUser.Role = Roles.User;
                }
            }


            Write("\nIf you want to add user enter 'y' and 'n' other wise : ");
            string option = ReadLine().ToLower();
            if (option[0] == 'n')
                return User.getAllUsers();

            SystemManager.ShowLoadingAnimation(3);

            foreach(User user in User.getAllUsers())
            {
                if( user.email == NewUser.email)
                {
                    SystemManager.DisplayMessageForDuration("Failed to add new user (email already exist)",3);
                    return User.getAllUsers();
                }
            }

            string filePath = @"D:\Bank system c-sharp\Bank-C-Sharp\data\users.txt";
            string userEntry = $"{NewUser.id},{NewUser.first_name},{NewUser.last_name},{NewUser.email},{NewUser.password},{role},allowed";
            using (StreamWriter writer = new StreamWriter(filePath, append: true))
            {
                writer.WriteLine(userEntry);
            }
            SystemManager.DisplayMessageForDuration("User added successfully", 2);

            return User.getAllUsers();
        }

        public static List<User> getAllUsers() {
            string path = @"D:\Bank system c-sharp\Bank-C-Sharp\data\users.txt";
            List<User> users = new List<User>();

            try
            {
                foreach (var line in File.ReadAllLines(path))
                {
                    var fields = line.Split(',');


                    var user = new User
                    {
                        Id = int.Parse(fields[0]),
                        FirstName = fields[1],
                        LastName = fields[2],
                        Email = fields[3],
                        Set_password_without_hash = fields[4],
                        Role = (fields[5].ToLower() == "owner" ? Roles.Owner :  (fields[5].ToLower() == "admin" ? Roles.Admin : Roles.User) ),
                        Status = (fields[6].ToLower() == "allowed" ? UserStatus.Allowed : UserStatus.Baned)
                    };

                    users.Add(user);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return users;
        }

        public static string Hash(string input)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha1.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }


        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string FirstName
        {
            get { return first_name; }
            set { first_name = value; }
        }

        public string LastName
        {
            get { return last_name; }
            set { last_name = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Set_password_without_hash
        {
            get { return password; }
            set { password = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = User.Hash(value); }
        }

        public UserStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        public Roles Role
        {
            get { return role; }
            set { role = value; }
        }

    }
}
