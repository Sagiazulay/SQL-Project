using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil5b
{
    class Movies_actors
    {
        public int ID { get; set; }
        public int Movie_id { get; set; }
        public int Actor_id { get; set; }

        public Movies_actors()
        {

        }

        public Movies_actors(int iD, int movie_id, int actor_id)
        {
            ID = iD;
            Movie_id = movie_id;
            Actor_id = actor_id;
        }
        public override string ToString()
        {
            return $"{Newtonsoft.Json.JsonConvert.SerializeObject(this)}";
        }
        public override bool Equals(object obj)
        {
            return this.ID == ((Movies_actors)obj).ID;
        }

        public override int GetHashCode()
        {
            return (int)ID;
        }
        public static bool operator ==(Movies_actors c1, Movies_actors c2)
        {
            return c1.ID == c2.ID;
        }
        public static bool operator !=(Movies_actors c1, Movies_actors c2)
        {
            return !(c1 == c2);
        }
    }
}
