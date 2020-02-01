using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SwimLib
{
    public class SwimmersManager : ISwimmersRepository
    {
        private List<Registrant> swimmers = new List<Registrant>();
        private ClubsManager clmg;

        public List<Registrant> Swimmers
        {
            get { return swimmers; }
            set { swimmers = value; }
        }
        public int Number
        {
            get { return Swimmers.Count; }
        }

        public SwimmersManager() { }

        public SwimmersManager(ClubsManager clmg)
        {
            this.clmg = clmg;
        }
        public void Add(Registrant swimmer)
        {
            if (!Swimmers.Contains(swimmer))
            {
                Swimmers.Add(swimmer);
            }
            else
            {
                throw new Exception("The swimmer already added into swimmer manager");
            }
        }

        public Registrant GetByRegNum(int regNumber)
        {
            for (int i = 0; i < Number; i++)
            {
                if (Swimmers[i].RegNumber == regNumber)
                {
                    return Swimmers[i];
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
                    string[] fields = record.Split(deli);
                    int regNumber;
                    DateTime dateTime;
                    long phoneNumber;
                    int clubNumber;
                    try
                    {
                        try
                        {
                            regNumber = Convert.ToInt32(fields[0]);
                        }
                        catch (Exception)
                        {
                            throw new Exception($"Invalid swimmer record. Invalid registration number:\n         {record}");
                        }
                        try
                        {
                            dateTime = Convert.ToDateTime(fields[2]);
                        }
                        catch (Exception)
                        {
                            throw new Exception($"Invalid swimmer record. Birth date is invalid:\n         {record}");
                        }
                        try
                        {
                            phoneNumber = Convert.ToInt64(fields[7]);
                        }
                        catch (Exception)
                        {
                            throw new Exception($"Invalid swimmer record. Phone number wrong format:\n         {record}");
                        }
                        if (fields[1] == "")
                        {
                            throw new Exception($"Invalid swimmer record. Invalid swimmer name: \n         {record}");
                        }

                        Registrant result = new Registrant(regNumber, fields[1], dateTime, new Address(fields[3], fields[4], fields[5], fields[6]), phoneNumber);
                        // assign the corresponding club to swimmer
                        //if (fields[8] != "")
                        //{
                        //    clubNumber = Convert.ToInt32(fields[8]);
                        //    result.Club = clmg.GetByRegNum(clubNumber);
                        //}
                        if (GetByRegNum(regNumber) == null)
                        {
                            Swimmers.Add(result);
                        }
                        else
                        {
                            throw new Exception($"Invalid Swimmer record. Swimmer with the registration number already exists:\n         {record}");
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

        public void Save(string fileName, string delimeter)
        {
            FileStream fileOut = null;
            StreamWriter writer = null;
            try
            {
                fileOut = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                writer = new StreamWriter(fileOut);

                foreach (Registrant swimmer in Swimmers)
                {
                    string result = swimmer.RegNumber + delimeter + swimmer.Name + delimeter + swimmer.DateOfBirth
                        + delimeter + swimmer.Address.Street + delimeter + swimmer.Address.City
                        + delimeter + swimmer.Address.Province + delimeter + swimmer.Address.PostalCode
                        + delimeter + swimmer.PhoneNumber + delimeter;
                    //if (swimmer.Club != null)
                    //{
                    //    result += swimmer.Club.ClubNumber;
                    //}
                    writer.WriteLine(result);
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

    }
}

