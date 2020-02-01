using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwimLib
{
    [Serializable]
    public class Club
    {
        private int clubNumber;
        private string name;
        private Address address;
        private long phoneNumber;
        public static int nOfClubs;
        private List<Registrant> swimmers = new List<Registrant>();
        private List<Coach> coachs = new List<Coach>();

        public Club()
        {
            nOfClubs++;
            ClubNumber = nOfClubs;
        }
        public Club(string name, Address address, long phoneNumber)
        {
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            nOfClubs++;
            ClubNumber = nOfClubs;
        }
        public Club(int clubNumber, string name, Address address, long phoneNumber)
        {
            ClubNumber = clubNumber;
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
        }
        public int ClubNumber
        {
            private set { clubNumber = value; }
            get { return clubNumber; }
        }
        public string Name
        {
            set { name = value; }
            get { return name; }
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
            get { return phoneNumber; }
        }
        public override string ToString()
        {
            string result;
            result = $"Name: {Name}\n" +
                $"Address:\n{Address}" +
                $"Phone: {PhoneNumber}\n" +
                $"Reg number: {ClubNumber}\n" +
                $"Swimmers: \n";
            for (int i = 0; i < swimmers.Count; i++)
            {
                result += $"        {swimmers[i].Name}\n";
            }
            result += $"Coaches: \n";
            for (int i = 0; i < coachs.Count; i++)
            {
                result += $"        {coachs[i].Name}\n";
            }
            return result;
        }

        public void AddSwimmer(Registrant swimmer)
        {

            if (swimmer.Club != this && swimmer.Club != null)
            {
                throw (new Exception($"Swimmer {swimmer.Name} already assigned to {swimmer.Club.Name} club"));
            }
            else if (swimmer.Club == null)
            {
                swimmers.Add(swimmer);
                swimmer.Club = this;
            }
            else if (swimmer.Club == this && !swimmers.Contains(swimmer))
            {
                swimmers.Add(swimmer);
            }
        }

        public void AddCoach(Coach coach)
        {
            if (coach.Club != this && coach.Club != null)
            {
                throw (new Exception($"Swimmer {coach.Name} already assigned to {coach.Club.Name} club"));
            }
            else if (coach.Club == null)
            {
                coachs.Add(coach);
                coach.Club = this;
            }
            else if (coach.Club == this && !coachs.Contains(coach))
            {
                coachs.Add(coach);
            }
        }

        public static List<Club> GetClubs()
        {
            List<Club> clubs = new List<Club>();

            Club club1 = new Club();
            club1.PhoneNumber = 4164444444;
            club1.Name = "NYAC";

            Club club2 = new Club("CCAC", new Address("35 River St", "Toronto", "ON", "M2M 5M5"), 4165555555);

            clubs.Add(club1);
            clubs.Add(club2);
            return clubs;
        }
    }
}
