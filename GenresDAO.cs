using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil5b
{
    class GenresDAO
    {
        private string m_conn = "Host=localhost;Username=postgres;Password=admin;Database=postgres";
        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public GenresDAO(string conn)
        {
            m_conn = conn;
        }

        private int ExecuteNonQuery(string query)
        {
            int result = 0;
            try
            {
                using (var my_conn = new NpgsqlConnection(m_conn))
                {
                    my_conn.Open();
                    my_logger.Debug($"Connection sting is Open");
                    NpgsqlCommand command = new NpgsqlCommand(query, my_conn);
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }

                my_logger.Info("*ExecuteNonQuery Secceeded!*");
                return result;
            }
            catch (Exception Ex)
            {
                Console.WriteLine($"Could not connect to server!{Ex}");
                return 0;
            }
        }
        public void AddGenre(int id, string name)
        {
            ExecuteNonQuery($"INSERT INTO Genres (ID, Name) VALUES ('{id}','{name}');");
            my_logger.Info($"New Genre '{name}' Was Added Seccessfully");
            Console.WriteLine($"New Genre '{name}' Was Added Seccessfully");
        }
        public void UpdateGenre(int Genre_id, string name)
        {
            ExecuteNonQuery($"UPDATE Genres SET Name = '{name}' WHERE ID = {Genre_id};");
            my_logger.Info($"Genre '{name}' Was Updated Seccessfully");
            Console.WriteLine($"Genre '{name}' Was Updated Seccessfully");
        }
        public void DeleteGenre(int Genre_id)
        {
            ExecuteNonQuery($"DELETE FROM Genres WHERE ID = {Genre_id};");
            my_logger.Info($"Genre '{Genre_id}' Was Deleted Seccessfully");
            Console.WriteLine($"Genre '{Genre_id}' Was Deleted Seccessfully");
        }

        public void GetAllGenres()
        {
            string query = "SELECT * FROM Genres";

            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand(query, new NpgsqlConnection(m_conn));
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                List<Genres> list = new List<Genres>();
                while (reader.Read() == true)
                {
                    list.Add(
                        new Genres
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Name = reader["NAME"].ToString()
                        });
                    Console.WriteLine($"Actor ID : {reader["ID"]}," +
                        $" Actor Name : {reader["NAME"].ToString()}");
                }
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get Genres from DB [SELECT * FROM Genres]. Error : {ex}");
            }
        }

        public Genres GetGenreByID(int id)
        {
            string query = $"SELECT * FROM Genres WHERE ID = {id}";

            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand(query, new NpgsqlConnection(m_conn));
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                reader.Read();
                Genres c = new Genres
                {
                    ID = Convert.ToInt32(reader["ID"]),
                    Name = reader["NAME"].ToString()
                };
                Console.WriteLine($"Genre ID : {reader["ID"]}," +
                 $" Genre Name : {reader["NAME"].ToString()}");
                return c;
            }

            catch (Exception ex)
            {
                my_logger.Error($"Failed to get Genres from DB [SELECT * FROM Genres]. Error : {ex}");
                return null;
            }
        }
    }
}
