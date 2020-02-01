using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwimLib
{
    public interface ISwimmersRepository
    {
        List<Registrant> Swimmers { get; set; }
        int Number { get; }
        void Add(Registrant swimmer);
        Registrant GetByRegNum(int regNumber);
        void Load(string fileName, string delimeter);
        void Save(string fileName, string delimeter);
    }
}
