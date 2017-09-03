using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace TestAnimal 
{
    //изменение
    [Table(Name = "Animal")]//для linq
   public class Animal: IFields
    {

        // private int id;
        //private double mass;
        //private String name;

        // public static List<Animal> list = new List<Animal>();
        [Column(IsPrimaryKey = true)]
        public int id
        {
            get;
            set;
        }




        [Column(Name = "Mass")]
        public double mass
        {
            get;
            set;
        }
        [Column(Name = "Name")]
        public String name
        {
            get;
            set;
        }

        [Column(Name = "TypeAnimal")]
        public int typeAnimal
        {
            get;
            set;
        }




        public Animal(int id, double mass, String name)
        {
            this.id = id;
            this.mass = mass;
            this.name = name;
        }

        public Animal(int id, double mass, String name, int type)
        {
            this.id = id;
            this.mass = mass;
            this.name = name;
            this.typeAnimal = type;
        }

        public Animal()
        {

        }


        public virtual void Print()
        {
            Console.WriteLine("Id={0}, Масса={1}, Имя={2}", this.id, this.mass, this.name);
        }

        public virtual void Insert()
        {
            /*
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = Program.connect.ConnectionString;
            cn.Open();*/
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = RepositoryBase<Animal>.sqlConnect.ConnectionString;
                cn.Open();
                SqlCommand command1 = new SqlCommand("INSERT INTO Animal(id,mass,name,typeAnimal) VALUES(@id,@mass,@name,1)", cn);
                command1.Parameters.AddWithValue("@id", this.id);
                command1.Parameters.AddWithValue("@mass", this.mass);
                command1.Parameters.AddWithValue("@name", this.name);

                command1.ExecuteNonQuery();
            }

        }
        /*
        public virtual Animal Serialize(SqlDataReader reader)
        {
            RepositoryBase<Animal>.list.Add(new Animal(System.Convert.ToInt16(reader["id"]), System.Convert.ToDouble(reader["mass"]), System.Convert.ToString(reader["name"])));
            return new Animal(System.Convert.ToInt16(reader["id"]), System.Convert.ToDouble(reader["mass"]), System.Convert.ToString(reader["name"]));
        }
        */


    }
}
