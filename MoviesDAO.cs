using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil5b
{
    class MoviesDAO
    {
        private string m_conn = "Host=localhost;Username=postgres;Password=admin;Database=postgres";
        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public MoviesDAO(string conn)
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
        public void AddMovie(int id, string name, DateTime release_date, int genre_id)
        {
            ExecuteNonQuery($"INSERT INTO Movies (id, name, release_date, genre_id) VALUES ('{id}','{name}'), '{release_date}', '{genre_id}');");
            my_logger.Info($"New Movie '{name}' Was Added Seccessfully");
            Console.WriteLine($"New Movie '{name}' Was Added Seccessfully");
        }
        public void UpdateMovie(int movie_id, string name)
        {
            ExecuteNonQuery($"UPDATE Movies SET Name = '{name}' WHERE ID = {movie_id};");
            my_logger.Info($"Movie '{name}' Was Updated Seccessfully");
            Console.WriteLine($"Movie '{name}' Was Updated Seccessfully");
        }
        public void DeleteMovie(int Movie_id)
        {
            ExecuteNonQuery($"DELETE FROM Movies WHERE ID = {Movie_id};");
            my_logger.Info($"Movie '{Movie_id}' Was Deleted Seccessfully");
            Console.WriteLine($"Movie '{Movie_id}' Was Deleted Seccessfully");
        }

        public void GetAllMovies()
        {
            string query = "SELECT * FROM Movies";

            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand(query, new NpgsqlConnection(m_conn));
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                List<Movies> list = new List<Movies>();
                while (reader.Read() == true)
                {
                    list.Add(
                        new Movies
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Name = reader["NAME"].ToString(),
                            Release_date = Convert.ToDateTime(reader["release_date"]),
                            Genre_id = Convert.ToInt32(reader["genre_id"])
                        });
                    Console.WriteLine($"Movie ID : {reader["ID"]}," +
                        $" Movie Name : {reader["NAME"].ToString()}"+
                         $" Release_date : {reader["release_date"]}),"+
                         $" Genre Id : {reader["genre_id"]}"
                        );
                }
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get Movie from DB [SELECT * FROM Movies]. Error : {ex}");
            }
        }

        public Movies GetMovieByID(int id)
        {
            string query = $"SELECT * FROM Movies WHERE ID = {id}";

            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand(query, new NpgsqlConnection(m_conn));
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                reader.Read();
                Movies c = new Movies
                {
                    ID = Convert.ToInt32(reader["ID"]),
                    Name = reader["NAME"].ToString(),
                    Release_date = Convert.ToDateTime(reader["release_date"]),
                    Genre_id = Convert.ToInt32(reader["genre_id"])
                };
                Console.WriteLine($"Movie ID : {reader["ID"]}," +
                    $" Movie Name : {reader["NAME"].ToString()}" +
                     $"Release_date : {reader["release_date"]})," +
                     $"Genre Id : {reader["genre_id"]}"
                    );
                return c;
            }

            catch (Exception ex)
            {
                my_logger.Error($"Failed to get Movies from DB [SELECT * FROM Movies]. Error : {ex}");
                return null;
            }
        }
    }
}
