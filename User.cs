using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Airport_2
{
    [Serializable]
    class User
    {
        static List<User> list = new List<User>();
        public static List<User> users { get => list; }
        string UserName;
        public string  userName { get => UserName;}
        string Password;
        public string  password { get=>Password;}
        string Name;
        string FamilyName;
        int Age;
        public enum Gender { man, woman };
        Gender gender;
        public User(string userName, string name, string familyName, string password, int Gender, int age)
        {
            UserName = userName;
            Password = password;
            Name = name;
            FamilyName = familyName;
            gender = (Gender)Gender;
            Age = age;
            list.Add(this);
        }
        List<Flight> flights = new List<Flight>();
        public bool CheckPassword(string password)
        {
            if(password == Password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool PasswordCheck(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";
            bool Validation = Regex.IsMatch(password, pattern);
            if (!Validation)
            {
                Console.WriteLine("The password does not match!");
                return false;
            }
            else
            {
                Console.WriteLine("successfully got the password");
                return true;
            }

        }
        public static void SaveUser()
        {
            FileStream stream = new FileStream(@"D:\elmos\AP\homeworks\tamrin 3\Airport 2\Airport 2\bin\Debug\Users.txt", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, list);
            stream.Close();
        }
        public static void ReadFile()
        {
            FileStream fs = File.Open(@"D:\elmos\AP\homeworks\tamrin 3\Airport 2\Airport 2\bin\Debug\Users.txt", FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            list = (List<User>)formatter.Deserialize(fs);
            fs.Close();

        }
        public static void AddUserToFlight(string Username, string Id, int seatNumber, int flight)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].UserName == Username)
                {
                    Passenger passenger = new Passenger(users[i].UserName, Id, seatNumber,flight, users[i].Name,users[i].FamilyName, users[i].Age, users[i].gender);
                    passenger.addPassengerToFlight();

                    break;
                }
            }
        }
        public static void AddFLight(Flight flight, string username)
        {
            foreach(var user in users)
            {
                if (user.UserName == username)
                {
                    user.flights.Add(flight);
                    break;
                }
            }
        }
        public static bool CheckUsername (string username)
        {
            foreach(var user in users)
            {
                if(user.UserName == username)
                {
                    return false;
                }
            }
            return true;
        }

        public static void CancellFlight(string UserName)
        {
            foreach(User user in users)
            {
                if(user.UserName == UserName)
                {
                     user.cancellFLight();
                }
            }

        }
        public void cancellFLight()
        {
            Console.Write("Flight Id: ");
            string flightId = Console.ReadLine();
            for(int i = 0; i< flights.Count; i++)
            {
                if (flights[i].checkId(flightId, UserName))
                {
                    flights.RemoveAt(i);
                }
            }
        }
        
    }
}

