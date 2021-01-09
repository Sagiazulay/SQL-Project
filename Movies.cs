using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil5b
{
    class Movies
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Release_date { get; set; }
        public int Genre_id { get; set; }

        public Movies()
        {

        }

        public Movies(int iD, string name, DateTime release_date, int genre_id)
        {
            ID = iD;
            Name = name;
            Release_date = release_date;
            Genre_id = genre_id;
        }
        public override string ToString()
        {
            return $"{Newtonsoft.Json.JsonConvert.SerializeObject(this)}";
        }
        public override bool Equals(object obj)
        {
            return this.ID == ((Movies)obj).ID;
        }

        public override int GetHashCode()
        {
            return (int)ID;
        }
        public static bool operator ==(Movies c1, Movies c2)
        {
            return c1.ID == c2.ID;
        }
        public static bool operator !=(Movies c1, Movies c2)
        {
            return !(c1 == c2);
        }
    }
}
