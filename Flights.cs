using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Airport_2
{
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

        public static void AddPassenger(Passenger passenger, string Id, string username)
        {
            for (int i = 0; i < flights.Count; i++)
            {
                if (flights[i].Id == Id)
                {
                    flights[i].passengers.Add(passenger);
                    flights[i].FreeSeats--;
                    flights[i].PassengerMembers++;
                    User.AddFLight(flights[i], username);
                    break;
                }
            }
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
                    break;
                }
            }
            if (t == 0)
            {
                Console.WriteLine("Couldn't find such a familyname among passengers");
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
            Console.WriteLine($"Men static: {MenPer}, Womrn static: {womenPer}\nAges statics: children: {childrenPer}, youngs: {youngPer}, middle-Aged people:{middleAgedPer}, elderly: {elderlyPer}");
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
        public static void SearchByStartTime(int x , int h)
        {
            int a = 1;
            foreach (var flight in flights)
            {
                if ((x<=int.Parse(flight.StartTime))&&(h>=int.Parse(flight.StartTime)))
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
                    Console.WriteLine(a + ". " + "Id: " + flight.Id+ " End time: " + flight.EndTime);
                    a++;
                }
            }
        }
        public bool checkId(string Id, string UserName)
        {
            if(this.Id == Id)
            {
                for(int i = 0; i<passengers.Count; i++)
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
            foreach(Flight flight in flights)
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

}
