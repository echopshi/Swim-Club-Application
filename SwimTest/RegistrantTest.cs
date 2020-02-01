using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SwimLib;

namespace SwimTest
{
    [TestClass]
    public class RegistrantTest
    {

        [TestMethod]
        public void Registrant__CreatedWithValidInput_Success()
        {
            Registrant swimmer1 = new Registrant("Bob Smith", new DateTime(1970, 1, 1),
                                                    new Address("35 Elm St", "Toronto", "ON", "M2M 2M2"), 4161234567);
            Assert.AreEqual(swimmer1.Name, "Bob Smith");
            Assert.AreEqual(swimmer1.DateOfBirth, new DateTime(1970, 1, 1));
            Assert.AreEqual(swimmer1.Address, new Address("35 Elm St", "Toronto", "ON", "M2M 2M2"));
            Assert.AreEqual(swimmer1.PhoneNumber, 4161234567);

        }

        [TestMethod]
        public void Registrant_CreatedWithInvalidPhone_Output()
        {
            Registrant swimmer2 = new Registrant();
            Assert.AreEqual(swimmer2.Name, null);
            Assert.AreEqual(swimmer2.PhoneNumber, 0);

            swimmer2.PhoneNumber = 123;
            Assert.AreEqual(swimmer2.PhoneNumber, 0);

            swimmer2.PhoneNumber = 4161234567;
            Assert.AreEqual(swimmer2.PhoneNumber, 4161234567);
        }

        [TestMethod]
        public void Registrant_CheckClub_Output()
        {
            Registrant swimmer2 = new Registrant();
            Assert.AreEqual(swimmer2.Club, null);

            Club club1 = new Club();
            swimmer2.Club = club1;
            Assert.AreEqual(swimmer2.Club, club1);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Registrant_CheckClub_Exception()
        {
            Registrant swimmer3 = new Registrant();
            Club club1 = new Club();
            Club club2 = new Club();
            swimmer3.Club = club1;

            // Exception happens here because we cannot assign different club to same registarnt
            swimmer3.Club = club2;
        }
    }
}
