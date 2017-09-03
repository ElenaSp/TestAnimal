using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestAnimal
{

    class RepositoryAnimal : RepositoryBase<Animal>
    {
        public Dictionary<int, List<Animal>> dictAnimal = new Dictionary<int, List<Animal>>();       

        public List<Animal> LoadByType(int type)
        {
            List<Animal> l = new List<Animal>();

            if (!dictAnimal.TryGetValue(type, out l))
            {
            l = Load("SELECT * FROM Animal WHERE typeAnimal=" + type);
            dictAnimal.Add(type, l);
            }
             else            
             l=dictAnimal[type];                

            return l;
        }
    }
}