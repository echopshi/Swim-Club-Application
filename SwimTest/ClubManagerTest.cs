using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SwimLib;
using System.Collections.Generic;

namespace SwimTest
{
    [TestClass]
    public class ClubManagerTest
    {
        [TestMethod]
        public void ClubManager_CreateWithValidInput_Success()
        {
            ClubsManager clbMng = new ClubsManager();
            List<Club> clubs = new List<Club>();

            clbMng.Clubs = clubs;

            Assert.AreEqual(clbMng.Clubs, clubs);
            Assert.AreEqual(clbMng.Number, 0);
        }


        [TestMethod]
        public void ClubManager_AddClubCorrectly_Success()
        {
            ClubsManager clbMng = new ClubsManager();
            Club club = new Club();

            clbMng.Add(club);

            Assert.AreEqual(clbMng.Clubs[0], club);
            Assert.AreEqual(clbMng.Number, 1);
        }

        [TestMethod]
        public void ClubManager_AddDuplicateClub_Exception()
        {
            ClubsManager clbMng = new ClubsManager();
            Club club = new Club();
            clbMng.Add(club);

            try
            {
                clbMng.Add(club);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "Club already exist!");
            }
        }


        [TestMethod]
        public void ClubManager_GetClubByRegNum_Success()
        {
            ClubsManager clbMng = new ClubsManager();
            Club club = new Club();
            clbMng.Add(club);

            Club newClub = clbMng.GetByRegNum(club.ClubNumber);

            Assert.AreEqual(club, newClub);
        }

        [TestMethod]
        public void ClubManager_GetClubByInvalidNum_Output()
        {
            ClubsManager clbMng = new ClubsManager();
            Club club = new Club();
            clbMng.Add(club);

            Club newClub = clbMng.GetByRegNum(0);

            Assert.AreEqual(newClub, null);
        }
    }
}
