using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SwimLib
{
    [Serializable]
    public class ClubsManager : IClubsRepository
    {
        private List<Club> clubs = new List<Club>();

        public List<Club> Clubs
        {
            get { return clubs; }
            set { clubs = value; }
        }
        public int Number
        {
            get { return Clubs.Count; }
        }
        public ClubsManager() { }

        public void Add(Club club)
        {
            if (Clubs.Contains(club))
            {
                throw (new Exception("Club already exist!"));
            }
            else
            {
                Clubs.Add(club);
            }
        }
        public Club GetByRegNum(int clubNumber)
        {
            for (int i = 0; i < Number; i++)
            {
                if (Clubs[i].ClubNumber == clubNumber)
                {
                    return Clubs[i];
                }
            }
            return null;
        }
        public void Load(string fileName, string delimeter)
        {
            char deli = Convert.ToChar(delimeter);
            string record;
            FileStream fileIn = null;
            StreamReader reader = null;

            try
            {
                fileIn = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                reader = new StreamReader(fileIn);

                record = reader.ReadLine();
                while (record != null)
                {
                    try
                    {
                        string[] fields = record.Split(deli);
                        if (fields[0] != "" && fields[6].Length == 10)
                        {
                            int clubNumber = Convert.ToInt32(fields[0]);
                            long phoneNumber = Convert.ToInt64(fields[6]);
                            Club result = new Club(clubNumber, fields[1], new Address(fields[2], fields[3], fields[4], fields[5]), phoneNumber);
                            if (GetByRegNum(clubNumber) == null)
                            {
                                Clubs.Add(result);
                            }
                            else
                            {
                                throw new Exception($"Invalid club record. Club with the registration number already exists:\n         {record}");
                            }

                        }
                        else if (fields[0] == "")
                        {
                            throw new Exception($"Invalid club record. Club number is not valid:\n         {record}");
                        }
                        else if (fields[6].Length != 10)
                        {
                            throw new Exception($"Invalid club record. Phone number wrong format:\n         {record}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        record = reader.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (fileIn != null)
                {
                    fileIn.Close();
                }
            }
        }
        //private void AddNewClub(string record, char deli) {

        //}
        public void Save(string fileName, string delimeter)
        {
            FileStream fileOut = null;
            StreamWriter writer = null;
            try
            {
                fileOut = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                writer = new StreamWriter(fileOut);

                foreach (Club club in Clubs)
                {
                    writer.WriteLine(club.ClubNumber + delimeter + club.Name
                        + delimeter + club.Address.Street + delimeter + club.Address.City
                        + delimeter + club.Address.Province + delimeter + club.Address.PostalCode
                        + delimeter + club.PhoneNumber);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
                if (fileOut != null)
                {
                    fileOut.Close();
                }
            }
        }

        public void Load(string fileName)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileIn = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            Clubs = (List<Club>)binaryFormatter.Deserialize(fileIn);
        }
    }
}
