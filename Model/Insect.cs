using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAnimal
{
    class Insect : Animal , IFields
    {
        // int quantity;//количество насекомых
        [DataMapper(true)]
        public int quantity
        {
            get;
            set;
        }
        public Insect(int id, double mass, string name,int quantity) : base(id, mass, name)
        {
            this.quantity = quantity;
        }

        public override void Print()
        {
            Console.WriteLine("Id={0}, Масса={1}, Имя={2}, Количество={3}", base.id, base.mass, base.name,this.quantity);
        }
        public Insect()
        { }
        public override void Insert()
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = RepositoryBase<Animal>.sqlConnect.ConnectionString; cn.Open();
                SqlCommand command1 = new SqlCommand("INSERT INTO Animal(id,mass,name,typeAnimal) VALUES(@id,@mass,@name,2)", cn);
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
            RepositoryBase<Animal>.list.Add(new Insect(System.Convert.ToInt16(reader["id"]), System.Convert.ToDouble(reader["mass"]), System.Convert.ToString(reader["name"]), 10));
            return new Insect(System.Convert.ToInt16(reader["id"]), System.Convert.ToDouble(reader["mass"]), System.Convert.ToString(reader["name"]),10);

        }*/

    }
}
