using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string CurrentUser = null;
            int? IsMangaer = null;
            Manager manager = new Manager("Manager", "Managaer6004");
            User.ReadFile();
            Flight.ReadFlights();
            while (true)
            {
                Console.WriteLine("What are you up to?");
                Console.WriteLine("1. Log in\n2. Sign up\n" +
                    "3. Create a flight\n4. Cancel a flight by manager\n" +
                    "5. Searching among passengers\n6. sorting the flights\n7. Searching between the flights\n8. Book a ticket" +
                    "\n9. Cancel a fly by passenger\n10. Change password\n11. Exit");
                int Choise = int.Parse(Console.ReadLine());
                int Break = 0;
                switch (Choise)
                {
                    case 1:
                        if (CurrentUser != null)
                        {
                            Console.WriteLine("You have already signed in!");
                        }
                        else
                        {
                            Console.Write("User Name: ");
                            string UserName = Console.ReadLine();
                            int t = 0;
                            if (UserName == "Manager")
                            {
                                Console.Write("Password: ");
                                string Password = Console.ReadLine();
                                if (Password == "Manager6004")
                                {
                                    CurrentUser = "Manager";
                                    IsMangaer = 1;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < User.users.Count; i++)
                                {
                                    if (User.users[i].userName == UserName)
                                    {
                                        t = 1;

                                        Console.Write("Password: ");
                                        string Password = Console.ReadLine();
                                        if (User.users[i].CheckPassword(Password))
                                        {
                                            CurrentUser = User.users[i].userName;
                                            IsMangaer = 0;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Wrong password!");
                                            CurrentUser = null;
                                        }
                                    }



                                }
                                if (t == 0)
                                {
                                    Console.WriteLine("\n\nWrong username!\n\n");
                                }
                            }
                        }
                        break;
                    case 2:
                        if (CurrentUser != null)
                        {
                            Console.WriteLine("You have already signed in!");
                        }
                        else
                        {

                            string userName = null;
                            while (true)
                            {
                                Console.Write("Enter your username: ");
                                userName = Console.ReadLine();
                                if (User.CheckUsername(userName))
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("the username is repetitious");
                                }
                            }
                            string password;
                            while (true)
                            {
                                Console.Write("Enter your password: ");
                                password = Console.ReadLine();
                                if (User.PasswordCheck(password))
                                {
                                    break;
                                }
                            }
                            Console.Write("Enter your Name: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter your Family Name: ");
                            string familyName = Console.ReadLine();
                            Console.WriteLine("Enter your Gender (1. man , 2. woman");
                            int gender = int.Parse(Console.ReadLine()) - 1;
                            Console.WriteLine("Age:");
                            int age = int.Parse(Console.ReadLine());
                            new User(userName, name, familyName, password, gender, age);
                        }
                        break;
                    case 3:
                        if (CurrentUser == null)
                        {
                            Console.WriteLine("You have not logged in!");
                        }
                        else if (IsMangaer == 0)
                        {
                            Console.WriteLine("You must enter as a manager");
                        }
                        else
                        {

                            Console.Write("Id of the Flight: ");
                            string Id;
                            while (true)
                            {
                                Id = Console.ReadLine();

                                if (Flight.IdCheck(Id))
                                {
                                    Console.WriteLine("This Id Exists!");
                                }
                                else
                                {
                                    break;
                                }
                            }
                            int flightKindNumber;
                            while (true)
                            {
                                Console.Write("Kind of the Flight (Passenger, Charter, Post, Military): ");
                                string FlightKind = Console.ReadLine();
                                flightKindNumber = Flight.flightKindMethod(FlightKind);
                                if (flightKindNumber == 0)
                                {
                                    Console.WriteLine("Please Enter the correct word!");
                                }
                                else
                                {
                                    break;
                                }
                            }
                            Console.Write("Capacity of the Flight: ");
                            int capacity;
                            while (true)
                            {
                                capacity = int.Parse(Console.ReadLine());
                                if (capacity < 0)
                                {
                                    Console.WriteLine("please enter a correct and positive number!");

                                }
                                else
                                {
                                    break;
                                }
                            }
                            string startTime;
                            while (true)
                            {
                                Console.Write("Start time of the Flight: ");
                                startTime = Console.ReadLine();
                                int t = 0;
                                try
                                {
                                    int s = int.Parse(startTime);
                                    if ((s < 0) || (s >= 24))
                                    {
                                        t = 0;
                                        Console.WriteLine("Invalid!");
                                    }
                                    else
                                    {
                                        t = 1;
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine("Invalid!");
                                }
                                if (t == 1)
                                {
                                    break;
                                }
                            }
                            string endTime;
                            while (true)
                            {
                                Console.Write("Ende time of the Flight: ");
                                endTime = Console.ReadLine();
                                int t = 0;
                                try
                                {
                                    int s = int.Parse(endTime);
                                    if ((s < 0) || (s >= 24))
                                    {
                                        t = 0;
                                        Console.WriteLine("Invalid!");
                                    }
                                    else
                                    {
                                        t = 1;
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine("Invalid!");
                                }
                                if (t == 1)
                                {
                                    break;
                                }
                            }

                            Console.Write("Start locatoin of the Flight: ");
                            string startlocation = Console.ReadLine();
                            Console.Write("Destination of the Flight: ");
                            string destination = Console.ReadLine();
                            new Flight(Id, flightKindNumber, capacity, startTime, endTime, startlocation, destination);
                        }
                        break;
                    case 4:
                        if (CurrentUser == "Manager")
                        {
                            Console.WriteLine("Enter the Id of the flight which you wanna cancel");
                            string Id = Console.ReadLine();
                            bool t = Flight.CancellingFlight(Id);
                            if (t == false)
                            {
                                Console.WriteLine($"Couldn't find a flight with id: {Id}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Log in as a Manager!");
                        }
                        break;
                    case 5:
                        if (IsMangaer == 1)
                        {
                            Console.Write("flight Id:");
                            string Id = Console.ReadLine();
                            Console.WriteLine("1. by username\n2. by name\n3. by familyname\n4. flight class\n5. statics");
                            int choice = int.Parse(Console.ReadLine());
                            switch (choice)
                            {
                                case 1:
                                    Console.Write("Enter the Username:");
                                    string username = Console.ReadLine();
                                    Flight.SearchByUsername(Id, username);
                                    break;
                                case 2:
                                    Console.Write("Enter the Name:");
                                    string name = Console.ReadLine();
                                    Flight.SearchByName(Id, name);
                                    break;
                                case 3:
                                    Console.Write("Enter the FamilyName:");
                                    string FamliyName = Console.ReadLine();
                                    Flight.SearchByFamilyName(Id, FamliyName);
                                    break;
                                case 4:
                                    Console.WriteLine("Enter the kind of the flight class(1.Economy, 2.Business, 3.FirstClass)");
                                    int Choic = int.Parse(Console.ReadLine()) - 1;
                                    Passenger.flightMode flightMode = (Passenger.flightMode)Choic;
                                    Flight.SearchByFlightClass(Id, flightMode);
                                    break;
                                case 5:
                                    Flight.showStatics(Id);
                                    break;

                            }

                        }
                        else
                        {
                            Console.WriteLine("Please enter as a manager");
                        }
                        break;
                    case 6:
                        Console.WriteLine("sort by \n1. Capacity\n2. Free Seats\n3. Start time\n4. End time\n5. Flight kind\n6. Start location\n7. Destination");
                        int Choice2 = int.Parse(Console.ReadLine());
                        switch (Choice2)
                        {
                            case 1:
                                Flight.SortbyCapacity();
                                break;
                            case 2:
                                Flight.SortbyFreeSeats();
                                break;
                            case 3:
                                Flight.SortbyStartTime();
                                break;
                            case 4:
                                Flight.SortbyEndTime();
                                break;
                            case 5:
                                Flight.SortbyFlightKind();
                                break;
                            case 6:
                                Flight.SortbyStartLocation();
                                break;
                            case 7:
                                Flight.SortbyDestination();
                                break;
                        }
                        break;
                    case 7:
                        Console.WriteLine("1. Not completed\n2. Base on start location\n3. Base on destination\n4. Base on flight kind\n5. Base on start time\n6. Base on end time");
                        int Choice = int.Parse(Console.ReadLine());
                        switch (Choice)
                        {
                            case 1:
                                Flight.SearchByUncomplete();
                                break;
                            case 2:
                                Console.Write("Start location: ");
                                string choice = Console.ReadLine();
                                Flight.SearchByStartLocation(choice);
                                break;
                            case 3:
                                Console.Write("Destination: ");
                                choice = Console.ReadLine();
                                Flight.SearchByDestination(choice);
                                break;
                            case 4:
                                Console.WriteLine("17. Passenger, 21. Charter, 41.Post, 63. Military");
                                int ChosenKind = int.Parse(Console.ReadLine());
                                Flight.SearchByFlightKind(ChosenKind);
                                break;
                            case 5:
                                Console.Write("t: ");
                                int x = int.Parse(Console.ReadLine());
                                Console.Write("h: ");
                                int h = int.Parse(Console.ReadLine());
                                if (x > h)
                                {
                                    int hold = x;
                                    x = h;
                                    h = hold;
                                }
                                Flight.SearchByStartTime(x, h);
                                break;
                            case 6:
                                Console.Write("t: ");
                                x = int.Parse(Console.ReadLine());
                                Console.Write("h: ");
                                h = int.Parse(Console.ReadLine());
                                if (x > h)
                                {
                                    int hold = x;
                                    x = h;
                                    h = hold;
                                }
                                Flight.SearchByEndTime(x, h);
                                break;

                        }
                        break;


                    case 8:
                        if (IsMangaer == 0)
                        {
                            Console.Write("please enter the Id of the flight: ");
                            string ID = Console.ReadLine();
                            Console.Write("Enter the seat number please: ");
                            int seatNumber = int.Parse(Console.ReadLine());
                            Console.WriteLine("Whar is the type of your ticket?\n1.Economy\n2.Business\n3.FirstClass");
                            int type = int.Parse(Console.ReadLine());
                            User.AddUserToFlight(CurrentUser, ID, seatNumber, type);
                        }
                        else
                        {
                            Console.WriteLine("Please enter as a user!");
                        }
                        break;
                    case 9:
                        User.CancellFlight(CurrentUser);
                        break;

                    case 11:
                        User.SaveUser();
                        Flight.SaveFlight();
                        Break = 1;
                        break;

                }
                if (Break == 1)
                {
                    break;
                }

            }

        }
    }
}
//using System;
//using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;

//[Serializable] // This attribute is necessary for serialization.
//public class Person
//{
//    public int Age { get; set; }
//    public string Name { get; set; }
//}

//class Program
//{
//    static void Main()
//    {
//        //    // Create a new Person object.
//        //    Person person = new Person { Age = 30, Name = "John Doe" };

//        //    // Create a stream to serialize the object to.
//        //    FileStream stream = new FileStream(@"D:\elmos\AP\homeworks\tamrin 3\Airport 2\Airport 2\bin\Debug\people2.txt", FileMode.Create);

//        //    // Create a formatter object to perform the serialization.
//        BinaryFormatter formatter = new BinaryFormatter();

//        // Use the formatter to serialize the Person object to the stream.
//        formatter.Serialize(stream, person);

//        // Close the stream.
//        stream.Close();
//        Console.WriteLine("It is done!");

//        // Deserialize the object from the file
//        FileStream fs = File.Open(@"D:\elmos\AP\homeworks\tamrin 3\Airport 2\Airport 2\bin\Debug\people2.txt", FileMode.Open);
//        Person obj = (Person)formatter.Deserialize(fs);
//        Console.WriteLine(obj.Age);
//    }
//}

