using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAnimal
{
    [AttributeUsage(AttributeTargets.Property)]

    public sealed class DataMapperAttribute : System.Attribute
    {
        public bool Column;

        public DataMapperAttribute(bool b)
        {
            Column = b;
        }
    }
}