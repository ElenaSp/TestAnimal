using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAnimal
{
    class Butterfly:Insect, IFields
    {

        [DataMapper(true)]
        public String field1
        {
            get;
            set;
        }
        [DataMapper(true)]
        public String field2
        {
            get;
            set;
        }

        public Butterfly(int id, double mass, string name, int quantity, String field1, String field2) : base(id, mass, name, quantity)
        {
            this.field1 = field1;
            this.field2 = field2;
        }
        public Butterfly()
        { }

        public override void Print()
        {
            Console.WriteLine("Id={0}, Масса={1}, Имя={2}, Количество={3}, field1={4}, field2={5}", base.id, base.mass, base.name, base.quantity, this.field1, this.field2);
        }

        public override void Insert()
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = RepositoryBase<Animal>.sqlConnect.ConnectionString; cn.Open();
                SqlCommand command1 = new SqlCommand("INSERT INTO Animal(id,mass,name,typeAnimal) VALUES(@id,@mass,@name,3)", cn);
                command1.Parameters.AddWithValue("@id", base.id);
                command1.Parameters.AddWithValue("@mass", base.mass);
                command1.Parameters.AddWithValue("@name", base.name);

                command1.ExecuteNonQuery();
                cn.Close();
            }
        }
        /*
        public override Animal Serialize(SqlDataReader reader)
        {
            RepositoryBase<Animal>.list.Add(new Butterfly(System.Convert.ToInt16(reader["id"]), System.Convert.ToDouble(reader["mass"]), System.Convert.ToString(reader["name"]), 10,"1","2"));
            return new Butterfly(System.Convert.ToInt16(reader["id"]), System.Convert.ToDouble(reader["mass"]), System.Convert.ToString(reader["name"]), 10, "1", "2");
        }
        */
    }
}
