using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwimLib
{
    [Serializable]
    public struct Address
    {
        string street;
        string city;
        string province;
        string postalCode;

        public Address(string street, string city, string province, string postalCode)
        {
            this.street = street;
            this.city = city;
            this.province = province;
            this.postalCode = postalCode;
        }

        public string Street
        {
            get { return street; }
            set { street = value; }
        }
        public string City
        {
            get { return city; }
            set { city = value; }
        }
        public string PostalCode
        {
            get { return postalCode; }
            set { postalCode = value; }
        }
        public string Province
        {
            get { return province; }
            set { province = value; }
        }
        public override string ToString()
        {
            string result;
            result = $"{Street}, {City}, {Province}, {PostalCode}";
            return result;
        }
    }
}
