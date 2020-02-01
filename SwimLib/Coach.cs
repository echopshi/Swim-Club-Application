using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwimLib
{
    [Serializable]
    public class Coach : Registrant
    {
        private string credentials;
        private List<Swimmer> swimmers = new List<Swimmer>();

        public string Credentials
        {
            get { return credentials; }
            set { credentials = value; }
        }

        public List<Swimmer> Swimmers
        {
            get { return swimmers; }
            set { swimmers = value; }
        }

        public override Club Club
        {
            get { return club; }
            set
            {
                if (club == null)
                {
                    club = value;
                    club.AddCoach(this);
                }
                else
                {
                    value.AddCoach(this);
                }
            }
        }

        public Coach() { }

        public Coach(string credentials, List<Swimmer> swimmers)
        {
            Credentials = credentials;
            Swimmers = swimmers;
        }

        public Coach(string name, DateTime dateOfBirth, Address address, long phoneNumber) : base(name, dateOfBirth, address, phoneNumber) { }

        public void AddSwimmer(Swimmer swimmer)
        {
            if (!Swimmers.Contains(swimmer))
            {
                if (swimmer.Club == null)
                {
                    throw new Exception($"swimmer {swimmer.Name} not assigned club");
                }
                else if (Club == null)
                {
                    throw new Exception($"Coach {Name} not assigned club");
                }
                else if (swimmer.Club != Club)
                {
                    throw new Exception($"Coach {Name} and swimmer {swimmer.Name} are not in the same club");
                }
                else if (swimmer.Club == Club)
                {
                    if (swimmer.Coach == null)
                    {
                        swimmer.Coach = this;
                    }
                    else
                    {
                        Swimmers.Add(swimmer);
                    }
                }
            }
        }

        public override string ToString()
        {
            string result = base.ToString() + $"Credentials: {Credentials}\n" + "Swimmers:\n";
            for (int i = 0; i < Swimmers.Count; i++)
            {
                result += $"        {Swimmers[i].Name}\n";
            }
            return result;
        }
    }
}
