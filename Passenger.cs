using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Airport_2
{
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

        public enum flightMode { Economy, Business, FirstClass }
        flightMode fl;
        public void addPassengerToFlight()
        {
            Flight.AddPassenger(this, FlightId, UserName);
            FileStream fs = new FileStream(@"D:\elmos\AP\homeworks\tamrin 3\Airport 2\Airport 2\bin\Debug\Flights\" + FlightId + "\\" + UserName + ".txt", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, this);
            fs.Close();

        }
        public void gradeGiving()
        {

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
            foreach(var passenger in passengers)
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
}
