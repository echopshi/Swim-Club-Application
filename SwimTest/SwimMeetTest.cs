using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SwimLib;

namespace SwimTest
{
    [TestClass]
    public class SwimMeetTest
    {
        [TestMethod]
        public void Swim_createdWithValidInput_Success()
        {
            SwimMeet meet1 = new SwimMeet();
            meet1.Name = "Winter Splash";
            meet1.StartDate = new DateTime(2017, 1, 10);
            meet1.EndDate = new DateTime(2017, 1, 12);

            Assert.AreEqual(meet1.Name, "Winter Splash");
            Assert.AreEqual(meet1.StartDate, new DateTime(2017, 1, 10));
            Assert.AreEqual(meet1.EndDate, new DateTime(2017, 1, 12));
            // as not defined, number of lanes should be 8 in default
            Assert.AreEqual(meet1.NoOfLanes, 8);
            // as not defined, the course for this swim meet is SCM in default
            Assert.AreEqual(meet1.MyCourse, PoolType.SCM);
        }

        [TestMethod]
        public void Swim_createdWithInvalidEndDate_Output()
        {
            SwimMeet meet2 = new SwimMeet();
            meet2.StartDate = new DateTime(2017, 1, 15);
            meet2.EndDate = new DateTime(2017, 1, 12);

            Assert.AreEqual(meet2.StartDate, new DateTime(2017, 1, 15));
            // since the end date is less than the start date, end date will be set to the start date
            Assert.AreEqual(meet2.EndDate, new DateTime(2017, 1, 15));
        }

        [TestMethod]
        public void Swim_checkAddEvent_Success()
        {
            SwimMeet meet3 = new SwimMeet();
            Event myEvent = new Event();

            meet3.AddEvent(myEvent);
            Assert.AreEqual(meet3.Events[0], myEvent);
        }



        //[TestMethod]
        //public void Swim_SeedEachEvent_Success()
        //{
        //    SwimMeet meet5 = new SwimMeet();
        //    Event myEvent = new Event();

        //    meet5.AddEvent(myEvent);
        //    meet5.Events[0].seed(meet5.NoOfLanes);

        //}
    }
}
