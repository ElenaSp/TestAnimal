using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAnimal
{
    interface IFields
    {
        int id { get; set; }
        double mass { get; set; }
        string name { get; set; }
        int typeAnimal { get; set; }
        void Print();

    }
}
