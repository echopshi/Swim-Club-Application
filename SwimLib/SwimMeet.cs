using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwimLib
{
    public enum PoolType
    {
        SCM,
        SCY,
        LCM
    };
    public class SwimMeet
    {

        private DateTime startDate;
        private DateTime endDate;
        private string name;
        private PoolType myCourse;
        private int noOfLanes = 8;
        private List<Event> events = new List<Event>();


        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                if (startDate < value) { endDate = value; }
                else
                {
                    //string message = "ERROR, the end date must be greater than the start date!";
                    endDate = StartDate;
                }
            }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public PoolType MyCourse
        {
            get { return myCourse; }
            set { myCourse = value; }
        }
        public int NoOfLanes
        {
            get { return noOfLanes; }
            set { noOfLanes = value; }
        }
        public List<Event> Events
        {
            get { return events; }
        }

        public SwimMeet() { }
        public SwimMeet(string name, DateTime startTime, DateTime endDate, PoolType myCourse, int noOfLanes)
        {
            Name = name;
            StartDate = startTime;
            EndDate = endDate;
            MyCourse = myCourse;
            NoOfLanes = noOfLanes;
        }
        public override string ToString()
        {
            string result;
            result = $"Swim meet name: {Name}\n" +
                $"From-to: {StartDate.ToShortDateString()} to {EndDate.ToShortDateString()}\n" +
                $"Pool type: {MyCourse}\n" +
                $"No lanes: {NoOfLanes}\n" +
                $"Events: ";
            for (int i = 0; i < events.Count; i++)
            {
                result += $"\n{ events[i],-8}" + $"        Swimmers: \n";
                if (events[i].NoOfSwimmer != 0)
                {
                    for (int j = 0; j < events[i].Swimmers.Count; j++)
                    {
                        result += ($"        {events[i].Swimmers[j].Name,-20}");
                        if (events[i].Swims.Count == 0)
                        {
                            result += ("\n               Not seeded/no swim\n");
                        }
                        else
                        {
                            result += ($"{events[i].Swims[j]}\n");
                        }
                    }
                }
            }
            return result;
        }

        public void AddEvent(Event newEvent)
        {
            newEvent.MySwimMeet = (int)MyCourse;
            events.Add(newEvent);
        }
        public void Seed()
        {
            for (int i = 0; i < events.Count; i++)
            {
                events[i].seed(NoOfLanes);
            }
        }
        
    }
}

