
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SwimLib;

namespace SwimTest
{
    [TestClass]
    public class ClubTest
    {
        [TestMethod]
        public void Club_CreateClubWithValidInput_Success()
        {
            // test 1 creating the club by calling the proporties 
            Club club1 = new Club();
            club1.PhoneNumber = 4164444444;
            club1.Name = "NYAC";

            // test 2 creating club by calling the constructor
            Club club2 = new Club("CCAC", new Address("35 River St", "Toronto", "ON", "M2M 5M5"), 4165555555);

            // check the information if that matched
            Assert.AreEqual(club1.Address, new Address());
            Assert.AreEqual(club1.PhoneNumber, 4164444444);
            Assert.AreEqual(club1.Name, "NYAC");

            Assert.AreEqual(club2.PhoneNumber, 4165555555);
            Assert.AreEqual(club2.Name, "CCAC");
        }

        [TestMethod]
        public void Club_CreateClubWithInvalidPhone_Output()
        {
            Club club2 = new Club();
            Assert.AreEqual(club2.Name, null);
            Assert.AreEqual(club2.PhoneNumber, 0);

            club2.PhoneNumber = 123;
            Assert.AreEqual(club2.PhoneNumber, 0);

            club2.PhoneNumber = 4161234567;
            Assert.AreEqual(club2.PhoneNumber, 4161234567);
        }

        [TestMethod]
        public void Club_AddSwimmerToClub_UpdateSwimmer()
        {
            Club club1 = new Club();
            Registrant swimmer1 = new Registrant();
            Assert.AreEqual(swimmer1.Club, null);

            club1.AddSwimmer(swimmer1);
            Assert.AreEqual(swimmer1.Club, club1);
        }

        [TestMethod]
        public void Club_AddSwimmerToClub_ShouldThrowException()
        {
            Club club1 = new Club();
            Club club2 = new Club();
            Registrant swimmer1 = new Registrant();

            club1.AddSwimmer(swimmer1);
            try
            {
                club2.AddSwimmer(swimmer1);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "Swimmer  already assigned to  club");
                return;
            }
        }

    }
}
