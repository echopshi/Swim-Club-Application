using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SwimLib;

namespace SwimTest
{
    [TestClass]
    public class EventTest
    {
        [TestMethod]
        public void Event_WithValidInput_Success()
        {
            Event _50free1 = new Event();
            _50free1.Distance = EventDistance._50;
            _50free1.Stroke = Stroke.Freestyle;

            Assert.AreEqual(_50free1.Distance, EventDistance._50);
            Assert.AreEqual(_50free1.Stroke, Stroke.Freestyle);

            Event _100fly = new Event(EventDistance._100, Stroke.Butterfly);
            Assert.AreEqual(_100fly.Distance, EventDistance._100);
            Assert.AreEqual(_100fly.Stroke, Stroke.Butterfly);
        }

        [TestMethod]
        public void Event_AddSwimmerWithValidInput_Success()
        {
            Event myEvent = new Event();
            Registrant swimmer1 = new Registrant();

            myEvent.AddSwimmer(swimmer1);

            Assert.AreEqual(myEvent.Swimmers[0], swimmer1);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Event_AddSwimmerWithDuplicateInput_ThrowException()
        {
            Event myEvent = new Event();
            Registrant swimmer1 = new Registrant();

            myEvent.AddSwimmer(swimmer1);

            // there will be an exception if add duplicate swimmer in one event
            myEvent.AddSwimmer(swimmer1);
        }


        [TestMethod]
        public void Event_EnterSwimmersTimeWithValidInput_Success()
        {
            Event myEvent = new Event();
            Registrant swimmer1 = new Registrant();
            myEvent.AddSwimmer(swimmer1);
            myEvent.seed(8);

            myEvent.EnterSwimmersTime(swimmer1, "00:30.13");

            Assert.AreEqual(myEvent.Swims[0].TimeSwam, TimeSpan.Parse("00:" + "00:30.13"));
        }

        [TestMethod]
        public void Event_EnterSwimmersTimeWithInvalidInput_Exception()
        {
            Event myEvent = new Event();
            Registrant swimmer1 = new Registrant();
            myEvent.AddSwimmer(swimmer1);
            myEvent.seed(8);

            try
            {
                myEvent.EnterSwimmersTime(swimmer1, "");
            }
            catch (FormatException e)
            {
                StringAssert.Contains(e.Message, "String was not recognized as a valid TimeSpan");
            }
        }

        [TestMethod]
        public void Event_SeedWithCorrectInput_success()
        {
            Event myEvent = new Event();
            Registrant swimmer1 = new Registrant();
            Registrant swimmer2 = new Registrant();
            myEvent.AddSwimmer(swimmer1);
            myEvent.AddSwimmer(swimmer2);

            myEvent.seed(2);

            Assert.AreEqual(myEvent.Swims[0].Heat, 1);
            Assert.AreEqual(myEvent.Swims[1].Heat, 1);

            Assert.AreEqual(myEvent.Swims[0].Lane, 1);
            Assert.AreEqual(myEvent.Swims[1].Lane, 2);
        }

        [TestMethod]
        public void Event_SeedWithInputMoreThanNofLanes_success()
        {
            Event myEvent = new Event();
            Registrant swimmer1 = new Registrant();
            Registrant swimmer2 = new Registrant();
            Registrant swimmer3 = new Registrant();
            myEvent.AddSwimmer(swimmer1);
            myEvent.AddSwimmer(swimmer2);
            myEvent.AddSwimmer(swimmer3);

            myEvent.seed(2);

            Assert.AreEqual(myEvent.Swims[0].Heat, 1);
            Assert.AreEqual(myEvent.Swims[1].Heat, 1);
            Assert.AreEqual(myEvent.Swims[2].Heat, 2);

            Assert.AreEqual(myEvent.Swims[0].Lane, 1);
            Assert.AreEqual(myEvent.Swims[1].Lane, 2);
            Assert.AreEqual(myEvent.Swims[2].Lane, 1);
        }
    }
}
