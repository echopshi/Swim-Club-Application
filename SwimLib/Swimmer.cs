using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwimLib
{
    [Serializable]
    public class Swimmer : Registrant
    {
        private List<TimeSpan> bestTimes = new List<TimeSpan>();
        //private TimeSpan[] bestTimes = new TimeSpan[100];
        private Coach myCoach;

        public List<TimeSpan> BestTimes
        {
            get { return bestTimes; }
            set { bestTimes = value; }
        }
        public Coach Coach
        {
            get { return myCoach; }
            set
            {
                if (value.Club == null)
                {
                    throw new Exception("Coach is not assigned to the club");
                }
                else if (value.Club == Club)
                {
                    myCoach = value;

                    if (!myCoach.Swimmers.Contains(this))
                    {
                        myCoach.AddSwimmer(this);
                    }

                }
            }
        }
        public Swimmer() { }
        public Swimmer(List<TimeSpan> bestTimes, Coach myCoach)
        {
            BestTimes = bestTimes;
            Coach = myCoach;
        }

        public Swimmer(string name, DateTime dateOfBirth, Address address, long phoneNumber) : base(name, dateOfBirth, address, phoneNumber) { }

        public Swimmer(int regNumber, string name, DateTime dateOfBirth, Address address, long phoneNumber) : base(regNumber, name, dateOfBirth, address, phoneNumber)
        {
        }


        public TimeSpan GetBestTime(PoolType course, Stroke stroke, EventDistance distance)
        {
            BestTimes = new List<TimeSpan>();
            TimeSpan result = new TimeSpan();

            for (int i = 0; i < SwimMeetInfo.Count; i++)
            {
                if (SwimMeetInfo[i][0] == (int)course && SwimMeetInfo[i][1] == (int)stroke && SwimMeetInfo[i][2] == (int)distance)
                {
                    BestTimes.Add(SwimTimeSpan[i]);
                }
            }

            if (BestTimes.Count > 0)
            {
                result = BestTimes[0];
                for (int i = 0; i < BestTimes.Count; i++)
                {
                    if (TimeSpan.Compare(result, BestTimes[i]) == 1)
                    {
                        result = BestTimes[i];
                    }
                }
            }
            return result;
        }
        public void AddAsBestTime(TimeSpan time, EventDistance distance, Stroke stroke, PoolType course)
        {
            TimeSpan result = GetBestTime(course, stroke, distance);
            if (time > result)
            {
                throw new Exception("Sorry, this is not the best time!");
            }
            else
            {
                BestTimes.Add(time);
            }
        }

        public override string ToString()
        {
            return base.ToString() + $"Coach: {myCoach?.Name ?? "not assigned"}\n";
        }
    }
}
