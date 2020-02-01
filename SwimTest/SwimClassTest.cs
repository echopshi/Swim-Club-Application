using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SwimLib;

namespace SwimTest
{
    [TestClass]
    public class SwimClassTest
    {
        [TestMethod]
        public void Swim_CreatedWithValidInput_Success()
        {
            Swim swim1 = new Swim();
            swim1.Heat = 1;
            swim1.Lane = 1;

            Assert.AreEqual(swim1.Heat, 1);
            Assert.AreEqual(swim1.Lane, 1);
            Assert.AreEqual(swim1.TimeSwam, new TimeSpan());
        }

        [TestMethod]
        public void Swim_CreatedWithInvalidInput_Exception()
        {
            try
            {
                Swim swim1 = new Swim(TimeSpan.Parse(""), 1, 1);
            }
            catch (FormatException e)
            {
                StringAssert.Contains(e.Message, "String was not recognized as a valid TimeSpan");
            }
        }
    }
}
