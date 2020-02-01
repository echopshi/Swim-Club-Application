using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwimLib
{
    [Serializable]
    public class Registrant
    {
        private int regNumber;
        private string name;
        private DateTime dateOfBirth;
        private Address address;
        private long phoneNumber;
        public Club club;
        private List<List<int>> swimMeetInfo = new List<List<int>>();
        private List<TimeSpan> swimTimeSpan = new List<TimeSpan>();

        public int RegNumber
        {
            private set { regNumber = value; }
            get { return regNumber; }
        }
        public string Name
        {
            set { name = value; }
            get { return name; }
        }
        public DateTime DateOfBirth
        {
            set { dateOfBirth = value; }
            get { return dateOfBirth; }
        }
        public Address Address
        {
            get { return address; }
            set { address = value; }
        }
        public long PhoneNumber
        {
            set
            {
                if (value.ToString().Length == 10)
                {
                    phoneNumber = value;
                }
                else
                {
                    phoneNumber = 0;
                }
            }
            get
            {
                return phoneNumber;
            }
        }
        public virtual Club Club
        {
            get { return club; }
            set
            {
                if (club == null)
                {
                    club = value;
                    club.AddSwimmer(this);
                }
                else
                {
                    value.AddSwimmer(this);
                }
            }
        }
        public List<List<int>> SwimMeetInfo
        {
            get { return swimMeetInfo; }
            set { swimMeetInfo = value; }
        }

        public List<TimeSpan> SwimTimeSpan
        {
            get { return swimTimeSpan; }
            set { swimTimeSpan = value; }
        }

        public Registrant()
        {
            Club.nOfClubs++;
            RegNumber = Club.nOfClubs;
        }
        public Registrant(string name, DateTime dateOfBirth, Address address, long phoneNumber)
        {
            Club.nOfClubs++;
            RegNumber = Club.nOfClubs;

            Name = name;
            DateOfBirth = dateOfBirth;
            Address = address;
            PhoneNumber = phoneNumber;
        }

        public Registrant(int regNumber, string name, DateTime dateOfBirth, Address address, long phoneNumber)
        {
            RegNumber = regNumber;
            Name = name;
            DateOfBirth = dateOfBirth;
            Address = address;
            PhoneNumber = phoneNumber;
        }
        public override string ToString()
        {
            string result;
            result = $"Name: {Name}\n" +
                $"Address:\n{Address}" +
                $"Phone: {PhoneNumber}\n" +
                $"DOB: {DateOfBirth.ToString()}\n" +
                $"Reg number: {RegNumber}\n" +
                $"Club: {Club?.Name ?? "not assigned"}\n";
            return result;
        }

        public static List<Registrant> GetRegistrant()
        {
            List<Registrant> swimmers = new List<Registrant>();

            Registrant swimmer1 = new Swimmer("Bob Smith", new DateTime(1970, 01, 01),
                                                    new Address("35 Elm St", "Toronto", "ON", "M2M 2M2"), 4161234567);
            Registrant swimmer2 = new Swimmer();
            swimmer2.Address = new Address("1 King St", "Toronto", "ON", "M2M 3M3");
            swimmer2.Name = "John Lee";
            swimmer2.PhoneNumber = 4162222222;
            swimmer2.DateOfBirth = new DateTime(1950, 12, 01);

            Registrant swimmer3 = new Swimmer("Ann Smith", new DateTime(1975, 01, 01),
                                                    new Address("5 Queen St", "Toronto", "ON", "M2M 4M4"), 4163333333);

            swimmers.Add(swimmer1);
            swimmers.Add(swimmer2);
            swimmers.Add(swimmer3);

            return swimmers;
        }
        
    }
}
