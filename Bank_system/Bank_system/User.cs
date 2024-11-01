using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

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
                        Role = (fields[5].ToLower() == "admin" ? Roles.Admin : Roles.User)
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

        public Roles Role
        {
            get { return role; }
            set { role = value; }
        }

    }
}
