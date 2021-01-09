using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil5b
{
    class ActorsDAO
    {
        private string m_conn = "Host=localhost;Username=postgres;Password=admin;Database=postgres";
        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ActorsDAO(string conn)
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
        public void AddActor(int id, string name, DateTime birthday)
        {
            ExecuteNonQuery($"INSERT INTO Actors (ID, Name, Birthday) VALUES ('{id}','{name}', '{birthday}');");
            my_logger.Info($"New Actor '{name}' Was Added Seccessfully");
            Console.WriteLine($"New Actor '{name}' Was Added Seccessfully");
        }
        public void UpdateActor(int actor_id, string name)
        {
            ExecuteNonQuery($"UPDATE Actors SET Name = '{name}' WHERE ID = {actor_id};");
            my_logger.Info($"Actor '{name}' Was Updated Seccessfully");
            Console.WriteLine($"Actor '{name}' Was Updated Seccessfully");
        }
        public void DeleteActor(int actor_id)
        {
            ExecuteNonQuery($"DELETE FROM Actors WHERE ID = {actor_id};");
            my_logger.Info($"Actor '{actor_id}' Was Deleted Seccessfully");
            Console.WriteLine($"Actor '{actor_id}' Was Deleted Seccessfully");
        }

        public void GetAllActors()
        {
            string query = "SELECT * FROM Actors";

            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand(query, new NpgsqlConnection(m_conn));
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                List<Actors> list = new List<Actors>();

                while (reader.Read() == true)
                {
                    list.Add(
                        new Actors
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Name = reader["NAME"].ToString(),
                            Birthday = Convert.ToDateTime(reader["Birthday"]),
                        });
                    Console.WriteLine($"Actor ID : {reader["ID"]}," +
                        $" Actor Name : {reader["NAME"].ToString()}," +
                        $" Birthday : {reader["Birthday"]},");
                }
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get Actors from DB [SELECT * FROM Actors]. Error : {ex}");
            }
        }
        public Actors GetActorByID(int actor_id)
        {
            string query = $"SELECT * FROM Actors WHERE ID = {actor_id}";

            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand(query, new NpgsqlConnection(m_conn));
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                reader.Read();
                Actors c = new Actors
                {
                    ID = Convert.ToInt32(reader["ID"]),
                    Name = reader["NAME"].ToString(),
                    Birthday = Convert.ToDateTime(reader["Birthday"]),
                };
                Console.WriteLine($"Actor ID : {reader["ID"]}," +
                 $" Actor Name : {reader["NAME"].ToString()}," +
                 $" Birthday : {reader["Birthday"]},");
                return c;
            }
                
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get Actors from DB [SELECT * FROM Actors]. Error : {ex}");
                return null;
            }
        } 
    }
}
