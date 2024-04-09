using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
                int Choise;
                try
                {
                    Choise = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Please enter a number");
                    continue;
                }
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
                            int gender = 0;
                            while (true)
                            {
                                Console.WriteLine("Enter your Gender (1. man , 2. woman)");
                                try
                                {
                                    gender = int.Parse(Console.ReadLine()) - 1;
                                    if ((gender < 2) && (gender > -1))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Please enter a number between one and two");

                                    }
                                }
                                catch
                                {
                                    Console.WriteLine("Please enter a number between one and two");
                                }

                            }
                            int age;
                            while (true)
                            {
                                Console.WriteLine("Age:");
                                try
                                {
                                    age = int.Parse(Console.ReadLine());
                                    break;
                                }
                                catch
                                {
                                    Console.WriteLine("Please enter a number");
                                }

                            }
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
                                try
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
                                catch
                                {
                                    Console.WriteLine("Please enter a number");
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
                            int choice = 10;
                            try
                            {
                                choice = int.Parse(Console.ReadLine());
                            }
                            catch
                            {
                                Console.WriteLine("Please enter a number");
                            }
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
                                default:
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
                        int Choice2 = 10;
                        try
                        {
                            Choice2 = int.Parse(Console.ReadLine());

                        }
                        catch
                        {
                            Console.WriteLine("Please enter a number");
                        }
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
                        int Choice = 10;
                        try
                        {
                            Choice = int.Parse(Console.ReadLine());

                        }
                        catch
                        {
                            Console.WriteLine("Please enter a number");
                        }
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
                                int x;
                                while (true)
                                {
                                    Console.Write("t: ");
                                    try
                                    {
                                        x = int.Parse(Console.ReadLine());
                                        break;
                                    }

                                    catch
                                    {
                                        Console.WriteLine("Please enter a number");
                                    }
                                }
                                int h;
                                while (true)
                                {
                                    Console.Write("h: ");
                                    try
                                    {
                                        h = int.Parse(Console.ReadLine());
                                        break;
                                    }

                                    catch
                                    {
                                        Console.WriteLine("Please enter a number");
                                    }
                                }

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
                            default:
                                break;

                        }
                        break;


                    case 8:
                        if (IsMangaer == 0)
                        {
                            Console.Write("please enter the Id of the flight: ");
                            string ID = Console.ReadLine();
                            int seatNumber;
                            while (true)
                            {
                                Console.Write("Enter the seat number please: ");
                                try
                                {
                                    seatNumber = int.Parse(Console.ReadLine());
                                    break;
                                }
                                catch
                                {
                                    Console.WriteLine("Please enter a number");
                                }
                            }
                            int type;
                            while (true)
                            {
                                Console.WriteLine("What is the type of your ticket?\n1.Economy\n2.Business\n3.FirstClass");
                                try
                                {
                                    type = int.Parse(Console.ReadLine());
                                    if ((type > 3) || (type < 0))
                                    {
                                        Console.WriteLine("Enter a correct number");
                                    }
                                    else
                                    {
                                        break;

                                    }
                                }
                                catch
                                {
                                    Console.WriteLine("Please enter a number");
                                }
                            }
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
                    case 10:
                        User.ChangePassword(CurrentUser);
                        break;

                    case 11:
                        User.SaveUser();
                        Flight.SaveFlight();
                        CurrentUser = null;
                        IsMangaer = null;
                        break;

                }


            }

        }

        private static string GenerateRandomKey(int v)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] keyBytes = new byte[v];
                rng.GetBytes(keyBytes);
                return Convert.ToBase64String(keyBytes);
            }

        }
    }




    [Serializable]
    class Flight
    {

        static List<Flight> flights = new List<Flight>();
        static List<string> Ids = new List<string>();
        enum FlightKind { Passenger = 17, Charter = 21, Post = 41, Military = 63 }
        FlightKind? flightKind = null;
        string Id = null;
        int? PassengerMembers = null;
        int? Capacity = null;
        int? FreeSeats = null;
        string StartTime = null;
        string EndTime = null;
        string StartLocation = null;
        string Destination = null;
        List<Passenger> passengers = new List<Passenger>();
        static public bool IdCheck(string IdEx)
        {
            foreach (Flight c in flights)
            {
                if (IdEx == c.Id)
                {
                    return true;
                }
            }
            return false;
        }
        static public int flightKindMethod(string x)
        {
            switch (x)
            {
                case "Passenger":
                    return 17;
                    break;
                case "Charter":
                    return 21;
                    break;
                case "Post":
                    return 41;
                    break;
                case "Military":
                    return 63;
                    break;
                default:
                    break;
            }
            return 0;
        }
        public static void give()
        {
            foreach (var flight in flights)
            {
                try
                {
                    foreach (var passenger in flight.passengers)
                    {
                        passenger.gradeGiving();
                    }
                }
                catch
                {

                }
            }
        }
        public Flight(string id, int FlightKindNumber, int capacity, string starttime, string endtime, string startlocation, string destination)
        {
            Id = id;
            Capacity = capacity;
            PassengerMembers = 0;
            FreeSeats = capacity;
            StartTime = starttime;
            EndTime = endtime;
            StartLocation = startlocation;
            Destination = destination;
            flightKind = (FlightKind)FlightKindNumber;
            flights.Add(this);
            CreateDirectory(Id);

        }
        public static void CreateDirectory(string flightId)
        {
            Directory.CreateDirectory(@"D:\elmos\AP\homeworks\tamrin 3\Airport 2\Airport 2\bin\Debug\Flights\" + flightId);
        }
        public static void SaveFlight()
        {
            FileStream fs = new FileStream(@"D:\elmos\AP\homeworks\tamrin 3\Airport 2\Airport 2\bin\Debug\Flights\Flights.txt", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, flights);
            fs.Close();
        }

        public static void ReadFlights()
        {
            FileStream fs = File.Open(@"D:\elmos\AP\homeworks\tamrin 3\Airport 2\Airport 2\bin\Debug\Flights\Flights.txt", FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            try { flights = (List<Flight>)formatter.Deserialize(fs); }
            catch
            {

            }
            finally { fs.Close(); }


        }
        public static bool CancellingFlight(string ID)
        {
            for (int i = 0; i < flights.Count; i++)
            {
                if (flights[i].Id == ID)
                {
                    flights.RemoveAt(i);
                    Directory.Delete(@"D:\elmos\AP\homeworks\tamrin 3\Airport 2\Airport 2\bin\Debug\Flights\" + ID, true);
                    Console.WriteLine($"The flight with Id: {ID} deleted successflully");

                    return true;
                }
            }
            return false;

        }
        public static void free()
        {
            foreach (var flight in flights)
            {
                try
                {
                    flight.FreeSeats = flight.Capacity - flight.passengers.Count;
                }
                catch
                {

                }
            }
        }

        public static bool AddPassenger(Passenger passenger, string Id, string username, int seat)
        {
            for (int i = 0; i < flights.Count; i++)
            {
                if (flights[i].Id == Id)
                {
                    int t = 0;
                    if (flights[i].passengers != null)
                    {
                        foreach (Passenger passenger1 in flights[i].passengers)
                        {
                            if (passenger1.seatNumber == seat)
                            {
                                t = 1;
                            }
                        }
                    }
                    if (t == 0)
                    {

                        flights[i].passengers.Add(passenger);
                        flights[i].FreeSeats--;
                        flights[i].PassengerMembers++;
                        User.AddFLight(flights[i], username);
                        return true;
                    }
                    else
                    {

                        Console.WriteLine("This seat number is already booked");
                        return false;
                    }
                }
            }
            Console.WriteLine("Could not find a flight with such an Id");
            return false;
            //passengers.Add(passenger);
        }
        public static void SearchByUsername(string Id, string UserName)
        {
            int t = 0;
            foreach (var flight in flights)
            {
                if (flight.Id == Id)
                {
                    foreach (var passenger in flight.passengers)
                    {
                        if (passenger.SearchByUserName(UserName))
                        {
                            t = 1;
                            break;
                        }
                    }
                    break;
                }
            }
            if (t == 0)
            {
                Console.WriteLine("Couldn't find such a username among passengers");
            }
        }
        public static void SearchByName(string Id, string Name)
        {
            int t = 0;
            foreach (var flight in flights)
            {
                if (flight.Id == Id)
                {
                    foreach (var passenger in flight.passengers)
                    {
                        if (passenger.SearchByName(Name))
                        {
                            t = 1;
                            break;
                        }
                    }
                    break;
                }
            }
            if (t == 0)
            {
                Console.WriteLine("Couldn't find such a name among passengers");
            }
        }
        public static void SearchByFamilyName(string Id, string FamilyName)
        {
            int t = 0;
            foreach (var flight in flights)
            {
                if (flight.Id == Id)
                {
                    foreach (var passenger in flight.passengers)
                    {
                        if (passenger.SearchByFamilyName(FamilyName))
                        {
                            t = 1;
                            break;
                        }
                    }
                    break;
                }
            }
            if (t == 0)
            {
                Console.WriteLine("Couldn't find such a familyname among passengers");
            }
        }
        public static void SearchByFlightClass(string Id, Passenger.flightMode flightMode)
        {
            int t = 0;
            foreach (var flight in flights)
            {
                if (flight.Id == Id)
                {
                    foreach (var passenger in flight.passengers)
                    {
                        if (passenger.SearchByFlightClass(flightMode))
                        {
                            t = 1;
                            break;
                        }
                    }
                    break;
                }
            }
            if (t == 0)
            {
                Console.WriteLine("Couldn't find such a flightclass among passengers");
            }
        }
        public static void showStatics(string Id)
        {
            int t = 0;
            foreach (var flight in flights)
            {
                if (flight.Id == Id)
                {
                    flight.showStatics();
                    t = 1;
                    break;
                }
            }
            if (t == 0)
            {
                Console.WriteLine("Couldn't find such an Id among flights");
            }
        }
        public static int childrenCOunter;
        public static int youngCounter;
        public static int middleAgedCounter;
        public static int elderlyCounter;

        void showStatics()
        {
            Passenger.GradeCounter(passengers);
            int members = passengers.Count;
            double MenPer = (Passenger.Mencounter(passengers) / members) * 100;
            double womenPer = 100 - MenPer;
            double childrenPer = (childrenCOunter / members) * 100;
            double youngPer = (youngCounter / members) * 100;
            double middleAgedPer = (middleAgedCounter / members) * 100;
            double elderlyPer = (elderlyCounter / members) * 100;
            Console.WriteLine($"Men static: {MenPer}, Women static: {womenPer}\nAges statics: children: {childrenPer}, youngs: {youngPer}, middle-Aged people:{middleAgedPer}, elderly: {elderlyPer}");
        }
        public static void SortbyCapacity()
        {
            List<Flight> sortedFlights = new List<Flight>();
            foreach (var flight in flights)
            {
                sortedFlights.Add(flight);
            }
            for (int i = 0; i < sortedFlights.Count; i++)
            {
                for (int j = 0; j < sortedFlights.Count - 1; j++)
                {
                    if (sortedFlights[j].Capacity < sortedFlights[j + 1].Capacity)
                    {
                        Flight holder = sortedFlights[j];
                        sortedFlights[j] = sortedFlights[j + 1];
                        sortedFlights[j + 1] = holder;
                    }
                }
            }
            foreach (var flight in sortedFlights)
            {
                Console.WriteLine($"Id: {flight.Id}, capacity: {flight.Capacity}");
            }
        }
        public static void SortbyFreeSeats()
        {
            List<Flight> sortedFlights = new List<Flight>();
            foreach (var flight in flights)
            {
                sortedFlights.Add(flight);
            }
            for (int i = 0; i < sortedFlights.Count; i++)
            {
                for (int j = 0; j < sortedFlights.Count - 1; j++)
                {
                    if (sortedFlights[j].FreeSeats < sortedFlights[j + 1].FreeSeats)
                    {
                        Flight holder = sortedFlights[j];
                        sortedFlights[j] = sortedFlights[j + 1];
                        sortedFlights[j + 1] = holder;
                    }
                }
            }
            foreach (var flight in sortedFlights)
            {
                Console.WriteLine($"Id: {flight.Id}, free seats: {flight.FreeSeats}");
            }
        }
        public static void SortbyStartTime()
        {
            List<Flight> sortedFlights = new List<Flight>();
            foreach (var flight in flights)
            {
                sortedFlights.Add(flight);
            }
            for (int i = 0; i < sortedFlights.Count; i++)
            {
                for (int j = 0; j < sortedFlights.Count - 1; j++)
                {
                    if (int.Parse(sortedFlights[j].StartTime) < int.Parse(sortedFlights[j + 1].StartTime))
                    {
                        Flight holder = sortedFlights[j];
                        sortedFlights[j] = sortedFlights[j + 1];
                        sortedFlights[j + 1] = holder;
                    }
                }
            }
            foreach (var flight in sortedFlights)
            {
                Console.WriteLine($"Id: {flight.Id}, start time: {flight.StartTime}");
            }
        }
        public static void SortbyEndTime()
        {
            List<Flight> sortedFlights = new List<Flight>();
            foreach (var flight in flights)
            {
                sortedFlights.Add(flight);
            }
            for (int i = 0; i < sortedFlights.Count; i++)
            {
                for (int j = 0; j < sortedFlights.Count - 1; j++)
                {
                    if (int.Parse(sortedFlights[j].EndTime) < int.Parse(sortedFlights[j + 1].EndTime))
                    {
                        Flight holder = sortedFlights[j];
                        sortedFlights[j] = sortedFlights[j + 1];
                        sortedFlights[j + 1] = holder;
                    }
                }
            }
            foreach (var flight in sortedFlights)
            {
                Console.WriteLine($"Id: {flight.Id}, end time: {flight.EndTime}");
            }
        }
        public static void SortbyFlightKind()
        {
            List<Flight> sortedFlights = new List<Flight>();
            foreach (var flight in flights)
            {
                sortedFlights.Add(flight);
            }
            for (int i = 0; i < sortedFlights.Count; i++)
            {
                for (int j = 0; j < sortedFlights.Count - 1; j++)
                {
                    if ((int)(sortedFlights[j].flightKind) < (int)(sortedFlights[j + 1].flightKind))
                    {
                        Flight holder = sortedFlights[j];
                        sortedFlights[j] = sortedFlights[j + 1];
                        sortedFlights[j + 1] = holder;
                    }
                }
            }
            foreach (var flight in sortedFlights)
            {
                Console.WriteLine($"Id: {flight.Id}, flight kind: {flight.flightKind}");
            }
        }

        public static void SortbyStartLocation()
        {
            List<Flight> sortedFlights = new List<Flight>();
            foreach (var flight in flights)
            {
                sortedFlights.Add(flight);
            }
            for (int i = 0; i < sortedFlights.Count; i++)
            {
                for (int j = 0; j < sortedFlights.Count - 1; j++)
                {
                    if ((int)(sortedFlights[j].StartLocation[0]) < (int)(sortedFlights[j + 1].StartLocation[0]))
                    {
                        Flight holder = sortedFlights[j];
                        sortedFlights[j] = sortedFlights[j + 1];
                        sortedFlights[j + 1] = holder;
                    }
                    else if ((int)(sortedFlights[j].StartLocation[0]) == (int)(sortedFlights[j + 1].StartLocation[0]))
                    {
                        try
                        {
                            if ((int)(sortedFlights[j].StartLocation[1]) < (int)(sortedFlights[j + 1].StartLocation[1]))
                            {
                                Flight holder = sortedFlights[j];
                                sortedFlights[j] = sortedFlights[j + 1];
                                sortedFlights[j + 1] = holder;
                            }
                        }
                        catch { }
                    }
                }
            }
            foreach (var flight in sortedFlights)
            {
                Console.WriteLine($"Id: {flight.Id}, start location: {flight.StartLocation}");
            }
        }
        public static void SortbyDestination()
        {
            List<Flight> sortedFlights = new List<Flight>();
            foreach (var flight in flights)
            {
                sortedFlights.Add(flight);
            }
            for (int i = 0; i < sortedFlights.Count; i++)
            {
                for (int j = 0; j < sortedFlights.Count - 1; j++)
                {
                    if ((int)(sortedFlights[j].Destination[0]) < (int)(sortedFlights[j + 1].Destination[0]))
                    {
                        Flight holder = sortedFlights[j];
                        sortedFlights[j] = sortedFlights[j + 1];
                        sortedFlights[j + 1] = holder;
                    }
                    else if ((int)(sortedFlights[j].Destination[0]) == (int)(sortedFlights[j + 1].Destination[0]))
                    {
                        try
                        {
                            if ((int)(sortedFlights[j].Destination[1]) < (int)(sortedFlights[j + 1].Destination[1]))
                            {
                                Flight holder = sortedFlights[j];
                                sortedFlights[j] = sortedFlights[j + 1];
                                sortedFlights[j + 1] = holder;
                            }
                        }
                        catch { }
                    }
                }
            }
            foreach (var flight in sortedFlights)
            {
                Console.WriteLine($"Id: {flight.Id}, destination: {flight.Destination}");
            }
        }

        public static void SearchByUncomplete()
        {
            int a = 1;
            foreach (var flight in flights)
            {
                if ((flight.FreeSeats) != 0)
                {
                    Console.WriteLine(a + ". " + "flight Id: " + flight.Id);
                    a++;
                }
            }
        }
        public static void SearchByStartLocation(string StartLocation)
        {
            int a = 1;
            foreach (var flight in flights)
            {
                if ((flight.StartLocation) == StartLocation)
                {
                    Console.WriteLine(a + ". " + "Id: " + flight.Id);
                    a++;
                }
            }
        }
        public static void SearchByDestination(string Destination)
        {
            int a = 1;
            foreach (var flight in flights)
            {
                if ((flight.Destination) == Destination)
                {
                    Console.WriteLine(a + ". " + "Id: " + flight.Id);
                    a++;
                }
            }
        }
        public static void SearchByFlightKind(int kind)
        {
            int a = 1;
            FlightKind Kind;
            Kind = (FlightKind)kind;
            foreach (var flight in flights)
            {
                if ((flight.flightKind) == Kind)
                {
                    Console.WriteLine(a + ". " + "Id: " + flight.Id);
                    a++;
                }
            }
        }
        public static void SearchByStartTime(int x, int h)
        {
            int a = 1;
            foreach (var flight in flights)
            {
                if ((x <= int.Parse(flight.StartTime)) && (h >= int.Parse(flight.StartTime)))
                {
                    Console.WriteLine(a + ". " + "Id: " + flight.Id + " Start time: " + flight.StartTime);
                    a++;
                }
            }
        }
        public static void SearchByEndTime(int x, int h)
        {
            int a = 1;
            foreach (var flight in flights)
            {
                if ((x <= int.Parse(flight.EndTime)) && (h >= int.Parse(flight.EndTime)))
                {
                    Console.WriteLine(a + ". " + "Id: " + flight.Id + " End time: " + flight.EndTime);
                    a++;
                }
            }
        }
        public bool checkId(string Id, string UserName)
        {
            if (this.Id == Id)
            {
                for (int i = 0; i < passengers.Count; i++)
                {
                    if (passengers[i].userName == UserName)
                    {
                        passengers.RemoveAt(i);
                        PassengerMembers = passengers.Count;
                        FreeSeats++;
                    }
                }
                return true;
            }
            return false;
        }
        public static void giveFreeseats()
        {
            foreach (Flight flight in flights)
            {
                try
                {
                    flight.FreeSeats = flight.Capacity - flight.passengers.Count;
                    flight.PassengerMembers = flight.passengers.Count;
                }
                catch
                {
                    flight.FreeSeats = flight.Capacity;
                    flight.PassengerMembers = 0;
                }
            }
        }
        //fixing the issue of 
        //fixing the issue of start and time giving at first
        //public static void time()
        //{
        //    for (int i = 0; i < flights.Count; i++)
        //    {
        //        flights[i].StartTime = i.ToString();
        //    }
        //}

    }

    [Serializable]
    class User
    {
        static List<User> list = new List<User>();
        public static List<User> users { get => list; }
        string UserName;
        public string userName { get => UserName; }
        string Password;
        public string password { get => Password; }
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
        public static void ChangePassword(string Username)
        {
            foreach (var user in users)
            {
                if (user.UserName == Username)
                {
                    while (true)
                    {
                        Console.Write("Password: ");
                        string pass = Console.ReadLine();

                        if (PasswordCheck(pass))
                        {
                            user.Password = pass;
                            break;
                        }

                    }
                    break;
                }
            }

        }
        List<Flight> flights = new List<Flight>();
        public bool CheckPassword(string password)
        {
            if (password == Password)
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
            List<string> Passwords = new List<string>();
            foreach (var user in list)
            {
                Passwords.Add(user.password);
            }

            foreach (var user in list)
            {
                user.Password = Encrypting.encrypting(user.Password, "VXAQtfAhbuNL/nR0T0VQWw==");
            }
            formatter.Serialize(stream, list);
            stream.Close();
            for (int i = 0; i < users.Count; i++)
            {
                users[i].Password = Passwords[i];
            }
        }
        public static void ReadFile()
        {
            FileStream fs = File.Open(@"D:\elmos\AP\homeworks\tamrin 3\Airport 2\Airport 2\bin\Debug\Users.txt", FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            list = (List<User>)formatter.Deserialize(fs);
            foreach (var user in users)
            {
                user.Password = Encrypting.decrypting(user.Password, "VXAQtfAhbuNL/nR0T0VQWw==");
            }

            fs.Close();

        }
        public static void AddUserToFlight(string Username, string Id, int seatNumber, int flight)
        {
            int t = 0;
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].UserName == Username)
                {
                    t = 1;
                    Passenger passenger = new Passenger(users[i].UserName, Id, seatNumber, flight, users[i].Name, users[i].FamilyName, users[i].Age, users[i].gender);
                    passenger.addPassengerToFlight(seatNumber);

                    break;
                }
            }
            if (t == 0)
            {
                Console.WriteLine("Could not find this UserName among the users");
            }
        }
        public static void AddFLight(Flight flight, string username)
        {
            foreach (var user in users)
            {
                if (user.UserName == username)
                {
                    user.flights.Add(flight);
                    break;
                }
            }
        }
        public static bool CheckUsername(string username)
        {
            foreach (var user in users)
            {
                if (user.UserName == username)
                {
                    return false;
                }
            }
            return true;
        }

        public static void CancellFlight(string UserName)
        {
            foreach (User user in users)
            {
                if (user.UserName == UserName)
                {
                    user.cancellFLight();
                }
            }

        }
        public void cancellFLight()
        {
            Console.Write("Flight Id: ");
            string flightId = Console.ReadLine();
            for (int i = 0; i < flights.Count; i++)
            {
                if (flights[i].checkId(flightId, UserName))
                {
                    flights.RemoveAt(i);
                }
            }
        }

    }

    class Encrypting
    {
        private static object aesAlg;

        public static string encrypting(string Password, string key)
        {
            byte[] encrypted;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.Mode = CipherMode.ECB;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var msEncrypt = new System.IO.MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(Password);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encrypted);
        }
        public static string decrypting(string Password, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.Mode = CipherMode.ECB;
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                byte[] cipherBytes = Convert.FromBase64String(Password);

                using (var msDecrypt = new System.IO.MemoryStream(cipherBytes))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

        }
    }




    [Serializable]
    class Passenger
    {
        public static List<Passenger> passengers = new List<Passenger>();
        public Passenger(string username, string flightid, int seatnumber, int flight, string Name, string Familyname, int age, User.Gender gender)
        {
            UserName = username;
            FlightId = flightid;
            SeatNumber = seatnumber;
            this.fl = (flightMode)(flight - 1);
            passengers.Add(this);
            this.Name = Name;
            FamilyName = Familyname;
            Age = age;
            this.gender = gender;
            if (Age < 14)
            {
                grade = Grade.children;
            }
            else if ((Age >= 14) || (Age < 30))
            {
                grade = Grade.young;
            }
            else if ((Age >= 30) || (Age < 60))
            {
                grade = Grade.middleAge;
            }
            else
            {
                grade = Grade.elderly;
            }
        }
        public enum Grade { children, young, middleAge, elderly }
        Grade grade;
        string UserName;
        string Name;
        string FamilyName;
        int Age;
        User.Gender gender;
        public string userName { get => UserName; }
        string FlightId;
        int SeatNumber;
        public int seatNumber { get => SeatNumber; }

        public enum flightMode { Economy, Business, FirstClass }
        flightMode fl;
        public void addPassengerToFlight(int seat)
        {
            if (Flight.AddPassenger(this, FlightId, UserName, seat))
            {

                FileStream fs = new FileStream(@"D:\elmos\AP\homeworks\tamrin 3\Airport 2\Airport 2\bin\Debug\Flights\" + FlightId + "\\" + UserName + ".txt", FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, this);
                fs.Close();
            }

        }
        public void gradeGiving()
        {

            if (Age < 14)
            {
                grade = Grade.children;
            }
            else if ((Age >= 14) && (Age < 30))
            {
                grade = Grade.young;
            }
            else if ((Age >= 30) && (Age < 60))
            {
                grade = Grade.middleAge;
            }
            else
            {
                grade = Grade.elderly;
            }

        }



        //for searching
        public bool SearchByUserName(string username)
        {
            if (UserName == username)
            {
                Console.WriteLine($"UserName: {UserName}, Name: {Name}, FamilyName: {FamilyName}, Age: {Age} ,flight Id: {FlightId}, seatNuber: {SeatNumber}, class: {fl} ");
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool SearchByName(string name)
        {
            if (Name == name)
            {
                Console.WriteLine($"UserName: {UserName}, Name: {Name}, FamilyName: {FamilyName}, Age: {Age} ,flight Id: {FlightId}, seatNuber: {SeatNumber}, class: {fl} ");
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool SearchByFamilyName(string Familyname)
        {
            if (FamilyName == Familyname)
            {
                Console.WriteLine($"UserName: {UserName}, Name: {Name}, FamilyName: {FamilyName}, Age: {Age} ,flight Id: {FlightId}, seatNuber: {SeatNumber}, class: {fl} ");
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool SearchByFlightClass(flightMode flightClass)
        {
            if (fl == flightClass)
            {
                Console.WriteLine($"UserName: {UserName}, Name: {Name}, FamilyName: {FamilyName}, Age: {Age} ,flight Id: {FlightId}, seatNuber: {SeatNumber}, class: {fl} ");
                return true;
            }
            else
            {
                return false;
            }
        }
        public static int Mencounter(List<Passenger> passengers)
        {
            int counter = 0;
            foreach (var passenger in passengers)
            {
                if (passenger.gender == User.Gender.man)
                {
                    counter++;
                }
            }
            return counter;
        }
        public static void GradeCounter(List<Passenger> passengers)
        {
            int childrenCOunter = 0;
            int youngCounter = 0;
            int middleAgedCounter = 0;
            int elderlyCounter = 0;
            foreach (var passenger in passengers)
            {
                switch (passenger.grade)
                {
                    case Grade.children:
                        childrenCOunter++;
                        break;
                    case Grade.young:
                        youngCounter++;
                        break;
                    case Grade.middleAge:
                        middleAgedCounter++;
                        break;
                    case Grade.elderly:
                        elderlyCounter++;
                        break;

                }
            }
            Flight.childrenCOunter = childrenCOunter;
            Flight.youngCounter = youngCounter;
            Flight.middleAgedCounter = middleAgedCounter;
            Flight.elderlyCounter = elderlyCounter;
        }
    }


    [Serializable]
    class Manager
    {
        string UserName;
        string Password;
        public Manager(string username, string password)
        {
            UserName = username;
            Password = password;
        }
    }



}
