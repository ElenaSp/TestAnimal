using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAnimal
{
    class AnimalType
    {

        public int idType
        {

            get;
            set;

        }
        public string nameType
        {
            get;
            set;
        }





        public  AnimalType(int id, string name)
        {
            idType = id;
            nameType = name;
        }


    }
}
