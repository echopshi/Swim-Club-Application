using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwimLib
{
    public interface IClubsRepository
    {
        int Number { get; }
        List<Club> Clubs { get; set; }
        void Add(Club club);
        Club GetByRegNum(int clubNumber);
        void Load(string fileName, string delimeter);
        void Save(string fileName, string delimeter);

    }
}
