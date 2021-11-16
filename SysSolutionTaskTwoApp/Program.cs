using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SysSolutionTaskTwoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            updateTable();

        }


        private static void updateTable()
        {
            string connectionString = "server=localhost; port=3306; uid=syssolution; password=123456; database=syssolutiondb; charset=utf8; sslMode=none;";

            MySqlConnection connection = new MySqlConnection(connectionString);

            using (connection)
            {
                try
                {
                    connection.Open();

                    MySqlCommand cmdSelect = new MySqlCommand("SELECT * FROM TableX", connection);
                    MySqlDataReader reader = cmdSelect.ExecuteReader();

                    if (reader.HasRows)
                    {
                        int id = 0;

                        while (reader.Read())
                        {
                            id = (int)reader["Id"];
                        }

                        reader.Close();

                        while (true)
                        {
                            MySqlCommand cmdUpdate = new MySqlCommand("UPDATE TableX SET Time_Now=@Time_Now WHERE Id=@Id", connection);

                            cmdUpdate.Parameters.AddWithValue("@Time_Now", DateTime.Now);
                            cmdUpdate.Parameters.AddWithValue("Id", id);

                            cmdUpdate.ExecuteNonQuery();
                        }

                    }
                    else
                    {
                        reader.Close();

                        MySqlCommand cmdInsert = new MySqlCommand("INSERT INTO TableX (Time_Now) VALUES (@Time_Now)", connection);

                        cmdInsert.Parameters.AddWithValue("@Time_Now", DateTime.Now);

                        cmdInsert.ExecuteNonQuery();
                    }

                    connection.Close();

                }
                catch (MySqlException ex)
                {

                    Console.WriteLine("ERROR : " + ex.Message.ToString());
                }
            }
        }
    }
}
