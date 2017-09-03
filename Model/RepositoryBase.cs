using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Linq;
using System.Threading;

namespace TestAnimal
{

    class RepositoryBase<T> where T : class,IFields, new()
    {

        static string connectionString = @"Data Source=DESKTOP-O8EQ9F6;Initial Catalog=Animals;Integrated Security=True";
        public static SqlConnectionStringBuilder sqlConnect;
       

        public RepositoryBase()
        {
            sqlConnect = new SqlConnectionStringBuilder();
            sqlConnect.InitialCatalog = "Animals";
            sqlConnect.DataSource = @"DESKTOP-O8EQ9F6";
            sqlConnect.ConnectTimeout = 30;
            sqlConnect.IntegratedSecurity = true;

        }


        public List<T> Load(String query)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = sqlConnect.ConnectionString;
                cn.Open();
                SqlCommand command = new SqlCommand(query, cn);
                SqlDataReader reader = command.ExecuteReader();
                var l = new List<T>();

                while (reader.Read())
                {
                    T an = new T();
                     l.Add(Serialize(reader,an));

                }
                return l;

            }

        }
        public static List<T> Load(Func<T, bool> linq)
        {
            List<T> list = new List<T>();
            DataContext db = new DataContext(connectionString);
            list = db.GetTable<T>().Where(linq).ToList();

            return list;
        }

        public static async Task InsertLinq(T obj)
        {
            //Console.WriteLine("acync поток начало");
            //await InsertTask(obj);
           // Console.WriteLine("acync поток завершен");


            return Task.Run(() => {
                DataContext db = new DataContext(connectionString);
                try
                {
                    //Добавление в таблицу
                    db.GetTable<T>().InsertOnSubmit(obj);
                    db.SubmitChanges();
                }
                catch (SqlException)
                {
                    Console.WriteLine("Такой ID существует");
                }
                Console.WriteLine("Конец задачи");
            });
        }
        /*
        static Task InsertTask(T obj)
        {
            Console.WriteLine("Начало задачи");
            return Task.Run(() => {
                DataContext db = new DataContext(connectionString);
                try
                {
                    //Добавление в таблицу
                    db.GetTable<T>().InsertOnSubmit(obj);
                    db.SubmitChanges();
                }
                catch (SqlException)
                {
                    Console.WriteLine("Такой ID существует");
                }
                Console.WriteLine("Конец задачи");
            });

        }
        */


        public virtual T Serialize(SqlDataReader reader,T obj)
        {
            Type t = typeof(T);
            PropertyInfo[] fieldNames = t.GetProperties();

            DataMapperAttribute MyAttribute;
                foreach (PropertyInfo fil in fieldNames)
                {
                    MyAttribute = (DataMapperAttribute)Attribute.GetCustomAttribute(fil, typeof(DataMapperAttribute));
                    if (MyAttribute == null || MyAttribute.Column == false)
                    {
                        fil.SetValue(obj, reader[fil.Name]);                       
                    }                  
                }

                return obj;
            }


        public void InsertAnimal(T obj) //(int id, double mass, String name, int type)
        {
            Type t = typeof(T);
            PropertyInfo[] fieldNames = t.GetProperties();
            int type=1;

            //определяем тип
            
            if (obj.GetType() == typeof(Butterfly)) type = 3;
            else if (obj.GetType()== typeof(Insect)) type = 2;
          
            /*
            foreach (PropertyInfo fil in fieldNames)
            {                
                if (fil.Name == "name") name = obj.name;
                if (fil.Name == "id") id = obj.id;
                if (fil.Name == "mass") mass = obj.mass;    
            }*/
            
                using (var cn = new SqlConnection())
            {
                cn.ConnectionString = sqlConnect.ConnectionString;
                cn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Animal VALUES (" + obj.id + "," + obj.mass + ",'" + obj.name + "'," + type + ")", cn);
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                }catch (SqlException)
                {
                    MessageBox.Show("Такой ID существует");
                }
            }            
        }

    }

   


}
