using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwimLib
{
    public class Swim
    {
        private TimeSpan timeSwam;
        private int heat;
        private int lane;

        public TimeSpan TimeSwam
        {
            get { return timeSwam; }
            set { timeSwam = value; }
        }
        public int Heat
        {
            get { return heat; }
            set { heat = value; }
        }
        public int Lane
        {
            get { return lane; }
            set { lane = value; }
        }

        public Swim() { }

        public Swim(TimeSpan timeSwam, int heat, int lane)
        {
            TimeSwam = timeSwam;
            Heat = heat;
            Lane = lane;
        }

        public override string ToString()
        {
            string result;
            result = $"H{heat}L{Lane}  time: ";
            if (TimeSwam.ToString() != "00:00:00")
            {
                result += TimeSwam.ToString().Substring(3, 8);
            }
            else
            {
                result += "no time";
            }
            return result;
        }
    }
}
