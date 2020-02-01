using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwimLib
{
    public enum EventDistance
    {
        _50,
        _100,
        _200,
        _400,
        _800,
        _1500
    };
    public enum Stroke
    {
        Butterfly,
        Backstroke,
        Breaststroke,
        Freestyle,
        IndividualMedley
    };
    public class Event
    {
        private EventDistance distance;
        private Stroke stroke;
        private List<Registrant> swimmers = new List<Registrant>();
        private List<Swim> swims = new List<Swim>();
        public EventDistance Distance
        {
            get { return distance; }
            set { distance = value; }
        }
        public Stroke Stroke
        {
            get { return stroke; }
            set { stroke = value; }
        }
        public List<Registrant> Swimmers
        {
            get { return swimmers; }
        }
        public List<Swim> Swims
        {
            get { return swims; }
        }
        public int NoOfSwimmer
        {
            get { return swimmers.Count; }
        }

        public int MySwimMeet
        {
            get;
            set;
        }
        public Event() { }
        public Event(EventDistance distance, Stroke stroke)
        {
            Distance = distance;
            Stroke = stroke;
        }
        public override string ToString()
        {
            string result = $"{Distance} {Stroke}\n";
            return result;
        }
        public void AddSwimmer(Registrant swimmer)
        {
            if (swimmers.Contains(swimmer))
            {
                throw (new Exception($"Swimmer {swimmer.Name}, {swimmer.RegNumber} is already entered"));
            }
            else
            {
                swimmer.SwimMeetInfo.Add(new List<int> { MySwimMeet, (int)Stroke, (int)Distance });
                if (swimmer.SwimMeetInfo.Count > swimmer.SwimTimeSpan.Count)
                {
                    swimmer.SwimTimeSpan.Add(new TimeSpan());
                }
                swimmers.Add(swimmer);
            }
        }
        public void EnterSwimmersTime(Registrant swimmer, string timeSwam)
        {
            if (!swimmers.Contains(swimmer))
            {
                throw new Exception("Swimmer has not entered event");
            }

            timeSwam = "00:" + timeSwam;
            TimeSpan newTimeSwan = TimeSpan.Parse(timeSwam);
            for (int i = 0; i < swimmers.Count; i++)
            {
                if (Swimmers[i] == swimmer)
                {
                    swims[i].TimeSwam = newTimeSwan;
                }
            }

            for (int i = 0; i < swimmer.SwimMeetInfo.Count; i++)
            {
                if (swimmer.SwimMeetInfo[i][0] == MySwimMeet
                    && swimmer.SwimMeetInfo[i][1] == (int)stroke
                    && swimmer.SwimMeetInfo[i][2] == (int)distance
                    && !swimmer.SwimTimeSpan.Contains(newTimeSwan)
                    && swimmer.SwimTimeSpan[i] == new TimeSpan())
                {
                    swimmer.SwimTimeSpan[i] = newTimeSwan;
                }
            }
        }
        public void seed(int noOfLanes)
        {
            int currentHeat = 0;
            int currentLane = 0;
            for (int i = 0; i < swimmers.Count; i++)
            {
                currentHeat = (i + noOfLanes) / noOfLanes;
                currentLane++;
                if (currentLane > noOfLanes)
                {
                    currentLane -= noOfLanes;
                }
                swims.Add(new Swim(new TimeSpan(), currentHeat, currentLane));
            }
        }

        public static List<Event> GetEvents()
        {
            List<Event> events = new List<Event>();

            Event _50free1 = new Event();
            _50free1.Distance = EventDistance._50;
            _50free1.Stroke = Stroke.Freestyle;
            events.Add(_50free1);
            events.Add(new Event(EventDistance._100, Stroke.Butterfly));
            events.Add(new Event(EventDistance._200, Stroke.Breaststroke));
            events.Add(new Event(EventDistance._400, Stroke.Freestyle));
            events.Add(new Event(EventDistance._1500, Stroke.Freestyle));
            events.Add(new Event(EventDistance._1500, Stroke.Freestyle));

            return events;
        }

    }
}
